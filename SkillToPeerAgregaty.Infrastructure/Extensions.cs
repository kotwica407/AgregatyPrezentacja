using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using SkillToPeerAgregaty.Application;
using SkillToPeerAgregaty.Application.Repositories;
using SkillToPeerAgregaty.Infrastructure.DAL;
using SkillToPeerAgregaty.Infrastructure.DAL.Repositories;
using SkillToPeerAgregaty.Infrastructure.Services;

namespace SkillToPeerAgregaty.Infrastructure;
public static class Extensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

        services.AddScoped<IReadProductRepository, ReadProductRepository>();
        services.AddScoped<IReadOrderRepository, ReadOrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IClock, Clock>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddHttpContextAccessor();
        services.AddSqlServer(configuration);
        services.AddResilience();
        return services;
    }

    private static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Agregaty");
        services.AddDbContext<AgregatyDbContext>(x => x.UseSqlServer(connectionString));
        return services;
    }

    private static IServiceCollection AddResilience(this IServiceCollection services)
    {
        services.AddResiliencePipeline("optimistic-concurrency-pipeline", builder =>
        {
            builder
                .AddRetry(new Polly.Retry.RetryStrategyOptions
                {
                    ShouldHandle = new PredicateBuilder().Handle<DbUpdateConcurrencyException>(),
                    MaxRetryAttempts = 2,
                    DelayGenerator = static args =>
                    {
                        var delay = args.AttemptNumber switch
                        {
                            0 => TimeSpan.Zero,
                            1 => TimeSpan.FromSeconds(1),
                            _ => TimeSpan.FromSeconds(5)
                        };

                        // This example uses a synchronous delay generator,
                        // but the API also supports asynchronous implementations.
                        return new ValueTask<TimeSpan?>(delay);
                    }
                });
        });

        return services;
    }
}

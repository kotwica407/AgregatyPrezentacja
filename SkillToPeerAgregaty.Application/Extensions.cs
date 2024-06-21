using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SkillToPeerAgregaty.Application;
public static class Extensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        var assembly = typeof(Extensions).GetTypeInfo().Assembly;

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        return services;
    }
}

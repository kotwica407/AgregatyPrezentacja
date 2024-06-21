using Microsoft.EntityFrameworkCore;
using SkillToPeerAgregaty.Domain.Order.Entities;
using SkillToPeerAgregaty.Domain.Product.Entities;

namespace SkillToPeerAgregaty.Infrastructure.DAL;
internal class AgregatyDbContext : DbContext
{
    public AgregatyDbContext(DbContextOptions<AgregatyDbContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("agregaty");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}

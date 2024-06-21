using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillToPeerAgregaty.Domain.Order.Entities;
using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Infrastructure.DAL.Configurations;
internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Order");

        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasDefaultValueSql("(newsequentialid())")
            .HasConversion(v => v.Value, v => new OrderId(v));

        builder.Property(x => x.UserId)
            .HasColumnName("UserId")
            .HasConversion(v => v.Value, v => new UserId(v))
            .IsRequired();

        builder.Property(x => x.Status)
            .HasColumnName("Status")
            .IsRequired();

        builder.ConfigureAggregateRootEntity();
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillToPeerAgregaty.Domain.Order.Entities;
using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Infrastructure.DAL.Configurations;
internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItem");

        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasDefaultValueSql("(newsequentialid())")
            .HasConversion(v => v.Value, v => new OrderItemId(v));

        builder.Property(x => x.Amount)
            .HasColumnName("Amount")
            .IsRequired()
            .HasConversion(v => v.Value, v => new Quantity(v));

        builder.Property(x => x.Price)
            .HasColumnName("Price")
            .IsRequired()
            .HasConversion(v => v.Value, v => new Price(v));

        builder.Property(x => x.ProductId)
            .HasColumnName("ProductId")
            .HasConversion(v => v.Value, v => new ProductId(v))
            .IsRequired();

        builder.Property(x => x.OrderId)
            .HasColumnName("OrderId")
            .HasConversion(v => v.Value, v => new OrderId(v))
            .IsRequired();

        builder.HasOne<Order>(x => x.Order)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.OrderId);

        builder.ConfigureAuditableEntity();
        builder.ConfigureRemovableEntity();
    }
}

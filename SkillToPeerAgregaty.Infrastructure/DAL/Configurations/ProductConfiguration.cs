using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillToPeerAgregaty.Domain.Product.Entities;
using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Infrastructure.DAL.Configurations;
internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");

        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasDefaultValueSql("(newsequentialid())")
            .HasConversion(v => v.Value, v => new ProductId(v));

        builder.Property(x => x.AvailableAmount)
            .HasColumnName("AvailableAmount")
            .HasConversion(v => v.Value, v => new Quantity(v));

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .IsRequired();

        builder.ConfigureAggregateRootEntity();
    }
}

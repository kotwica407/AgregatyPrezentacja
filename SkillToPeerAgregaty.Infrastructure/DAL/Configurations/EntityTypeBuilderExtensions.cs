using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Infrastructure.DAL.Configurations;
internal static class EntityTypeBuilderExtensions
{
    internal static void ConfigureAggregateRootEntity<T>(this EntityTypeBuilder<T> builder) where T : AggregateRoot
    {
        builder.Property(x => x.Version)
            .HasColumnName("Version")
            .IsRequired()
            .IsConcurrencyToken();

        builder.ConfigureAuditableEntity();
        builder.ConfigureRemovableEntity();
    }
    internal static void ConfigureAuditableEntity<T>(this EntityTypeBuilder<T> builder) where T : class, IAuditableEntity
    {
        builder.Property(x => x.CreatedUtcDate)
                .HasColumnName(Consts.CreatedUtcDateDbColumnName)
                .HasConversion(v => v.Value, v => new CreatedUtcDate(DateTime.SpecifyKind(v, DateTimeKind.Utc)));

        builder.Property(x => x.ModifiedUtcDate)
            .HasColumnName(Consts.ModifiedUtcDateDbColumnName)
            .HasConversion<DateTime?>(v => v == null ? null : v.Value, v => v.HasValue ? new ModifiedUtcDate(DateTime.SpecifyKind(v.Value, DateTimeKind.Utc)) : null);
    }

    internal static void ConfigureRemovableEntity<T>(this EntityTypeBuilder<T> builder) where T : class, IRemovableEntity
    {
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.Property(x => x.IsDeleted)
            .HasColumnName(Consts.IsDeletedDbColumnName);

        builder.Property(x => x.DeletedUtcDate)
            .HasColumnName(Consts.DeletedUtcDateDbColumnName)
            .HasConversion<DateTime?>(v => v == null ? null : v.Value, v => v.HasValue ? new DeletedUtcDate(DateTime.SpecifyKind(v.Value, DateTimeKind.Utc)) : null);
    }
}

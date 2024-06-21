using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SkillToPeerAgregaty.Application;
using SkillToPeerAgregaty.Domain.Shared;
using SkillToPeerAgregaty.Infrastructure.Services;

namespace SkillToPeerAgregaty.Infrastructure.DAL;
internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly AgregatyDbContext _context;
    private readonly IClock _clock;

    public UnitOfWork(AgregatyDbContext context, IClock clock)
    {
        _context = context;
        _clock = clock;
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        await _context.Database.CommitTransactionAsync(cancellationToken);
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        await _context.Database.RollbackTransactionAsync(cancellationToken);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditableEntities();
        UpdateRemovableEntities();

        await DispatchEventsAsync(cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task DispatchEventsAsync(CancellationToken cancellationToken = default)
    {
        var domainEvents = _context.ChangeTracker
            .Entries<AggregateRoot>()
            .Select(x => x.Entity)
            .SelectMany(aggregateRoot =>
            {
                var events = aggregateRoot.GetDomainEvents().ToList();
                aggregateRoot.ClearDomainEvents();
                return events;
            })
            .ToList();

        await _context.OutboxMessages.AddRangeAsync(domainEvents.Select(x => OutboxMessage.Create(x, _clock)), cancellationToken);
    }

    private void UpdateRemovableEntities()
    {
        var removableEntries = _context.ChangeTracker
            .Entries<IRemovableEntity>()
            .Where(x => x.State == EntityState.Deleted);

        var removedEnitites = new List<EntityEntry<IRemovableEntity>>();

        foreach (var entityEntry in removableEntries)
        {
            foreach (var targetEntry in entityEntry.References.Select(x => x.TargetEntry))
            {
                if (targetEntry?.Entity is IValueObject && targetEntry is not null)
                {
                    targetEntry.State = EntityState.Unchanged;
                }
            }
            entityEntry.State = EntityState.Modified;
            entityEntry.Property(e => e.IsDeleted).CurrentValue = true;
            entityEntry.Property(e => e.DeletedUtcDate).CurrentValue = new DeletedUtcDate(_clock.UtcNowDateTime);
            removedEnitites.Add(entityEntry);
        }
    }

    private void UpdateAuditableEntities()
    {
        var auditableEntries = _context.ChangeTracker
            .Entries<IAuditableEntity>();

        foreach (var entityEntry in auditableEntries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(e => e.CreatedUtcDate).CurrentValue = new CreatedUtcDate(_clock.UtcNowDateTime);
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(e => e.ModifiedUtcDate).CurrentValue = new ModifiedUtcDate(_clock.UtcNowDateTime);
            }
        }
    }
}

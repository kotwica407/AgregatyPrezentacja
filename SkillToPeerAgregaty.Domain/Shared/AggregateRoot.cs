namespace SkillToPeerAgregaty.Domain.Shared;
public abstract class AggregateRoot : IAuditableEntity, IRemovableEntity
{
    private bool _versionIncremented;

    protected List<IDomainEvent> _events = new();
    public int Version { get; protected set; } = 1;

    public CreatedUtcDate CreatedUtcDate { get; private set; }

    public ModifiedUtcDate? ModifiedUtcDate { get; private set; }

    public DeletedUtcDate? DeletedUtcDate { get; private set; }

    public bool IsDeleted { get; private set; }

    protected void IncrementVersion()
    {
        if (_versionIncremented) return;

        Version++;
        _versionIncremented = true;
    }

    protected void AddDomainEvent(IDomainEvent @event)
    {
        _events.Add(@event);
    }

    public IReadOnlyCollection<IDomainEvent> GetDomainEvents()
    {
        return _events.AsReadOnly();
    }

    public void ClearDomainEvents()
    {
        _events.Clear();
    }
}

public abstract class AggregateRoot<T> : AggregateRoot
{
    public T Id { get; protected set; }
}

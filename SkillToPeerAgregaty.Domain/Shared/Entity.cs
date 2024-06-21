namespace SkillToPeerAgregaty.Domain.Shared;
public abstract class Entity<T>
{
    public T Id { get; protected set; }
}

public interface IAuditableEntity
{
    public CreatedUtcDate CreatedUtcDate { get; }
    public ModifiedUtcDate? ModifiedUtcDate { get; }
}

public interface IRemovableEntity
{
    public DeletedUtcDate? DeletedUtcDate { get; }
    public bool IsDeleted { get; }
}
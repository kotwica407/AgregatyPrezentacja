namespace SkillToPeerAgregaty.Domain.Shared;
public record ModifiedUtcDate(DateTime Value) : IValueObject
{
    public static bool operator >(ModifiedUtcDate lhs, ModifiedUtcDate rhs)
    {
        return lhs.Value > rhs.Value;
    }

    public static bool operator <(ModifiedUtcDate lhs, ModifiedUtcDate rhs)
    {
        return lhs.Value < rhs.Value;
    }
}

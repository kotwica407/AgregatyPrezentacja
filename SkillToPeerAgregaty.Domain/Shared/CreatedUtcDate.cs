namespace SkillToPeerAgregaty.Domain.Shared;
public record struct CreatedUtcDate(DateTime Value) : IValueObject
{
    public static bool operator >(CreatedUtcDate lhs, CreatedUtcDate rhs)
    {
        return lhs.Value > rhs.Value;
    }

    public static bool operator <(CreatedUtcDate lhs, CreatedUtcDate rhs)
    {
        return lhs.Value < rhs.Value;
    }

    public static bool operator >=(CreatedUtcDate lhs, CreatedUtcDate rhs)
    {
        return lhs.Value >= rhs.Value;
    }

    public static bool operator <=(CreatedUtcDate lhs, CreatedUtcDate rhs)
    {
        return lhs.Value <= rhs.Value;
    }
}
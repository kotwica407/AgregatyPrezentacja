using SkillToPeerAgregaty.Domain.Shared.Exceptions;

namespace SkillToPeerAgregaty.Domain.Shared;
public readonly record struct Quantity : IValueObject
{
    public decimal Value { get; init; }
    public Quantity(decimal value)
    {
        if (value < 0) throw new QuantityOutOfRangeException();
        Value = value;
    }
    public static bool operator >(Quantity lhs, Quantity rhs) => lhs.Value > rhs.Value;

    public static bool operator <(Quantity lhs, Quantity rhs) => lhs.Value < rhs.Value;

    public static bool operator >=(Quantity lhs, Quantity rhs) => lhs.Value >= rhs.Value;

    public static bool operator <=(Quantity lhs, Quantity rhs) => lhs.Value <= rhs.Value;

    public readonly Quantity GetReduced(Quantity reducedByAmount) => new(Value - reducedByAmount.Value);
    public readonly Quantity GetIncreased(Quantity increasedByAmount) => new(Value + increasedByAmount.Value);
}

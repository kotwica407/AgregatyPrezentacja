using FluentAssertions;
using SkillToPeerAgregaty.Domain.Shared;
using SkillToPeerAgregaty.Domain.Shared.Exceptions;

namespace SkillToPeerAgregaty.Domain.UnitTests.ValueObjectTests;
public class QuantityTests
{
    [Theory]
    [MemberData(nameof(NegativeSet))]
    private void ShouldThrow_QuantityOutOfRangeException_WhenAmount_IsNegative(decimal amount)
    {
        Action func = () =>
        {
            var quantity = new Quantity(amount);
        };

        func.Should().Throw<QuantityOutOfRangeException>();
    }

    [Theory]
    [MemberData(nameof(GreaterThanSet))]
    private void Check_GreaterThan_Operators(decimal lhs, decimal rhs, bool expected)
    {
        var qlhs = new Quantity(lhs);
        var qrhs = new Quantity(rhs);

        var actual = qlhs > qrhs;

        actual.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(GreaterThanOrEqualSet))]
    private void Check_GreaterThanOrEqual_Operators(decimal lhs, decimal rhs, bool expected)
    {
        var qlhs = new Quantity(lhs);
        var qrhs = new Quantity(rhs);

        var actual = qlhs >= qrhs;

        actual.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(LessThanSet))]
    private void Check_LessThan_Operators(decimal lhs, decimal rhs, bool expected)
    {
        var qlhs = new Quantity(lhs);
        var qrhs = new Quantity(rhs);

        var actual = qlhs < qrhs;

        actual.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(LessThanOrEqualSet))]
    private void Check_LessThanOrEqual_Operators(decimal lhs, decimal rhs, bool expected)
    {
        var qlhs = new Quantity(lhs);
        var qrhs = new Quantity(rhs);

        var actual = qlhs <= qrhs;

        actual.Should().Be(expected);
    }

    public static IEnumerable<object[]> NegativeSet()
    {
        yield return [-10.5m];
        yield return [-1m];
        yield return [-1000m];
    }

    public static IEnumerable<object[]> GreaterThanSet()
    {
        yield return [10, 1, true];
        yield return [10, 10, false];
        yield return [1, 10, false];
    }

    public static IEnumerable<object[]> GreaterThanOrEqualSet()
    {
        yield return [10, 1, true];
        yield return [10, 10, true];
        yield return [1, 10, false];
    }

    public static IEnumerable<object[]> LessThanSet()
    {
        yield return [10, 1, false];
        yield return [10, 10, false];
        yield return [1, 10, true];
    }

    public static IEnumerable<object[]> LessThanOrEqualSet()
    {
        yield return [10, 1, false];
        yield return [10, 10, true];
        yield return [1, 10, true];
    }
}

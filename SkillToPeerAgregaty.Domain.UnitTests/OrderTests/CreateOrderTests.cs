using FluentAssertions;
using SkillToPeerAgregaty.Domain.Order.Events;
using SkillToPeerAgregaty.Domain.Shared;
using SkillToPeerAgregaty.Domain.Shared.Errors;

namespace SkillToPeerAgregaty.Domain.UnitTests.OrderTests;
public class CreateOrderTests
{
    private static ProductId _productId1 = new(Guid.NewGuid());
    private static ProductId _productId2 = new(Guid.NewGuid());
    private static ProductId _productId3 = new(Guid.NewGuid());

    private static UserId _userId = new(Guid.NewGuid());

    [Fact]
    private void ShouldRaise_OrderCreatedEvent_When_CreateOrder()
    {
        var orderResult = Order.Entities.Order.Create(
        [
            new(_productId1, new Quantity(3), new Price(123m)),
            new(_productId2, new Quantity(1), new Price(12.3m)),
            new(_productId3, new Quantity(5), new Price(21.54m)),
        ], _userId);

        orderResult.IsSuccess.Should().BeTrue();

        var order = orderResult.Value;

        order.GetDomainEvents().Should().HaveCount(1)
            .And.ContainItemsAssignableTo<OrderCreatedEvent>();
    }

    [Fact]
    private void ShouldHave_ThreeItems_When_CreateOrderWithThreeItems()
    {
        var orderResult = Order.Entities.Order.Create(
        [
            new(_productId1, new Quantity(3), new Price(123m)),
            new(_productId2, new Quantity(1), new Price(12.3m)),
            new(_productId3, new Quantity(5), new Price(21.54m)),
        ], _userId);

        orderResult.IsSuccess.Should().BeTrue();

        var order = orderResult.Value;

        order.Items.Should().HaveCount(3);
    }

    [Fact]
    private void ShouldHave_AwaitingApprovalStatus_When_CreateOrder()
    {
        var orderResult = Order.Entities.Order.Create(
        [
            new(_productId1, new Quantity(3), new Price(123m)),
            new(_productId2, new Quantity(1), new Price(12.3m)),
            new(_productId3, new Quantity(5), new Price(21.54m)),
        ], _userId);

        orderResult.IsSuccess.Should().BeTrue();

        var order = orderResult.Value;

        order.Status.Should().Be(Order.ValueObjects.OrderStatus.AwaitingApproval);
    }


    [Fact]
    private void ShouldReturn_Failure_SameProducts_When_ItemsAreNotDifferentProducts()
    {
        var orderResult = Order.Entities.Order.Create(
        [
            new(_productId1, new Quantity(3), new Price(123m)),
            new(_productId1, new Quantity(1), new Price(12.3m)),
            new(_productId3, new Quantity(5), new Price(21.54m)),
        ], _userId);

        orderResult.IsSuccess.Should().BeFalse();

        orderResult.Error.Should().Be(DomainErrors.Order.SameProducts);
    }
}

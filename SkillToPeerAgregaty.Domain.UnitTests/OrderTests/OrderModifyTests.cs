using FluentAssertions;
using SkillToPeerAgregaty.Domain.Order.Events;
using SkillToPeerAgregaty.Domain.Shared.Errors;

namespace SkillToPeerAgregaty.Domain.UnitTests.OrderTests;
public class OrderModifyTests
{
    #region APPROVE_TESTS
    [Fact]
    private void ShouldReturn_Success_When_OrderInAwaitingApproval_Approved()
    {
        var order = OrderTestsHelpers.BuildAwaitingApprovalOrder();
        var result = order.Approve();

        result.IsSuccess.Should().BeTrue();
        order.Status.Should().Be(Order.ValueObjects.OrderStatus.Approved);
        order.GetDomainEvents().Should().HaveCount(1)
            .And.ContainItemsAssignableTo<OrderApprovedEvent>();
    }

    [Fact]
    private void ShouldReturn_Failure_When_CancelledOrder_Approved()
    {
        var order = OrderTestsHelpers.BuildCancelledOrder();
        var result = order.Approve();

        result.IsFailure.Should().BeTrue();
        order.Status.Should().Be(Order.ValueObjects.OrderStatus.Cancelled);
        result.Error.Should().Be(DomainErrors.Order.IsNotAwaitingApproval);
    }

    [Fact]
    private void ShouldReturn_Failure_When_ApprovedOrder_Approved()
    {
        var order = OrderTestsHelpers.BuildApprovedOrder();
        var result = order.Approve();

        result.IsFailure.Should().BeTrue();
        order.Status.Should().Be(Order.ValueObjects.OrderStatus.Approved);
        result.Error.Should().Be(DomainErrors.Order.IsNotAwaitingApproval);
    }

    [Fact]
    private void ShouldReturn_Failure_When_DeliveredOrder_Approved()
    {
        var order = OrderTestsHelpers.BuildDeliveredOrder();
        var result = order.Approve();

        result.IsFailure.Should().BeTrue();
        order.Status.Should().Be(Order.ValueObjects.OrderStatus.Delivered);
        result.Error.Should().Be(DomainErrors.Order.IsNotAwaitingApproval);
    }
    #endregion

    #region CANCEL_TESTS
    [Fact]
    private void ShouldReturn_Success_When_OrderInAwaitingApproval_Cancelled()
    {
        var order = OrderTestsHelpers.BuildAwaitingApprovalOrder();
        var result = order.Cancel();

        result.IsSuccess.Should().BeTrue();
        order.Status.Should().Be(Order.ValueObjects.OrderStatus.Cancelled);
        order.GetDomainEvents().Should().HaveCount(1)
            .And.ContainItemsAssignableTo<OrderCancelledEvent>();
    }
    [Fact]
    private void ShouldReturn_Success_When_ApprovedOrder_Cancelled()
    {
        var order = OrderTestsHelpers.BuildApprovedOrder();
        var result = order.Cancel();

        result.IsSuccess.Should().BeTrue();
        order.Status.Should().Be(Order.ValueObjects.OrderStatus.Cancelled);
        order.GetDomainEvents().Should().HaveCount(1)
            .And.ContainItemsAssignableTo<OrderCancelledEvent>();
    }

    [Fact]
    private void ShouldReturn_Failure_When_DeliveredOrder_Cancelled()
    {
        var order = OrderTestsHelpers.BuildDeliveredOrder();
        var result = order.Cancel();

        result.IsFailure.Should().BeTrue();
        order.Status.Should().Be(Order.ValueObjects.OrderStatus.Delivered);
        result.Error.Should().Be(DomainErrors.Order.IsDelivered);
    }

    [Fact]
    private void ShouldReturn_Failure_When_CancelledOrder_Cancelled()
    {
        var order = OrderTestsHelpers.BuildCancelledOrder();
        var result = order.Cancel();

        result.IsFailure.Should().BeTrue();
        order.Status.Should().Be(Order.ValueObjects.OrderStatus.Cancelled);
        result.Error.Should().Be(DomainErrors.Order.IsCancelled);
    }
    #endregion

    #region DELIVER_TESTS
    [Fact]
    private void ShouldReturn_Success_When_ApprovedOrder_Delivered()
    {
        var order = OrderTestsHelpers.BuildApprovedOrder();
        var result = order.Deliver();

        result.IsSuccess.Should().BeTrue();
        order.Status.Should().Be(Order.ValueObjects.OrderStatus.Delivered);
        order.GetDomainEvents().Should().HaveCount(1)
            .And.ContainItemsAssignableTo<OrderDeliveredEvent>();
    }

    [Fact]
    private void ShouldReturn_Failure_When_CancelledOrder_Delivered()
    {
        var order = OrderTestsHelpers.BuildCancelledOrder();
        var result = order.Deliver();

        result.IsFailure.Should().BeTrue();
        order.Status.Should().Be(Order.ValueObjects.OrderStatus.Cancelled);
        result.Error.Should().Be(DomainErrors.Order.IsNotApproved);
    }

    [Fact]
    private void ShouldReturn_Failure_When_AwaitingApproval_Delivered()
    {
        var order = OrderTestsHelpers.BuildAwaitingApprovalOrder();
        var result = order.Deliver();

        result.IsFailure.Should().BeTrue();
        order.Status.Should().Be(Order.ValueObjects.OrderStatus.AwaitingApproval);
        result.Error.Should().Be(DomainErrors.Order.IsNotApproved);
    }

    [Fact]
    private void ShouldReturn_Failure_When_DeliveredOrder_Delivered()
    {
        var order = OrderTestsHelpers.BuildDeliveredOrder();
        var result = order.Deliver();

        result.IsFailure.Should().BeTrue();
        order.Status.Should().Be(Order.ValueObjects.OrderStatus.Delivered);
        result.Error.Should().Be(DomainErrors.Order.IsNotApproved);
    }
    #endregion
}

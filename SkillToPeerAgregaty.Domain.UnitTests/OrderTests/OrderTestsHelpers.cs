using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Domain.UnitTests.OrderTests;

internal class OrderTestsHelpers
{
    private static ProductId _productId1 = new(Guid.NewGuid());
    private static ProductId _productId2 = new(Guid.NewGuid());
    private static ProductId _productId3 = new(Guid.NewGuid());

    private static UserId _userId = new(Guid.NewGuid());

    private static readonly List<ProductReservationRow> _productReservationRows = [
        new(_productId1, new Quantity(3), new Price(123m)),
        new(_productId2, new Quantity(1), new Price(12.3m)),
        new(_productId3, new Quantity(5), new Price(21.54m)),
    ];

    internal static Order.Entities.Order BuildAwaitingApprovalOrder()
    {
        var orderResult = Order.Entities.Order.Create(_productReservationRows, _userId);
        var order = orderResult.Value;
        order.ClearDomainEvents(); //list is clear when fetched record from db
        return order;
    }

    internal static Order.Entities.Order BuildCancelledOrder()
    {
        var orderResult = Order.Entities.Order.Create(_productReservationRows, _userId);
        var order = orderResult.Value;
        order.Cancel();
        order.ClearDomainEvents();//list is clear when fetched record from db
        return order;
    }

    internal static Order.Entities.Order BuildApprovedOrder()
    {
        var orderResult = Order.Entities.Order.Create(_productReservationRows, _userId);
        var order = orderResult.Value;
        order.Approve();
        order.ClearDomainEvents();//list is clear when fetched record from db
        return order;
    }

    internal static Order.Entities.Order BuildDeliveredOrder()
    {
        var orderResult = Order.Entities.Order.Create(_productReservationRows, _userId);
        var order = orderResult.Value;
        order.Approve();
        order.Deliver();
        order.ClearDomainEvents();//list is clear when fetched record from db
        return order;
    }
}
using SkillToPeerAgregaty.Domain.Order.Events;
using SkillToPeerAgregaty.Domain.Order.ValueObjects;
using SkillToPeerAgregaty.Domain.Shared;
using SkillToPeerAgregaty.Domain.Shared.Errors;

namespace SkillToPeerAgregaty.Domain.Order.Entities;

public class Order : AggregateRoot<OrderId>
{
    private List<OrderItem> _items = [];
    public UserId UserId { get; private set; }
    public OrderStatus Status { get; private set; }

    public IEnumerable<OrderItem> Items
    {
        get => _items;
        private set => _items = new List<OrderItem>(value);
    }

    private Order()//EF Core constructor
    {
    }

    private Order(UserId userId, IEnumerable<OrderItem> orderItems, OrderStatus orderStatus)
    {
        Id = new OrderId(Guid.NewGuid());
        UserId = userId;
        Items = orderItems;
        Status = orderStatus;

        AddDomainEvent(new OrderCreatedEvent(Id));
    }

    public static Result<Order> Create(List<ProductReservationRow> orderItems, UserId userId)
    {
        if (orderItems.Select(x => x.ProductId).Distinct().Count() != orderItems.Count)
            return Result.Failure<Order>(DomainErrors.Order.SameProducts);

        var order = new Order(userId, orderItems.Select(x => OrderItem.Create(x.Amount, x.Price, x.ProductId)), OrderStatus.AwaitingApproval);
        return Result.Success(order);
    }

    public Result Approve()
    {
        if (Status != OrderStatus.AwaitingApproval)
            return Result.Failure(DomainErrors.Order.IsNotAwaitingApproval);

        Status = OrderStatus.Approved;

        AddDomainEvent(new OrderApprovedEvent(Id));
        IncrementVersion();

        return Result.Success();
    }

    public Result Deliver()
    {
        if (Status != OrderStatus.Approved)
            return Result.Failure(DomainErrors.Order.IsNotApproved);

        Status = OrderStatus.Delivered;
        AddDomainEvent(new OrderDeliveredEvent(Id));
        IncrementVersion();
        return Result.Success();
    }

    public Result Cancel()
    {
        if (Status == OrderStatus.Delivered)
            return Result.Failure(DomainErrors.Order.IsDelivered);

        if (Status == OrderStatus.Cancelled)
            return Result.Failure(DomainErrors.Order.IsCancelled);

        Status = OrderStatus.Cancelled;
        AddDomainEvent(new OrderCancelledEvent(Id));
        IncrementVersion();
        return Result.Success();
    }
}
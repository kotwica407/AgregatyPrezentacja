using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Domain.Order.Entities;

public class OrderItem : Entity<OrderItemId>, IAuditableEntity, IRemovableEntity
{
    public OrderId OrderId { get; private set; }
    public Order Order { get; private set; }
    public Quantity Amount { get; private set; }
    public Price Price { get; private set; }
    public ProductId ProductId { get; private set; }
    public CreatedUtcDate CreatedUtcDate { get; private set; }

    public ModifiedUtcDate? ModifiedUtcDate { get; private set; }

    public DeletedUtcDate? DeletedUtcDate { get; private set; }

    public bool IsDeleted { get; private set; }

    private OrderItem()//EF Core constructor
    {

    }

    private OrderItem(Quantity amount, Price price, ProductId productId)
    {
        Id = new OrderItemId(Guid.NewGuid());
        Amount = amount;
        Price = price;
        ProductId = productId;
    }

    public static OrderItem Create(Quantity amount, Price price, ProductId productId) => new(amount, price, productId);
}
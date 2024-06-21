using SkillToPeerAgregaty.Domain.Order.ValueObjects;

namespace SkillToPeerAgregaty.Application.Queries.GetOrder;
public class OrderDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public OrderStatus Status { get; set; }
    public ItemDto[] Items { get; set; }
}

public class ItemDto
{
    public Guid ProductId { get; set; }
    public decimal Price { get; set; }
    public decimal Amount { get; set; }
}

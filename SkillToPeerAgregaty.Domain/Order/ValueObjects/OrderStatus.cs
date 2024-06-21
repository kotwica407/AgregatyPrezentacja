namespace SkillToPeerAgregaty.Domain.Order.ValueObjects;
public enum OrderStatus : byte
{
    AwaitingApproval = 1,
    Approved = 2,
    Delivered = 3,
    Cancelled = 4
}

using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Domain.Order.Events;
public record OrderDeliveredEvent(OrderId OrderId) : IDomainEvent;

using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Domain.Order.Events;
public record OrderCancelledEvent(OrderId OrderId) : IDomainEvent;

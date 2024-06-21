using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Domain.Order.Events;
public record OrderCreatedEvent(OrderId OrderId) : IDomainEvent;

using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Domain.Order.Events;
public record OrderApprovedEvent(OrderId OrderId) : IDomainEvent;

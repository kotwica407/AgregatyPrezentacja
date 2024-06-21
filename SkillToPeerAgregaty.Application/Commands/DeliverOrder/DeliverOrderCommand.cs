using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Application.Commands.DeliverOrder;
public sealed record DeliverOrderCommand(OrderId OrderId) : ICommand<Result>;

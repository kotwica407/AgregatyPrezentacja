using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Application.Commands.CancelOrder;
public sealed record CancelOrderCommand(OrderId OrderId) : ICommand<Result>;
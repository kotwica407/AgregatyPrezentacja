using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Application.Commands.ApproveOrder;
public record ApproveOrderCommand(OrderId OrderId) : ICommand<Result>;

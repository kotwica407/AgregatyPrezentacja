using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Application.Commands.CreateOrder;
public sealed record CreateOrderCommand(UserId UserId, List<ProductReservationRow> Rows) : ICommand<Result<OrderId>>;

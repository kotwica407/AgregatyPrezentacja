using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Application.Queries.GetOrder;
public record GetOrderQuery(OrderId OrderId) : IQuery<Result<OrderDto>>;

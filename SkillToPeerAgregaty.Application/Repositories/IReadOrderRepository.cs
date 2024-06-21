using SkillToPeerAgregaty.Application.Queries.GetOrder;
using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Application.Repositories;
public interface IReadOrderRepository
{
    Task<OrderDto?> GetOneAsync(OrderId orderId, CancellationToken cancellationToken = default);
    Task<IEnumerable<OrderDto>> GetAsync(CancellationToken cancellationToken = default);
}

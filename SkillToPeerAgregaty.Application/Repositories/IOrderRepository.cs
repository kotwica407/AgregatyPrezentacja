using SkillToPeerAgregaty.Domain.Order.Entities;
using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Application.Repositories;
public interface IOrderRepository
{
    Task AddAsync(Order order, CancellationToken cancellationToken = default);
    Task<Order?> GetAsync(OrderId orderId, CancellationToken cancellationToken = default);
    void Update(Order order);
}

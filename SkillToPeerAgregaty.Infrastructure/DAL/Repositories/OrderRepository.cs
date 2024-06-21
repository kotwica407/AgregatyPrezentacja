using Microsoft.EntityFrameworkCore;
using SkillToPeerAgregaty.Application.Repositories;
using SkillToPeerAgregaty.Domain.Order.Entities;
using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Infrastructure.DAL.Repositories;
internal class OrderRepository : IOrderRepository
{
    private readonly AgregatyDbContext _context;
    private readonly DbSet<Order> _orders;

    public OrderRepository(AgregatyDbContext context)
    {
        _context = context;
        _orders = _context.Orders;
    }
    public async Task AddAsync(Order order, CancellationToken cancellationToken = default)
    {
        await _orders.AddAsync(order, cancellationToken);
    }

    public async Task<Order?> GetAsync(OrderId orderId, CancellationToken cancellationToken = default)
    {
        return await _orders
            .Include(x => x.Items)
            .Where(x => x.Id == orderId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public void Update(Order order)
    {
        _orders.Update(order);
    }
}

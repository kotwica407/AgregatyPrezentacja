using Microsoft.EntityFrameworkCore;
using SkillToPeerAgregaty.Application.Queries.GetOrder;
using SkillToPeerAgregaty.Application.Repositories;
using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Infrastructure.DAL.Repositories;
internal class ReadOrderRepository : IReadOrderRepository
{
    private readonly AgregatyDbContext _context;

    public ReadOrderRepository(AgregatyDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrderDto>> GetAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .OrderBy(x => x.CreatedUtcDate)
            .Select(x => new OrderDto
            {
                Id = x.Id.Value,
                UserId = x.UserId.Value,
                Status = x.Status,
                Items = x.Items.Select(i => new ItemDto
                {
                    ProductId = i.ProductId.Value,
                    Amount = i.Amount.Value,
                    Price = i.Price.Value
                })
                .ToArray()
            }).ToListAsync(cancellationToken);
    }

    public async Task<OrderDto?> GetOneAsync(OrderId orderId, CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .Where(x => x.Id == orderId)
            .Select(x => new OrderDto
            {
                Id = x.Id.Value,
                UserId = x.UserId.Value,
                Status = x.Status,
                Items = x.Items.Select(i => new ItemDto
                {
                    ProductId = i.ProductId.Value,
                    Amount = i.Amount.Value,
                    Price = i.Price.Value
                })
                .ToArray()
            }).FirstOrDefaultAsync(cancellationToken);
    }
}

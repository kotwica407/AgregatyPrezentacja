using Microsoft.EntityFrameworkCore;
using SkillToPeerAgregaty.Application.Queries.GetProduct;
using SkillToPeerAgregaty.Application.Repositories;
using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Infrastructure.DAL.Repositories;
internal class ReadProductRepository : IReadProductRepository
{
    private readonly AgregatyDbContext _context;

    public ReadProductRepository(AgregatyDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductDto>> GetAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Select(x => new ProductDto
            {
                Id = x.Id.Value,
                Name = x.Name,
                AvailableAmount = x.AvailableAmount.Value
            }).ToListAsync(cancellationToken);
    }

    public async Task<ProductDto?> GetOneAsync(ProductId productId, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Where(x => x.Id == productId)
            .Select(x => new ProductDto
            {
                Id = x.Id.Value,
                Name = x.Name,
                AvailableAmount = x.AvailableAmount.Value
            }).FirstOrDefaultAsync(cancellationToken);
    }
}

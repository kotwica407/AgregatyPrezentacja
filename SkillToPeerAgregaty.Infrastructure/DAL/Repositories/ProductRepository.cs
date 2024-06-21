using Microsoft.EntityFrameworkCore;
using SkillToPeerAgregaty.Application.Repositories;
using SkillToPeerAgregaty.Domain.Product.Entities;
using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Infrastructure.DAL.Repositories;
internal class ProductRepository : IProductRepository
{
    private readonly AgregatyDbContext _context;
    private readonly DbSet<Product> _products;

    public ProductRepository(AgregatyDbContext context)
    {
        _context = context;
        _products = _context.Products;
    }
    public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _products.AddAsync(product, cancellationToken);
    }

    public async Task<Product?> GetAsync(ProductId productId, CancellationToken cancellationToken = default)
    {
        return await _products
            .Where(x => x.Id == productId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(IEnumerable<ProductId> productIds, CancellationToken cancellationToken = default)
    {
        return await _products
            .Where(x => productIds.Contains(x.Id))
            .ToListAsync(cancellationToken);
    }

    public void Update(Product product)
    {
        _products.Update(product);
    }
}

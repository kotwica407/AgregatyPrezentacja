using SkillToPeerAgregaty.Domain.Product.Entities;
using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Application.Repositories;
public interface IProductRepository
{
    Task AddAsync(Product product, CancellationToken cancellationToken = default);
    Task<Product?> GetAsync(ProductId productId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Product>> GetProductsAsync(IEnumerable<ProductId> productIds, CancellationToken cancellationToken = default);
    void Update(Product product);
}

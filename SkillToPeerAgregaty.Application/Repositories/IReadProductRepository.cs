using SkillToPeerAgregaty.Application.Queries.GetProduct;
using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Application.Repositories;
public interface IReadProductRepository
{
    Task<ProductDto?> GetOneAsync(ProductId productId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ProductDto>> GetAsync(CancellationToken cancellationToken = default);
}

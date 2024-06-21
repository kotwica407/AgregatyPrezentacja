using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Application.Queries.GetProduct;
public record GetProductQuery(ProductId ProductId) : IQuery<Result<ProductDto>>;

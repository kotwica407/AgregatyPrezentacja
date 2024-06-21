using SkillToPeerAgregaty.Application.Repositories;

namespace SkillToPeerAgregaty.Application.Queries.GetProduct;
internal sealed class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IReadProductRepository _repository;

    public GetProductsQueryHandler(IReadProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAsync(cancellationToken);
    }
}

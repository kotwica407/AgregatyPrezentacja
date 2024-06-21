using SkillToPeerAgregaty.Application.Repositories;
using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Application.Queries.GetProduct;
internal sealed class GetProductQueryHandler : IQueryHandler<GetProductQuery, Result<ProductDto>>
{
    private readonly IReadProductRepository _readProductRepository;

    public GetProductQueryHandler(IReadProductRepository readProductRepository)
    {
        _readProductRepository = readProductRepository;
    }

    public async Task<Result<ProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var dto = await _readProductRepository.GetOneAsync(request.ProductId, cancellationToken);
        if (dto is null) return Result.Failure<ProductDto>(Validation.ValidationErrors.Product.NotFound);
        return Result.Success(dto);
    }
}

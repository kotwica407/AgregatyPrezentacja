using SkillToPeerAgregaty.Application.Repositories;
using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Application.Commands.IncreaseProduct;
internal sealed class IncreaseProductCommandHandler : ICommandHandler<IncreaseProductCommand, Result>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public IncreaseProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(IncreaseProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetAsync(request.ProductId, cancellationToken);

        if (product is null) return Result.Failure(Validation.ValidationErrors.Product.NotFound);

        await Task.Delay(5000);

        var result = product.Increase(request.Amount);

        if (result.IsFailure) return result;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result;
    }
}

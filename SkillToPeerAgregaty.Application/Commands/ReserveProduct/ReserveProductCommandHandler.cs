using SkillToPeerAgregaty.Application.Repositories;
using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Application.Commands.ReserveProduct;
internal sealed class ReserveProductCommandHandler : ICommandHandler<ReserveProductCommand, Result>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReserveProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ReserveProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetAsync(request.ProductId, cancellationToken);

        if (product is null) return Result.Failure(Validation.ValidationErrors.Product.NotFound);

        await Task.Delay(5000);

        var result = product.Reserve(request.Amount);

        if (result.IsFailure) return result;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result;
    }
}

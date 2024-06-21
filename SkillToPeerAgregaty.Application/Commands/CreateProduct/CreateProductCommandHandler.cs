using SkillToPeerAgregaty.Application.Repositories;
using SkillToPeerAgregaty.Domain.Product.Entities;
using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Application.Commands.CreateProduct;
internal sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Result<ProductId>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ProductId>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productResult = Product.Create(request.Name, request.StartAmount);

        if (productResult.IsFailure) return Result.Failure<ProductId>(productResult.Error);

        var product = productResult.Value;
        await _productRepository.AddAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success(product.Id);
    }
}

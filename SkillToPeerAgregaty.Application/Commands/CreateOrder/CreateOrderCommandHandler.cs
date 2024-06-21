using SkillToPeerAgregaty.Application.Repositories;
using SkillToPeerAgregaty.Domain.Order.Entities;
using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Application.Commands.CreateOrder;
internal sealed class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, Result<OrderId>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<OrderId>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetProductsAsync(request.Rows.Select(x => x.ProductId), cancellationToken);

        if (products.Count() != request.Rows.Count)
            return Result.Failure<OrderId>(Validation.ValidationErrors.Product.NotFound);

        foreach (var product in products)
        {
            var row = request.Rows.First(x => x.ProductId == product.Id);

            var reservationResult = product.Reserve(row.Amount);

            if (reservationResult.IsFailure) return Result.Failure<OrderId>(reservationResult.Error);
        }

        var orderResult = Order.Create(request.Rows, request.UserId);

        if (orderResult.IsFailure) return Result.Failure<OrderId>(orderResult.Error);

        await _orderRepository.AddAsync(orderResult.Value, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(orderResult.Value.Id);
    }
}

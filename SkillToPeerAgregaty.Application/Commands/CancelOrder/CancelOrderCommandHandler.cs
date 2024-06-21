using SkillToPeerAgregaty.Application.Repositories;
using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Application.Commands.CancelOrder;
internal sealed class CancelOrderCommandHandler : ICommandHandler<CancelOrderCommand, Result>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CancelOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
    }

    public async Task<Result> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetAsync(request.OrderId, cancellationToken);
        if (order is null)
            return Result.Failure(Validation.ValidationErrors.Order.NotFound);

        var products = await _productRepository.GetProductsAsync(order.Items.Select(x => x.ProductId), cancellationToken);

        var result = order.Cancel();

        if (result.IsFailure) return result;

        foreach (var product in products)
        {
            var correspondingItem = order.Items.First(x => x.ProductId == product.Id);
            product.Increase(correspondingItem.Amount);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result;
    }
}

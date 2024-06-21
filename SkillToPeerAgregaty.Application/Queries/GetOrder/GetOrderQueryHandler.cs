using SkillToPeerAgregaty.Application.Repositories;
using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Application.Queries.GetOrder;
internal sealed class GetOrderQueryHandler : IQueryHandler<GetOrderQuery, Result<OrderDto>>
{
    private readonly IReadOrderRepository _readOrderRepository;

    public GetOrderQueryHandler(IReadOrderRepository readOrderRepository)
    {
        _readOrderRepository = readOrderRepository;
    }

    public async Task<Result<OrderDto>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var result = await _readOrderRepository.GetOneAsync(request.OrderId, cancellationToken);
        if (result is null) return Result.Failure<OrderDto>(Validation.ValidationErrors.Order.NotFound);
        return result;
    }
}

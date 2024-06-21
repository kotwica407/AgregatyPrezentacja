using SkillToPeerAgregaty.Application.Repositories;

namespace SkillToPeerAgregaty.Application.Queries.GetOrder;
internal sealed class GetOrdersQueryHandler : IQueryHandler<GetOrdersQuery, IEnumerable<OrderDto>>
{
    private readonly IReadOrderRepository _readOrderRepository;

    public GetOrdersQueryHandler(IReadOrderRepository readOrderRepository)
    {
        _readOrderRepository = readOrderRepository;
    }

    public async Task<IEnumerable<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        return await _readOrderRepository.GetAsync(cancellationToken);
    }
}

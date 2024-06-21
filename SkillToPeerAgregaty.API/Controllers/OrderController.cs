using MediatR;
using Microsoft.AspNetCore.Mvc;
using SkillToPeerAgregaty.Application.Commands.ApproveOrder;
using SkillToPeerAgregaty.Application.Commands.CancelOrder;
using SkillToPeerAgregaty.Application.Commands.CreateOrder;
using SkillToPeerAgregaty.Application.Commands.DeliverOrder;
using SkillToPeerAgregaty.Application.Queries.GetOrder;
using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : BaseController
{
    public OrderController(IServiceProvider serviceProvider, IMediator mediator) : base(serviceProvider, mediator)
    {
    }

    [HttpGet()]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var result = await QueryAsync(new GetOrdersQuery(), cancellationToken);
        return new OkObjectResult(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOne([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var result = await QueryAsync(new GetOrderQuery(new OrderId(id)), cancellationToken);

        return result.IsFailure
            ? new NotFoundResult()
            : new OkObjectResult(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderDto dto, CancellationToken cancellationToken = default)
    {
        var command = new CreateOrderCommand(new UserId(dto.UserId),
            dto.Items.Select(x => new ProductReservationRow(new ProductId(x.ProductId), new Quantity(x.Amount), new Price(x.Price))).ToList());

        var result = await CommandAsync(command, cancellationToken);

        return result.IsFailure
            ? new BadRequestObjectResult(result.Error)
            : new OkObjectResult(result.Value.Value);
    }

    [HttpPost("{id:guid}/approve")]
    public async Task<IActionResult> Approve([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var command = new ApproveOrderCommand(new OrderId(id));
        var result = await CommandAsync(command, cancellationToken);

        return result.IsFailure
            ? new BadRequestObjectResult(result.Error)
            : new OkResult();
    }

    [HttpPost("{id:guid}/deliver")]
    public async Task<IActionResult> Deliver([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var command = new DeliverOrderCommand(new OrderId(id));
        var result = await CommandAsync(command, cancellationToken);

        return result.IsFailure
            ? new BadRequestObjectResult(result.Error)
            : new OkResult();
    }

    [HttpPost("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var command = new CancelOrderCommand(new OrderId(id));
        var result = await CommandAsync(command, cancellationToken);

        return result.IsFailure
            ? new BadRequestObjectResult(result.Error)
            : new OkResult();
    }

    public class CreateOrderDto
    {
        public Guid UserId { get; set; }
        public ItemDto[] Items { get; set; }
    }
    public class ItemDto
    {
        public Guid ProductId { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
    }
}

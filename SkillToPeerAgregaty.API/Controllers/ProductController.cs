using MediatR;
using Microsoft.AspNetCore.Mvc;
using SkillToPeerAgregaty.Application.Commands.CreateProduct;
using SkillToPeerAgregaty.Application.Commands.IncreaseProduct;
using SkillToPeerAgregaty.Application.Commands.ReserveProduct;
using SkillToPeerAgregaty.Application.Queries.GetProduct;
using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : BaseController
{
    public ProductController(IServiceProvider serviceProvider, IMediator mediator) : base(serviceProvider, mediator)
    {
    }

    [HttpGet()]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var result = await QueryAsync(new GetProductsQuery(), cancellationToken);
        return new OkObjectResult(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOne([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var result = await QueryAsync(new GetProductQuery(new ProductId(id)), cancellationToken);

        return result.IsFailure
            ? new NotFoundResult()
            : new OkObjectResult(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductDto dto, CancellationToken cancellationToken = default)
    {
        var command = new CreateProductCommand(dto.Name, new Quantity(dto.StartAmount));
        var result = await CommandAsync(command, cancellationToken);

        return result.IsFailure
            ? new BadRequestObjectResult(result.Error)
            : new OkObjectResult(result.Value.Value);
    }

    [HttpPost("{id:guid}/increase")]
    public async Task<IActionResult> Increase([FromRoute] Guid id, [FromBody] ChangeAmountDto dto, CancellationToken cancellationToken = default)
    {
        var command = new IncreaseProductCommand(new ProductId(id), new Quantity(dto.Amount));
        var result = await CommandAsync(command, cancellationToken);

        return result.IsFailure
            ? new BadRequestObjectResult(result.Error)
            : new OkResult();
    }

    [HttpPost("{id:guid}/reserve")]
    public async Task<IActionResult> Reserve([FromRoute] Guid id, [FromBody] ChangeAmountDto dto, CancellationToken cancellationToken = default)
    {
        var command = new ReserveProductCommand(new ProductId(id), new Quantity(dto.Amount));
        var result = await CommandAsync(command, cancellationToken);

        return result.IsFailure
            ? new BadRequestObjectResult(result.Error)
            : new OkResult();
    }

    public class CreateProductDto
    {
        public string Name { get; set; }
        public decimal StartAmount { get; set; }
    }

    public class ChangeAmountDto
    {
        public decimal Amount { get; set; }
    }
}

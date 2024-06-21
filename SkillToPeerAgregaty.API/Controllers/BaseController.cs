using MediatR;
using Microsoft.AspNetCore.Mvc;
using Polly;
using SkillToPeerAgregaty.Application.Commands;
using SkillToPeerAgregaty.Application.Queries;
using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.API.Controllers;

public abstract class BaseController : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMediator _mediator;
    private readonly ResiliencePipeline _pipeline;

    protected BaseController(IServiceProvider serviceProvider, IMediator mediator)
    {
        _serviceProvider = serviceProvider;
        _mediator = mediator;
        _pipeline = serviceProvider.GetRequiredKeyedService<ResiliencePipeline>("optimistic-concurrency-pipeline");
    }

    protected async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(query, cancellationToken);
    }

    protected async Task<TResult> CommandAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
        where TResult : Result
    {
        return await _mediator.Send(command, cancellationToken);
        //return await _pipeline.ExecuteAsync(async token =>
        //{
        //    using (var scope = _serviceProvider.CreateScope())
        //    {
        //        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        //        return await mediator.Send(command, token);
        //    }
        //}, cancellationToken);
    }
}

using MediatR;
using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Application.Commands;
public interface ICommand<out TResponse> : IRequest<TResponse>
    where TResponse : Result
{
}

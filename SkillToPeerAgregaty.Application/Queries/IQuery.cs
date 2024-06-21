using MediatR;

namespace SkillToPeerAgregaty.Application.Queries;
public interface IQuery<out T> : IRequest<T>
{
}

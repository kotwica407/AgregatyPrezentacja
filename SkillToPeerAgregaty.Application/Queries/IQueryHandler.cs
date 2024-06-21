using MediatR;

namespace SkillToPeerAgregaty.Application.Queries;
public interface IQueryHandler<TQuery, T> : IRequestHandler<TQuery, T> where TQuery : class, IQuery<T>
{
}
using MediatR;

namespace SkillToPeerAgregaty.Application.Queries.GetProduct;

public interface IQueryHandler<TQuery, T> : IRequestHandler<TQuery, T> where TQuery : class, IQuery<T>
{
}
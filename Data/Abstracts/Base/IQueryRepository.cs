using System.Linq.Expressions;

namespace Data.Abstracts.Base;
public interface IQueryRepository<T> where T : class
{
    Task<List<T>?> GetAll(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);

    Task<T?> GetById(Guid id, CancellationToken cancellationToken);

    Task<T?> Get(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);
}

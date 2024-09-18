namespace Data.Abstracts.Base;
public interface ICommandRepository<T> where T : class
{
    Task<T?> Create(T entity, CancellationToken cancellationToken);

    Task<T?> Delete(Guid id, CancellationToken cancellationToken);

    Task<int> SaveChanges(CancellationToken cancellationToken);
}

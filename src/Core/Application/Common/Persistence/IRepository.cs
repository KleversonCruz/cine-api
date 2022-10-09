using Domain.Common;

namespace Application.Common.Persistence
{
    public interface IRepository<T> : IRepositoryBase<T>
        where T : class, IAggregateRoot
    {
        Task<T> CheckIfExistAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
        Task<TResult> CheckIfExistAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);
    }
    public interface IReadRepository<T> : IReadRepositoryBase<T>
        where T : class, IAggregateRoot
    {

    }
}

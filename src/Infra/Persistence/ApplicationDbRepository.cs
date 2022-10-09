using Application.Common.Exceptions;
using Application.Common.Persistence;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Domain.Common;
using Mapster;

namespace Infra.Persistence
{
    public class ApplicationDbRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T>
        where T : class, IAggregateRoot
    {
        public ApplicationDbRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<T> CheckIfExistAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            return await FirstOrDefaultAsync(specification, cancellationToken)
                ?? throw new NotFoundException(string.Format("{0} não encontrado(a).", typeof(T).Name));
        }

        public async Task<TResult> CheckIfExistAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
        {
            return await FirstOrDefaultAsync(specification, cancellationToken)
                ?? throw new NotFoundException(string.Format("{0} não encontrado(a).", typeof(T).Name));
        }

        protected override IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> specification) =>
            ApplySpecification(specification, false)
                .ProjectToType<TResult>();
    }
}

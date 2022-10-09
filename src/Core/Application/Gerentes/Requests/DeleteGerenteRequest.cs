using Application.Gerentes.Specs;

namespace Application.Gerentes.Requests
{
    public class DeleteGerenteRequest : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public DeleteGerenteRequest(Guid id) => Id = id;
    }

    public class DeleteGerenteRequestHandler : IRequestHandler<DeleteGerenteRequest, Guid>
    {
        private readonly IRepository<Gerente> _repository;
        public DeleteGerenteRequestHandler(IRepository<Gerente> repository) => _repository = repository;

        public async Task<Guid> Handle(DeleteGerenteRequest request, CancellationToken cancellationToken)
        {
            var gerente = await _repository.CheckIfExistAsync(new GetGerenteByIdSpec(request.Id), cancellationToken);

            await _repository.DeleteAsync(gerente, cancellationToken);
            return request.Id;
        }
    }
}

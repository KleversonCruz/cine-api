using Application.Sessoes.Specs;

namespace Application.Sessoes.Requests
{
    public class DeleteSessaoRequest : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public DeleteSessaoRequest(Guid id) => Id = id;
    }

    public class DeleteSessaoRequestHandler : IRequestHandler<DeleteSessaoRequest, Guid>
    {
        private readonly IRepository<Sessao> _repository;
        public DeleteSessaoRequestHandler(IRepository<Sessao> repository) => _repository = repository;

        public async Task<Guid> Handle(DeleteSessaoRequest request, CancellationToken cancellationToken)
        {
            var sessao = await _repository.CheckIfExistAsync(new GetSessaoByIdSpec(request.Id), cancellationToken);

            await _repository.DeleteAsync(sessao, cancellationToken);
            return request.Id;
        }
    }
}

using Application.Filmes.Specs;

namespace Application.Filmes.Requests
{
    public class DeleteFilmeRequest : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public DeleteFilmeRequest(Guid id) => Id = id;
    }

    public class DeleteFilmeRequestHandler : IRequestHandler<DeleteFilmeRequest, Guid>
    {
        private readonly IRepository<Filme> _repository;
        public DeleteFilmeRequestHandler(IRepository<Filme> repository) => _repository = repository;

        public async Task<Guid> Handle(DeleteFilmeRequest request, CancellationToken cancellationToken)
        {
            var filme = await _repository.CheckIfExistAsync(new GetFilmeByIdSpec(request.Id), cancellationToken);

            await _repository.DeleteAsync(filme, cancellationToken);
            return request.Id;
        }
    }
}

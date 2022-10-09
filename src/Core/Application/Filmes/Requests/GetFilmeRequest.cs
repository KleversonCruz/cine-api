using Application.Filmes.Dtos;
using Application.Filmes.Specs;

namespace Application.Filmes.Requests
{

    public class GetFilmeRequest : IRequest<FilmeDto>
    {
        public Guid Id { get; set; }

        public GetFilmeRequest(Guid id) => Id = id;
    }

    public class GetFilmeRequestHandler : IRequestHandler<GetFilmeRequest, FilmeDto>
    {
        private readonly IRepository<Filme> _repository;

        public GetFilmeRequestHandler(IRepository<Filme> repository) => _repository = repository;

        public async Task<FilmeDto> Handle(GetFilmeRequest request, CancellationToken cancellationToken)
            => await _repository.CheckIfExistAsync(new GetFilmeByIdSpec<FilmeDto>(request.Id), cancellationToken);
    }
}

using Application.Filmes.Dtos;
using Application.Filmes.Specs;

namespace Application.Filmes.Requests
{
    public class GetFilmesRequest : IRequest<List<FilmeDto>> { }

    public class GetGerentesRequestHandler : IRequestHandler<GetFilmesRequest, List<FilmeDto>>
    {
        private readonly IRepository<Filme> _repository;

        public GetGerentesRequestHandler(IRepository<Filme> repository) => _repository = repository;

        public async Task<List<FilmeDto>> Handle(GetFilmesRequest request, CancellationToken cancellationToken) =>
            await _repository.ListAsync(new GetFilmesSpec<FilmeDto>(), cancellationToken);
    }
}

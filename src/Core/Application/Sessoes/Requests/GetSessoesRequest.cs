using Application.Sessoes.Dtos;
using Application.Sessoes.Specs;

namespace Application.Sessoes.Requests
{
    public class GetSessoesRequest : IRequest<List<SessaoDto>> { }

    public class GetSessoesRequestHandler : IRequestHandler<GetSessoesRequest, List<SessaoDto>>
    {
        private readonly IRepository<Sessao> _repository;

        public GetSessoesRequestHandler(IRepository<Sessao> repository) => _repository = repository;

        public async Task<List<SessaoDto>> Handle(GetSessoesRequest request, CancellationToken cancellationToken) =>
            await _repository.ListAsync(new GetSessoesSpec<SessaoDto>(), cancellationToken);
    }
}

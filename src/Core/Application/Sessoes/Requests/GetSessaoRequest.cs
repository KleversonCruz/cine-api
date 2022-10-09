using Application.Sessoes.Dtos;
using Application.Sessoes.Specs;

namespace Application.Sessoes.Requests
{

    public class GetSessaoRequest : IRequest<SessaoDto>
    {
        public Guid Id { get; set; }

        public GetSessaoRequest(Guid id) => Id = id;
    }

    public class GetSessaoRequestHandler : IRequestHandler<GetSessaoRequest, SessaoDto>
    {
        private readonly IRepository<Sessao> _repository;

        public GetSessaoRequestHandler(IRepository<Sessao> repository) => _repository = repository;

        public async Task<SessaoDto> Handle(GetSessaoRequest request, CancellationToken cancellationToken)
            => await _repository.CheckIfExistAsync(new GetSessaoByIdSpec<SessaoDto>(request.Id), cancellationToken);
    }
}

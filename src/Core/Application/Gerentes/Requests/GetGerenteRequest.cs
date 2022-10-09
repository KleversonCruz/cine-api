using Application.Gerentes.Dtos;
using Application.Gerentes.Specs;

namespace Application.Gerentes.Requests
{

    public class GetGerenteRequest : IRequest<GerenteDto>
    {
        public Guid Id { get; set; }

        public GetGerenteRequest(Guid id) => Id = id;
    }

    public class GetGerenteRequestHandler : IRequestHandler<GetGerenteRequest, GerenteDto>
    {
        private readonly IRepository<Gerente> _repository;

        public GetGerenteRequestHandler(IRepository<Gerente> repository) => _repository = repository;

        public async Task<GerenteDto> Handle(GetGerenteRequest request, CancellationToken cancellationToken)
            => await _repository.CheckIfExistAsync(new GetGerenteByIdSpec<GerenteDto>(request.Id), cancellationToken);
    }
}

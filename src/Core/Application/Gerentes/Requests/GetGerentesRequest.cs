using Application.Gerentes.Dtos;
using Application.Gerentes.Specs;

namespace Application.Gerentes.Requests
{
    public class GetGerentesRequest : IRequest<List<GerenteDto>> { }

    public class GetGerentesRequestHandler : IRequestHandler<GetGerentesRequest, List<GerenteDto>>
    {
        private readonly IRepository<Gerente> _repository;

        public GetGerentesRequestHandler(IRepository<Gerente> repository) => _repository = repository;

        public async Task<List<GerenteDto>> Handle(GetGerentesRequest request, CancellationToken cancellationToken) =>
            await _repository.ListAsync(new GetGerentesSpec<GerenteDto>(), cancellationToken);
    }
}

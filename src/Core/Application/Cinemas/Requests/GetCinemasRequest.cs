using Application.Cinemas.Dtos;
using Application.Cinemas.Specs;

namespace Application.Cinemas.Requests
{
    public class GetCinemasRequest : IRequest<List<CinemaDto>> { }

    public class GetCinemasRequestHandler : IRequestHandler<GetCinemasRequest, List<CinemaDto>>
    {
        private readonly IRepository<Cinema> _repository;

        public GetCinemasRequestHandler(IRepository<Cinema> repository) => _repository = repository;

        public async Task<List<CinemaDto>> Handle(GetCinemasRequest request, CancellationToken cancellationToken) =>
            await _repository.ListAsync(new GetCinemasSpec<CinemaDto>(), cancellationToken);
    }
}

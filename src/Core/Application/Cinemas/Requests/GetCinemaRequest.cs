using Application.Cinemas.Dtos;
using Application.Cinemas.Specs;

namespace Application.Cinemas.Requests
{
    public class GetCinemaRequest : IRequest<CinemaDetailsDto>
    {
        public Guid Id { get; set; }

        public GetCinemaRequest(Guid id) => Id = id;
    }

    public class GetCinemaRequestHandler : IRequestHandler<GetCinemaRequest, CinemaDetailsDto>
    {
        private readonly IRepository<Cinema> _repository;

        public GetCinemaRequestHandler(IRepository<Cinema> repository) => _repository = repository;

        public async Task<CinemaDetailsDto> Handle(GetCinemaRequest request, CancellationToken cancellationToken)
            => await _repository.CheckIfExistAsync(new GetCinemaByIdSpec<CinemaDetailsDto>(request.Id), cancellationToken);
    }
}

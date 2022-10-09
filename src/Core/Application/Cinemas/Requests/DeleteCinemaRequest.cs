using Application.Cinemas.Specs;

namespace Application.Cinemas.Requests
{
    public class DeleteCinemaRequest : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public DeleteCinemaRequest(Guid id) => Id = id;
    }

    public class DeleteCinemaRequestHandler : IRequestHandler<DeleteCinemaRequest, Guid>
    {
        private readonly IRepository<Cinema> _repository;
        public DeleteCinemaRequestHandler(IRepository<Cinema> repository) => _repository = repository;

        public async Task<Guid> Handle(DeleteCinemaRequest request, CancellationToken cancellationToken)
        {
            var cinema = await _repository.CheckIfExistAsync(new GetCinemaByIdSpec(request.Id), cancellationToken);

            await _repository.DeleteAsync(cinema, cancellationToken);
            return request.Id;
        }
    }
}

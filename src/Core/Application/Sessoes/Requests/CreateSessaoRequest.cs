using Application.Cinemas.Specs;
using Application.Filmes.Specs;

namespace Application.Sessoes.Requests
{
    public class CreateSessaoRequest : IRequest<Guid>
    {
        public Guid CinemaId { get; set; }
        public Guid FilmeId { get; set; }
        public DateTime HorarioDeEncerramento { get; set; }
    }

    public class CreateSessaoRequestValidator : CustomValidator<CreateSessaoRequest>
    {
        public CreateSessaoRequestValidator()
        {
            RuleFor(s => s.CinemaId)
                .NotEmpty();

            RuleFor(s => s.FilmeId)
                .NotEmpty();

            RuleFor(s => s.HorarioDeEncerramento)
                .NotEmpty();
        }
    }

    public class CreateSessaoRequestHandler : IRequestHandler<CreateSessaoRequest, Guid>
    {
        private readonly IRepository<Sessao> _sessaoRepository;
        private readonly IRepository<Cinema> _cinemaRepository;
        private readonly IRepository<Filme> _filmeRepository;

        public CreateSessaoRequestHandler(IRepository<Sessao> sessaoRepository, IRepository<Cinema> cinemaRepository, IRepository<Filme> filmeRepository)
        {
            _sessaoRepository = sessaoRepository;
            _cinemaRepository = cinemaRepository;
            _filmeRepository = filmeRepository;
        }

        public async Task<Guid> Handle(CreateSessaoRequest request, CancellationToken cancellationToken)
        {
            await _filmeRepository.CheckIfExistAsync(new GetFilmeByIdSpec(request.FilmeId), cancellationToken);
            await _cinemaRepository.CheckIfExistAsync(new GetCinemaByIdSpec(request.CinemaId), cancellationToken);

            var sessao = new Sessao(request.CinemaId, request.FilmeId, request.HorarioDeEncerramento);
            await _sessaoRepository.AddAsync(sessao, cancellationToken);
            return sessao.Id;
        }
    }
}

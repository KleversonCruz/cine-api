using Application.Cinemas.Specs;
using Application.Filmes.Specs;
using Application.Sessoes.Specs;

namespace Application.Sessoes.Requests
{
    public class UpdateSessaoRequest : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public Guid CinemaId { get; set; }
        public Guid FilmeId { get; set; }
        public DateTime HorarioDeEncerramento { get; set; }
    }

    public class UpdateSessaoRequestValidator : CustomValidator<UpdateSessaoRequest>
    {
        public UpdateSessaoRequestValidator()
        {
            RuleFor(s => s.Id)
                .NotEmpty();

            RuleFor(s => s.CinemaId)
                .NotEmpty();

            RuleFor(s => s.FilmeId)
                .NotEmpty();

            RuleFor(s => s.HorarioDeEncerramento)
                .NotEmpty();
        }
    }

    public partial class UpdateSessaoRequestHandler : IRequestHandler<UpdateSessaoRequest, Guid>
    {
        private readonly IRepository<Sessao> _sessaoRepository;
        private readonly IRepository<Cinema> _cinemaRepository;
        private readonly IRepository<Filme> _filmeRepository;

        public UpdateSessaoRequestHandler(IRepository<Sessao> sessaoRepository, IRepository<Cinema> cinemaRepository, IRepository<Filme> filmeRepository)
        {
            _sessaoRepository = sessaoRepository;
            _cinemaRepository = cinemaRepository;
            _filmeRepository = filmeRepository;
        }

        public async Task<Guid> Handle(UpdateSessaoRequest request, CancellationToken cancellationToken)
        {
            await _filmeRepository.CheckIfExistAsync(new GetFilmeByIdSpec(request.FilmeId), cancellationToken);
            await _cinemaRepository.CheckIfExistAsync(new GetCinemaByIdSpec(request.CinemaId), cancellationToken);

            var sessao = await _sessaoRepository.CheckIfExistAsync(new GetSessaoByIdSpec(request.Id), cancellationToken);
            var updatedSessao = sessao.Update(request.CinemaId, request.FilmeId, request.HorarioDeEncerramento);

            await _sessaoRepository.UpdateAsync(updatedSessao, cancellationToken);
            return sessao.Id;
        }
    }
}

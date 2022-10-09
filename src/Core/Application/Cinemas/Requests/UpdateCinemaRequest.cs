using Application.Cinemas.Specs;
using Application.Gerentes.Specs;

namespace Application.Cinemas.Requests
{
    public class UpdateCinemaRequest : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = default!;
        public CreateEnderecoRequest Endereco { get; set; } = default!;
        public Guid GerenteId { get; set; }
    }

    public class UpdateCinemaRequestValidator : CustomValidator<UpdateCinemaRequest>
    {
        public UpdateCinemaRequestValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty();

            RuleFor(c => c.Nome)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p.Endereco)
                .SetNonNullableValidator(new CreateEnderecoRequestValidator());

            RuleFor(c => c.GerenteId)
                .NotEmpty();
        }
    }

    public partial class UpdateCinemaRequestHandler : IRequestHandler<UpdateCinemaRequest, Guid>
    {
        private readonly IRepository<Cinema> _cinemaRepository;
        private readonly IRepository<Gerente> _gerenteRepository;

        public UpdateCinemaRequestHandler(IRepository<Cinema> cinemaRepository, IRepository<Gerente> gerenteRepository)
            => (_cinemaRepository, _gerenteRepository) = (cinemaRepository, gerenteRepository);

        public async Task<Guid> Handle(UpdateCinemaRequest request, CancellationToken cancellationToken)
        {
            await _gerenteRepository.CheckIfExistAsync(new GetGerenteByIdSpec(request.GerenteId), cancellationToken);

            var cinema = await _cinemaRepository.CheckIfExistAsync(new GetCinemaByIdSpec(request.Id), cancellationToken);
            var endereco = new Endereco(request.Endereco.Logradouro, request.Endereco.Bairro, request.Endereco.Numero);
            var updatedCinema = cinema.Update(request.Nome, endereco, request.GerenteId);

            await _cinemaRepository.UpdateAsync(updatedCinema, cancellationToken);
            return cinema.Id;
        }
    }
}

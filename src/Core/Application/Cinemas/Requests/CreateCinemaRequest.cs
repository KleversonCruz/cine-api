using Application.Gerentes.Specs;

namespace Application.Cinemas.Requests
{
    public class CreateCinemaRequest : IRequest<Guid>
    {
        public string Nome { get; set; } = default!;
        public CreateEnderecoRequest Endereco { get; set; } = default!;
        public Guid GerenteId { get; set; }
    }

    public class CreateCinemaRequestValidator : CustomValidator<CreateCinemaRequest>
    {
        public CreateCinemaRequestValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p.Endereco)
                .SetNonNullableValidator(new CreateEnderecoRequestValidator());

            RuleFor(c => c.GerenteId)
                .NotEmpty();
        }
    }

    public class CreateCinemaRequestHandler : IRequestHandler<CreateCinemaRequest, Guid>
    {
        private readonly IRepository<Cinema> _cinemaRepository;
        private readonly IRepository<Gerente> _gerenteRepository;

        public CreateCinemaRequestHandler(IRepository<Cinema> cinemaRepository, IRepository<Gerente> gerenteRepository)
            => (_cinemaRepository, _gerenteRepository) = (cinemaRepository, gerenteRepository);

        public async Task<Guid> Handle(CreateCinemaRequest request, CancellationToken cancellationToken)
        {
            await _gerenteRepository.CheckIfExistAsync(new GetGerenteByIdSpec(request.GerenteId), cancellationToken);

            var endereco = new Endereco(request.Endereco.Logradouro, request.Endereco.Bairro, request.Endereco.Numero);
            var cinema = new Cinema(request.Nome)
                .WithEndereco(endereco)
                .WithGerente(request.GerenteId);

            await _cinemaRepository.AddAsync(cinema, cancellationToken);
            return cinema.Id;
        }
    }
}

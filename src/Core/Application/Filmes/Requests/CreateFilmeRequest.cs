namespace Application.Filmes.Requests
{
    public class CreateFilmeRequest : IRequest<Guid>
    {
        public string Titulo { get; set; } = default!;
        public int Duracao { get; set; }
        public string Diretor { get; set; } = default!;
        public string Genero { get; set; } = default!;
        public int ClassificacaoEtaria { get; set; }
    }

    public class CreateFilmeRequestValidator : CustomValidator<CreateFilmeRequest>
    {
        public CreateFilmeRequestValidator()
        {
            RuleFor(f => f.Titulo)
                .NotEmpty()
                .MaximumLength(50)
                .MinimumLength(3);

            RuleFor(f => f.Duracao)
                .GreaterThan(30);

            RuleFor(f => f.Diretor)
                .NotEmpty();

            RuleFor(f => f.Genero)
                .NotEmpty();

            RuleFor(c => c.ClassificacaoEtaria)
                .GreaterThanOrEqualTo(0);
        }
    }

    public class CreateFilmeRequestHandler : IRequestHandler<CreateFilmeRequest, Guid>
    {
        private readonly IRepository<Filme> _repository;

        public CreateFilmeRequestHandler(IRepository<Filme> repository) => _repository = repository;

        public async Task<Guid> Handle(CreateFilmeRequest request, CancellationToken cancellationToken)
        {
            var filme = new Filme(request.Titulo, request.Duracao, request.Diretor, request.Genero, request.ClassificacaoEtaria);
            await _repository.AddAsync(filme, cancellationToken);
            return filme.Id;
        }
    }
}

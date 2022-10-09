namespace Application.Gerentes.Requests
{
    public class CreateGerenteRequest : IRequest<Guid>
    {
        public string Nome { get; set; } = default!;
    }

    public class CreateGerenteRequestValidator : CustomValidator<CreateGerenteRequest>
    {
        public CreateGerenteRequestValidator()
        {
            RuleFor(g => g.Nome)
                .NotEmpty()
                .MaximumLength(50)
                .MinimumLength(3);
        }
    }

    public class CreateGerenteRequestHandler : IRequestHandler<CreateGerenteRequest, Guid>
    {
        private readonly IRepository<Gerente> _repository;

        public CreateGerenteRequestHandler(IRepository<Gerente> repository) => _repository = repository;

        public async Task<Guid> Handle(CreateGerenteRequest request, CancellationToken cancellationToken)
        {
            var gerente = new Gerente(request.Nome);
            await _repository.AddAsync(gerente, cancellationToken);
            return gerente.Id;
        }
    }
}

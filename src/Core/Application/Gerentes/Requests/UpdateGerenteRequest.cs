using Application.Gerentes.Specs;

namespace Application.Gerentes.Requests
{
    public class UpdateGerenteRequest : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = default!;
    }

    public class UpdateGerenteRequestValidator : CustomValidator<UpdateGerenteRequest>
    {
        public UpdateGerenteRequestValidator()
        {
            RuleFor(g => g.Id)
                .NotEmpty();

            RuleFor(g => g.Nome)
                .NotEmpty()
                .MaximumLength(50)
                .MinimumLength(3);
        }
    }

    public partial class UpdateGerenteRequestHandler : IRequestHandler<UpdateGerenteRequest, Guid>
    {
        private readonly IRepository<Gerente> _repository;

        public UpdateGerenteRequestHandler(IRepository<Gerente> cinemaRepository)
            => (_repository) = (cinemaRepository);

        public async Task<Guid> Handle(UpdateGerenteRequest request, CancellationToken cancellationToken)
        {
            var gerente = await _repository.CheckIfExistAsync(new GetGerenteByIdSpec(request.Id), cancellationToken);
            var updatedGerente = gerente.Update(request.Nome);

            await _repository.UpdateAsync(updatedGerente, cancellationToken);
            return gerente.Id;
        }
    }
}

using Application.Filmes.Specs;

namespace Application.Filmes.Requests
{
    public class UpdateFilmeRequest : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; } = default!;
        public int Duracao { get; set; }
        public string Diretor { get; set; } = default!;
        public string Genero { get; set; } = default!;
        public int ClassificacaoEtaria { get; set; }
    }

    public class UpdateFilmeRequestValidator : CustomValidator<UpdateFilmeRequest>
    {
        public UpdateFilmeRequestValidator()
        {
            RuleFor(f => f.Id)
               .NotEmpty();

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

    public partial class UpdateFilmeRequestHandler : IRequestHandler<UpdateFilmeRequest, Guid>
    {
        private readonly IRepository<Filme> _repository;

        public UpdateFilmeRequestHandler(IRepository<Filme> cinemaRepository)
            => (_repository) = (cinemaRepository);

        public async Task<Guid> Handle(UpdateFilmeRequest request, CancellationToken cancellationToken)
        {
            var filme = await _repository.CheckIfExistAsync(new GetFilmeByIdSpec(request.Id), cancellationToken);
            var updatedFilme = filme.Update(request.Titulo, request.Duracao, request.Diretor, request.Genero, request.ClassificacaoEtaria);

            await _repository.UpdateAsync(updatedFilme, cancellationToken);
            return filme.Id;
        }
    }
}

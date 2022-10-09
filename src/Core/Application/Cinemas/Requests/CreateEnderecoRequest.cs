namespace Application.Cinemas.Requests
{
    public class CreateEnderecoRequest
    {
        public string Numero { get; set; } = default!;
        public string Logradouro { get; set; } = default!;
        public string Bairro { get; set; } = default!;
    }

    public class CreateEnderecoRequestValidator : CustomValidator<CreateEnderecoRequest>
    {
        public CreateEnderecoRequestValidator()
        {
            RuleFor(e => e.Numero)
                .NotEmpty();
            RuleFor(e => e.Logradouro)
                .NotEmpty();
            RuleFor(e => e.Bairro)
                .NotEmpty();
        }
    }
}

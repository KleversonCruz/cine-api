namespace Application.Identity.Users
{
    public class UpdateUserRequest
    {
        public string Id { get; set; } = default!;
        public string? PhoneNumber { get; set; }
        public DateTime DataNascimento { get; set; }
        public string? Email { get; set; }
    }

    public class UpdateUserRequestValidator : CustomValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty();

            RuleFor(p => p.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
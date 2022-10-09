namespace Application.Identity.Users
{
    public class CreateUserRequest
    {
        public string Email { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public DateTime DataNascimento { get; set; }
        public string Password { get; set; } = default!;
        public string ConfirmPassword { get; set; } = default!;
        public string? PhoneNumber { get; set; }
    }

    public class CreateUserRequestValidator : CustomValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(u => u.Email).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress();

            RuleFor(u => u.UserName).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(p => p.Password).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(p => p.ConfirmPassword).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Equal(p => p.Password);
        }
    }
}

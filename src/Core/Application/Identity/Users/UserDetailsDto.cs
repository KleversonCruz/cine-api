namespace Application.Identity.Users
{
    public class UserDetailsDto
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
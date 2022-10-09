using Microsoft.AspNetCore.Identity;

namespace Infra.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime DataNascimento { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}

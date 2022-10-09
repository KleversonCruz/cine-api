using Application.Common.Interfaces;

namespace Application.Identity.Tokens
{
    public interface ITokenService : ITransientService
    {
        Task<TokenResponse> GetTokenAsync(TokenRequest request);

        Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest request);
    }
}

using Application.Identity.Tokens;

namespace Api.Controllers.Identity
{
    public sealed class TokensController : BaseApiController
    {
        private readonly ITokenService _tokenService;

        public TokensController(ITokenService tokenService) => _tokenService = tokenService;

        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Request an access token using credentials.")]
        public Task<TokenResponse> GetTokenAsync(TokenRequest request)
        {
            return _tokenService.GetTokenAsync(request);
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Request an access token using a refresh token.")]
        public Task<TokenResponse> RefreshAsync(RefreshTokenRequest request)
        {
            return _tokenService.RefreshTokenAsync(request);
        }
    }
}

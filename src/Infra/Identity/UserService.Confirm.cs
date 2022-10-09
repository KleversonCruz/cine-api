using Application.Common.Exceptions;
using Infra.Common;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Infra.Identity
{
    internal partial class UserService
    {
        private async Task<string> GetEmailVerificationUriAsync(ApplicationUser user, string origin)
        {
            string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            const string route = "api/users/confirm-email/";
            var endpointUri = new Uri(string.Concat($"{origin}/", route));
            string verificationUri = QueryHelpers.AddQueryString(endpointUri.ToString(), QueryStringKeys.UserId, user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, QueryStringKeys.Code, code);
            return verificationUri;
        }

        public async Task<string> ConfirmEmailAsync(string userId, string code, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .Where(u => u.Id == userId && !u.EmailConfirmed)
                .FirstOrDefaultAsync(cancellationToken);

            _ = user ?? throw new InternalServerException("Um erro ocorreu ao confirmar o e-mail.");

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);

            return result.Succeeded
                ? string.Format("Conta confirmada para o e-mail {0}. Você pode utilizar a rota /api/tokens para gerar um JWT.", user.Email)
                : throw new InternalServerException(string.Format("Um erro ocorreu enquanto confirmava o e-mail {0}.", user.Email));
        }
    }
}

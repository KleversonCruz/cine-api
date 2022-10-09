using Application.Common.Exceptions;
using Application.Common.Mailing;
using Application.Identity.Users.Password;
using Microsoft.AspNetCore.WebUtilities;

namespace Infra.Identity
{
    internal partial class UserService
    {
        public async Task<string> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
        {
            var user = await _userManager.FindByEmailAsync(request.Email.Normalize());
            if (user is null || !await _userManager.IsEmailConfirmedAsync(user))
                throw new InternalServerException("Um erro ocorreu!");

            string code = await _userManager.GeneratePasswordResetTokenAsync(user);
            const string route = "account/reset-password";
            var endpointUri = new Uri(string.Concat($"{origin}/", route));
            string passwordResetUrl = QueryHelpers.AddQueryString(endpointUri.ToString(), "Token", code);
            var mailRequest = new MailRequest(
                new List<string> { request.Email },
                "Recuperar senha",
                $"Seu código de recuperação é: '{code}'. Você pode recuperar sua senha utilizando a rota {endpointUri}.");
            await _mailService.SendAsync(mailRequest);

            return "Um email de recuperação foi enviado com sucesso.";

        }

        public async Task<string> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email?.Normalize());

            _ = user ?? throw new InternalServerException("Um erro ocorreu!");

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            return result.Succeeded
                ? "Senha restaurada com sucesso!"
                : throw new InternalServerException("Um erro ocorreu!");
        }

        public async Task ChangePasswordAsync(ChangePasswordRequest model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            _ = user ?? throw new NotFoundException("Usuário não encontrado");

            var result = await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);

            if (!result.Succeeded)
            {
                throw new InternalServerException("Mudança de senha falhou", result.Errors.Select(v => v.Description).ToList());
            }
        }
    }
}

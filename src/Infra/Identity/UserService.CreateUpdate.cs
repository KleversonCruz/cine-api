using Application.Common.Exceptions;
using Application.Common.Mailing;
using Application.Identity.Users;
using Shared.Authorization;

namespace Infra.Identity
{
    internal partial class UserService
    {
        public async Task<string> CreateAsync(CreateUserRequest request, string origin)
        {
            if (await ExistsWithEmailAsync(request.Email))
                throw new ConflictException("E-mail já está registrado");
            if (await ExistsWithNameAsync(request.UserName))
                throw new ConflictException("Usuário já foi registrado");


            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                throw new InternalServerException("Um ou mais erros ocorreram.", result.Errors.Select(v => v.Description).ToList());
            }

            await _userManager.AddToRoleAsync(user, AppRoles.Basic);

            var messages = new List<string> { string.Format("Usuário {0} cadastrado.", user.UserName) };

            await SendVerificationEmail(origin, user, messages);
            return string.Join(Environment.NewLine, messages);
        }

        private async Task SendVerificationEmail(string origin, ApplicationUser user, List<string> messages)
        {
            if (_securitySettings.RequireConfirmedAccount && !string.IsNullOrEmpty(user.Email))
            {
                string emailVerificationUri = await GetEmailVerificationUriAsync(user, origin);
                RegisterUserEmailModel eMailModel = new()
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    Url = emailVerificationUri
                };
                MailRequest mailRequest = new(
                    new List<string> { user.Email },
                    "Confirmar registro",
                    _templateService.GenerateEmailTemplate("email-confirmation", eMailModel));
                await _mailService.SendAsync(mailRequest);
                messages.Add($"Verifique em seu e-mail {user.Email} para confirmar seu registro!");
            }
        }

        public async Task UpdateAsync(UpdateUserRequest request, string userId)
        {
            if ((request.Email is not null) && await ExistsWithEmailAsync(request.Email))
                throw new ConflictException("E-mail já está registrado");

            var user = await _userManager.FindByIdAsync(userId);
            _ = user ?? throw new NotFoundException("Usuário não encontrado.");

            user.PhoneNumber = request.PhoneNumber;
            string phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (request.PhoneNumber != phoneNumber)
            {
                await _userManager.SetPhoneNumberAsync(user, request.PhoneNumber);
            }
            var result = await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            if (!result.Succeeded)
            {
                throw new InternalServerException("Falha ao atualizar usuário", result.Errors.Select(v => v.Description).ToList());
            }
        }
    }
}

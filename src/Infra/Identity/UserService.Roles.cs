using Application.Common.Exceptions;
using Application.Identity.Users;
using Microsoft.EntityFrameworkCore;
using Shared.Authorization;

namespace Infra.Identity
{
    internal partial class UserService
    {
        public async Task<List<UserRoleDto>> GetRolesAsync(string userId, CancellationToken cancellationToken)
        {
            var userRoles = new List<UserRoleDto>();

            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _roleManager.Roles.AsNoTracking().ToListAsync(cancellationToken);
            foreach (var role in roles)
            {
                userRoles.Add(new UserRoleDto
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    Description = role.Description,
                    Enabled = await _userManager.IsInRoleAsync(user, role.Name)
                });
            }

            return userRoles;
        }

        public async Task<string> AssignRolesAsync(string userId, UserRolesRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            var user = await _userManager.Users.Where(u => u.Id == userId).FirstOrDefaultAsync(cancellationToken);

            _ = user ?? throw new NotFoundException("Usuário não encontrado.");

            // Check if the user is an admin for which the admin role is getting disabled
            if (await _userManager.IsInRoleAsync(user, AppRoles.Admin)
                && request.UserRoles.Any(a => !a.Enabled && a.RoleName == AppRoles.Admin))
            {
                int adminCount = (await _userManager.GetUsersInRoleAsync(AppRoles.Admin)).Count;
                if (adminCount <= 1)
                {
                    throw new ConflictException("Deve haver ao menos um usuário admin.");
                }
            }

            foreach (var userRole in request.UserRoles)
            {
                // Check if Role Exists
                if (await _roleManager.FindByNameAsync(userRole.RoleName) is not null)
                {
                    if (userRole.Enabled)
                    {
                        if (!await _userManager.IsInRoleAsync(user, userRole.RoleName))
                        {
                            await _userManager.AddToRoleAsync(user, userRole.RoleName);
                        }
                    }
                    else
                    {
                        await _userManager.RemoveFromRoleAsync(user, userRole.RoleName);
                    }
                }
            }
            return "Roles atualizadas com sucesso.";
        }
    }
}

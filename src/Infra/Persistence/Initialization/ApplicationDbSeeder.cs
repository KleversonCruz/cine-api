using Infra.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Authorization;

namespace Infra.Persistence.Initialization
{
    internal class ApplicationDbSeeder
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CustomSeederRunner _seederRunner;

        public ApplicationDbSeeder(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, CustomSeederRunner seederRunner)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _seederRunner = seederRunner;
        }

        public async Task SeedDatabaseAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken)
        {
            await SeedRolesAsync(dbContext);
            await SeedAdminUserAsync();
            await _seederRunner.RunSeedersAsync(cancellationToken);
        }

        private async Task SeedRolesAsync(ApplicationDbContext dbContext)
        {
            foreach (string roleName in AppRoles.DefaultRoles)
            {
                if (await _roleManager.Roles.SingleOrDefaultAsync(r => r.Name == roleName)
                    is not ApplicationRole role)
                {
                    role = new ApplicationRole(roleName, $"{roleName} Role");
                    await _roleManager.CreateAsync(role);
                }

                // Assign permissions
                if (roleName == AppRoles.Basic)
                {
                    await AssignPermissionsToRoleAsync(dbContext, AppPermissions.Basic, role);
                }
                else if (roleName == AppRoles.Admin)
                {
                    await AssignPermissionsToRoleAsync(dbContext, AppPermissions.Admin, role);
                }
            }
        }

        private async Task AssignPermissionsToRoleAsync(ApplicationDbContext dbContext, IReadOnlyList<AppPermission> permissions, ApplicationRole role)
        {
            var currentClaims = await _roleManager.GetClaimsAsync(role);
            foreach (var permission in permissions)
            {
                if (!currentClaims.Any(c => c.Type == AppClaims.Permission && c.Value == permission.Name))
                {
                    dbContext.RoleClaims.Add(new IdentityRoleClaim<string>
                    {
                        RoleId = role.Id,
                        ClaimType = AppClaims.Permission,
                        ClaimValue = permission.Name
                    });
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private async Task SeedAdminUserAsync()
        {
            if (await _userManager.Users.FirstOrDefaultAsync(u => u.Email == AppConstants.Root.EmailAddress)
                is not ApplicationUser adminUser)
            {
                adminUser = new ApplicationUser
                {
                    Email = AppConstants.Root.EmailAddress,
                    UserName = AppConstants.Root.Name,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    NormalizedEmail = AppConstants.Root.EmailAddress.ToUpperInvariant(),
                    NormalizedUserName = AppConstants.Root.Name.ToUpperInvariant(),
                };
                var password = new PasswordHasher<ApplicationUser>();
                adminUser.PasswordHash = password.HashPassword(adminUser, AppConstants.DefaultPassword);
                await _userManager.CreateAsync(adminUser);
            }

            // Assign role to user
            if (!await _userManager.IsInRoleAsync(adminUser, AppRoles.Admin))
            {
                await _userManager.AddToRoleAsync(adminUser, AppRoles.Admin);
            }
        }
    }
}
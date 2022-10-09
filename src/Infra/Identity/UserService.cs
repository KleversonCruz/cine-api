using Application.Common.Exceptions;
using Application.Common.Mailing;
using Application.Identity.Users;
using Infra.Auth;
using Infra.Mailing;
using Infra.Persistence;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infra.Identity
{
    internal partial class UserService : IUserService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDbContext _db;
        private readonly IMailService _mailService;
        private readonly MailSettings _mailSettings;
        private readonly SecuritySettings _securitySettings;
        private readonly IEmailTemplateService _templateService;

        public UserService(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            ApplicationDbContext db,
            IEmailTemplateService templateService,
            IMailService mailService,
            IOptions<MailSettings> mailSettings,
            IOptions<SecuritySettings> securitySettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
            _templateService = templateService;
            _mailService = mailService;
            _securitySettings = securitySettings.Value;
            _mailSettings = mailSettings.Value;
        }

        public async Task<bool> ExistsWithNameAsync(string name)
        {
            return await _userManager.FindByNameAsync(name) is not null;
        }

        public async Task<bool> ExistsWithEmailAsync(string email, string? exceptId = null)
        {
            return await _userManager.FindByEmailAsync(email.Normalize()) is ApplicationUser user && user.Id != exceptId;
        }

        public async Task<List<UserDetailsDto>> GetListAsync(CancellationToken cancellationToken) =>
            (await _userManager.Users
                    .AsNoTracking()
                    .ToListAsync(cancellationToken))
                .Adapt<List<UserDetailsDto>>();

        public async Task<UserDetailsDto> GetAsync(string userId, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .AsNoTracking()
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync(cancellationToken);

            _ = user ?? throw new NotFoundException("Usuário não encontrado.");

            return user.Adapt<UserDetailsDto>();
        }
    }
}

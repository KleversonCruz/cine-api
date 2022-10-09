using Application.Identity.Users;
using Application.Identity.Users.Password;

namespace Api.Controllers.Identity
{
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService) => _userService = userService;

        [HttpGet]
        [MustHavePermission(AppAction.List, AppResource.Users)]
        [SwaggerOperation(Summary = "Get list of all users.")]
        public Task<List<UserDetailsDto>> GetListAsync(CancellationToken cancellationToken)
        {
            return _userService.GetListAsync(cancellationToken);
        }

        [HttpGet("{id}")]
        [MustHavePermission(AppAction.View, AppResource.Users)]
        [SwaggerOperation(Summary = "Get a user's details.")]
        public Task<UserDetailsDto> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            return _userService.GetAsync(id, cancellationToken);
        }

        [HttpGet("{id}/roles")]
        [MustHavePermission(AppAction.View, AppResource.UserRoles)]
        [SwaggerOperation(Summary = "Get a user's roles.")]
        public Task<List<UserRoleDto>> GetRolesAsync(string id, CancellationToken cancellationToken)
        {
            return _userService.GetRolesAsync(id, cancellationToken);
        }

        [HttpPost("{id}/roles")]
        [MustHavePermission(AppAction.Update, AppResource.UserRoles)]
        [SwaggerOperation(Summary = "Update a user's assigned roles.")]
        [ApiConventionMethod(typeof(AppApiConventions), nameof(AppApiConventions.Register))]
        public Task<string> AssignRolesAsync(string id, UserRolesRequest request, CancellationToken cancellationToken)
        {
            return _userService.AssignRolesAsync(id, request, cancellationToken);
        }

        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Creates a new user.")]
        public Task<string> CreateAsync(CreateUserRequest request)
        {
            return _userService.CreateAsync(request, GetOriginFromRequest());
        }

        [HttpGet("confirm-email")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Confirm email address for a user.")]
        [ApiConventionMethod(typeof(AppApiConventions), nameof(AppApiConventions.Search))]
        public Task<string> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string code, CancellationToken cancellationToken)
        {
            return _userService.ConfirmEmailAsync(userId, code, cancellationToken);
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Request a pasword reset email for a user.")]
        [ApiConventionMethod(typeof(AppApiConventions), nameof(AppApiConventions.Register))]
        public Task<string> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            return _userService.ForgotPasswordAsync(request, GetOriginFromRequest());
        }

        [HttpPost("reset-password")]
        [SwaggerOperation(Summary = "Reset a user's password.")]
        [ApiConventionMethod(typeof(AppApiConventions), nameof(AppApiConventions.Register))]
        public Task<string> ResetPasswordAsync(ResetPasswordRequest request)
        {
            return _userService.ResetPasswordAsync(request);
        }

        private string GetOriginFromRequest() => $"{Request.Scheme}://{Request.Host.Value}{Request.PathBase.Value}";
    }
}

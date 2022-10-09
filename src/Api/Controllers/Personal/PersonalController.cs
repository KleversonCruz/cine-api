using Application.Identity.Users;
using Application.Identity.Users.Password;

namespace Api.Controllers.Personal
{
    public class PersonalController : BaseApiController
    {
        private readonly IUserService _userService;

        public PersonalController(IUserService userService) => _userService = userService;

        [HttpGet("profile")]
        [SwaggerOperation(Summary = "Get profile details of currently logged in user.")]
        public async Task<ActionResult<UserDetailsDto>> GetProfileAsync(CancellationToken cancellationToken)
        {
            return User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId)
                ? Unauthorized()
                : Ok(await _userService.GetAsync(userId, cancellationToken));
        }

        [HttpPut("profile")]
        [SwaggerOperation(Summary = "Update profile details of currently logged in user.")]
        public async Task<ActionResult> UpdateProfileAsync(UpdateUserRequest request)
        {
            if (User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            await _userService.UpdateAsync(request, userId);
            return Ok();
        }

        [HttpPut("change-password")]
        [SwaggerOperation(Summary = "Change password of currently logged in user.")]
        [ApiConventionMethod(typeof(AppApiConventions), nameof(AppApiConventions.Register))]
        public async Task<ActionResult> ChangePasswordAsync(ChangePasswordRequest model)
        {
            if (User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            await _userService.ChangePasswordAsync(model, userId);
            return Ok();
        }

        [HttpGet("permissions")]
        [SwaggerOperation(Summary = "Get permissions of currently logged in user.")]
        public async Task<ActionResult<List<string>>> GetPermissionsAsync(CancellationToken cancellationToken)
        {
            return User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId)
                ? Unauthorized()
                : Ok(await _userService.GetPermissionsAsync(userId, cancellationToken));
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Shared.Authorization;

namespace Infra.Auth.Permissions
{
    public class MustHavePermissionAttribute : AuthorizeAttribute
    {
        public MustHavePermissionAttribute(string action, string resource) =>
            Policy = AppPermission.NameFor(action, resource);
    }
}

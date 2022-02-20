using marking_api.Global.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

namespace marking_api.API.Config
{
    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;
        private readonly UtilService _utilService;

        public ClaimRequirementFilter()
        {

        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasClaim = HasAccess(context.HttpContext.User);

            if (!hasClaim)
            {
                context.Result = new ForbidResult();
            }
        }

        public bool HasAccess(ClaimsPrincipal user)
        {
            if (user.IsInRole("Admin"))
                return true;

            if (_utilService.IsUserDisabled(user.Identity.Name))
                return false;

            if (!string.IsNullOrWhiteSpace(_claim.Value))
                return (_utilService.GenerateUserMenu(user.Identity.Name)?.Any(x => x.LinkSecurity == _claim.Value)).GetValueOrDefault();

            return true;
        }
    }
}

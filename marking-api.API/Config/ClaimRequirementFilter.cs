using marking_api.DataModel.Identity;
using marking_api.Global.Repositories;
using marking_api.Global.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace marking_api.API.Config
{
    /// <summary>
    /// Claim requirement filter used as attribute
    /// </summary>
    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Claim _claim;
        private readonly UtilService _utilService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public ClaimRequirementFilter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Checks if user is authorised to the controller
        /// Creates unauthorised result if they don't
        /// </summary>
        /// <param name="context">AuthorizationFilterContext</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasClaim = HasAccess(context.HttpContext.User);

            if (!hasClaim)
            {
                context.Result = new UnauthorizedResult();
            }
        }

        /// <summary>
        /// Checks the user's links and role permissions for if they have access to the controller
        /// </summary>
        /// <param name="user"></param>
        /// <returns>True if the user has access</returns>
        public bool HasAccess(ClaimsPrincipal user)
        {
            if (user.IsInRole("Admin"))
                return true;

            if (_utilService.IsUserDisabled(user.Identity.Name))
                return false;
            
            if (!string.IsNullOrWhiteSpace(_claim.Value))
                return (_utilService.GenerateUserMenu(user.Identity.Name)?.Any(x => x.LinkSecurity == _claim.Value)).GetValueOrDefault();

            if (!string.IsNullOrWhiteSpace(_claim.Value))
                return (_unitOfWork.UserRoles.Get(include: x => x.Include(y => y.User).Include(y => y.Role).ThenInclude(a => a.RolePermissions), filter: x => x.User.UserName.Equals(user.Identity.Name))?
                    .Any(x => x.Role.RolePermissions == null ? false : x.Role.RolePermissions.Any(y => y.PermissionSecurity == _claim.Value))).GetValueOrDefault();                 

            return true;
        }
    }
}

using marking_api.Data;
using marking_api.DataModel.Enums;
using marking_api.DataModel.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace marking_api.Global.Services
{
    /// <summary>
    /// Filtering service to filter returned data from the database by role and permission
    /// </summary>
    public class DataFilterService
    {
        private readonly MarkingDbContext _dbContext;
        private readonly IHttpContextAccessor _context;

        private RoleType userAccessRole;
        private User user;
        private bool accessRoleFound = false;
        private List<RolePermission> userRolePermissions;

        /// <summary>
        /// Constructor initialising db context and http context
        /// Also assigns user and useraccessrole for use in filtering data
        /// </summary>
        /// <param name="dbContext">MarkingDbContext</param>
        /// <param name="context">IHttpContextAccessor</param>
        /// <param name="signInManager">SignInManager(User)</param>
        public DataFilterService(MarkingDbContext dbContext, IHttpContextAccessor context, SignInManager<User> signInManager)
        {
            _dbContext = dbContext;
            _context = context;
            user = signInManager.UserManager.GetUserAsync(_context.HttpContext.User).Result;
            user.UserRoles = _dbContext.UserRoles.Where(x => x.UserId.Equals(user.Id)).Include(y => y.Role).ToList();
            userRolePermissions = new List<RolePermission>();
            //Need a different solution to the below if / foreach block as this will eventually get really slow as the system database expands
            if (user.UserRoles?.Any() == true)
            {
                foreach (var userRole in user.UserRoles)
                {
                    if (Convert.ToInt32(userRole.Role.AccessRole) > Convert.ToInt32(userAccessRole))
                        userAccessRole = userRole.Role.AccessRole;

                    if (userRole.Role.RolePermissions?.Any() == true)
                        userRolePermissions.AddRange(userRole.Role.RolePermissions);                    
                }
                userRolePermissions.Distinct();
                accessRoleFound = true;
            }            
        }

        /// <summary>
        /// Filter data by access role
        /// </summary>
        /// <returns>True if the access role has access to the data</returns>
        public bool FilterByRole(RoleType roleType)
        {
            if (_context == null)
                return false;
            if (user == null)
                return false;
            if (!accessRoleFound)
                return false;
            if (userAccessRole == RoleType.Admin)
                return true;
            if (roleType == userAccessRole)
                return true;

            return false;
        }

        /// <summary>
        /// Filter data by permission
        /// </summary>
        /// <returns>True if the user has the correct permission to access the data</returns>
        public bool FilterByPermission(RolePermission permission)
        {
            if (_context == null)
                return false;
            if (user == null)
                return false;
            if (userRolePermissions == null)
                return false;
            if (permission == null)
                return false;
            if (userRolePermissions.Contains(permission))
                return true;
            return false;
        }
    }
}

using marking_api.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marking_api.DataModel.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace marking_api.API
{
    public class AppInitialiserFilter : IAsyncActionFilter
    {
        private readonly MarkingDbContext _dbContext;

        public AppInitialiserFilter(MarkingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string userId = null;

            var claimsIdentity = (ClaimsIdentity)context.HttpContext.User.Identity;
            var claimUserId = claimsIdentity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            
            if (claimUserId != null)
            {
                userId = claimUserId.Value;
            }

            _dbContext.UserId = userId;

            var resultContext = await next();
        }
    }
}

using marking_api.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marking_api.DataModel.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using marking_api.Global.Services;

namespace marking_api.API
{
    public class AppInitialiserFilter : IAsyncActionFilter
    {
        private readonly MarkingDbContext _dbContext;
        private readonly UtilService _utilService;

        public AppInitialiserFilter(MarkingDbContext dbContext, UtilService utilService)
        {
            _dbContext = dbContext;
            _utilService = utilService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string userId = null;

            //var claimsIdentity = (ClaimsPrincipal)context.HttpContext.User;
            //var claimuserId = claimsIdentity.FindFirstValue(ClaimTypes.NameIdentifier); //.Claims.SingleOrDefault(c => c.Type == ClaimTypes.);
            //var user = _userManager.GetUserAsync(context.HttpContext.User.I).Result;

            var token = context.HttpContext.Request.Headers["authorization"].FirstOrDefault()?.Split(" ").Last();
            //var token = context.HttpContext.Request.Cookies;
            var jwtuserId = _utilService.ValidateToken(token);


            if (jwtuserId != null)
            {
                userId = jwtuserId;
            }

            _dbContext.UserId = userId;

            var resultContext = await next();
        }
    }
}

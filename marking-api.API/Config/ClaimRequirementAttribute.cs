using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace marking_api.API.Config
{
    public static class MarkingClaimTypes
    {
        public const string Permission = "";
    }
    
    public class ClaimRequirementAttribute : TypeFilterAttribute
    {
        public ClaimRequirementAttribute(string claimType, string claimValue) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { new Claim(claimType, claimValue) };
        }
    }
}

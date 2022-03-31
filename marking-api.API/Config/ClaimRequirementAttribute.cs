using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace marking_api.API.Config
{
    /// <summary>
    /// Marking claim type string that is required to access the controller
    /// </summary>
    public static class MarkingClaimTypes
    {
        /// <summary>
        /// Claim permission string
        /// </summary>
        public const string Permission = "";
    }
    
    /// <summary>
    /// Claim requirement filter attributed
    /// </summary>
    public class ClaimRequirementAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Create new claim depending on the type and value
        /// </summary>
        /// <param name="claimType">string</param>
        /// <param name="claimValue">string</param>
        public ClaimRequirementAttribute(string claimType, string claimValue) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { new Claim(claimType, claimValue) };
        }
    }
}

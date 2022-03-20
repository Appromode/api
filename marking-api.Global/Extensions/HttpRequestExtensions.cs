using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;
using System;

namespace marking_api.Global.Extensions
{
    /// <summary>
    /// HttpRequest extension methods
    /// </summary>
    public static class HttpRequestExtensions
    {
        private const string RequestedWithHeader = "X-Requested-With";
        private const string XmlHttpRequest = "XMLHttpRequest";

        /// <summary>
        /// Evaluates if a httprequest is an ajax request
        /// </summary>
        /// <param name="request">HttpRequest - Extended HttpRequest</param>
        /// <returns>True if ajax request</returns>
        /// <exception cref="ArgumentNullException">If request is null then thrown</exception>
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            if (request.Headers != null)
            {
                return request.Headers[RequestedWithHeader] == XmlHttpRequest;
            }

            return false;
        }
    }

    /// <summary>
    /// Ajax attribute extension class
    /// </summary>
    public class AjaxOnlyAttribute : ActionMethodSelectorAttribute
    {
        /// <summary>
        /// Is a request a valid ajax request
        /// </summary>
        /// <param name="routeContext">RouteContext - Context to determine request from</param>
        /// <param name="action">ActionDescriptor</param>
        /// <returns>True if request is valid</returns>
        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            return routeContext.HttpContext.Request.IsAjaxRequest();
        }
    }
}

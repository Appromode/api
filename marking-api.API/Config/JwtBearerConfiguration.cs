using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text.Json;

namespace marking_api.API.Config
{
    /// <summary>
    /// Swagger jwt bearer configuration class
    /// </summary>
    public static class JwtBearerConfiguration
    {
        /// <summary>
        /// Add jwt bearer configuration to the application
        /// </summary>
        /// <param name="builder">AuthenticationBuilder</param>
        /// <param name="issuer">string</param>
        /// <param name="audience">string</param>
        /// <returns></returns>
        public static AuthenticationBuilder AddJwtBearerConfiguration(this AuthenticationBuilder builder, string issuer, string audience)
        {
            return builder.AddJwtBearer(options =>
            {
                options.Authority = issuer;
                options.Audience = audience;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ClockSkew = new TimeSpan(0, 0, 30)
                };
                options.Events = new JwtBearerEvents()
                {
                    OnChallenge = context =>
                    {
                        //Return 401 unauthorised if the request has not got a valid jwt token
                        context.HandleResponse();
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";
                        if (string.IsNullOrEmpty(context.Error))
                            context.Error = "invalid_token";
                        if (string.IsNullOrEmpty(context.ErrorDescription))
                            context.ErrorDescription = "This request requires a valid JWT access tokent to be provided";

                        if (context.AuthenticateFailure != null && context.AuthenticateFailure.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            var authException = context.AuthenticateFailure as SecurityTokenExpiredException;
                            context.Response.Headers.Add("x-token-expired", authException.Expires.ToString("o"));
                            context.ErrorDescription = $"The token expired on {authException.Expires.ToString("o")}";
                        }

                        return context.Response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            error = context.Error,
                            error_desc = context.ErrorDescription,
                        }));
                    }
                };
            });
        }
    }
}

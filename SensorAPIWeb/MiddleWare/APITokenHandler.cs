using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using SensorAPIWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SensorAPIWeb.MiddleWare
{
    public class APITokenHandler
    {
        private RequestDelegate _next;
        public APITokenHandler(RequestDelegate next)
        {
            _next = next;
            
        }

        public async Task Invoke(HttpContext context, ISecurityCheckRepository securityCheckRepository)
        {
            bool validToken = false;
            //Require HTTPS
            if (context.Request.IsHttps)
            {
                //Skip token authentication for test controller
                if (context.Request.Path.StartsWithSegments("/") || context.Request.Path.StartsWithSegments("/DevicesDataCentre"))
                {
                    validToken = true;
                }

                //Token header exists in the request
                if (context.Request.Headers.ContainsKey("Token"))
                {
                    //Check for a valid device by API token in my DB and set validToken to true if found
                    if (securityCheckRepository.TokenValidation(context.Request.Headers["Token"]))
                    {
                        validToken = true;
                    }
                }

                if (!validToken)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    await context.Response.WriteAsync("Invalid Token");
                }
                else
                {
                    await _next.Invoke(context);
                }
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.HttpVersionNotSupported;
                await context.Response.WriteAsync("HTTP not supported");
            }
        }
    }
    public static class TkenHandlerExtensions
    {
        public static IApplicationBuilder UseTokenAuth(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<APITokenHandler>();
        }
    }
}


using Demo.Application.Implementations.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Implementations.MiddleWare
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var jwtHandler = context.RequestServices.GetRequiredService<JwtSecurityTokenHandlerWrapper>(); // Resolve here

            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                try
                {
                    var principal = jwtHandler.ValidateJwtToken(token);
                    context.User = principal; // Set the user principal in the context
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync(ex.Message);
                    return;
                }
            }

            await _next(context); // Call the next middleware
        }
    }
}

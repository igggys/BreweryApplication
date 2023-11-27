using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;
using WebAppGeoCodingServices.DataLayer;

namespace WebAppGeoCodingServices.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            await _next(httpContext);

        }
    }
}

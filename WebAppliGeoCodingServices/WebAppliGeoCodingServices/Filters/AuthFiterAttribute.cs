using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using WebAppGeoCodingServices.DataLayer;

namespace WebAppGeoCodingServices.Filters
{
    public class AuthFiterAttribute : Attribute, IAuthorizationFilter
    {
        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            StringValues sessionId;
            if (!context.HttpContext.Request.Headers.TryGetValue("sessionId", out sessionId))
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            
        }
    }
}

using Microsoft.AspNetCore.Mvc.Filters;
using System.Globalization;

namespace BreweryApplication.Filters
{
    public class CultureSetterFilterAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var language = context.RouteData.Values["language"];
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language?.ToString() ?? "uk");
            Thread.CurrentThread.CurrentCulture = new CultureInfo(language?.ToString() ?? "uk");
            context.HttpContext.Items.Add("CurrentUICulture", new CultureInfo(language?.ToString() ?? "uk"));
            await next();
        }
    }
}

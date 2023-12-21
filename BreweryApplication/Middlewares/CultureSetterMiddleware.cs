using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Threading.Tasks;
using WLog;
using static System.Net.Mime.MediaTypeNames;

namespace BreweryApplication.Middlewares
{
    public class CultureSetterMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<RequestLocalizationOptions> _localizationOptions;

        public CultureSetterMiddleware(RequestDelegate next, IOptions<RequestLocalizationOptions> localizationOptions)
        {
            _next = next;
            _localizationOptions = localizationOptions;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                var language = httpContext.Request.RouteValues["language"].ToString();
                var currentCulture = _localizationOptions.Value.SupportedCultures.FirstOrDefault(lang => lang.TwoLetterISOLanguageName == language);
                if (currentCulture == null)
                {
                    language = _localizationOptions.Value.DefaultRequestCulture.Culture.TwoLetterISOLanguageName;
                }

                Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
                Thread.CurrentThread.CurrentCulture = new CultureInfo(language);

                await _next.Invoke(httpContext);
            }
            catch
            {
                httpContext.Response.Redirect("/en/Errors/PageNotFound/", true);
            }
        }
    }
}

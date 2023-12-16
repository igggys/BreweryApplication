using BreweryApplication.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Extensions.Localization;
using System.Globalization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using BreweryApplication.Extensions;
using BreweryApplication.Models;
using Microsoft.AspNetCore.Http.Extensions;

namespace BreweryApplication.Controllers
{
    
    public class HomeController : Controller
    {

        private readonly IStringLocalizer<HomeController> _localizer;
        public LanguageInfo[] _supportedCultures;
        public string _currentCulture;
        public HomeController(IStringLocalizer<HomeController> localizer, IOptions<RequestLocalizationOptions> localizationOptions)
        {
            _localizer = localizer;

            _supportedCultures = localizationOptions.Value.SupportedCultures.Select(
                item => new LanguageInfo
                {
                    TwoLetterISOLanguageName = item.TwoLetterISOLanguageName,
                    NativeName = item.NativeName.CapitalizeFirstLetter()
                }).ToArray();

            
        }

        public IActionResult Index()
        {
            UpdateSupportedCultures();
            ViewBag.CulturesList = _supportedCultures;
            ViewBag.currentLanguage = _currentCulture;
            ViewBag.value_join = _localizer.GetString("joinButtonText");
            ViewBag.application_description = _localizer.GetString("ApplicationDescription");
            return View();
        }

        private void UpdateSupportedCultures()
        {
            CultureInfo currentCulture = ((CultureInfo)(HttpContext.Items["CurrentUICulture"]));
            var currentLanguage = currentCulture.TwoLetterISOLanguageName;
            var displayUrl = Request.GetDisplayUrl();

            if (displayUrl.Contains(currentLanguage))
                displayUrl = displayUrl.Replace(currentLanguage, "lang");
            else
                displayUrl += "lang";

            foreach (var supportedCulture in _supportedCultures)
            {
                supportedCulture.DisplayUrl = displayUrl.Replace("lang", supportedCulture.TwoLetterISOLanguageName);
            }

            _currentCulture = currentCulture.NativeName.CapitalizeFirstLetter();
        }

    }
}
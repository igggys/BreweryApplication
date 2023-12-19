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
using PhoneModel.Services;
using WLog;

namespace BreweryApplication.Controllers
{
    

    public class HomeController : Controller
    {
        private readonly WLogger _logger;
        private readonly IStringLocalizer<HomeController> _localizer;
        public LanguageInfo[] _supportedCultures;
        public string _currentLanguage;
        public string _currentCulture;
        public HomeController(IStringLocalizer<HomeController> localizer, IOptions<RequestLocalizationOptions> localizationOptions, WLogger logger)
        {
            _localizer = localizer;

            _supportedCultures = localizationOptions.Value.SupportedCultures.Select(
                item => new LanguageInfo
                {
                    TwoLetterISOLanguageName = item.TwoLetterISOLanguageName,
                    NativeName = item.NativeName.CapitalizeFirstLetter()
                }).ToArray();

            _logger = logger;
        }

        public IActionResult Index()
        {
            UpdateSupportedCultures();
            ViewBag.CulturesList = _supportedCultures;
            ViewBag.currentLanguage = _currentLanguage;
            ViewBag.value_join = _localizer.GetString("joinButtonText");
            ViewBag.application_description = _localizer.GetString("ApplicationDescription");
            ViewBag.currentCulture = _currentCulture;
            return View();
        }

        private void UpdateSupportedCultures()
        {
            CultureInfo currentCulture = ((CultureInfo)(HttpContext.Items["CurrentUICulture"]));
            _currentCulture = currentCulture.TwoLetterISOLanguageName;
            var displayUrl = Request.GetDisplayUrl();

            if (displayUrl.Contains(_currentCulture))
                displayUrl = displayUrl.Replace(_currentCulture, "lang");
            else
                displayUrl += "lang";

            foreach (var supportedCulture in _supportedCultures)
            {
                supportedCulture.DisplayUrl = displayUrl.Replace("lang", supportedCulture.TwoLetterISOLanguageName);
            }

            _currentLanguage = currentCulture.NativeName.CapitalizeFirstLetter();
        }

    }
}
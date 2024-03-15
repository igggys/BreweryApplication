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
        private readonly IOptions<RequestLocalizationOptions> _localizationOptions;
        public LanguageInfo[] _supportedCultures;
        public string _currentLanguage;
        public string _currentCulture;

        public HomeController(IStringLocalizer<HomeController> localizer, IOptions<RequestLocalizationOptions> localizationOptions, WLogger logger)
        {
            _localizer = localizer;
            _logger = logger;
            _localizationOptions = localizationOptions;

            _supportedCultures = _localizationOptions.Value.SupportedCultures.Select(
                item => new LanguageInfo
                {
                    TwoLetterISOLanguageName = item.TwoLetterISOLanguageName,
                    NativeName = item.NativeName.CapitalizeFirstLetter()
                }).ToArray();

            _currentCulture = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            _currentLanguage = Thread.CurrentThread.CurrentUICulture.NativeName.CapitalizeFirstLetter();
        }

        public IActionResult Index()
        {
            ViewBag.CulturesList = _supportedCultures;
            ViewBag.currentLanguage = _currentLanguage;
            ViewBag.currentCulture = _currentCulture;

            ViewBag.value_join = _localizer.GetString("joinButtonText");
            ViewBag.application_description = _localizer.GetString("ApplicationDescription");
            
            return View();
        }
    }
}
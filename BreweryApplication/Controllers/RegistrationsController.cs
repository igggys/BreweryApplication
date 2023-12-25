using BreweryApplication.Extensions;
using BreweryApplication.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using PhoneModel.Models;
using PhoneModel.Services;
using System.Globalization;
using WLog;

namespace BreweryApplication.Controllers
{
    public class RegistrationsController : Controller
    {
        private readonly WLogger _logger;
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly IOptions<RequestLocalizationOptions> _localizationOptions;
        private readonly PhonesService _phonesService;
        public LanguageInfo[] _supportedCultures;
        public string _currentLanguage;
        public string _currentCulture;
        public Country[] countries;

        public RegistrationsController(IStringLocalizer<HomeController> localizer, IOptions<RequestLocalizationOptions> localizationOptions, WLogger logger, PhonesService phonesService)
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

            _phonesService = phonesService;
            var _countries = _phonesService.CountriesList(_currentCulture);
        }

        public IActionResult ManufactureRegistration()
        {
            ViewBag.CulturesList = _supportedCultures;
            ViewBag.currentLanguage = _currentLanguage;
            ViewBag.currentCulture = _currentCulture;

            return View();
        }
    }
}

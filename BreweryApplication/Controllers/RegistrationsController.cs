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
using Newtonsoft.Json;

namespace BreweryApplication.Controllers
{
    public class RegistrationsController : Controller
    {
        private readonly WLogger _logger;
        private readonly IStringLocalizer<RegistrationsController> _localizer;
        private readonly IOptions<RequestLocalizationOptions> _localizationOptions;
        private readonly PhonesService _phonesService;
        public LanguageInfo[] _supportedCultures;
        public string _currentLanguage;
        public string _currentCulture;
        public Country[] _countries;

        public RegistrationsController(IStringLocalizer<RegistrationsController> localizer, IOptions<RequestLocalizationOptions> localizationOptions, WLogger logger, PhonesService phonesService)
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
            _countries = _phonesService.CountriesList(_currentCulture);
        }

        public IActionResult ManufactureRegistration()
        {
            ViewBag.CulturesList = _supportedCultures;
            ViewBag.currentLanguage = _currentLanguage;
            ViewBag.currentCulture = _currentCulture;
            ViewBag.countries = _countries;
            ViewBag.countryCodesJson = JsonConvert.SerializeObject(_countries);

            ViewBag.Registration_for_brewers = _localizer.GetString("Registration_for_brewers");
            ViewBag.The_name_of_the_brewery = _localizer.GetString("The_name_of_the_brewery");
            ViewBag.Description_of_production = _localizer.GetString("Description_of_production");
            ViewBag.Email = _localizer.GetString("Email");
            ViewBag.Phone = _localizer.GetString("Phone");
            ViewBag.The_name_of_the_manufacturers_contact_person = _localizer.GetString("The_name_of_the_manufacturers_contact_person");
            ViewBag.if_there_is = _localizer.GetString("if_there_is");
            ViewBag.necessarily = _localizer.GetString("necessarily");
            ViewBag.Registration = _localizer.GetString("Registration");
            ViewBag.cancellation = _localizer.GetString("cancellation");

            return View();
        }
    }
}

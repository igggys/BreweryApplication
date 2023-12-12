using BreweryApplication.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Extensions.Localization;

namespace BreweryApplication.Controllers
{
    
    public class HomeController : Controller
    {

        public readonly IStringLocalizer<HomeController> _localizer;
        public HomeController(IStringLocalizer<HomeController> localizer)
        {
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            return View();
        }
        [CultureSetterFilter]
        public IActionResult IndexHome()
        {
            ViewBag.value_join = _localizer.GetString("join");
            return View();
        }


    }
}
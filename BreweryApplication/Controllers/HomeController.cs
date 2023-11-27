using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BreweryApplication.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            //
        }

        public IActionResult Index()
        {
            return View();
        }

        
    }
}
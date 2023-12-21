using Microsoft.AspNetCore.Mvc;

namespace BreweryApplication.Controllers
{
    public class ErrorsController : Controller
    {
        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}

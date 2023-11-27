using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WLog;

namespace WebApplicationTestWLogger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly WLogger _logger;
        public ValuesController(WLogger wLogger) 
        {
            _logger = wLogger;
        }
        [HttpGet]
        [Route("get")]
        public IActionResult GetValue([FromQuery] string input) 
        {
            _logger.AddToLod(input);
            int a = 1;
            int b = 0;
            double d = a / b;
            return Ok();
        }
    }
}

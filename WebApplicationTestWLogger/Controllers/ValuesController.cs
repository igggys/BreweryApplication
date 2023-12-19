using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneModel.Services;
using WLog;

namespace WebApplicationTestWLogger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly WLogger _logger;
        private readonly PhonesService _phonesService;
        public ValuesController(WLogger wLogger, PhonesService phonesService) 
        {
            _logger = wLogger;
            _phonesService = phonesService;
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

        [HttpGet]
        [Route("phone")]
        public IActionResult GetPhone(string phone)
        {
            _phonesService.GetValidationNumber(phone);
            return Ok();
        }
    }
}

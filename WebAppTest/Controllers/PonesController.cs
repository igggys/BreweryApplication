using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneModel.Services;

namespace WebAppTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PonesController : ControllerBase
    {
        private readonly PhonesService phonesService;
        public PonesController(PhonesService phonesService) 
        {
            this.phonesService = phonesService;
        }

        [HttpGet]
        [Route("phonestest")]
        public IActionResult PhonesTest(string number)
        {
            string result = phonesService.GetValidationNumber(number);
            return Ok(result);
        }
    }
}

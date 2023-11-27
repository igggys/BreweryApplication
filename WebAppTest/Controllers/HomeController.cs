using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebAppTest.DataLayer;
using WebAppTest.Filters;
using WebAppTest.Infrastructure;
using WebAppTest.Services;

namespace WebAppTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly SessionsManager _sessionsManager;
        private readonly List<ServiceProperties> _servicesProperties;
        private readonly Connector _connector;

        public HomeController(SessionsManager sessionsManager, IOptions<List<ServiceProperties>> servicesProperties, Connector connector)
        {
            _sessionsManager = sessionsManager;
            _servicesProperties = servicesProperties.Value;
            _connector = connector;
        }

        [HttpGet]
        [Route("geoCodingServicesList")]
        [TypeFilter(typeof(SessionsStartAttribute))]
        public async Task<IActionResult> GeoCodingServicesList()
        {
            await _connector.Get(Guid.NewGuid(), _servicesProperties.FirstOrDefault(item => item.ServiceName == "GeoCoding").ServiceName, "https://localhost:44304/api/GeoCoding/ServicesList");
            return Ok();
        }
    }
}

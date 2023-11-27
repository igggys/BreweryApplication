using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Net.Http;
using System.Reflection;
using WebAppGeoCodingServices.Filters;
using WebAppGeoCodingServices.Infrastructure;
using WebAppGeoCodingServices.Services.GeoCoding;
using Newtonsoft.Json;

namespace WebAppGeoCodingServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthFiter]
    public class GeoCodingController : ControllerBase
    {
        private readonly IOptions<List<GeoCodingServiceSettings>> _servicesSettings;
        private readonly IEnumerable<IServiceGeoCoding> _servicesGeoCoding;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly MemorySession _memorySession;
        public GeoCodingController(IOptions<List<GeoCodingServiceSettings>> servicesSettings, IEnumerable<IServiceGeoCoding> servicesGeoCoding, IHttpClientFactory httpClientFactory, MemorySession memorySession) 
        {
            _servicesSettings = servicesSettings;
            _servicesGeoCoding = servicesGeoCoding;
            _httpClientFactory = httpClientFactory;
            _memorySession = memorySession;
        }

        [HttpGet]
        [Route("ServicesList")]
        public IActionResult ServicesList([FromHeader(Name = "SessionId")] string sessionId)
        {
            var result = _servicesSettings.Value.Select(item => item.ServiceName).ToList();

            return Ok(new ApplicationResponse { IsError = false, Message = string.Empty, Data = result });
        }

        [HttpGet]
        [Route("GeoCoding")]
        public async Task<IActionResult> GeoCoding([FromHeader(Name = "SessionId")] string sessionId, [FromQuery] string service, [FromQuery] string address, [FromQuery] string culture)
        {
            CultureInfo cultureInfo;

            if (string.IsNullOrEmpty(service) || string.IsNullOrEmpty(address))
            {
                return BadRequest(new ApplicationResponse { IsError = true, Message = "Service or address parameter values are not specified", Data = null });
            }

            try
            {
                cultureInfo = new CultureInfo(culture);
            }
            catch
            {
                return BadRequest(new ApplicationResponse { IsError = true, Message = "The language parameter value is not set or set incorrectly", Data = null });
            }

            if (!_servicesSettings.Value.Any(item => item.ServiceName.Equals(service)))
            {
                return BadRequest(new ApplicationResponse { IsError = true, Message = "The Service parameter value is not set or set incorrectly", Data = null });
            }

            IServiceGeoCoding currentService = _servicesGeoCoding.FirstOrDefault(item => item.GetType().AssemblyQualifiedName != null && item.GetType().AssemblyQualifiedName.Contains(service));
            if (currentService == null)
            {
                return BadRequest(new ApplicationResponse { IsError = true, Message = "The Service parameter value is not set or set incorrectly", Data = null });
            }

            var httpClient = _httpClientFactory.CreateClient(service);
            try
            {
                var serviceResult = await currentService.GetAsync(address, cultureInfo, httpClient);
                if (serviceResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok(new ApplicationResponse { IsError = false, Message = string.Empty, Data = serviceResult });
                }
                return StatusCode((int)serviceResult.StatusCode, new ApplicationResponse { IsError = true, Message = serviceResult.Message, Data = null });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApplicationResponse { IsError = true, Message = ex.Message, Data = null });
            }
        }

    }
}

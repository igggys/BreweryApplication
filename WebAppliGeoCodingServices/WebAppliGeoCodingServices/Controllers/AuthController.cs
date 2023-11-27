using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Options;
using WebAppGeoCodingServices.DataLayer;
using WebAppGeoCodingServices.Infrastructure;
using CryptoLib;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace WebAppGeoCodingServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly List<ServiceProperties> _serviceProperties;
        private readonly SessionsManager _sessionsManager;
        private readonly IMemoryCache _memoryCache;
        public AuthController(IOptions<List<ServiceProperties>> servicesProperties, SessionsManager sessionsManager, IMemoryCache memoryCache)
        {
            _serviceProperties = servicesProperties.Value;
            _sessionsManager = sessionsManager;
            _memoryCache = memoryCache;
        }

        [HttpPost]
        [Route("start")]
        public async Task<ActionResult> Start([FromHeader] string sessionId, [FromHeader] string serviceId, [FromBody] string publicKey)
        {
            var service = _serviceProperties.FirstOrDefault(item => item.ServiceId.ToString() == serviceId);
            if (service == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new ApplicationResponse { IsError = true, Message = "Unauthorized", Data = null });
            }

            Guid sessionIdGuid;
            if (!Guid.TryParse(sessionId, out sessionIdGuid))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new ApplicationResponse { IsError = true, Message = "Unauthorized", Data = null });
            }

            Guid serviceIdGuid = Guid.Parse(serviceId);

            var dbResult = await _sessionsManager.SessionValidate(sessionIdGuid, serviceIdGuid);
            if (!dbResult)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new ApplicationResponse { IsError = true, Message = "Unauthorized", Data = null });
            }

            SymmetricEncryption symmetricEncryption = new();
            var key = symmetricEncryption.KeysGenerator();

            MemorySession memorySession = new() { ServiceId = service.ServiceId, SessionId = Guid.NewGuid(), SymmtricKey = key };
            _memoryCache.Set(memorySession.SessionId, memorySession, DateTimeOffset.UtcNow.AddMinutes(20));
            
            ApplicationResponse applicationResponse = new() { IsError = false, Message = string.Empty, Data = key, SessionId = memorySession.SessionId };

            AsymmetricEncryption asymmetricEncryption = new();
            string result = asymmetricEncryption.Encrypt(JsonConvert.SerializeObject(applicationResponse), publicKey);

            return Ok(result);
        }
    }
}

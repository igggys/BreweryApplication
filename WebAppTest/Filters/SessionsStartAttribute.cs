using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.RegularExpressions;
using WebAppTest.DataLayer;
using WebAppTest.Infrastructure;
using CryptoLib;
using System.Text;
using System.Net.Http.Headers;
using Microsoft.Extensions.Caching.Memory;

namespace WebAppTest.Filters
{
    public class SessionsStartAttribute : Attribute, IAsyncActionFilter
    {
        private readonly SessionsManager _sessionsManager;
        private readonly List<ServiceProperties> _servicesProperties;
        private readonly ServiceProperties _serviceProperties;
        private readonly IMemoryCache _memoryCache;
        public SessionsStartAttribute(SessionsManager sessionsManager, IOptions<List<ServiceProperties>> servicesProperties, IOptions<ServiceProperties> serviceProperties, IMemoryCache memoryCache)
        {
            _sessionsManager = sessionsManager;
            _servicesProperties = servicesProperties.Value;
            _serviceProperties = serviceProperties.Value;
            _memoryCache = memoryCache;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Guid? sessionId = null;
            string serverName = string.Empty;
            switch (context.HttpContext.Request.Path)
            {
                case "/api/Home/geoCodingServicesList":
                    serverName = "GeoCoding";
                    break;
            }

            if (string.IsNullOrEmpty(serverName))
            {
                return;
            }

            sessionId = await _sessionsManager.SessionCreation(serverName);

            if (sessionId == null) 
            {
                return;
            }

            List<KeyValuePair<string, string>> headers = new()
            {
                new KeyValuePair<string, string>("sessionId", sessionId.ToString()),
                new KeyValuePair<string, string>("serviceId", _serviceProperties.ServiceId.ToString())
            };

            AsymmetricEncryption asymmetricEncryption = new();
            AsymmetricEncryption.Keys keys = asymmetricEncryption.KeysGenerator();

            HttpResponseMessage httpResponseMessage = await Send(serverName, headers, keys.PublicKey);
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string resultString = await httpResponseMessage.Content.ReadAsStringAsync();
                string resultStringDecrypt = asymmetricEncryption.Decrypt(resultString, keys.PrivateKey);
                ApplicationResponse applicationResponse = JsonConvert.DeserializeObject<ApplicationResponse>(resultStringDecrypt);
                if (!applicationResponse.IsError)
                {
                    SymmetricEncryption.SymmtricKey symmtricKey = JsonConvert.DeserializeObject<SymmetricEncryption.SymmtricKey>(applicationResponse.Data.ToString());
                    MemorySession memorySession = new() { ServiceId = _serviceProperties.ServiceId, SessionId = (Guid)sessionId, SymmtricKey = symmtricKey };
                    _memoryCache.Set(serverName, memorySession, DateTimeOffset.UtcNow.AddMinutes(19));
                }
            }

            await next();
        }

        private async Task<HttpResponseMessage> Send(string serverName, List<KeyValuePair<string, string>> headers, string content)
        {
            _servicesProperties.FirstOrDefault(item => item.ServiceName == serverName);
            using (HttpClient client = new HttpClient())
            {
                var service = _servicesProperties.FirstOrDefault(item => item.ServiceName == serverName);

                HttpContent httpContent = new StringContent($"\"{content}\"", Encoding.UTF8);

                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                foreach (var header in headers)
                {
                    httpContent.Headers.Add(header.Key, header.Value);
                }
                string url = $"{service.ServiceBaseUrl}{service.StartSessionAddress}";
                var result = await client.PostAsync(url, httpContent);
                return result;
            }
        }
    }
}

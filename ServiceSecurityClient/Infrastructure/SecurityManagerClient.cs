using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoLib;
using ServiceSecurityServer.DataLayer;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServiceSecurityServer.Infrastructure
{
    public class SecurityManagerClient
    {
        private readonly IdentityDataManager _identityDataManager;
        public SecurityManagerClient(IdentityDataManager identityDataManager) 
        {
            _identityDataManager = identityDataManager;
        }

        public void Start(string fromServiceName, Guid fromServiceId, string toServiceName, Guid toServiceId, string toServiceUrl)
        {
            var sessionId = _identityDataManager.GetSessionId(fromServiceName, fromServiceId, toServiceName, toServiceId);
            var asymmetricEncryption = new CryptoLib.AsymmetricEncryption();
            var key = asymmetricEncryption.KeysGenerator();
            
        }

        private async Task<string> Send(string sessionId, string url, string data)
        {
            HttpContent content = new StringContent("\"" + data + "\"", Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                content.Headers.Add("session", sessionId);
                var response = await client.PostAsync(url, content);
                var responseContentString = await response.Content.ReadAsStringAsync();
                return responseContentString;
            }
        }
    }
}

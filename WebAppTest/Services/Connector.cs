using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;
using System;
using WebAppTest.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using CryptoLib;
using System.Reflection.PortableExecutable;

namespace WebAppTest.Services
{
    public class Connector
    {
        private readonly IMemoryCache _memoryCache;
        public Connector(IMemoryCache memoryCache) 
        {
            _memoryCache = memoryCache;
        }

        public async Task<ApplicationResponse> Get(Guid requestId, string serviceName, string path)
        {
            object? memorySession;
            if (!_memoryCache.TryGetValue(serviceName, out memorySession))
            {
                throw new InvalidOperationException("Session data not found.");
            }
            MemorySession session = ((MemorySession)(memorySession));
            ApplicationResponse result = null;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("sessionId", session.SessionId.ToString());
                client.DefaultRequestHeaders.Add("serviceId", session.ServiceId.ToString());
                try
                {
                    var response = await client.GetAsync(path);

                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        result = new() { IsError = true, Message = $"Response StatusCode is {response.StatusCode}", Data = null };
                    }
                    else
                    {
                        var responseContentString = await response.Content.ReadAsStringAsync();
                        result = new() { IsError = false, Message = string.Empty, Data = responseContentString };
                    }
                }
                catch (System.Exception exception)
                {
                    result = new() { IsError = true, Message = exception.Message, Data = null };
                }
            }

            return result;
        }
    }
}

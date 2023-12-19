using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using WLog.Filters;
using WLog.Models;
using WLog.Infrastructure;
using WLog.DataLayer;

namespace WLog
{
    public class WLogger
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly WLogSettings _wLogSettings;

        public WLogger(IHttpContextAccessor httpContextAccessor) 
        {
            _httpContextAccessor = httpContextAccessor;
            try
            {
                _wLogSettings = JsonConvert.DeserializeObject<WLogSettings>(File.ReadAllText("WLogSettings.json"));
            }
            catch
            {
                _wLogSettings = null;
            }
        }

        public void AddToLod(string message)
        {
            var stackTrace = new StackTrace();
            var frame = stackTrace.GetFrame(1);
            var type = frame.GetMethod().DeclaringType.FullName;
            var method = frame.GetMethod().Name;
            ((LogRecord)(_httpContextAccessor.HttpContext.Items["LogRecord"])).Messages.Add(new() { MessageTimePoint = DateTime.UtcNow, ClassName = type, MethodName = method, Text = message });
                
        }
    }

    public static class WLoggerExtension
    {
        public static void AddWLogger(this IServiceCollection services, IConfiguration configuration = null)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<WLogConfigurationManager>();
            services.AddSingleton<DataManager>();
            services.AddSingleton<WLogger>();

            services.AddMvcCore(
                options =>
                {
                    options.Filters.Add<WLogActionFilterAttribute>();
                    options.Filters.Add<WLogExceptionFilterAttribute>();
                    options.Filters.Add<WLogResultFilterAttribute>();
                });
        }
    }
}

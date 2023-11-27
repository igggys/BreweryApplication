using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WLog.Models;
using WLog.DataLayer;
using WLog.Infrastructure;

namespace WLog.Filters
{
    public class WLogResultFilterAttribute : Attribute, IAsyncResultFilter
    {
        private readonly DataManager _dataManager;
        private readonly WLogConfigurationManager _wLogConfigurationManager;
        public WLogResultFilterAttribute(DataManager dataManager, WLogConfigurationManager wLogConfigurationManager)
        {
            _wLogConfigurationManager = wLogConfigurationManager;
            _dataManager = dataManager;
        }
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (_wLogConfigurationManager.CanRead && !_wLogConfigurationManager.Settings.ExceptionsOnly)
            {
                ((LogRecord)(context.HttpContext.Items["LogRecord"])).Result = JsonConvert.SerializeObject(context.Result);
                ((LogRecord)(context.HttpContext.Items["LogRecord"])).End = DateTime.UtcNow;
                await _dataManager.WriteAsync(((LogRecord)(context.HttpContext.Items["LogRecord"])));
            }
            
            await next();
        }
    }
}

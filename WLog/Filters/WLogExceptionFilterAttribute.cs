using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLog.DataLayer;
using WLog.Infrastructure;
using WLog.Models;

namespace WLog.Filters
{
    public class WLogExceptionFilterAttribute : Attribute, IAsyncExceptionFilter
    {
        private readonly DataManager _dataManager;
        private readonly WLogConfigurationManager _wLogConfigurationManager;
        public WLogExceptionFilterAttribute(DataManager dataManager, WLogConfigurationManager wLogConfigurationManager)
        {
            _wLogConfigurationManager = wLogConfigurationManager;
            _dataManager = dataManager;
        }
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            if (_wLogConfigurationManager.CanRead)
            {
                ((LogRecord)(context.HttpContext.Items["LogRecord"])).Exception = $"{context.Exception.Message}:\r\n{context.Exception.StackTrace}";
                ((LogRecord)(context.HttpContext.Items["LogRecord"])).End = DateTime.UtcNow;
                await _dataManager.WriteAsync(((LogRecord)(context.HttpContext.Items["LogRecord"])));
            }
        }
    }
}

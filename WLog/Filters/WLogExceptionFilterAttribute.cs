using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                LogRecord logRecord = ((LogRecord)(context.HttpContext.Items["LogRecord"]));
                if (logRecord == null)
                {
                    StringValues RequestIdValue;
                    Guid GuidRequestId;
                    if
                    (
                        !context.HttpContext.Request.Headers.TryGetValue("RequestId", out RequestIdValue) ||
                        string.IsNullOrEmpty(RequestIdValue.ToString()) ||
                        !Guid.TryParse(RequestIdValue.ToString(), out GuidRequestId)
                    )
                    {
                        GuidRequestId = Guid.NewGuid();
                    }

                    logRecord = new()
                    {
                        RequestId = GuidRequestId,
                        Arguments = string.Empty,
                        Method = context.HttpContext.Request.Method,
                        Url = $"{context.HttpContext.Request.Scheme}://{context.HttpContext.Request.Host}{context.HttpContext.Request.Path}{context.HttpContext.Request.QueryString.ToString()}",
                        ApplicationName = Assembly.GetEntryAssembly().GetName().Name,
                        Start = DateTime.UtcNow,
                        End = null,
                        Messages = new()
                    };
                    context.HttpContext.Items.Add("LogRecord", logRecord);
                }

                logRecord.Exception = $"{context.Exception.Message}:\r\n{context.Exception.StackTrace}";
                logRecord.End = DateTime.UtcNow;
                await _dataManager.WriteAsync(logRecord);
            }
        }
    }
}

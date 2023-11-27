using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WLog.Models;
using System.Reflection;
using WLog.DataLayer;
using WLog.Infrastructure;

namespace WLog.Filters
{
    public class WLogActionFilterAttribute : Attribute, IAsyncActionFilter
    {
        private readonly DataManager _dataManager;
        private readonly WLogConfigurationManager _wLogConfigurationManager;
        public WLogActionFilterAttribute(DataManager dataManager, WLogConfigurationManager wLogConfigurationManager) 
        {
            _wLogConfigurationManager = wLogConfigurationManager;
            _dataManager = dataManager;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (_wLogConfigurationManager.CanRead)
            {
                LogRecord logRecord = new()
                {
                    Arguments = JsonConvert.SerializeObject(context.ActionArguments),
                    Method = context.HttpContext.Request.Method,
                    Url = $"{context.HttpContext.Request.Scheme}://{context.HttpContext.Request.Host}{context.HttpContext.Request.Path}{context.HttpContext.Request.QueryString.ToString()}",
                    ApplicationName = Assembly.GetEntryAssembly().GetName().Name,
                    Start = DateTime.UtcNow,
                    End = null,
                    Messages = new()
                };

                StringValues RequestIdValue;
                Guid GuidRequestId;
                if
                (
                    !context.HttpContext.Request.Headers.TryGetValue("RequestId", out RequestIdValue) ||
                    string.IsNullOrEmpty(RequestIdValue.ToString()) ||
                    !Guid.TryParse(RequestIdValue.ToString(), out GuidRequestId)
                )
                {
                    logRecord.RequestId = Guid.NewGuid();
                }
                else
                {
                    logRecord.RequestId = GuidRequestId;
                }

                context.HttpContext.Items.Add("LogRecord", logRecord);
            }
            
            await next();
        }
    }
}

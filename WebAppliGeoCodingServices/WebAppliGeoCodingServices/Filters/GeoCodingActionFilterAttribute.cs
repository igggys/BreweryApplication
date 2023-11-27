using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using WebAppGeoCodingServices.DataLayer;
using WebAppGeoCodingServices.Infrastructure;

namespace WebAppGeoCodingServices.Filters
{
    public class GeoCodingActionFilterAttribute : Attribute, IActionFilter
    {
        private readonly LogWriter _logWriter;
        public GeoCodingActionFilterAttribute(LogWriter logWriter)
        {
            _logWriter = logWriter;
        }

        public void OnActionExecuting(ActionExecutingContext context) 
        {
            var inputData = context.ActionArguments;
            object requestIdParameter;
            string RequestId;
            if (inputData.TryGetValue("requestId", out requestIdParameter))
            {
                RequestId = requestIdParameter.ToString();
            }
            else
            {
                RequestId = Guid.NewGuid().ToString();
            }

            LogRecord logRecord = new() { RequestId = RequestId, ApplicationName = Assembly.GetExecutingAssembly().GetName().Name,  Path = context.HttpContext.Request.GetDisplayUrl(), Parameters = JsonConvert.SerializeObject(context.ActionArguments), StartAction = DateTime.UtcNow };
            context.HttpContext.Items.Add("LogRecord", logRecord);
        }

        public async void OnActionExecuted(ActionExecutedContext context)
        {
            LogRecord logRecord = ((LogRecord)(context.HttpContext.Items["LogRecord"]));
            logRecord.EndAction = DateTime.UtcNow;
            try
            {
                ObjectResult result = ((ObjectResult)(context.Result));
                if (result != null && result.StatusCode != 200)
                {
                    logRecord.ResponseStatusCode = result.StatusCode;
                    logRecord.Result = JsonConvert.SerializeObject(result.Value);
                    logRecord.Exeption = ((ApplicationResponse)(result.Value)).Message;
                    await _logWriter.LogWriteAsync(logRecord);
                }
            }
            catch
            {
                logRecord.Result = JsonConvert.SerializeObject(context.Result);
                await _logWriter.LogWriteAsync(logRecord);
            }
        }
    }
}

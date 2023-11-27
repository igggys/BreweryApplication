using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Reflection;
using WebAppTest.DataLayer;
using WebAppTest.Infrastructure;

namespace WebAppTest.Filters
{
    public class LogingActionAttribute : Attribute, IActionFilter
    {
        private readonly LogWriter _logWriter;
        public LogingActionAttribute(LogWriter logWriter)
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

            LogRecord logRecord = new() { RequestId = RequestId, ApplicationName = Assembly.GetExecutingAssembly().GetName().Name, Path = context.HttpContext.Request.GetDisplayUrl(), Parameters = JsonConvert.SerializeObject(context.ActionArguments), StartAction = DateTime.UtcNow };
            context.HttpContext.Items.Add("LogRecord", logRecord);
        }

        public async void OnActionExecuted(ActionExecutedContext context)
        {
            LogRecord logRecord = ((LogRecord)(context.HttpContext.Items["LogRecord"]));
            logRecord.EndAction = DateTime.UtcNow;
            try
            {
                if (context.Exception != null)
                {
                    logRecord.Result = JsonConvert.SerializeObject(context.Result);
                    logRecord.Exeption = context.Exception.Message;
                    await _logWriter.LogWriteAsync(logRecord);
                }
                //else
                //{
                //    logRecord.Result = JsonConvert.SerializeObject(context.Result);
                //    logRecord.Exeption = string.Empty;
                //    await _logWriter.LogWriteAsync(logRecord);
                //}
            }
            catch(System.Exception ex)
            {
                logRecord.Result = JsonConvert.SerializeObject(context.Result);
                logRecord.Exeption = ex.Message;
                await _logWriter.LogWriteAsync(logRecord);
            }
        }
    }
}

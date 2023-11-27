using Microsoft.AspNetCore.Mvc.Filters;
using WebAppGeoCodingServices.DataLayer;
using WebAppGeoCodingServices.Infrastructure;

namespace WebAppGeoCodingServices.Filters
{
    public class GeoCodingExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        private readonly LogWriter _logWriter;
        public GeoCodingExceptionFilterAttribute(LogWriter logWriter)
        {
            _logWriter = logWriter;
        }
        public async void OnException(ExceptionContext context)
        {
            try
            {
                LogRecord logRecord = ((LogRecord)(context.HttpContext.Items["LogRecord"]));
                logRecord.Exeption = context.Exception.ToString();
                logRecord.EndAction = DateTime.UtcNow;
                await _logWriter.LogWriteAsync(logRecord);
            }
            catch
            {
                //
            }
            
        }
    }
}

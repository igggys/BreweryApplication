using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLog.Models
{
    public class LogRecord
    {
        public Guid? RequestId { get; set; }
        public string ApplicationName { get; set; }
        public DateTime Start { get; set; }
        public string Url { get; set; }
        public string Method { get; set; }
        public string Arguments { get; set; }
        public List<Message> Messages { get; set; }
        public string Result { get; set; }
        public string Exception { get; set; }
        public DateTime? End { get; set; }
    }

    public class Message
    {
        public DateTime MessageTimePoint { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string Text { get; set; }
    }
}

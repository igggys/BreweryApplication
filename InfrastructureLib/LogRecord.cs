using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLib
{
    public class LogRecord
    {
        public string RequestId { get; set; }
        public string ApplicationName { get; set; }
        public string Path { get; set; }
        public string Parameters { get; set; }
        public DateTime StartAction { get; set; }
        public DateTime EndAction { get; set; }
        public int? ResponseStatusCode { get; set; }
        public string Result { get; set; }
        public string Exeption { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLib
{
    public class ApplicationResponse
    {
        public bool IsError { get; set; }
        public string Message { get; set; }
        public Guid? SessionId { get; set; }
        public object Data { get; set; }
    }
}

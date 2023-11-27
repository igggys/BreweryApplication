using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLib
{
    public class ServiceProperties
    {
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string ServiceBaseUrl { get; set; }
        public string StartSessionAddress { get; set; }
    }
}

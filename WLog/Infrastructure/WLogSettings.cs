using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WLog.Infrastructure
{
    public class WLogSettings
    {
        public string ConnectionString { get; set; }
        public string DbShema { get; set; }
        public bool ExceptionsOnly { get; set; }
    }
}

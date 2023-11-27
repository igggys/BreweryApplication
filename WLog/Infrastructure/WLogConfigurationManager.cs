using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WLog.Infrastructure
{
    public class WLogConfigurationManager
    {
        private readonly WLogger _logger;
        public WLogSettings Settings { get; set; }
        public bool CanRead { get; set; }

        public WLogConfigurationManager() 
        {
            try
            {
                Settings = JsonConvert.DeserializeObject<WLogSettings>(File.ReadAllText("WLogSettings.json"));
                CanRead = true;
            }
            catch
            {
                CanRead = false;
            }
        }
    }
}

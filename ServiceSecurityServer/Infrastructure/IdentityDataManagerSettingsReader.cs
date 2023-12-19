using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServiceSecurityServer.Infrastructure
{
    public class IdentityDataManagerSettingsReader
    {
        public IdentityDataManagerSettings IdentityDataManagerSettingsRead() 
        {
            try
            {
                return JsonConvert.DeserializeObject<IdentityDataManagerSettings>(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "IdentityDataManagerSettings.json")));
            }
            catch
            {
                return null;
            }
        }
    }
}

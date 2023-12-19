using ServiceSecurityServer.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceSecurityServer.DataLayer
{
    public class IdentityDataManager
    {
        private readonly string _connectionString;
        public IdentityDataManager(IdentityDataManagerSettingsReader identityDataManagerSettingsReader) 
        {
            _connectionString = identityDataManagerSettingsReader.IdentityDataManagerSettingsRead()?.ConnectionString;
        }
    }
}

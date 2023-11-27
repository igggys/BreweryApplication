using CryptoLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLib
{
    public class MemorySession
    {
        public Guid SessionId { get; set; }
        public Guid ServiceId { get; set; }
        public SymmetricEncryption.SymmtricKey SymmtricKey { get; set; }        
    }

}

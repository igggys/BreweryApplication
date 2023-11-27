using CryptoLib;

namespace WebAppTest.Infrastructure
{
    public class MemorySession
    {
        public Guid SessionId { get; set; }
        public Guid ServiceId { get; set; }
        public SymmetricEncryption.SymmtricKey SymmtricKey { get; set; }
    }
}

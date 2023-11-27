using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLib
{
    public class AsymmetricEncryption
    {
        public Keys KeysGenerator()
        {
            RSA rsa = RSA.Create();

            return new Keys
            {
                PublicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey()),
                PrivateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey())
            };
        }

        public string Encrypt(string input, string publicKey)
        {
            byte[] key = Convert.FromBase64String(publicKey);
            RSA rsa = RSA.Create();
            int count;
            rsa.ImportRSAPublicKey(key, out count);
            byte[] result = rsa.Encrypt(Encoding.UTF8.GetBytes(input), RSAEncryptionPadding.Pkcs1);
            return Convert.ToBase64String(result);
        }

        public string Decrypt(string input, string privateKey)
        {
            byte[] key = Convert.FromBase64String(privateKey);
            RSA rsa = RSA.Create();
            int count;
            rsa.ImportRSAPrivateKey(key, out count);
            byte[] result = rsa.Decrypt(Convert.FromBase64String(input), RSAEncryptionPadding.Pkcs1);
            return Encoding.UTF8.GetString(result);
        }

        public class Keys
        {
            public string PrivateKey { get; set; }
            public string PublicKey { get; set; }
        }
    }
}

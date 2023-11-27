using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLib
{
    public class SymmetricEncryption
    {
        public SymmtricKey KeysGenerator()
        {
            Aes aes = Aes.Create();
            aes.GenerateIV();
            aes.GenerateKey();
            return new SymmtricKey 
            { 
                Key = Convert.ToBase64String(aes.Key), 
                Vector = Convert.ToBase64String(aes.IV), 
                KeySize = aes.KeySize, 
                BlockSize = aes.BlockSize 
            };
        }

        public string Encrypt(string input, SymmtricKey keys)
        {
            CryptoStream cryptoStream = null;
            byte[] InputByteArray = null;
            byte[] OutputByteArray = null;
            string EncryptedString = string.Empty;
            if (!string.IsNullOrEmpty(input) && !string.IsNullOrEmpty(keys.Key) && !string.IsNullOrEmpty(keys.Vector))
            {
                Aes aes = Aes.Create();
                aes.KeySize = keys.KeySize;
                aes.BlockSize = keys.BlockSize;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = Convert.FromBase64String(keys.Key);
                aes.IV = Convert.FromBase64String(keys.Vector);
                MemoryStream OutputMemoryStream = new MemoryStream();
                InputByteArray = System.Text.Encoding.UTF8.GetBytes(input);
                cryptoStream = new CryptoStream(OutputMemoryStream, aes.CreateEncryptor(aes.Key, aes.IV), CryptoStreamMode.Write);
                cryptoStream.Write(InputByteArray, 0, InputByteArray.Length);
                cryptoStream.FlushFinalBlock();
                cryptoStream.Close();
                OutputByteArray = OutputMemoryStream.ToArray();
                OutputMemoryStream.Close();
                EncryptedString = Convert.ToBase64String(OutputByteArray);
            }
            return EncryptedString;
        }

        public string Decrypt(string input, SymmtricKey keys)
        {
            CryptoStream cryptoStream = null;
            byte[] InputByteArray = null;
            byte[] OutputByteArray = null;
            string DencryptedString = string.Empty;
            if (!string.IsNullOrEmpty(input) && !string.IsNullOrEmpty(keys.Key) && !string.IsNullOrEmpty(keys.Vector))
            {
                Aes aes = Aes.Create();
                aes.KeySize = keys.KeySize;
                aes.BlockSize = keys.BlockSize;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = Convert.FromBase64String(keys.Key);
                aes.IV = Convert.FromBase64String(keys.Vector);
                MemoryStream OutputMemoryStream = new MemoryStream();
                InputByteArray = Convert.FromBase64String(input);
                cryptoStream = new CryptoStream(OutputMemoryStream, aes.CreateDecryptor(aes.Key, aes.IV), CryptoStreamMode.Write);
                cryptoStream.Write(InputByteArray, 0, InputByteArray.Length);
                cryptoStream.FlushFinalBlock();
                cryptoStream.Close();
                OutputByteArray = OutputMemoryStream.ToArray();
                OutputMemoryStream.Close();
                DencryptedString = System.Text.Encoding.UTF8.GetString(OutputByteArray);
            }
            return DencryptedString;
        }

        public class SymmtricKey
        {
            public string Key { get; set; }
            public string Vector { get; set; }
            public int KeySize { get; set; }
            public int BlockSize { get; set; }
        }
    }
}

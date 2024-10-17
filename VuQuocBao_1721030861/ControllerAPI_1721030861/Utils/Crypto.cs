using System.Security.Cryptography;
using System.Text;

namespace ControllerAPI_1721030861.Utils
{
    public static class Crypto
    {
        public static string Encrypt(string plainText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                var keyBytes = Encoding.UTF8.GetBytes(key.PadRight(32)); // length key 32 byte
                aesAlg.Key = keyBytes;
                aesAlg.IV = new byte[16]; // (Initialization Vector)
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                        swEncrypt.Write(plainText);

                    var encrypted = msEncrypt.ToArray();
                    return Convert.ToBase64String(encrypted);
                }
            }
        }

        public static string Decrypt(string cipherText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                var keyBytes = Encoding.UTF8.GetBytes(key.PadRight(32));
                aesAlg.Key = keyBytes;
                aesAlg.IV = new byte[16];

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                var cipherBytes = Convert.FromBase64String(cipherText);

                using (var msDecrypt = new MemoryStream(cipherBytes))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new StreamReader(csDecrypt))
                    return srDecrypt.ReadToEnd();
            }
        }
    }
}

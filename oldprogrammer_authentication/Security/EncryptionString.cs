using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace oldprogrammer_authentication.Security
{
    public class EncryptionString : IEncryptionString
    {
        private readonly ILogger<EncryptionString> _logger;
        public EncryptionString(ILogger<EncryptionString> logger)
        {
            this._logger = logger;
        }
        public string Encrypt(string value, string keyValue)
        {
            try
            {
                if (string.IsNullOrEmpty(value)) return value;

                var key = Encoding.UTF8.GetBytes(keyValue);

                using var aesAlg = Aes.Create();
                using var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV);
                using var msEncrypt = new MemoryStream();
                using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
                using var swEncrypt = new StreamWriter(csEncrypt);
                swEncrypt.Write(value);

                var iv = aesAlg.IV;
                var decryptedContent = msEncrypt.ToArray();
                var result = new byte[iv.Length + decryptedContent.Length];
                Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                var str = Convert.ToBase64String(result);
                //var fullCipher = Convert.FromBase64String(str);
                return str;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred, method: Encrypt, class: EncryptionString, MessageException: {Message}", ex.Message);
                throw;
            }
        }
        public string Decrypt(string value, string keyValue)
        {
            try
            {
                if (string.IsNullOrEmpty(value)) return value;

                value = value.Replace(" ", "+");
                var fullCipher = Convert.FromBase64String(value);
                var iv = new byte[16];
                var cipher = new byte[fullCipher.Length - iv.Length];
                Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
                Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, fullCipher.Length - iv.Length);

                var key = Encoding.UTF8.GetBytes(keyValue);

                using var aesAlg = Aes.Create();
                using var decryptor = aesAlg.CreateDecryptor(key, iv);
                using var msDecrypt = new MemoryStream(cipher);
                using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                using var srDecrypt = new StreamReader(csDecrypt);

                string result = srDecrypt.ReadToEnd();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred, method: Encrypt, class: EncryptionString, MessageException: {Message}", ex.Message);
                throw;
            }
        }
    }
}

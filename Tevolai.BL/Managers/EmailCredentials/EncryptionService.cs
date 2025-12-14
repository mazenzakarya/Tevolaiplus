using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Tevolai.BL.Managers.EmailCredentials
{
    public class EncryptionService
    {
        private readonly string _key;

        public EncryptionService(IConfiguration config)
        {
            _key = config["EncryptionKey"]!;
        }

        public string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_key);
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor();
            byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] cipherText = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);

            return Convert.ToBase64String(aes.IV) + ":" + Convert.ToBase64String(cipherText);
        }

        public string Decrypt(string encryptedText)
        {
            var parts = encryptedText.Split(':');
            byte[] iv = Convert.FromBase64String(parts[0]);
            byte[] cipherText = Convert.FromBase64String(parts[1]);

            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_key);
            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor();
            byte[] originalBytes = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);

            return Encoding.UTF8.GetString(originalBytes);
        }
    }
}
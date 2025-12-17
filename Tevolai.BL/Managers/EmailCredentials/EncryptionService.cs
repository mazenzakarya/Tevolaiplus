using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Tevolai.BL.Managers.EmailCredentials
{
    public class EncryptionService
    {
        private readonly byte[] _key;

        public EncryptionService(IConfiguration config)
        {
            var keyString = config["EncryptionKey"] ?? throw new InvalidOperationException("EncryptionKey not found in configuration");
            
            // Ensure key is exactly 32 bytes (256 bits) for AES-256
            var keyBytes = Encoding.UTF8.GetBytes(keyString);
            if (keyBytes.Length < 32)
            {
                // Pad with zeros if too short
                _key = new byte[32];
                Array.Copy(keyBytes, _key, keyBytes.Length);
            }
            else if (keyBytes.Length > 32)
            {
                // Truncate if too long
                _key = new byte[32];
                Array.Copy(keyBytes, _key, 32);
            }
            else
            {
                _key = keyBytes;
            }
        }

        public string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return string.Empty;

            using var aes = Aes.Create();
            aes.Key = _key;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor();
            byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] cipherText = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);

            return Convert.ToBase64String(aes.IV) + ":" + Convert.ToBase64String(cipherText);
        }

        public string Decrypt(string encryptedText)
        {
            if (string.IsNullOrEmpty(encryptedText))
                return string.Empty;

            try
            {
                var parts = encryptedText.Split(':');
                if (parts.Length != 2)
                    throw new CryptographicException("Invalid encrypted text format");

                byte[] iv = Convert.FromBase64String(parts[0]);
                byte[] cipherText = Convert.FromBase64String(parts[1]);

                using var aes = Aes.Create();
                aes.Key = _key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using var decryptor = aes.CreateDecryptor();
                byte[] originalBytes = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);

                return Encoding.UTF8.GetString(originalBytes);
            }
            catch (Exception ex)
            {
                throw new CryptographicException("Failed to decrypt password", ex);
            }
        }
    }
}
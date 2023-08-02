using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace LagoaTrading.Shared.Extensions
{
    public static class CryptoHelper
    {
        public static DateTime GetDateTimeFromUTC(double datetime)
        => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(datetime);

        public static string CreateRandomKey()
        => Guid.NewGuid().ToString().Replace("-", string.Empty).ToUpper();

        public static string Create256(string input, string salt)
        {
            var messageBytes = Encoding.UTF8.GetBytes(input);
            var keyBytes = Encoding.UTF8.GetBytes(salt);
            using (var hmacsha256 = new HMACSHA256(keyBytes))
            {
                var hashmessage = hmacsha256.ComputeHash(messageBytes);
                return BitConverter.ToString(hashmessage).Replace("-", "").ToUpper();
            }
        }

        public static string Create256(string input)
        {
            var messageBytes = Encoding.UTF8.GetBytes(input);
            using (var sha256 = SHA256.Create())
            {
                var hashmessage = sha256.ComputeHash(messageBytes);
                return BitConverter.ToString(hashmessage).Replace("-", "").ToUpper();
            }
        }

        public static string Create512(string input)
        {
            using (var sha512 = SHA512.Create())
            {
                var bytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(bytes).Replace("-", "").ToUpper();
            }
        }

        public static string CreateMD5(string input)
        {
            var md5Digest = new MD5Digest();
            var data = Encoding.UTF8.GetBytes(input);
            md5Digest.BlockUpdate(data, 0, data.Length);
            var result = new byte[md5Digest.GetDigestSize()];
            md5Digest.DoFinal(result, 0);
            return BitConverter.ToString(result).Replace("-", "").ToUpper();
        }

        public static string Encrypt(string input, string key)
        {
            var salt = CreateMD5(key);
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            IBufferedCipher cipher = CipherUtilities.GetCipher("AES/ECB/PKCS7Padding");
            cipher.Init(true, new KeyParameter(saltBytes));
            byte[] cypher = cipher.DoFinal(inputBytes);

            return BitConverter.ToString(cypher).Replace("-", "").ToUpper();
        }

        public static string Decrypt(string input, string key)
        {
            var salt = CreateMD5(key);
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
            byte[] cypheredBytes = new byte[input.Length / 2];

            for (int i = 0; i < cypheredBytes.Length; i++)
            {
                cypheredBytes[i] = Convert.ToByte(input.Substring(i * 2, 2), 16);
            }

            IBufferedCipher cipher = CipherUtilities.GetCipher("AES/ECB/PKCS7Padding");
            cipher.Init(false, new KeyParameter(saltBytes));

            byte[] plainText = cipher.DoFinal(cypheredBytes);

            return Encoding.UTF8.GetString(plainText);
        }

        public static void DecryptWithSecret(string CypheredWithSecret, out string plaintText, out string key)
        {
            var cyperedText = CypheredWithSecret.Substring(0, CypheredWithSecret.Length - 32);
            key = CypheredWithSecret.Substring(CypheredWithSecret.Length - 32);

            plaintText = Decrypt(cyperedText, key);
        }

        public static (string apiKey, string apiSecret) DecryptTuple(string cypheredKey, string cypheredSecret)
        {
            var hiddenSecret = cypheredSecret.Substring(0, cypheredSecret.Length - 32);
            var key = cypheredSecret.Substring(cypheredSecret.Length - 32);

            var secret = Decrypt(hiddenSecret, key);
            var apiKey = Decrypt(cypheredKey, key);
            return (apiKey, secret);
        }
    }
}

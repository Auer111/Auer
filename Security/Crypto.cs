using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Auer.Security
{
    public static class Crypto
    {
        private static string Key = "nP144dtDItgFuNsd/TWF0jhY6GgjjoNv1Slr5Z+AHt8=";
        private static string IV = "Za+1aYv/FnvwhSHyCXPVrQ==";

        public static void GenerateKeys()
        {
            Aes Aes = Aes.Create();
            Aes.GenerateKey();
            Aes.GenerateIV();

            Key = Convert.ToBase64String(Aes.Key);
            IV = Convert.ToBase64String(Aes.IV);
        }
        
        /// <summary>
        /// Takes raw text, return base64 cyphertext
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Encrypt(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);

            Aes aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Key = Convert.FromBase64String(Key);
            aes.IV = Convert.FromBase64String(IV);
            return Convert.ToBase64String(
                aes.CreateEncryptor()
                .TransformFinalBlock(bytes, 0, bytes.Length));
        }

        /// <summary>
        /// Takes base64 cyphertext, returns raw string
        /// </summary>
        /// <param name="b64str"></param>
        /// <returns></returns>
        public static string Decrypt(string b64str)
        {
            byte[] bytes = Convert.FromBase64String(b64str);

            Aes aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Key = Convert.FromBase64String(Key);
            aes.IV = Convert.FromBase64String(IV);
            return Encoding.UTF8.GetString(
                aes.CreateDecryptor()
                .TransformFinalBlock(bytes, 0, bytes.Length));
        }
    }
}

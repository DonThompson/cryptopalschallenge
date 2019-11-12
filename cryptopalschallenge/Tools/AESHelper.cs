using System.IO;
using System.Security.Cryptography;

namespace cryptopalschallenge.Tools
{
    public static class AESHelper
    {
        public static string Decrypt(byte[] encryptedData, byte[] key)
        {
            RijndaelManaged AES = new RijndaelManaged();
            AES.Padding = PaddingMode.PKCS7;
            AES.Mode = CipherMode.ECB;
            AES.KeySize = 128;
            AES.BlockSize = 128;

            string result = "";
            using (MemoryStream ms = new MemoryStream(encryptedData))
            {
                using (CryptoStream cs = new CryptoStream(ms, AES.CreateDecryptor(key, key), CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        result = sr.ReadToEnd();
                    }
                }
            }

            return result;
        }
    }
}

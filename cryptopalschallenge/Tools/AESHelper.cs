using System.Collections.Generic;
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

        /// <summary>
        /// Evaluates if a given hex string looks like an AES string encrypted in ECB mode.
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns></returns>
        public static bool DoesHexStringLookECB(string candidate)
        {
            //Is there any repetition in this block?  Use 16 bytes.
            const int SIZE = 16;    //# of bytes to use
            int offset = 0;         //where are we currently?
            int repetitions = 0;    //How many repetitions have we seen?

            HashSet<string> knownSections = new HashSet<string>();

            while (true)
            {
                //Get the next SIZE bytes.  If we can't get a full set, quit
                if (offset + SIZE > candidate.Length)
                {
                    break;
                }

                string nextSection = candidate.Substring(offset, SIZE);

                //Is this in a section we've already seen?
                if (knownSections.Contains(nextSection))
                {
                    repetitions++;
                }

                //Add this to our known list
                knownSections.Add(nextSection);

                //increment to next
                offset += SIZE;
            }

            if (repetitions > 0)
            {
                return true;
            }

            return false;
        }
    }
}

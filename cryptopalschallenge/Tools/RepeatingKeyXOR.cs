using System;
using System.Text;

namespace cryptopalschallenge.Tools
{
    public class RepeatingKeyXOR
    {
        /// <summary>
        /// Given inputs:
        /// </summary>
        /// <param name="input">A normal UTF8 string text that you want encrypted</param>
        /// <param name="key">A normal UTF8 string that you want to use as the encryption key</param>
        /// <returns>A hex string of the encrypted value</returns>
        public string EncryptInputWithKey(string input, string key)
        {
            byte[] inputArray = Encoding.UTF8.GetBytes(input);
            byte[] keyArray = Encoding.UTF8.GetBytes(key);

            byte[] result = new byte[inputArray.Length];
            for (int i = 0; i < inputArray.Length; i++)
            {
                //XOR each byte of input by a rotation through the key
                result[i] = (byte)(inputArray[i] ^ keyArray[i % keyArray.Length]);
            }

            return BitConverter.ToString(result).Replace("-", "");
        }
    }
}

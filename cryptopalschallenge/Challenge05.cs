using System;
using System.Collections.Generic;
using System.Text;

namespace cryptopalschallenge
{
    /// <summary>
    /// https://cryptopals.com/sets/1/challenges/5
    /// </summary>
    public class Challenge05
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string RepeatingKeyXOR(string input, string key)
        {
            byte[] inputArray = Encoding.UTF8.GetBytes(input);
            byte[] keyArray = Encoding.UTF8.GetBytes(key);

            byte[] result = new byte[inputArray.Length];
            for (int i = 0; i < inputArray.Length; i++)
            {
                    //XOR each byte of input by a rotation through the key
                    result[i] = (byte)(inputArray[i] ^ keyArray[i%keyArray.Length]);
            }

            return BitConverter.ToString(result).Replace("-", "");

        }
    }
}

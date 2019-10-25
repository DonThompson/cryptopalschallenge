using cryptopalschallenge.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace cryptopalschallenge
{
    /// <summary>
    /// https://cryptopals.com/sets/1/challenges/2
    /// </summary>
    public class Challenge02
    {
        /// <summary>
        /// Given 2 hex strings, compute the XOR
        /// </summary>
        /// <param name="val1">Hex string value 1</param>
        /// <param name="val2">Hex string value 2</param>
        public string DoChallenge02(string val1, string val2)
        {
            //Input values must be the same size
            if(val1.Length != val2.Length)
            {
                throw new Exception("Input values must be the same length.");
            }

            byte[] b1 = HexStringToByteArrayConverter.Convert(val1);
            byte[] b2 = HexStringToByteArrayConverter.Convert(val2);

            byte[] result = XOR.ExclusiveOR(b1, b2);

            return BitConverter.ToString(result).Replace("-", "");
        }
    }
}

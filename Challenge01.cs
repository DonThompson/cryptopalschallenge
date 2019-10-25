using cryptopalschallenge.Tools;
using System;

namespace cryptopalschallenge
{
    public class Challenge01
    {
        /// <summary>
        /// Given:  49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d
        /// 
        /// Output:  SSdtIGtpbGxpbmcgeW91ciBicmFpbiBsaWtlIGEgcG9pc29ub3VzIG11c2hyb29t
        /// </summary>
        /// <param name="hexString"></param>
        public string DoChallenge01(string hexString)
        {
            byte[] byteArray = HexStringToByteArrayConverter.Convert(hexString);

            string output = Convert.ToBase64String(byteArray);
            return output;
        }
    }
}

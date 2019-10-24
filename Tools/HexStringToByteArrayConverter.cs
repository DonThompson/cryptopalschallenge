using System;
using System.Globalization;

namespace cryptopalschallenge.Tools
{
    public class HexStringToByteArrayConverter
    {
        /// <summary>
        /// Given a hex string "A1B2C4F8" returns a byte array containing "A1", "B2", C4", F8"
        /// </summary>
        /// <param name="hexString">Must be an even number of characters or an exception will be thrown</param>
        /// <returns>The byte array will be of length hexString/2</returns>
        public static byte[] Convert(string hexString)
        {
            //Convert hex string to a byte array - each 2 characters are a hex byte
            if (hexString.Length % 2 == 1)
            {
                throw new Exception("Invalid hex string input specified.  Please ensure it has a even number of digits.");
            }

            byte[] byteArray = new byte[hexString.Length / 2];
            for (int i = 0; i < hexString.Length / 2; i++)
            {
                string byteValue = hexString.Substring(i * 2, 2);
                byteArray[i] = byte.Parse(byteValue, NumberStyles.HexNumber);
            }

            return byteArray;
        }
    }
}

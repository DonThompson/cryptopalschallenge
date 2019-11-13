using System;
using System.Text;

namespace cryptopalschallenge.Tools
{
    public static class PKCS7PaddingHelper
    {
        /// <summary>
        /// Given an input string, pads it, using PKCS7, to the desired length.
        /// REQUIRES that the given string is <= than the desired length.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="desiredLength"></param>
        /// <returns></returns>
        public static string PadStringToBytes(string input, int desiredLength)
        {
            if(input.Length > desiredLength)
            {
                throw new ArgumentException("Cannot pad a string that is longer than the desired length.");
            }

            int numberOfBytesToAdd = desiredLength - input.Length;

            byte[] result = new byte[desiredLength];
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);

            Array.Copy(inputBytes, 0, result, 0, input.Length);

            for (int i = 0; i < numberOfBytesToAdd; i++)
            {
                result[input.Length + i] = (byte)numberOfBytesToAdd;
            }

            return Encoding.ASCII.GetString(result);
        }
    }
}

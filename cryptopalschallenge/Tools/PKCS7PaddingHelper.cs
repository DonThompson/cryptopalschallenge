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

            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] result = PadBytes(inputBytes, desiredLength);
            return Encoding.ASCII.GetString(result);
        }

        public static byte[] PadBytes(byte[] inputBytes, int desiredLength)
        {
            if (inputBytes.Length > desiredLength)
            {
                throw new ArgumentException("Cannot pad input that is longer than the desired length.");
            }

            byte[] result = new byte[desiredLength];
            Array.Copy(inputBytes, 0, result, 0, inputBytes.Length);

            int numberOfBytesToAdd = desiredLength - inputBytes.Length;
            for (int i = 0; i < numberOfBytesToAdd; i++)
            {
                result[inputBytes.Length + i] = (byte)numberOfBytesToAdd;
            }

            return result;
        }
    }
}

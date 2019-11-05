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

        /// <summary>
        /// Given some encrypted data, attempts to find the key size by comparing the edit distance between blocks of various size.
        /// 
        /// TODO STATUS 11/5/2019 10:53 AM:  As best I can tell, this is implemented fully correct.  But it doesn't work.  On my simple phrase, 
        /// the real key length is the 4th best out of 10.  On the challenge 5 phrase, it's 15-20th best chosen.
        /// </summary>
        /// <param name="encryptedData">The encrypted data to be analyzed</param>
        /// <returns>The length in bytes of the key that likely encrypted that data</returns>
        public static int FindKeySize(byte[] encryptedData)
        {
            EditDistanceComputer edc = new EditDistanceComputer();

            int lowestKeySize = 100000000;
            float lowestNormalizedDistance = 100000000.0f;

            //debug
            StringBuilder debug = new StringBuilder();

            //Try various key size guesses.
            for (int keySize = 2; keySize <= 37; keySize++)
            {
                //If the string isn't long enough, don't try any further key sizes
                if (encryptedData.Length < keySize * 2)
                {
                    break;
                }

                //Get the first keySize bytes out of the encrypted data
                byte[] p1 = GetNBytes(encryptedData, 0, keySize);
                //Get the second keySize bytes out of the encrypted data
                byte[] p2 = GetNBytes(encryptedData, keySize, keySize);

                //Find the distance between those 2 values.
                int distance2 = edc.ComputeBitDistanceBetweenByteArrays(p1, p2);

                //Normalize the value by the length so the shortest key isn't automatically the winner
                //TODO:  Is this good enough?  Comments suggest we might want to be more particular.
                float normalized = ((float)distance2 / (float)keySize);
                if (normalized < lowestNormalizedDistance)
                {
                    lowestNormalizedDistance = normalized;
                    lowestKeySize = keySize;
                }

                debug.AppendFormat("Length:  {0}\tDistance:  {1}\tNormalized:{2}\r\n", keySize, distance2, normalized);
            }

            string s = debug.ToString();

            return lowestKeySize;
        }

        /// <summary>
        /// Retrieve N bytes from a given start position out of the given source array.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="startIndex"></param>
        /// <param name="numBytes"></param>
        /// <returns></returns>
        private static byte[] GetNBytes(byte[] source, int startIndex, int numBytes)
        {
            if(startIndex + numBytes > source.Length)
            {
                //We've been asked to retrieve more bytes than exist.  The distance comparison only works on matching sizes, so just return 
                //  an array of the requested size but with no data.
                return new byte[numBytes];
            }

            byte[] p2 = new byte[numBytes];
            Array.Copy(source, startIndex, p2, 0, numBytes);
            return p2;
        }
    }
}

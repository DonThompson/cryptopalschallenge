using System;
using System.Text;

namespace cryptopalschallenge.Tools
{
    public class EditDistanceComputer
    {
        /// <summary>
        /// Converts given UTF8 strings to bits and compares the bit distance between them.
        /// </summary>
        /// <param name="s1">A plain text UTF8 string</param>
        /// <param name="s2">A plain text UTF8 string</param>
        /// <returns>The number of differing bits</returns>
        public int ComputeBitDistanceBetweenStrings(string s1, string s2)
        {
            byte[] b1 = Encoding.UTF8.GetBytes(s1);
            byte[] b2 = Encoding.UTF8.GetBytes(s2);

            return ComputeBitDistanceBetweenByteArrays(b1, b2);   
        }

        public int ComputeBitDistanceBetweenByteArrays(byte[] b1, byte[] b2)
        {
            if (b1.Length != b2.Length)
            {
                throw new Exception("Cannot compute distance on strings of differing length.");
            }


            int distance = 0;
            //Iterate through each byte
            for (int i = 0; i < b1.Length; i++)
            {
                /*
                //Bitwise Calculation from wikipedia... https://en.wikipedia.org/wiki/Hamming_distance
                //Iterate through each bit in the byte.  If they don't match, add to our distance.  Could also do this with some bitwise shifting operators.
                for (uint val = (uint)(b1[i] ^ b2[i]); val > 0; val /= 2)
                {
                    uint test = val & 1;

                    if (test != 0)
                        distance++;
                }
                */

                //Trying an alternative calculation
                var v = (int)(b1[i] ^ b2[i]);
                while(v != 0)
                {
                    distance++;
                    v &= v-1;
                }
            }

            return distance;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace cryptopalschallenge.Tools
{
    public static class XOR
    {
        public static byte[] ExclusiveOR(byte[] val1, byte[] val2)
        {
            if (val1.Length != val2.Length)
            {
                throw new Exception("Input values must be the same length.");
            }

            byte[] result = new byte[val1.Length];
            for(int i = 0; i < val1.Length; i++)
            {
                result[i] = (byte)(val1[i] ^ val2[i]);
            }
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace cryptopalschallenge.Tools
{
    public class SingleCharXORResult
    {
        public string InputString { get; set; }
        public string BestKey { get; set; }
        public string DecodedString { get; set; }
        public double Rating { get; set; }
    }

    public class SingleCharXORDecoder
    {
        public SingleCharXORResult BestResults { get; set; }

        public SingleCharXORDecoder()
        {
            BestResults = new SingleCharXORResult();
        }

        public void TestAllKeys(byte[] encodedBytes)
        {
            //Work over the whole range of the byte (0-255)
            for (int key = 0; key < 256; key++)
            {
                //We need a byte array of the same length with the character to test
                byte[] testKey = new byte[encodedBytes.Length];
                for (int i = 0; i < encodedBytes.Length; i++)
                {
                    testKey[i] = (byte)key;
                }

                //Now XOR it
                byte[] result = XOR.ExclusiveOR(encodedBytes, testKey);

                //See if it rates as a valid string?
                string readable = Encoding.UTF8.GetString(result);
                CheckHighScore(BitConverter.ToString(encodedBytes).Replace("-", "").ToLower(), readable, (byte)key);
            }
        }

        /// <summary>
        /// Given an encoded string, tests all possible single character keys against it.  Attempts to retrieve the best results by 
        /// analyzing the resulting strings for character frequency.
        /// </summary>
        /// <param name="encodedString"></param>
        public void TestAllKeys(string encodedString)
        {
            byte[] data = HexStringToByteArrayConverter.Convert(encodedString);
            TestAllKeys(data);
        }

        private void CheckHighScore(string input, string readable, byte key)
        {
            //Eval for high score
            double score = LetterFrequencyScorer.ScoreStringBasic(readable);
            if (score > BestResults.Rating)
            {
                BestResults = new SingleCharXORResult()
                {
                    InputString = input,
                    DecodedString = readable,
                    BestKey = ((char)key).ToString(),
                    Rating = score
                };
            }
        }
    }
}

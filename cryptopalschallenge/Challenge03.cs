using System.Text;
using cryptopalschallenge.Tools;

namespace cryptopalschallenge
{
    /// <summary>
    /// https://cryptopals.com/sets/1/challenges/3
    /// </summary>
    public class Challenge03
    {
        private double highScore = 0.0;
        private byte bestKey = 0;

        /// <summary>
        /// Given a string encoded by XOR'ing only a single character, figure out the character
        /// </summary>
        /// <param name="input"></param>
        public string DoChallenge03(string input)
        {
            byte[] data = HexStringToByteArrayConverter.Convert(input);

            //Work over the whole range of the byte (0-255)
            for(int key = 0; key < 256; key++)
            {
                //We need a byte array of the same length with the character to test
                byte[] testKey = new byte[data.Length];
                for(int i = 0; i < data.Length; i++)
                {
                    testKey[i] = (byte)key;
                }

                //Now XOR it
                byte[] result = XOR.ExclusiveOR(data, testKey);

                //See if it rates as a valid string?
                string readable = Encoding.UTF8.GetString(result);
                CheckHighScore(readable, (byte)key);
            }

            return ((char)bestKey).ToString();
        }

        private void CheckHighScore(string readable, byte key)
        {
            //Eval for high score
            double score = LetterFrequencyScorer.ScoreStringBasic(readable);
            if (score > highScore)
            {
                highScore = score;
                bestKey = key;
            }
        }
    }
}

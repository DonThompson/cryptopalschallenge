using cryptopalschallenge.Tools;

namespace cryptopalschallenge
{
    /// <summary>
    /// https://cryptopals.com/sets/1/challenges/3
    /// </summary>
    public class Challenge03
    {
        /// <summary>
        /// Given a string encoded by XOR'ing only a single character, figure out the character
        /// </summary>
        /// <param name="input"></param>
        public string DoChallenge03(string input)
        {
            SingleCharXORDecoder decoder = new SingleCharXORDecoder();
            decoder.TestAllKeys(input);

            return decoder.BestResults.BestKey;
        }
    }
}

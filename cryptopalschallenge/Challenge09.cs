using cryptopalschallenge.Tools;

namespace cryptopalschallenge
{
    /// <summary>
    /// https://cryptopals.com/sets/2/challenges/9
    /// </summary>
    public class Challenge09
    {
        /// <summary>
        /// See PKCS7PaddingHelper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="desiredLength"></param>
        /// <returns></returns>
        public string DoChallenge09(string input, string desiredLength)
        {
            int length = int.Parse(desiredLength);

            return PKCS7PaddingHelper.PadStringToBytes(input, length);
        }
    }
}

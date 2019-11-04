using cryptopalschallenge.Tools;

namespace cryptopalschallenge
{
    /// <summary>
    /// https://cryptopals.com/sets/1/challenges/5
    /// </summary>
    public class Challenge05
    {
        /// <summary>
        /// Implement a repeating key XOR for the given input string
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string DoChallenge05(string input, string key)
        {
            RepeatingKeyXOR encryptor = new RepeatingKeyXOR();
            return encryptor.EncryptInputWithKey(input, key);
        }
    }
}

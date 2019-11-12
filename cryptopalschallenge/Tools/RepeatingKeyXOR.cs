using System;
using System.Collections.Generic;
using System.Linq;
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
        /// USAGE:  Don't use this one.  This is the most simplistic version, comparing only the first 2 blocks.  Despite a suggestion
        /// from the challenge authors to do this, it doesn't actually succeed.
        /// </summary>
        /// <param name="encryptedData">The encrypted data to be analyzed</param>
        /// <returns>The length in bytes of the key that likely encrypted that data</returns>
        public static int FindKeySize_DoNotUse(byte[] encryptedData)
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


        /// <summary>
        /// Same as FindKeySize, but uses the avg distance between the first and each subsequent block instead of just the first two blocks.  This results
        /// in a more fully evaluated result, which is more likely to be correct.
        /// 
        /// USAGE:  Doesn't actually work.  Despite the challenge author's claims, this doesn't work consistently.  I'm leaving it as-is for now and we'll
        /// see if we need to come back to it later.  See more notes on _CopyFromInternet version.
        /// </summary>
        /// <param name="encryptedData"></param>
        /// <returns></returns>
        public static int FindKeySize_AvgDistance(byte[] encryptedData)
        {
            EditDistanceComputer edc = new EditDistanceComputer();

            int lowestKeySize = 100000000;
            float lowestNormalizedDistance = 100000000.0f;

            //debug
            StringBuilder debug = new StringBuilder();

            //Try various key size guesses.
            for (int keySize = 2; keySize <= 40; keySize++)
            {
                //A bit of an assumption here... but we're 
                //  attempting a repeating XOR cipher.  If the key is, say, 10 bytes long, but the text is only 18 bytes long, 
                //  then we haven't really used a repeating XOR cipher - it finished before we repeated.  So let's terminate any keys
                //  if we reach half the distance of the encrypted data.
                if (encryptedData.Length < keySize * 2)
                {
                    break;
                }

                //Get the first keySize bytes out of the encrypted data
                byte[] p1 = GetNBytes(encryptedData, 0, keySize);

                float totalNormalizedDistance = 0.0f;

                //Make an array of all remaining blocks possible
                int numAddtlBlocks = (encryptedData.Length - keySize) / 2;
                for(int i = 1; i <= numAddtlBlocks; i++)
                {
                    byte[] compare = GetNBytes(encryptedData, keySize * i, keySize);

                    //Find the distance between those 2 values.
                    int distance = edc.ComputeBitDistanceBetweenByteArrays(p1, compare);

                    //Normalize the value by the length so the shortest key isn't automatically the winner
                    totalNormalizedDistance += ((float)distance / (float)keySize);
                }

                float avgNormalizedDistance = totalNormalizedDistance / numAddtlBlocks;


                if (avgNormalizedDistance < lowestNormalizedDistance)
                {
                    lowestNormalizedDistance = avgNormalizedDistance;
                    lowestKeySize = keySize;
                }

                debug.AppendFormat("Length:  {0}\tDistance:  {1}\tNormalized:{2}\r\n", keySize, totalNormalizedDistance, avgNormalizedDistance);
            }

            string s = debug.ToString();

            return lowestKeySize;
        }


        /// <summary>
        /// Like _AvgDistance, except instead of comparing block 0 to each remaining block, we just compare neighboring blocks
        /// 
        /// USAGE:  Doesn't actually work.  Despite the challenge author's claims, this doesn't work consistently.  I'm leaving it as-is for now and we'll
        /// see if we need to come back to it later.  See more notes on _CopyFromInternet version.
        /// </summary>
        /// <param name="encryptedData"></param>
        /// <returns></returns>
        public static int FindKeySize_AvgDistanceByNeighbor(byte[] encryptedData)
        {
            EditDistanceComputer edc = new EditDistanceComputer();

            int lowestKeySize = 100000000;
            float lowestNormalizedDistance = 100000000.0f;

            //debug
            StringBuilder debug = new StringBuilder();

            //Try various key size guesses.
            for (int keySize = 2; keySize <= 40; keySize++)
            {
                //A bit of an assumption here... but we're 
                //  attempting a repeating XOR cipher.  If the key is, say, 10 bytes long, but the text is only 18 bytes long, 
                //  then we haven't really used a repeating XOR cipher - it finished before we repeated.  So let's terminate any keys
                //  if we reach half the distance of the encrypted data.
                if (encryptedData.Length < keySize * 2)
                {
                    break;
                }

                float totalNormalizedDistance = 0.0f;

                int numBlocks = (int)Math.Ceiling(encryptedData.Length/(double)keySize);
                for (int i = 0; i < encryptedData.Length; i += keySize)
                {
                    byte[] b1 = GetNBytes(encryptedData, keySize * i, keySize);
                    byte[] b2 = GetNBytes(encryptedData, keySize * (i + 1), keySize);

                    //Find the distance between those 2 values.
                    int distance = edc.ComputeBitDistanceBetweenByteArrays(b1, b2);

                    //Normalize the value by the length so the shortest key isn't automatically the winner
                    //TODO:  totalNormalizedDistance += ((float)distance / (float)keySize);
                    totalNormalizedDistance += (float)distance;
                }
                
                float avgNormalizedDistance = totalNormalizedDistance / numBlocks / keySize;
                if (avgNormalizedDistance < lowestNormalizedDistance)
                {
                    lowestNormalizedDistance = avgNormalizedDistance;
                    lowestKeySize = keySize;
                }

                debug.AppendFormat("Length:  {0}\tDistance:  {1}\tNormalized:{2}\r\n", keySize, totalNormalizedDistance, avgNormalizedDistance);
            }

            string s = debug.ToString();

            return lowestKeySize;
        }

        /// <summary>
        /// https://www.scottbrady91.com/Cryptopals/Caesar-and-Vigenere-Ciphers
        /// 
        /// This should be the same algorithm I have above... but it gets different results, so I've done something wrong...
        /// 
        /// However, it *only* works for the given cryptopals example.  Any other test I give it fails to return the right key length.
        /// 
        /// USAGE:  This works to break the specific Challenge06 cypher.  It works for that phrase & key and returns the proper value.
        /// However, this is not a useful function.  It seems like they cherry picked something that happens to work.  I tried this with
        /// several other phrases/keys and none of them work right.  I also found several others online implementing this and they
        /// have the same problem - it doesn't break anything except rare cyphers (one of which just happens to be the challenge).
        /// Here's an online example with I believe similar code you can play with to see the faults:
        /// https://thmsdnnr.com/cryptopals/s1c6/index.html
        /// 
        /// Given the time investment so far, I'm going to leave this as-is and see how we go.
        /// </summary>
        /// <param name="encryptedData"></param>
        /// <returns></returns>
        public static int FindKeySize_CopyFromInternet(byte[] encryptedData)
        {
            //debug
            StringBuilder debug = new StringBuilder();
            //
            EditDistanceComputer edc = new EditDistanceComputer();
            var keySizeResults = new Dictionary<int, double>();

            // 1. "Let KEYSIZE be the guessed length of the key; try values from 2 to (say) 40."
            for (var keySize = 2; keySize <= 40; keySize++)
            {
                // 2. For hamming distance tests see Funcationlity\StringExtensionTests.cs
                // 3. "For each KEYSIZE, take the first KEYSIZE worth of bytes, 
                // and the second KEYSIZE worth of bytes, and find the edit distance between them.
                // Normalize this result by dividing by KEYSIZE."

                var hammingDistance = 0;
                var numberOfHams = 0;

                for (int i = 1; i < encryptedData.Length / keySize; i++)
                {
                    var firstKeySizeBytes = encryptedData.Skip(keySize * (i - 1)).Take(keySize);
                    var secondKeySizeBytes = encryptedData.Skip(keySize * i).Take(keySize);

                    hammingDistance += edc.ComputeBitDistanceBetweenByteArrays(firstKeySizeBytes.ToArray(), secondKeySizeBytes.ToArray());
                    numberOfHams++;
                }

                if (numberOfHams > 0)
                {
                    double normalizedDistance = (double)hammingDistance / numberOfHams / keySize;
                    keySizeResults.Add(keySize, normalizedDistance);
                    debug.AppendFormat("Length:  {0}\tNormalized:{1}\r\n", keySize, normalizedDistance);
                }
            }


            string s = debug.ToString();
            return keySizeResults.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace cryptopalschallenge.Tools
{
    public static class AESByHand
    {

        public static byte[] EncryptCBCMode(byte[] dataToEncrypt, byte[] key, byte[] IV, int blockLengthBytes)
        {
            //output len = input len + any padding
            int finalPaddedLength = GetNextMultiple(dataToEncrypt.Length, blockLengthBytes);
            byte[] final = new byte[finalPaddedLength];
            //Pad the input to start with
            byte[] input = PKCS7PaddingHelper.PadBytes(dataToEncrypt, finalPaddedLength);


            //Start with the IV
            byte[] previousBlock = IV;

            int currentPosition = 0;

            bool done = false;
            while(!done)
            {
                //shitty
                int lenToCopy = blockLengthBytes;


                //Do we have enough data?
                if(currentPosition + blockLengthBytes > input.Length)
                {
                    //TODO:  pad? or just truncate blocklength?
                    //  I think the below assumes truncation and final isn't sized to pad.

                    //PKCS7PaddingHelper.PadBytes()

                    lenToCopy = input.Length - currentPosition;


                    done = true;
                }


                //Get the next blockLength bytes
                byte[] nextBlock = new byte[blockLengthBytes];
                Array.Copy(input, currentPosition, nextBlock, 0, lenToCopy);

                //shitty
                if(lenToCopy != blockLengthBytes)
                {
                    PKCS7PaddingHelper.PadBytes(nextBlock, blockLengthBytes);
                }

                //XOR the previous block with this block
                byte[] results = XOR.ExclusiveOR(previousBlock, nextBlock);
                //Append to our final
                Array.Copy(results, 0, final, currentPosition, blockLengthBytes);
                //Save the encrypted results as our previous block (not the source text)
                previousBlock = results;
                //inc current position
                currentPosition += blockLengthBytes;
            }

            return final;
        }

        /// <summary>
        /// Given a length, find the next higher multiple of blockLengthBytes if it doesn't match already
        /// </summary>
        /// <param name="length">Any integer number</param>
        /// <param name="blockLengthBytes">The block length you want a multiple of</param>
        /// <returns>The next highest multiple of the block length, higher than the given length.  For example, an input of 445, 16 will return 448.</returns>
        private static int GetNextMultiple(int length, int blockLengthBytes)
        {
            int result = length;

            int missingBytes = length % blockLengthBytes;
            if (missingBytes != 0)
            {
                result += (blockLengthBytes - missingBytes);
            }

            return result;
        }
    }
}

using cryptopalschallenge.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace cryptopalschallenge.Tests
{
    [TestClass]
    public class RepeatingKeyXORTests
    {
        [TestMethod]
        public void TestFindKeySize_Challenge5Results()
        {
            //Results of challenge 5.  This is the below text encrypted with the key ICE via Repeating XOR
            //Burning 'em, if you ain't quick and nimble
            //I go crazy when I hear a cymbal
            string encrypted = "0b3637272a2b2e63622c2e69692a23693a2a3c6324202d623d63343c2a26226324272765272a282b2f20430a652e2c652a3124333a653e2b2027630c692b20283165286326302e27282f";
            byte[] bytes = HexStringToByteArrayConverter.Convert(encrypted);

            int keyLength = RepeatingKeyXOR.FindKeySize(bytes);

            //ICE
            Assert.AreEqual(3, keyLength);
        }

        [TestMethod]
        public void TestFindKeySize_SimplerText()
        {
            //This is the below text encrypted with the key ICE via Repeating XOR
            //very simple phrase
            string encrypted = "3F2637306336202E35252665392B37283020";
            byte[] bytes = HexStringToByteArrayConverter.Convert(encrypted);

            int keyLength = RepeatingKeyXOR.FindKeySize(bytes);

            //ICE
            Assert.AreEqual(3, keyLength);
        }
    }
}

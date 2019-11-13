using Microsoft.VisualStudio.TestTools.UnitTesting;
using cryptopalschallenge.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace cryptopalschallenge.Tools.Tests
{
    [TestClass()]
    public class AESByHandTests
    {
        [TestMethod()]
        public void EncryptCBCModeTest()
        {
            byte[] input = Encoding.ASCII.GetBytes("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.");
            byte[] key = Encoding.ASCII.GetBytes("YELLOW SUBMARINE");
            byte[] IV = Encoding.ASCII.GetBytes("\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00");

            byte[] results = AESByHand.EncryptCBCMode(input, key, IV, 16);

            //Now try to decrypt it using system utils
            string sysResults = AESHelper.DecryptCBC(input, key, IV);

            Assert.AreEqual(sysResults, Encoding.ASCII.GetString(results));
        }
    }
}
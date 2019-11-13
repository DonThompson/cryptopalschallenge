using cryptopalschallenge.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace cryptopalschallenge.Tests
{
    [TestClass]
    public class PKCS7PaddingHelperTests
    {
        private const string input16bytes = "abcdefghabcdefgh";


        /// <summary>
        /// Given 16 bytes pad to 16 bytes - expect no modifications
        /// </summary>
        [TestMethod]
        public void Test16_16_ExpectNoPadding()
        {
            string output = PKCS7PaddingHelper.PadStringToBytes(input16bytes, 16);
            Assert.AreEqual(input16bytes, output);
        }

        [TestMethod]
        public void Test16_20_Expect4Padding()
        {
            string expected = string.Format("{0}\x04\x04\x04\x04", input16bytes);

            string output = PKCS7PaddingHelper.PadStringToBytes(input16bytes, 20);
            Assert.AreEqual(expected, output);
        }

        [TestMethod]
        public void Test16_24_Expect8Padding()
        {
            string expected = string.Format("{0}\x08\x08\x08\x08\x08\x08\x08\x08", input16bytes);

            string output = PKCS7PaddingHelper.PadStringToBytes(input16bytes, 24);
            Assert.AreEqual(expected, output);
        }

        /// <summary>
        /// Test double-digits
        /// </summary>
        [TestMethod]
        public void Test16_32_Expect16Padding()
        {
            string expected = string.Format("{0}\x10\x10\x10\x10\x10\x10\x10\x10\x10\x10\x10\x10\x10\x10\x10\x10", input16bytes);

            string output = PKCS7PaddingHelper.PadStringToBytes(input16bytes, 32);
            Assert.AreEqual(expected, output);
        }

        /// <summary>
        /// Pad a 4 byte string to 2 bytes
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestTooLong_ExpectException()
        {
            PKCS7PaddingHelper.PadStringToBytes("aaaa", 2);
        }
    }
}

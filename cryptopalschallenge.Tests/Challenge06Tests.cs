using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace cryptopalschallenge.Tests
{
    [TestClass]
    public class Challenge06Tests
    {
        [TestMethod]
        public void ExpectedTest()
        {
            Challenge06 c = new Challenge06();
            string result = c.DoChallenge06();

            Assert.AreEqual("I don't know yet", result);
        }
    }
}

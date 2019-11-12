using cryptopalschallenge.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace cryptopalschallenge.Tests
{
    [TestClass]
    public class EditDistanceComputerTests
    {
        [TestMethod]
        public void ExpectedTest()
        {
            string s1 = "this is a test";
            string s2 = "wokka wokka!!!";

            EditDistanceComputer c = new EditDistanceComputer();
            int distance = c.ComputeBitDistanceBetweenStrings(s1, s2);
            Assert.AreEqual(37, distance);
        }
    }
}

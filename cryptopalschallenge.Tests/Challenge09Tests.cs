using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cryptopalschallenge.Tests
{
    [TestClass]
    public class Challenge09Tests
    {
        [TestMethod]
        public void TestExpected()
        {
            Challenge09 c09 = new Challenge09();
            string output = c09.DoChallenge09("YELLOW SUBMARINE", "20");
            Assert.AreEqual("YELLOW SUBMARINE\x04\x04\x04\x04", output);
        }
    }
}

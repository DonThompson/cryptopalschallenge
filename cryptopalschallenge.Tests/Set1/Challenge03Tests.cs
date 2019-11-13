using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cryptopalschallenge.Tests
{
    [TestClass]
    public class Challenge03Tests
    {
        [TestMethod]
        public void ExpectedTest()
        {
            Challenge03 c = new Challenge03();
            string output = c.DoChallenge03("1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736");
            Assert.AreEqual("X", output);
        }
    }
}

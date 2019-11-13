using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cryptopalschallenge.Tests
{
    [TestClass]
    public class Challenge04Tests
    {
        [TestMethod]
        public void ExpectedTest()
        {
            Challenge04 c = new Challenge04();
            string output = c.DoChallenge04();
            Assert.AreEqual("Key:  5.  Input string:  7b5a4215415d544115415d5015455447414c155c46155f4058455c5b523f.  Output string:  Now that the party is jumping.", output);
        }
    }
}

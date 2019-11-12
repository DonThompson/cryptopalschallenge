using Microsoft.VisualStudio.TestTools.UnitTesting;

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

            Assert.AreEqual("Terminator X: Bring the noise", result);
        }
    }
}

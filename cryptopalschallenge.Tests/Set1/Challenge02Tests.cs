using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cryptopalschallenge.Tests
{
    [TestClass]
    public class Challenge02Tests
    {
        [TestMethod]
        public void ExpectedTest()
        {
            Challenge02 c = new Challenge02();
            string output = c.DoChallenge02("1c0111001f010100061a024b53535009181c", "686974207468652062756c6c277320657965");
            Assert.AreEqual("746865206b696420646f6e277420706c6179", output.ToLower());
        }
    }
}

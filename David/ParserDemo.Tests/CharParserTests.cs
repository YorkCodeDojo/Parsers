using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParserDemo.Tests
{
    [TestClass]
    public class CharParserTests
    {
        [TestMethod]
        public void ParseAWorks()
        {
            var parser = new CharParser('A');
            var result = parser.Parse("A");

            Assert.IsTrue(result.Success);
            Assert.AreEqual("A", result.Parsed);
            Assert.AreEqual("", result.Remainder);
        }

        [TestMethod]
        public void ParseBADoesNotWork()
        {
            var parser = new CharParser('A');
            var result = parser.Parse("BA");

            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Parsed);
            Assert.AreEqual("BA", result.Remainder);
        }

    }
}

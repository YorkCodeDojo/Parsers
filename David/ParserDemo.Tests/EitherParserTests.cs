using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParserDemo.Tests
{
    [TestClass]
    public class EitherParserTests
    {
        [TestMethod]
        public void ParseAWorks()
        {
            var Aparser = new CharParser('A');
            var Bparser = new CharParser('B');
            var parser = new EitherParser(Aparser, Bparser);
            var result = parser.Parse("ADEF");

            Assert.IsTrue(result.Success);
            Assert.AreEqual("A", result.Parsed);
            Assert.AreEqual("DEF", result.Remainder);
        }

        [TestMethod]
        public void ParseBWorks()
        {
            var Aparser = new CharParser('A');
            var Bparser = new CharParser('B');
            var parser = new EitherParser(Aparser, Bparser);
            var result = parser.Parse("B");

            Assert.IsTrue(result.Success);
            Assert.AreEqual("B", result.Parsed);
            Assert.AreEqual("", result.Remainder);
        }


        [TestMethod]
        public void ParseCDoesNotWork()
        {
            var Aparser = new CharParser('A');
            var Bparser = new CharParser('B');
            var parser = new EitherParser(Aparser, Bparser);
            var result = parser.Parse("C");

            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Parsed);
            Assert.AreEqual("C", result.Remainder);
        }


        [TestMethod]
        public void ParseCWorks()
        {
            var Aparser = new CharParser('A');
            var Bparser = new CharParser('B');
            var Cparser = new CharParser('C');
            var ABparser = new EitherParser(Aparser, Bparser);
            var parser = new EitherParser(ABparser, Cparser);
            var result = parser.Parse("CXYZ");

            Assert.IsTrue(result.Success);
            Assert.AreEqual("C", result.Parsed);
            Assert.AreEqual("XYZ", result.Remainder);
        }

    }
}

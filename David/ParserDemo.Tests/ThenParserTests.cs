using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParserDemo.Tests
{
    [TestClass]
    public class ThenParserTests
    {
        [TestMethod]
        public void ParseABWorks()
        {
            var Aparser = new StartsWithChar('A');
            var Bparser = new StartsWithChar('B');
            var ABparser = new Then(Aparser, Bparser);
            var result = ABparser.Parse("AB");

            Assert.IsTrue(result.Success);

            var parsed = (List<object>)result.Parsed;
            Assert.AreEqual(2, parsed.Count);
            Assert.AreEqual("A", parsed[0]);
            Assert.AreEqual("B", parsed[1]);
            Assert.AreEqual("", result.Remainder);
        }

        [TestMethod]
        public void ParseABCWorks()
        {
            var Aparser = new StartsWithChar('A');
            var Bparser = new StartsWithChar('B');
            var Cparser = new StartsWithChar('C');
            var ABparser = new Then(Aparser, Bparser);
            var ABCparser = new Then(ABparser, Cparser);
            var result = ABCparser.Parse("ABCD");

            Assert.IsTrue(result.Success);

            var parsed = (List<object>)result.Parsed;
            Assert.AreEqual(3, parsed.Count);
            Assert.AreEqual("A", parsed[0]);
            Assert.AreEqual("B", parsed[1]);
            Assert.AreEqual("C", parsed[2]);
            Assert.AreEqual("D", result.Remainder);
        }


        [TestMethod]
        public void ParseA_plus_B_plus_CWorks()
        {
            var Aparser = new StartsWithChar('A');
            var Bparser = new StartsWithChar('B');
            var Cparser = new StartsWithChar('C');
            var ABCparser = Aparser + Bparser + Cparser; 
            var result = ABCparser.Parse("ABCD");

            Assert.IsTrue(result.Success);

            var parsed = (List<object>)result.Parsed;
            Assert.AreEqual(3, parsed.Count);
            Assert.AreEqual("A", parsed[0]);
            Assert.AreEqual("B", parsed[1]);
            Assert.AreEqual("C", parsed[2]);
            Assert.AreEqual("D", result.Remainder);
        }

        [TestMethod]
        public void ParseACBDoesNotWorks()
        {
            var Aparser = new StartsWithChar('A');
            var Bparser = new StartsWithChar('B');
            var ABparser = new Then(Aparser, Bparser);
            var result = ABparser.Parse("AC");

            Assert.IsFalse(result.Success);
        }

    }
}

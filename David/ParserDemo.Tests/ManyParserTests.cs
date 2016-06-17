using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ParserDemo.Tests
{
    [TestClass]
    public class ManyParserTests
    {
        [TestMethod]
        public void ParseAAAAWorks()
        {
            var Aparser = new StartsWithChar('A');
            var parser = new Many(Aparser);
            var result = parser.Parse("AAAAB");

            Assert.IsTrue(result.Success);
            Assert.AreEqual("AAAA", ListToString((List<object>)result.Parsed));
            Assert.AreEqual("B", result.Remainder);
        }

        [TestMethod]
        public void ParseABABAACWorks()
        {
            var Aparser = new StartsWithChar('A');
            var Bparser = new StartsWithChar('B');
            var BAparser = new Either(Aparser, Bparser);
            var parser = new Many(BAparser);
            var result = parser.Parse("ABABAAC");

            Assert.IsTrue(result.Success);
            Assert.AreEqual("ABABAA", ListToString((List<object>)result.Parsed));
            Assert.AreEqual("C", result.Remainder);
        }

        [TestMethod]
        public void ParseDABABAACDoesNotWork()
        {
            var Aparser = new StartsWithChar('A');
            var Bparser = new StartsWithChar('B');
            var BAparser = new Either(Aparser, Bparser);
            var parser = new Many(BAparser);
            var result = parser.Parse("DABABAAC");

            Assert.IsFalse(result.Success);
            Assert.AreEqual("", "");
            Assert.AreEqual("DABABAAC", result.Remainder);
        }

        private string ListToString(List<object> theList)
        {
            List<string> text = theList.Select(l => l.ToString()).ToList();
            return string.Join("", text);
        }

    }
}

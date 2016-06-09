using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ParserDemo.Tests
{
    [TestClass]
    public class ApplyTests
    {
        [TestMethod]
        public void OneIs1()
        {
            var parser = new CharParser('1').ApplyString(l => int.Parse(l));
            var result = parser.Parse("1");

            Assert.IsTrue(result.Success);
            Assert.AreEqual(1, result.Parsed);
            Assert.AreEqual("", result.Remainder);
        }

        [TestMethod]
        public void AplusBplusCisABC()
        {
            var Aparser = new CharParser('A');
            var Bparser = new CharParser('B');
            var Cparser = new CharParser('C');
            var ABCparser = (Aparser + Bparser + Cparser).Apply(ListToString);
            var result = ABCparser.Parse("ABCD");

            Assert.IsTrue(result.Success);
            Assert.AreEqual("ABC", result.Parsed);
            Assert.AreEqual("D", result.Remainder);
        }

        private string ListToString(object list) => string.Join("", ((List<object>)list).Cast<string>().ToArray());
    }
}

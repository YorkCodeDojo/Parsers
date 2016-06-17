using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParserDemo.Tests
{
    [TestClass]
    public class BinaryTest
    {
        [TestMethod]
        public void CheckParsingADigitWorks()
        {
            var digitParser = (new StartsWithChar('0') |
                               new StartsWithChar('1') |
                               new StartsWithChar('2') |
                               new StartsWithChar('3') |
                               new StartsWithChar('4') |
                               new StartsWithChar('5') |
                               new StartsWithChar('6') |
                               new StartsWithChar('7') |
                               new StartsWithChar('8') |
                               new StartsWithChar('9')).Apply(l => int.Parse((string)l));
            var result = digitParser.Parse("3");
            Assert.AreEqual(3, result.Parsed);
        }

        [TestMethod]
        public void CheckParsingADigitPlusADigitWorks()
        {
            var digitParser = (new StartsWithChar('0') |
                               new StartsWithChar('1') |
                               new StartsWithChar('2') |
                               new StartsWithChar('3') |
                               new StartsWithChar('4') |
                               new StartsWithChar('5') |
                               new StartsWithChar('6') |
                               new StartsWithChar('7') |
                               new StartsWithChar('8') |
                               new StartsWithChar('9')).Apply(l =>
                                                            new Digit() { Value = int.Parse((string)l) }
                                                          );

            var operatorParser = (new StartsWithChar('+')
                                | new StartsWithChar('-'));

            var expression = (digitParser + operatorParser + digitParser).Apply(BuildExpression);
            var expression2 = (digitParser + operatorParser + expression).Apply(BuildExpression);

            var result = expression.Parse("3+4");
            var parsed = (BinaryExpression)result.Parsed;
            Assert.AreEqual("", result.Remainder);

      
        }

        class Command
        {

        }

        class PrintCommand : Command
        {
            private string printWhat;
            public PrintCommand(string printWhat)
            {
                this.printWhat = printWhat;
            }
        }

        private BinaryExpression BuildExpression(object list)
        {
            var typedList = (List<object>)list;
            var result = new BinaryExpression()
            {
                LHS = (Digit)typedList[0] ,
                RHS = (Digit)typedList[2] ,
                Operator = ((string)typedList[1]).ToCharArray()[0]
            };
            return result;
        }
    }



    public class Expr
    {
    }

    public class Digit : Expr
    {
        public int Value { get; set; }
    }

    public class BinaryExpression : Expr
    {
        public Expr LHS { get; set; }
        public Expr RHS { get; set; }

        public char Operator { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserDemo.Tests.Commands;

namespace ParserDemo.Tests
{
    [TestClass]
    public class Exercises
    {

        [TestMethod]
        public void StartsWithChar()
        {
            var startsWithA = new StartsWithChar('A');
            Do_Parser_Test(startsWithA, "ABC", true, 'A', "BC");
            Do_Parser_Test(startsWithA, "AAAA", true, 'A', "AAA");
            Do_Parser_Test(startsWithA, "BCD", false, null, "BCD");
            Do_Parser_Test(startsWithA, "", false, null, "");
            Do_Parser_Test(startsWithA, "A", true, 'A', "");
        }

        [TestMethod]
        public void StartsWithAnyChar()
        {
            var startsWithAB = new StartsWithAnyChar("AB".ToCharArray());
            Do_Parser_Test(startsWithAB, "ABC", true, 'A', "BC");
            Do_Parser_Test(startsWithAB, "BCD", true, 'B', "CD");
            Do_Parser_Test(startsWithAB, "CDEF", false, null, "CDEF");

            var isLowerCaseCharacter = new StartsWithAnyChar("abcdefghijklmnopqrstuvwxyz".ToCharArray());
            var isUpperCaseCharacter = new StartsWithAnyChar("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray());
            var isDigit = new StartsWithAnyChar("0123456789".ToCharArray());

            Do_Parser_Test(isLowerCaseCharacter, "david", true, 'd', "avid");
            Do_Parser_Test(isLowerCaseCharacter, "DAVID", false, null, "DAVID");

            Do_Parser_Test(isUpperCaseCharacter, "DAVID", true, 'D', "AVID");
            Do_Parser_Test(isUpperCaseCharacter, "david", false, null, "david");

            Do_Parser_Test(isDigit, "1234", true, '1', "234");
            Do_Parser_Test(isDigit, "DAVID", false, null, "DAVID");
        }


        [TestMethod]
        public void Either()
        {
            var startsWithA = new StartsWithChar('A');
            var startsWithB = new StartsWithChar('B');
            var startsWithAorB = startsWithA | startsWithB;

            Do_Parser_Test(startsWithAorB, "ABC", true, 'A', "BC");
            Do_Parser_Test(startsWithAorB, "BAC", true, 'B', "AC");
            Do_Parser_Test(startsWithAorB, "CBA", false, null, "CBA");

            var isLowerCaseCharacter = new StartsWithAnyChar("abcdefghijklmnopqrstuvwxyz".ToCharArray());
            var isUpperCaseCharacter = new StartsWithAnyChar("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray());
            var isCharacter = isLowerCaseCharacter | isUpperCaseCharacter;

            Do_Parser_Test(isCharacter, "Abc", true, 'A', "bc");
            Do_Parser_Test(isCharacter, "123", false, null, "123");
        }


        [TestMethod]
        public void Many()
        {
            var startsWithA = new StartsWithChar('A');
            var startsWithB = new StartsWithChar('B');
            var startsWithAorB = startsWithA | startsWithB;
            var startsWithManyA = new Many(startsWithA);
            var startsWithManyAorB = new Many(startsWithAorB);

            Do_Parser_Test(startsWithManyA, "AAAB", true, StringToList("AAA"), "B");
            Do_Parser_Test(startsWithManyA, "CAAAB", true, null, "CAAAB");
            Do_Parser_Test(startsWithManyAorB, "ABABAABBC", true, StringToList("ABABAABB"), "C");

            var isDigit = new StartsWithAnyChar("0123456789".ToCharArray());
            var isInteger = new Many(isDigit);

            Do_Parser_Test(isInteger, "12345.67", true, StringToList("12345"), ".67");
            Do_Parser_Test(isInteger, "a12345.67", true, null, "a12345.67");
        }

        [TestMethod]
        public void OneOrMany()
        {
            var startsWithA = new StartsWithChar('A');
            var startsWithManyA = new OneOrMany(startsWithA);

            Do_Parser_Test(startsWithManyA, "AAAB", true, StringToList("AAA"), "B");
            Do_Parser_Test(startsWithManyA, "CAAAB", false, null, "CAAAB");

        }


        [TestMethod]
        public void Then()
        {
            var startsWithA = new StartsWithChar('A');
            var startsWithB = new StartsWithChar('B');
            var startsWithAthenB = startsWithA + startsWithB;

            Do_Parser_Test(startsWithAthenB, "ABA", true, StringToList("AB"), "A");
            Do_Parser_Test(startsWithAthenB, "AC", false, null, "AC");
            Do_Parser_Test(startsWithAthenB, "BA", false, null, "BA");

            var ab = new List<object> { 'A', 'B' };
            Do_Parser_Test(new Many(startsWithAthenB), "ABABABC", true, new List<List<object>>() { ab, ab, ab }, "C");

            var isDigit = new StartsWithAnyChar("0123456789".ToCharArray());
            var isInteger = new Many(isDigit);
            var isPeriod = new StartsWithChar('.');
            var isDecimal = (isInteger + isPeriod + isInteger) | isInteger;

            Do_Parser_Test(isDecimal, "1234.56", true, StringToList("1234.56"), "");
            Do_Parser_Test(isDecimal, "1234", true, StringToList("1234"), "");
            Do_Parser_Test(isDecimal, ".1234.", true, StringToList(".1234"), ".");
        }

        [TestMethod]
        public void Exercise1()
        {
            var print = new StartsWithWord("Print");
            var whitespace = new StartsWithAnyChar(new char[] { ' ', '\t' });
            var openBracket = new StartsWithChar('(');
            var closeBracket = new StartsWithChar(')');
            var doubleQuotes = new StartsWithChar('"');
            var anyChar = new StartsWith(c => c != '"');
            var stringLit = doubleQuotes + (new Many(anyChar)) + doubleQuotes;
            var semiColon = new StartsWithChar(';');
            var command = print + (new Many(whitespace)) + openBracket + stringLit + closeBracket + semiColon;

            var parseResult = command.Parse(@"Print     (""Hello World"");");
            Assert.AreEqual(true, parseResult.Success);
        }

        [TestMethod]
        public void Exercise2()
        {
            var isOut = new StartsWithWord("Out");
            var isPut = new StartsWithWord("put");
            var isExactlyOut = isOut + new Not(isPut);

            Do_Parser_Test(isOut, "Out", true, "Out", "");
            Do_Parser_Test(isOut, "Output", true, "Out", "put");

            Do_Parser_Test(isExactlyOut, "Out", true, new List<object>() { "Out" }, "");
            Do_Parser_Test(isExactlyOut, "Output", false, null, "Output");
        }

        [TestMethod]
        public void Exercise3()
        {
            var print = new StartsWithWord("Print");
            var whitespace = new Many(new StartsWithAnyChar(new char[] { ' ', '\t' })).Apply(ListToString);
            var openBracket = new StartsWithChar('(');
            var closeBracket = new StartsWithChar(')');
            var doubleQuotes = new StartsWithChar('"');
            var anyChar = new StartsWith(c => c != '"');
            var stringLit = (doubleQuotes + (new Many(anyChar)) + doubleQuotes).Apply(ListToString);
            var semiColon = new StartsWithChar(';');
            var command = print + whitespace + openBracket + stringLit + closeBracket + semiColon;
            command.Apply6((a, b, c, d, e, f) => new PrintCommand((string)d));
            var parseResult = command.Parse(@"Print     (""Hello World"");");
            Assert.AreEqual(true, parseResult.Success);
            Assert.IsInstanceOfType(parseResult.Parsed, typeof(PrintCommand));
        }


        [TestMethod]
        public void Exercise4()
        {
            //Dim a As String = "Hello"
            //Dim b As Integer = 6
            var Dim = new StartsWithWord("Dim");
            var whitespace = new Many(new StartsWithAnyChar(new char[] { ' ', '\t' })).Apply(ListToString);
            var As = new StartsWithWord("As");
            var varString = new StartsWithWord("String");
            var varInteger = new StartsWithWord("Integer");
            var equals = new StartsWithChar('=');
            var doubleQuotes = new StartsWithChar('"');
            var anyChar = new StartsWith(c => c != '"');
            var identifier = (new Many(new StartsWith(c => char.IsLetterOrDigit(c)))).Apply(ListToString);
            var stringLit = (doubleQuotes + (new Many(anyChar)) + doubleQuotes).Apply(ListToString);
            var isDigit = new StartsWithAnyChar("0123456789".ToCharArray());
            var isInteger = new Many(isDigit).Apply(ListToString);

            var varType = varString | varInteger;
            var defaultValue = stringLit | isInteger;
            var declaration = (Dim + whitespace + identifier + whitespace + As + whitespace + varType + whitespace + equals + whitespace + defaultValue).Apply(a => { var l = (List<object>)a; return new Declaration((string)l[2], (string)l[6], (string)l[10]); });

            var parseResult = declaration.Parse(@"Dim a As String = ""Hello""");
            Assert.AreEqual(true, parseResult.Success);

            parseResult = declaration.Parse(@"Dim b As Integer = 6");
            Assert.AreEqual(true, parseResult.Success);

        }

        [TestMethod]
        public void Exercise5()
        {
            var pWhitespace = new Many(new StartsWithAnyChar(new char[] { ' ', '\t' })).Apply(ListToString);
            var pDigit = new StartsWithAnyChar("0123456789".ToCharArray());
            var pInteger = new Many(pDigit).Apply(ListToInteger);
            var pNewLine = new StartsWithWord(System.Environment.NewLine).ApplyString(a => new BlankLine());
            var pMoveDown = (new StartsWithWord("Move Down") + pWhitespace + pInteger).Apply3((a, b, c) => new MoveDown((int)c));
            var pMoveUp = (new StartsWithWord("Move Up") + pWhitespace + pInteger).Apply3((a, b, c) => new MoveUp((int)c)); ;
            var pMoveLeft = (new StartsWithWord("Move Left") + pWhitespace + pInteger).Apply3((a, b, c) => new MoveLeft((int)c)); ;
            var pMoveRight = (new StartsWithWord("Move Right") + pWhitespace + pInteger).Apply3((a, b, c) => new MoveRight((int)c)); ;
            var pCommand = pMoveDown | pMoveUp | pMoveLeft | pMoveRight | pNewLine;
            var pProgram = new Many((pCommand + new OneOrMany(pNewLine)).Apply2((a, b) => a));

            var parseResult = pProgram.Parse(@"

Move Down 123
Move Left 123
Move Up 123
Move Right 123
");
            var theProgram = ((List<object>)parseResult.Parsed).Cast<Command>();
            var output = new StringBuilder();
            foreach (var cmd in theProgram)
            {
                if (cmd is MoveDown)
                {
                    var command = (MoveDown)cmd;
                    output.AppendLine($"newX = X;");
                    output.AppendLine($"newY = Y + {command.Distance};");
                    output.AppendLine($"g.DrawLine(Pens.Blue, X, Y, newX, newY);");
                    output.AppendLine($"X = newX;");
                    output.AppendLine($"Y = newY;");
                }

                if (cmd is MoveUp)
                {
                    var command = (MoveUp)cmd;
                    output.AppendLine($"newX = X;");
                    output.AppendLine($"newY = Y - {command.Distance};");
                    output.AppendLine($"g.DrawLine(Pens.Green, X, Y, newX, newY);");
                    output.AppendLine($"X = newX;");
                    output.AppendLine($"Y = newY;");
                }

                if (cmd is MoveLeft)
                {
                    var command = (MoveLeft)cmd;
                    output.AppendLine($"newX = X - {command.Distance};");
                    output.AppendLine($"newY = Y;");
                    output.AppendLine($"g.DrawLine(Pens.Red, X, Y, newX, newY);");
                    output.AppendLine($"X = newX;");
                    output.AppendLine($"Y = newY;");
                }

                if (cmd is MoveRight)
                {
                    var command = (MoveRight)cmd;
                    output.AppendLine($"newX = X + {command.Distance};");
                    output.AppendLine($"newY = Y;");
                    output.AppendLine($"g.DrawLine(Pens.Black, X, Y, newX, newY);");
                    output.AppendLine($"X = newX;");
                    output.AppendLine($"Y = newY;");
                }

            }

            var filename = @"C:\CodeDojo\York\Parsers\David\Generated\Form1.cs";
            var template = System.IO.File.ReadAllText(filename);
            var markedUp = template.Replace("//ADD_CODE_HERE", output.ToString());
            System.IO.File.WriteAllText(filename, markedUp);

        }

        private string ListToString(object l)
        {
            var asList = l as List<object>;
            if (asList == null) throw new Exception("Was expecting a list");

            return string.Join("", asList);
        }

        private object ListToInteger(object l)
        {
            var asList = l as List<object>;
            if (asList == null) throw new Exception("Was expecting a list");

            return int.Parse(string.Join("", asList));
        }

        private static void Do_Parser_Test(Parser parser, string input, bool result, object parsed, string remaining)
        {
            var parseResult = parser.Parse(input);
            Assert.AreEqual(result, parseResult.Success);

            if (parsed is List<List<object>>)
            {
                var expected = (List<List<object>>)(parsed);
                var actual = (List<object>)(parseResult.Parsed);
                Assert.AreEqual(expected.Count(), actual.Count());
                for (int i = 0; i < expected.Count(); i++)
                {
                    CollectionAssert.AreEqual(expected[i] as List<object>, actual[i] as List<object>);
                }
            }
            else if (parsed is List<object>)
                CollectionAssert.AreEqual(parsed as List<object>, parseResult.Parsed as List<object>);
            else
                Assert.AreEqual(parsed, parseResult.Parsed);

            Assert.AreEqual(remaining, parseResult.Remainder);
        }

        private static List<object> StringToList(string theString) => theString.ToCharArray().Select(a => (object)a).ToList();



    }
}

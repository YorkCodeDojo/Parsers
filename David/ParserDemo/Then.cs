using System.Collections.Generic;

namespace ParserDemo
{
    public class Then : Parser
    {
        private Parser firstParser;
        private Parser nextParser;
        public Then(Parser firstParser, Parser nextParser)
        {
            this.firstParser = firstParser;
            this.nextParser = nextParser;
        }

        // a + b + c
        // is then(then(a,b),c)
        // a + b => list(a,b)
        // ab + c => list(a,b,c)

        public override ParseResult Parse(string text)
        {
            var firstResult = this.firstParser.Parse(text);
            if (!firstResult.Success) return this.WasFailure(text);

            var secondResult = this.nextParser.Parse(firstResult.Remainder);
            if (!secondResult.Success) return this.WasFailure(text);

            var newList = new List<object>();
            if (firstResult.Parsed is List<object>)
                newList.AddRange((List<object>)firstResult.Parsed);
            else if (firstResult.Parsed != null)
            {
                newList.Add(firstResult.Parsed);
            }

            if (secondResult.Parsed is List<object>)
                newList.AddRange((List<object>)secondResult.Parsed);
            else if (secondResult.Parsed != null)
            {
                newList.Add(secondResult.Parsed);
            }

            return this.WasSuccess(newList, secondResult.Remainder);
        }


    }
}

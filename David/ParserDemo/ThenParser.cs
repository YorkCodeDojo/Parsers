using System.Collections.Generic;

namespace ParserDemo
{
    public class ThenParser : Parser
    {
        private Parser firstParser;
        private Parser nextParser;
        public ThenParser(Parser firstParser, Parser nextParser)
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
            if (!firstResult.Success) return firstResult;

            var secondResult = this.nextParser.Parse(firstResult.Remainder);
            if (!secondResult.Success) return secondResult;

            var newList = new List<object>();
            if (firstResult.Parsed as List<object> == null)
                newList.Add(firstResult.Parsed);
            else
                newList.AddRange((List<object>)firstResult.Parsed);

            newList.Add(secondResult.Parsed);

            return this.WasSuccess(newList, secondResult.Remainder);
        }


    }
}

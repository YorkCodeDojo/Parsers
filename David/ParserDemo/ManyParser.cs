using System;
using System.Collections.Generic;

namespace ParserDemo
{
    public class ManyParser : Parser
    {
        private Parser theParser;

        public ManyParser(Parser theParser)
        {
            this.theParser = theParser;
        }

        public override ParseResult Parse(string text)
        {
            // We track everything we have managed to parse in a list
            var parsed = new List<object>();

            var pr = theParser.Parse(text);
            if (!pr.Success) return pr;

            while (pr.Success)
            {
                parsed.Add(pr.Parsed);
                pr = theParser.Parse(pr.Remainder);
            }

            return WasSuccess(parsed, pr.Remainder);
        }
    }
}

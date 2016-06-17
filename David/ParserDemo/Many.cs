using System;
using System.Collections.Generic;

namespace ParserDemo
{
    public class Many : Parser
    {
        private Parser theParser;

        public Many(Parser theParser)
        {
            this.theParser = theParser;
        }

        public override ParseResult Parse(string text)
        {
            // We track everything we have managed to parse in a list
            var parsed = new List<object>();

            var pr = theParser.Parse(text);
            if (!pr.Success) return WasSuccess(null, text); ;

            while (pr.Success)
            {
                parsed.Add(pr.Parsed);
                pr = theParser.Parse(pr.Remainder);
            }

            return WasSuccess(parsed, pr.Remainder);
        }
    }
}

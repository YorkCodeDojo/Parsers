using System;
using System.Collections.Generic;

namespace ParserDemo
{
    public class Not : Parser
    {
        private Parser theParser;

        public Not(Parser theParser)
        {
            this.theParser = theParser;
        }

        public override ParseResult Parse(string text)
        {
            var pr = theParser.Parse(text);
            if (pr.Success)
                return this.WasFailure(text);
            else
                return this.WasSuccess(null, text);

        }
    }
}

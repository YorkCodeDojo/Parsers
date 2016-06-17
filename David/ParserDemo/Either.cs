using System;

namespace ParserDemo
{
    public class Either : Parser
    {
        private Parser aparser;
        private Parser bparser;

        public Either(Parser aparser, Parser bparser)
        {
            this.aparser = aparser;
            this.bparser = bparser;
        }

        public override ParseResult Parse(string text)
        {

            var lhs = this.aparser.Parse(text);
            if (lhs.Success) return WasSuccess(lhs.Parsed, lhs.Remainder);

            var rhs = this.bparser.Parse(text);
            if (rhs.Success) return WasSuccess(rhs.Parsed, rhs.Remainder);

            return WasFailure(text);
        }
    }
}

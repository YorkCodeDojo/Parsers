using System;

namespace ParserDemo
{
    public class EitherParser : Parser
    {
        private Parser aparser;
        private Parser bparser;

        public EitherParser(Parser aparser, Parser bparser)
        {
            this.aparser = aparser;
            this.bparser = bparser;
        }

        public override ParseResult Parse(string text)
        {

            var lhs = this.aparser.Parse(text);
            if (lhs.Success) return lhs;

            var rhs = this.bparser.Parse(text);
            if (rhs.Success) return rhs;

            return WasFailure(text);
        }
    }
}

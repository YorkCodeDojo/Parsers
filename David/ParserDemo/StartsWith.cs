using System;

namespace ParserDemo
{
    public class StartsWith: Parser
    {
        private Func<char,bool> fn;

        public StartsWith(Func<char,bool> fn)
        {
            this.fn = fn;
        }

        public override ParseResult Parse(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return WasFailure(text);

            if (fn(text[0]))
            {
                var remainder = (text.Length == 1) ? "" : text.Substring(1);
                return WasSuccess(text[0], remainder);
            }
            else
            {
                return WasFailure(text);
            }
        }


    }
}

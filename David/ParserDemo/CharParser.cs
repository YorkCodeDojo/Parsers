using System;

namespace ParserDemo
{
    public class CharParser : Parser
    {
        private char charToMatch;

        public CharParser(char charToMatch)
        {
            this.charToMatch = charToMatch;
        }

        public override ParseResult Parse(string text)
        {
            if (text.StartsWith(this.charToMatch.ToString()))
            {
                var remainder = (text == "") ? "" : text.Substring(1);
                return WasSuccess(text.Substring(0, 1), remainder);
            }
            else
            {
                return WasFailure(text);
            }
        }


    }
}

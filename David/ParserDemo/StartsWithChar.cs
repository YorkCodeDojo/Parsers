using System;

namespace ParserDemo
{
    public class StartsWithChar : Parser
    {
        private char charToMatch;

        public StartsWithChar(char charToMatch)
        {
            this.charToMatch = charToMatch;
        }

        public override ParseResult Parse(string text)
        {
            if (text.StartsWith(this.charToMatch.ToString()))
            {
                var remainder = (text == "") ? "" : text.Substring(1);
                return WasSuccess(text[0], remainder);
            }
            else
            {
                return WasFailure(text);
            }
        }


    }
}

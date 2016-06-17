using System.Linq;

namespace ParserDemo
{
    public class StartsWithAnyChar : Parser
    {
        private char[] charsToMatch;

        public StartsWithAnyChar(char[] charsToMatch)
        {
            this.charsToMatch = charsToMatch;
        }

        public override ParseResult Parse(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return WasFailure(text);

            if (this.charsToMatch.Contains(text[0]))
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

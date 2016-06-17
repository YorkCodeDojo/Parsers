using System;

namespace ParserDemo
{
    public class StartsWithWord : Parser
    {
        private string wordToMatch;

        public StartsWithWord(string wordToMatch)
        {
            this.wordToMatch = wordToMatch;
        }

        public override ParseResult Parse(string text)
        {
            if (text.StartsWith(this.wordToMatch))
            {
                var remainder = (text.Length == this.wordToMatch.Length) ? "" : text.Substring(this.wordToMatch.Length);
                return WasSuccess(this.wordToMatch, remainder);
            }
            else
            {
                return WasFailure(text);
            }
        }


    }
}

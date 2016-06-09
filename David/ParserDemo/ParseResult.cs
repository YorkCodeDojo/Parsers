
namespace ParserDemo
{
    public class ParseResult
    {
        public ParseResult(string remainder)
        {
            this.Remainder = remainder;
            this.Success = false;
            this.Parsed = null;
        }

        public ParseResult(object parsed, string remainder)
        {
            this.Parsed = parsed;
            this.Remainder = remainder;
            this.Success = true;
        }

        public object Parsed { get; private set; }
        public string Remainder { get; private set; }
        public bool Success { get; private set; }



    }
}

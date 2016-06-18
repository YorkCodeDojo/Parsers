using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserDemo
{
    public abstract class Parser
    {
        public abstract ParseResult Parse(string text);

        protected ParseResult WasFailure(string remainder)
        {
            return new ParseResult(remainder);
        }

        protected ParseResult WasSuccess(object parsed, string remainder)
        {
            if (fn == null)
                return new ParseResult(parsed, remainder);
            else
                return new ParseResult(fn(parsed), remainder);
        }


        private Func<object, object> fn;
        public Parser Apply(Func<object, object> fn)
        {
            this.fn = fn;
            return this;
        }

        public Parser Apply6(Func<object, object, object, object, object, object, object> fn)
        {
            this.Apply(l =>
            {
                var asList = l as List<object>;
                if (asList == null) throw new Exception("Was expecting a list");
                if (asList.Count() != 6) throw new Exception($"{asList.Count()} values were parsed,  not 6");
                return fn(asList[0], asList[1], asList[2], asList[3], asList[4], asList[5]);
            });
            return this;
        }

        public Parser Apply4(Func<object, object, object, object, object> fn)
        {
            this.Apply(l =>
            {
                var asList = l as List<object>;
                if (asList == null) throw new Exception("Was expecting a list");
                if (asList.Count() != 4) throw new Exception($"{asList.Count()} values were parsed,  not 4");
                return fn(asList[0], asList[1], asList[2], asList[3]);
            });
            return this;
        }

        public Parser Apply3(Func<object, object, object, object> fn)
        {
            this.Apply(l =>
            {
                var asList = l as List<object>;
                if (asList == null) throw new Exception("Was expecting a list");
                if (asList.Count() != 3) throw new Exception($"{asList.Count()} values were parsed,  not 3");
                return fn(asList[0], asList[1], asList[2]);
            });
            return this;
        }

        public Parser Apply2(Func<object, object, object> fn)
        {
            this.Apply(l =>
            {
                var asList = l as List<object>;
                if (asList == null) throw new Exception("Was expecting a list");
                if (asList.Count() != 2) throw new Exception($"{asList.Count()} values were parsed,  not 2");
                return fn(asList[0], asList[1]);
            });
            return this;
        }


        public Parser ApplyString(Func<string, object> fn)
        {
            this.Apply(l =>
            {
                var asList = l as string;
                if (asList == null) throw new Exception("Was expecting a string");
                return fn(asList);
            });

            return this;
        }


        public static Parser operator +(Parser p1, Parser p2)
        {
            return new Then(p1, p2);
        }

        public static Parser operator |(Parser p1, Parser p2)
        {
            return new Either(p1, p2);
        }

    }
}

using System;

namespace Graphy0_0_2.Parser.Exceptions
{
    public class UnableToParseException : Exception
    {
        public UnableToParseException() : base("Nie udało się skonwertować tekstu na obiekty.")
        {

        }
    }
}

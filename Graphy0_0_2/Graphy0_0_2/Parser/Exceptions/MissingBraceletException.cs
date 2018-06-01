using System;

namespace Graphy0_0_2.Parser.Exceptions
{
    public class MissingBraceletException : Exception
    {
        public MissingBraceletException() : base("Liczba nawiasów otwierających i zamykających nie jest równa") { }
    }
}

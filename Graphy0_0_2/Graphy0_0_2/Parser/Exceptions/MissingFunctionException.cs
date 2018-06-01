using System;

namespace Graphy0_0_2.Parser.Exceptions
{
    public class MissingFunctionException : Exception
    {
        public MissingFunctionException(string name) : base(String.Format("Funkcja o podanej nazwie {0} nie istnieje", name)) { }
    }
}

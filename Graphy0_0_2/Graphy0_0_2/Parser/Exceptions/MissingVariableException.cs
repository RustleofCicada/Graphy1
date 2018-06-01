using System;

namespace Graphy0_0_2.Parser.Exceptions
{
    public class MissingVariableException : Exception
    {
        public MissingVariableException(string name) : base(String.Format("Zmienna matematyczna o nazwie {0} nie figuruje w słowniku", name)) { }
    }
}

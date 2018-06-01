using System;
using System.Collections.Generic;

namespace Graphy0_0_2.Parser.Calculable
{
    /// <summary>
    /// Funkcja matematyczna
    /// </summary>
    public class Function : ICalculable
    {
        /// <summary>
        /// Wynik funkcji na podstawie jej argumentów
        /// </summary>
        public double Value
        {
            get { return Calculate(); }
        }
        /// <summary>
        /// Rodzaj funkcji
        /// </summary>
        public FunctionType Type { get; private set; }
        /// <summary>
        /// Lista argumentów funkcji
        /// <para>Przykład: f(arg1;arg2)</para>
        /// </summary>
        List<ICalculable> Arguments = new List<ICalculable>();
        /// <summary>
        /// Zapis matematyczny funkcji
        /// </summary>
        public string ShortName { get; private set; }
        /// <summary>
        /// Pełna nazwa funkcji
        /// </summary>
        public string LongName { get; private set; }

        /// <summary>
        /// Tworzy nową funkcję matematyczną
        /// </summary>
        /// <param name="type">Rodzaj funkcji</param>
        /// <param name="arguments">Lista argumentów funkcji</param>
        public Function(FunctionType type, List<ICalculable> arguments)
        {
            Type = type;
            Arguments = arguments;
            ShortName = type.ToShortString();
            LongName = type.ToLongString();
        }

        /// <summary>
        /// Oblicza wynik funkcji na podstawie jej argumentów
        /// </summary>
        private double Calculate()
        {
            switch (Type)
            {
                case FunctionType.Pow:
                    return Math.Pow(Arguments[0].Value, Arguments[1].Value);

                case FunctionType.Root:
                    return Math.Pow(Arguments[0].Value, 1 / Arguments[1].Value);
                
                case FunctionType.Sinus:
                    return Math.Sin(Arguments[0].Value);
                    
                case FunctionType.Cosinus:
                    return Math.Cos(Arguments[0].Value);
                    
                case FunctionType.Tangens:
                    return Math.Tan(Arguments[0].Value);
                    
                case FunctionType.Cotangens:
                    return -Math.Tan(Arguments[0].Value + (Math.PI / 2));
                    
                case FunctionType.Logarithm:
                    if (Arguments.Count == 1)
                        return Math.Log10(Arguments[0].Value);
                    else if (Arguments.Count == 2)
                        return Math.Log(Arguments[1].Value, Arguments[0].Value);
                    else return 0;
                    
                case FunctionType.NaturalLog:
                    return Math.Log(Arguments[0].Value, Math.E);
                    
                case FunctionType.Absolute:
                    return Math.Abs(Arguments[0].Value);

                case FunctionType.Signum:
                    return Math.Sign(Arguments[0].Value);
                    
                case FunctionType.Minimum:
                    return Math.Min(Arguments[0].Value, Arguments[1].Value);
                    
                case FunctionType.Maximum:
                    return Math.Max(Arguments[0].Value, Arguments[1].Value);
                    
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Zwraca funkcję w postaci tekstu
        /// </summary>
        public override string ToString()
        {
            string toReturn = ShortName + "(" + Arguments[0].ToString();

            for (int i = 1; i < Arguments.Count; i++)
            {
                //jeżeli to nie ostatni argument, dodaj średnik
                if (i != Arguments.Count) toReturn += ";";

                toReturn += Arguments[i].ToString();
            }
            toReturn += ")";

            return toReturn;
        }
        /// <summary>
        /// Zwraca funkcję w postaci znaczników
        /// </summary>
        public List<string> ToMEML()
        {
                                              //znacznik otwierający
            var toReturn = new List<string>() { "<Function Name=\"" + LongName + "\">" };
            //ciało znacznika
            foreach (ICalculable argument in Arguments)
            {
                toReturn.AddRange(argument.ToMEML().AddInsertion(2));
            }
            //znacznik zamykający
            toReturn.Add("</Function>");

            return toReturn;
        }
    }
}

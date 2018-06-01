using System;
using System.Collections.Generic;

namespace Graphy0_0_2.Parser.Calculable
{
    /// <summary>Działanie matematyczne</summary>
    public class Operation : ICalculable
    {
        /// <summary>Wynik działania matematycznego</summary>
        public double Value
        {
            get { return Calculate(); }
        }
        /// <summary>
        /// Rodzaj działania matematycznego
        /// </summary>
        public OperationType Type { get; private set; }
        /// <summary>
        /// Lista argumentów działania matematycznego
        /// <para>Przykład: arg1 + arg2 + arg3</para>
        /// </summary>
        public List<ICalculable> Arguments = new List<ICalculable>();

        /// <summary>
        /// Tworzy nowy obiekt działania matematycznego
        /// </summary>
        /// <param name="type">Rodzaj działania</param>
        /// <param name="arguments">Lista argumentów działania potrzebnych do jego obliczenia</param>
        public Operation(OperationType type, List<ICalculable> arguments)
        {
            Type = type;
            Arguments = arguments;
        }

        /// <summary>
        /// Oblicza wynik działania na podstawie jego argumentów
        /// </summary>
        private double Calculate()
        {
            double toReturn = Arguments[0].Value;
            for (int i = 1; i < Arguments.Count; i++)
            {
                switch (Type)
                {
                    case OperationType.Addition:
                        toReturn += Arguments[i].Value;
                        break;
                    case OperationType.Substraction:
                        toReturn -= Arguments[i].Value;
                        break;
                    case OperationType.Multiplication:
                        toReturn *= Arguments[i].Value;
                        break;
                    case OperationType.Division:
                        toReturn /= Arguments[i].Value;
                        break;
                    case OperationType.Exponentiation:
                        toReturn = Math.Pow(toReturn, Arguments[i].Value);
                        break;
                }
            }
            return toReturn;
        }

        /// <summary>
        /// Zwraca działanie w postaci tekstu
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //znak działania, przykład: +, -, *
            char OperationKey = Type.ToChar();
            string toReturn = "";

            for (int i = 0; i < Arguments.Count; i++)
            {
                //sprawdza czy należy postawić nawiasy
                bool needsBracelet = (Arguments[i].GetType() == typeof(Operation) &&
                                     (Arguments[i] as Operation).Type != Type);
                
                if (i != 0) toReturn += OperationKey;

                if (needsBracelet)
                    toReturn += "(";

                toReturn += Arguments[i].ToString();

                if (needsBracelet)
                    toReturn += ")";
            }

            return toReturn;
        }
        /// <summary>
        /// Zwraca działanie zapisane w postaci znaczników
        /// </summary>
        public List<string> ToMEML()
        {
                                              //znacznik otwierający
            var toReturn = new List<string>() { "<Operation Type=\"" + Type.ToString() + "\">" };
            //ciało znacznika
            foreach (ICalculable argument in Arguments)
            {
                toReturn.AddRange(argument.ToMEML().AddInsertion(2));
            }
            //znacznik zamykający
            toReturn.Add("</Operation>");

            return toReturn;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphy0_0_2.Parser.Calculable
{
    /// <summary>
    /// Liczba (double) w formie obiektu ICalculable
    /// </summary>
    public class Number : ICalculable
    {
        /// <summary>Wartość liczbowa</summary>
        public double Value { get; }

        /// <summary>
        /// Tworzy liczbę na podstawie podanego argumentu
        /// </summary>
        /// <param name="value">Wartość liczby</param>
        public Number(double value)
        {
            Value = value;
        }

        /// <summary>
        /// Zwraca liczbę w postaci tekstu
        /// </summary>
        public override string ToString()
        {
            return Value.ToMathString();
        }
        /// <summary>
        /// Zwraca liczbę w postaci znacznika
        /// </summary>
        public List<string> ToMEML()
        {
            return new List<string>() { "<Number Value=\"" + Value.ToString() + "\"/>" };
        }
    }
}

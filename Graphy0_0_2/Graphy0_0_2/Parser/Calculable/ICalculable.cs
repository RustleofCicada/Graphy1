using System.Collections.Generic;

namespace Graphy0_0_2.Parser.Calculable
{
    /// <summary> Obiekt ze zwracalną wartością liczbową</summary>
    public interface ICalculable
    {
        /// <summary>
        /// Wartość liczbowa obiektu
        /// </summary>
        double Value { get; }

        /// <summary>
        /// Zwraca obiekt w postaci tekstu
        /// </summary>
        /// <returns></returns>
        string ToString();
        /// <summary>
        /// Zwraca obiekt w postaci znaczników
        /// </summary>
        List<string> ToMEML();
    }
}

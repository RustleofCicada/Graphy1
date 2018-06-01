using System.Collections.Generic;
using System.Linq;

using Graphy0_0_2.Parser.Exceptions;

namespace Graphy0_0_2.Parser.Calculable
{
    /// <summary>
    /// Zmienna matematyczna w postaci obiektu ICalculable
    /// <para>Może posiadać mnożnik który wpływa na jej wartość</para>
    /// </summary>
    public class Variable : ICalculable
    {
        /// <summary>
        /// Wartość liczbowa zmiennej
        /// <para>Jeżeli zmienna posiada mnożnik, jej wartość jest przez niego pomnożona</para>
        /// <para>Przykład: x, 5y</para>
        /// </summary>
        public double Value
        {
            get { return VariableDictionary.Content[Name] * Multiplier; }
        }
        /// <summary>
        /// Mnożnik zmiennej
        /// </summary>
        public double Multiplier { get; private set; }
        /// <summary>
        /// Nazwa zmiennej
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Tworzy nową zmienną matematyczną
        /// </summary>
        /// <param name="name">Nazwa zmiennej</param>
        public Variable(string name)
        {
            Name = name;
            Multiplier = 1;

            CheckIfExists();
        }
        /// <summary>
        /// Tworzy nową zmienną matematyczną
        /// </summary>
        /// <param name="name">Nazwa zmiennej</param>
        /// <param name="multiplier">Mnożnik zmiennej</param>
        public Variable(string name, double multiplier)
        {
            Name = name;
            Multiplier = multiplier;

            CheckIfExists();
        }

        /// <summary>
        /// Metoda sprawdza czy zmienna o podanej nazwie istnieje, jeżeli nie zwracany jest wyjątek
        /// </summary>
        private void CheckIfExists()
        {
            if (!VariableDictionary.Content.Keys.Contains(Name))
                throw (new MissingVariableException(Name));
        }

        /// <summary>
        /// Zwraca zmienną w postaci tekstu
        /// </summary>
        public override string ToString()
        {
            return Name;
        }
        /// <summary>
        /// Zwraca zmienną w postaci znacznika
        /// </summary>
        public List<string> ToMEML()
        {
            if (Multiplier != 1) //jeżeli posiada mnożnik
                return new List<string>() { "<Variable Source=\"" + Name + "\" Multiplier=\"" + Multiplier + "\"/>" };
            else return new List<string>() { "<Variable Source=\"" + Name + "\"/>" };
        }
    }
}

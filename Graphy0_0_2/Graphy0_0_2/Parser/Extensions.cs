using System;
using System.Collections.Generic;
using System.Linq;

using Graphy0_0_2.Parser.Calculable;
using Graphy0_0_2.Parser.Exceptions;

namespace Graphy0_0_2.Parser
{
    public static class Extensions
    {
        /// <summary>
        /// Zwraca liczbę w postaci tekstu
        /// </summary>
        public static string ToMathString(this double value)
        {
            //dla funkcji trygonometrycznych istnieje błąd, sin(pi) nie jest równe 0, tylko liczbie poniżej
            //warunek ten koryguje tą nieprawidłowość
            if (Math.Abs(value) <= 0.000000000000000122460635382238)
                return "0";
            switch (value)
            {
                //jeżeli liczba posiada swój własny zapis matematyczny, zwraca go
                case Math.PI:
                    return "pi";
                case Math.E:
                    return "e";
                default:
                    return Convert.ToString(value);
            }
        }

        /// <summary>
        /// Sprawdza czy tekst zawiera jakikolwiek z podanych znaków z tablicy 
        /// </summary>
        /// <param name="chars">Tablica znaków do odnalezienia</param>
        public static bool ContainsAny(this string str, char[] chars)
        {
            foreach (char ch in str)
            {
                if (str.Contains(ch))
                    return true;
            }
            return false;
        }
        public static double ToDouble(this string str)
        {
            if (str == "pi")
                return Math.PI;
            else if (str == "e")
                return Math.E;
            else if (str == "")
                return 0;
            else return Convert.ToDouble(str);
        }
        /// <summary>
        /// Zwraca tekst w formie typu działania matematycznego
        /// </summary>
        public static OperationType ToOperationType(this string str)
        {
            switch (str)
            {
                case "Addition":
                    return OperationType.Addition;
                case "Substraction":
                    return OperationType.Substraction;
                case "Multiplication":
                    return OperationType.Multiplication;
                case "Division":
                    return OperationType.Division;
                case "Exponentiation":
                    return OperationType.Exponentiation;
                default:
                    return OperationType.Addition;
            }
        }
        /// <summary>
        /// Zwraca znak w formie typu działania matematycznego
        /// </summary>
        public static OperationType ToOperationType(this char key)
        {
            switch (key)
            {
                case '+':
                    return OperationType.Addition;
                case '-':
                    return OperationType.Substraction;
                case '*':
                    return OperationType.Multiplication;
                case '/':
                    return OperationType.Division;
                case '^':
                    return OperationType.Exponentiation;
                default:
                    return OperationType.Addition;
            }
        }
        /// <summary>
        /// Zwraca tekst w formie typu funkcji matematycznej
        /// </summary>
        public static FunctionType ToFunctionType(this string source)
        {
            switch (source)
            {
                case "Power":
                case "pow":
                    return FunctionType.Pow;
                case "Root":
                case "root":
                    return FunctionType.Root;
                case "Sqrt":
                case "sqrt":
                    return FunctionType.Sqrt;
                case "Sinus":
                case "sin":
                    return FunctionType.Sinus;
                case "Cosinus":
                case "cos":
                    return FunctionType.Cosinus;
                case "Tangens":
                case "tg":
                    return FunctionType.Tangens;
                case "Cotangens":
                case "ctg":
                    return FunctionType.Cotangens;
                case "Logartihm":
                case "log":
                    return FunctionType.Logarithm;
                case "NaturalLog":
                case "ln":
                    return FunctionType.NaturalLog;
                case "Absolute":
                case "abs":
                    return FunctionType.Absolute;
                case "Signum":
                case "sgn":
                    return FunctionType.Signum;
                case "Minimum":
                case "min":
                    return FunctionType.Minimum;
                case "Maximum":
                case "max":
                    return FunctionType.Minimum;
                default:
                    throw (new MissingFunctionException(source));
            }
        }

        /// <summary>
        /// Zwraca zapis matematyczny funkcji
        /// </summary>
        public static string ToShortString(this FunctionType type)
        {
            switch (type)
            {
                case FunctionType.Pow:
                    return "pow";
                case FunctionType.Root:
                    return "root";
                case FunctionType.Sqrt:
                    return "sqrt";
                case FunctionType.Sinus:
                    return "sin";
                case FunctionType.Cosinus:
                    return "cos";
                case FunctionType.Tangens:
                    return "tg";
                case FunctionType.Cotangens:
                    return "ctg";
                case FunctionType.Logarithm:
                    return "log";
                case FunctionType.NaturalLog:
                    return "ln";
                case FunctionType.Absolute:
                    return "abs";
                case FunctionType.Signum:
                    return "sgn";
                case FunctionType.Minimum:
                    return "min";
                case FunctionType.Maximum:
                    return "max";
                default:
                    return "f";
            }
        }
        /// <summary>
        /// Zwraca pełną nazwę funkcji
        /// </summary>
        public static string ToLongString(this FunctionType type)
        {
            switch (type)
            {
                case FunctionType.Pow:
                    return "Power";
                case FunctionType.Root:
                    return "Root";
                case FunctionType.Sqrt:
                    return "Sqrt";
                case FunctionType.Sinus:
                    return "Sinus";
                case FunctionType.Cosinus:
                    return "Cosinus";
                case FunctionType.Tangens:
                    return "Tangens";
                case FunctionType.Cotangens:
                    return "Cotangens";
                case FunctionType.Logarithm:
                    return "Logarithm";
                case FunctionType.NaturalLog:
                    return "NaturalLog";
                case FunctionType.Absolute:
                    return "Absolute";
                case FunctionType.Signum:
                    return "Signum";
                case FunctionType.Minimum:
                    return "Minimum";
                case FunctionType.Maximum:
                    return "Maximum";
                default:
                    return "Undefined";
            }
        }

        /// <summary>
        /// Zwraca znak działania
        /// </summary>
        public static char ToChar(this OperationType type)
        {
            switch (type)
            {
                case OperationType.Addition:
                    return '+';
                case OperationType.Substraction:
                    return '-';
                case OperationType.Multiplication:
                    return '*';
                case OperationType.Division:
                    return '/';
                case OperationType.Exponentiation:
                    return '^';
                default:
                    return '#';
            }
        }
        /// <summary>
        /// Zwraca pełną nazwę działania
        /// </summary>
        public static string ToString(this OperationType type)
        {
            switch (type)
            {
                case OperationType.Addition:
                    return "Addition";
                case OperationType.Substraction:
                    return "Substraction";
                case OperationType.Multiplication:
                    return "Multiplication";
                case OperationType.Division:
                    return "Division";
                case OperationType.Exponentiation:
                    return "Exponentiation";
                default:
                    return "Undefined";
            }
        }
        
        /// <summary>
        /// Dodaje wcięcie do każego elementu listy
        /// </summary>
        /// <param name="list">Lista do modyfikacji</param>
        /// <param name="spacing">Wielkość wcięcia</param>
        public static List<string> AddInsertion(this List<string> list, int spacing)
        {
            var toReturn = list;
            string insertion = "";
            for (int i = 0; i < spacing; i++)
            {
                insertion += " ";
            }
            for (int i = 0; i < toReturn.Count; i++)
            {
                toReturn[i] = insertion + toReturn[i];
            }
            return toReturn;
        }
    }
}

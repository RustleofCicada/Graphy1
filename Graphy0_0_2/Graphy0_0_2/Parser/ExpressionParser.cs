using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Graphy0_0_2.Parser.Calculable;
using Graphy0_0_2.Parser.Exceptions;

namespace Graphy0_0_2.Parser
{
    public static class ExpressionParser
    {
        /// <summary>
        /// Parsuje tekst na obiekt ICalculable
        /// </summary>
        /// <param name="toParse">Tekst to sparsowania</param>
        public static ICalculable FromText(string toParse)
        {
            //string na którym będzie odbywać się parsowanie
            string Operation = RemoveOuterBracelets(toParse);
            //tablica znaków działania matematycznego
            char[] operators = { '+', '-', '*', '/', '^' };
            //znak o najniższym priorytecie w tekście
            char choosenOperator = FindLowestOperator(Operation, operators);
            
            #region Jest działaniem matematycznym
            if (choosenOperator != '#')
            {
                //aktualnie pisany fragment pomiędzy znakami [choosenOperator]
                string currentFragment = "";
                //fragmenty tekstu które będą później argumentami działania
                var splitted = new List<string>();
                int nesting = 0; //zagnieżdżenie

                //wycinanie fragmentów
                for (int i = 0; i < Operation.Length; i++)
                {
                    if (Operation[i] == '(')
                        nesting++;
                    else if (Operation[i] == ')')
                        nesting--;

                    if (Operation[i] == choosenOperator &&
                        nesting == 0)
                    {
                        splitted.Add(currentFragment);
                        currentFragment = "";
                    }
                    else
                        currentFragment += Operation[i];
                }
                splitted.Add(currentFragment);

                return new Operation(choosenOperator.ToOperationType(), FromList(splitted));
            }
            #endregion
            #region Jest funkcją
            else if (Operation.Contains('(') &&
                     Operation.Contains(')'))
            {
                //fragment tekstu zawierający nazwę funkcji
                string typeString = Operation.Substring(0, Operation.IndexOf('('));
                //fragment tekstu zawierający argumenty
                string argumentsString = Operation.Substring(Operation.IndexOf('(') + 1,
                                                            Operation.Length - Operation.IndexOf('(') - 2);
                //argumenty jako fragmenty teksu
                List<string> arguments = argumentsString.Split(';').ToList();

                return new Function(typeString.ToFunctionType(), FromList(arguments));
            }
            #endregion
            else
            {
                try
                {
                    return new Number(Operation.ToDouble());
                }
                catch (System.FormatException)
                {
                    return new Variable(Operation);
                }
                catch
                {
                    throw (new UnableToParseException());
                }
            }
        }
        /// <summary>
        /// Parsuje każdy ze stringów listy i zwraca w postaci listy argumentów
        /// </summary>
        /// <param name="list">Lista do parsowania</param>
        public static List<ICalculable> FromList(List<string> list)
        {
            var toRetrun = new List<ICalculable>();
            foreach (string str in list)
            {
                toRetrun.Add(FromText(str));
            }
            return toRetrun;
        }

        /// <summary>
        /// Zwraca tekst o uciętych zewnętrznych nawiasach, jeżeli owe istnieją
        /// </summary>
        /// <param name="str">Tekst do przycięcia</param>
        static string RemoveOuterBracelets(string str)
        {
            if (str[0] == '(') //jeżeli rozpoczyna się od nawiasu
            {
                int nesting = 0; //ilość otwartych nawiasów
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] == '(')
                        nesting++;

                    else if (str[i] == ')')
                    {
                        nesting--;
                        if (nesting == 0) //jeżeli zamknięto już wszystkie nawiasy
                        {
                            if (i == str.Length - 1) //i jest to ostatni znak
                                return str.Substring(1, str.Length - 2); //utnij nawiasy
                            else return str; //jeśli nie, zwróć bez zmian
                        }
                    }
                }
                throw (new MissingBraceletException());
            }
            else //jeśli nie rozpoczyna się od nawiasu, zwróć bez zmian
                return str;
        }

        /// <summary>
        /// Zwraca operator o najniższym priorytecie w tekście
        /// </summary>
        /// <param name="str">Tekst to przeszukania</param>
        /// <param name="operators">Tablica operatorów</param>
        static char FindLowestOperator(string str, char[] operators)
        {
            int step = 0; //indeks aktualnie sprawdzanego operatora
            int nesting = 0; //ilość otwartych nawiasów

            do
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] == '(')
                        nesting++;
                    else if (str[i] == ')')
                        nesting--;

                    //jeżeli znaleziony operator nie znajduje się w nawiasie
                    if (nesting == 0 &&
                        str[i] == operators[step])
                        return operators[step];

                }
                step++;
            } while (step < operators.Length);

            return '#';
        }
    }
}

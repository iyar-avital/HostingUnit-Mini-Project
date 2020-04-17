using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Utilities
{
    public static class Functions
    {




        private static readonly Regex numericRegex = new Regex("^[0-9]+$");
        private static readonly Regex hebrewRegex = new Regex("^[א-ת\\ \\-]+$");

        

        public static bool IsNumber(this string str)
        {
            /*if (!numericRegex.IsMatch(str))
                throw new ExceptionMessage();
            */

            return (numericRegex.IsMatch(str));
        }

        public static bool IsHebrew(this string str)
        {
            return hebrewRegex.IsMatch(str);
        }


        public static bool IsEmail(this string emailAddress)
        {
            var regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            bool isValid = Regex.IsMatch(emailAddress, regex, RegexOptions.IgnoreCase);
            return isValid;
        }


        public static List<M> DictionaryToListItems<T, M>(this Dictionary<T, List<M>> dict)
        {
            List<M> myList = new List<M>();
            foreach (var l in dict)
            {
                foreach (var item in l.Value)
                {
                    myList.Add(item);
                }
            }
            return myList;
        }


        public static T[] Flatten<T>(this T[,] arr)
        {
            int rows = arr.GetLength(0);
            int columns = arr.GetLength(1);
            T[] arrFlattened = new T[rows * columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    var test = arr[i, j];
                    arrFlattened[i * rows + j] = arr[i, j];
                }
            }
            return arrFlattened;
        }

        public static T[,] Expand<T>(this T[] arr, int rows)
        {
            int length = arr.GetLength(0);
            int columns = length / rows;
            T[,] arrExpanded = new T[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    arrExpanded[i, j] = arr[i * rows + j];
                }
            }
            return arrExpanded;
        }
    }
}

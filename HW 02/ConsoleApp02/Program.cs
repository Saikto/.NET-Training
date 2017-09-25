using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp02
{
    class Program
    {
        //Prints list
        static void PrintList<T>(List<T> list)
        {
            foreach (var t in list)
            {
                int width = Console.WindowWidth;
                Console.WriteLine(t.ToString().PadLeft(width - 1));
            }
        }
        //Prints double values from list with 2 symbols after . separator. Example 5.75
        static void PrintListSpecialDouble(List<double> list)
        {
            string pattern = "([0-9]+)(\\.)([0-9]{2}\\z)";
            Regex reg = new Regex(pattern);
            foreach (double t in list)
            {
                if (reg.IsMatch(t.ToString()))
                {
                    int width = Console.WindowWidth;
                    Console.WriteLine(t.ToString().PadLeft(width - 1));
                }
            }
        }
        //Returns average value from list of integers
        static double IntegerAverage(List<int> list)
        {
            double sum = 0;
            foreach (int n in list)
            {
                sum = sum + n;
            }
            return sum / list.Count;
        }
        //Returns average value from list of doubles with 2 symbols after .
        static double SpecialDoubleAverage(List<double> list)
        {
            string pattern = "([0-9]+)(\\.)([0-9]{2}\\z)";
            Regex reg = new Regex(pattern);
            int counter = 0;
            double sum = 0;
            foreach (double n in list)
            {
                if (reg.IsMatch(n.ToString()))
                {
                    sum = sum + n;
                    counter++;
                }
            }
            return sum / counter;
        }

        static void Main(string[] args)
        {
            List<string> inputList = new List<string>();
            Console.WriteLine("Input your strings. Use \".\" in real numbers. To stop input type \"\\exit\\\". ");
            for (;;)
            {
                var tempStr = Console.ReadLine();
                if (tempStr != null && tempStr.Equals("\\exit\\"))
                {
                    break;
                }
                else inputList.Add(tempStr);
            }
            List<int> integers = new List<int>();
            List<double> doubles = new List<double>();
            List<string> strings = new List<string>();
            string pattern = "([0-9]+)(\\,)([0-9]+)";
            Regex reg = new Regex(pattern);
            //Resorts members of inputlist to 3 lists: integers, doubles, strings
            foreach (string str in inputList)
            {
                if (reg.IsMatch(str))
                {
                    bool b = (Double.TryParse(reg.Replace(str, "$1.$3"), out double j));
                    if (b)
                        doubles.Add(j);
                }
                else if (Int32.TryParse(str, out int i))
                {
                    integers.Add(i);
                    continue;
                }
                else if (Double.TryParse(str, out double d))
                {
                    doubles.Add(d);
                }
                else strings.Add(str);
            }
            string coutInt = $"---- INTEGERS COUNT: {integers.Count}";
            string coutDouble = $"---- DOUBLES COUNT: {doubles.Count}";
            string coutString = $"---- STRINGS COUNT: {strings.Count}";
            //Integers
            Console.WriteLine(coutInt + new string('-', Console.WindowWidth - 1 - coutInt.Length));
            PrintList(integers);
            Console.WriteLine(("Average: " + IntegerAverage(integers)).PadLeft(Console.WindowWidth - 1));
            //Doubles
            Console.WriteLine(coutDouble + new string('-', Console.WindowWidth - 1 - coutDouble.Length));
            PrintListSpecialDouble(doubles);
            Console.WriteLine(("Average (speial): " + SpecialDoubleAverage(doubles)).PadLeft(Console.WindowWidth - 1));
            //Strings
            Console.WriteLine(coutString + new string('-', Console.WindowWidth - 1 - coutString.Length));
            var sortedStrings = strings.OrderBy(item => item).ToList();
            PrintList(sortedStrings);
        }
    }
}

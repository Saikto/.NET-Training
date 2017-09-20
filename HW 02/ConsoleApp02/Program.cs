using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp02
{
    class Program
    {
        static void PrintList<T>(List<T> list)
        {
            foreach (var t in list)
            {
                int width = Console.WindowWidth;
                Console.WriteLine(t.ToString().PadLeft(width - 1));
            }
        }

        static double IntegerAverage(List<int> list)
        {
            double sum = 0;
            foreach (int n in list)
            {
                sum = sum + n;
            }
            return sum / list.Count;
        }

        static void ClearStringsUp(List<string> list)
        {
            List<int> stringsToDelete = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Equals(" ") || list[i].Equals(""))
                {
                    stringsToDelete.Add(i);
                }
            }
            stringsToDelete.Reverse();
            foreach (var index in stringsToDelete)
            {
                list.RemoveAt(index);
            }
        }

        static void Main(string[] args)
        {
            List<string> inputList = new List<string>();
            Console.WriteLine("Input your strings. To stop input type \"exit\". ");
            for (;;)
            {
                var tempStr = Console.ReadLine();
                if (tempStr != null && tempStr.Equals("exit"))
                {
                    break;
                }
                else inputList.Add(tempStr);
            }
            string outString = "";
            foreach (string str in inputList)
            {
                outString = outString + " " + str;
            }
            string[] outStringsArray = outString.Split();
            List<int> integers = new List<int>();
            List<double> doubles = new List<double>();
            List<string> strings = new List<string>();
            foreach (string str in outStringsArray)
            {
                if (Int32.TryParse(str, out int i))
                {
                    integers.Add(i);
                    continue;
                }
                else if (Double.TryParse(str, out double j))
                {
                    doubles.Add(j);
                }
                else strings.Add(str);
            }
            Console.WriteLine($"---- INTEGERS COUNT: {integers.Count}" );
            PrintList(integers);
            Console.WriteLine(("Average: " + IntegerAverage(integers)).PadLeft(Console.WindowWidth - 1));
            Console.WriteLine($"---- DOUBLES COUNT: {doubles.Count}");
            PrintList(doubles);
            ClearStringsUp(strings);
            Console.WriteLine($"---- STRINGS COUNT: {strings.Count}");
            PrintList(strings);
        }
    }
}

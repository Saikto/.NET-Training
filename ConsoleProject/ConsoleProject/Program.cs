using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = 0;
            var b = 0;
            bool flagForA = true;
            bool flagForB = true;
            if (!Int32.TryParse(Console.ReadLine(), out a))
            {
                flagForA = false;
                Console.WriteLine("First value is not a number!");
            }
            if (!Int32.TryParse(Console.ReadLine(), out b))
            {
                flagForB = false;
                Console.WriteLine("Second value is not a number!");
            }
            if (flagForA && flagForB)
            {
                Console.WriteLine(a+b);
            }
        }
    }
}

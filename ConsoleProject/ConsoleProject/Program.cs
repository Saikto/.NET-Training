using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryProject;

namespace ConsoleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 0;
            int b = 0;
            bool flagForA = true;
            bool flagForB = true;
            Console.WriteLine("Enter 2 numbers:");
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
                Console.Write("Sum: ");
                Console.WriteLine(ArifmeticOperations.NumbersSum(a, b));
                Console.Write("Substraction: ");
                Console.WriteLine(ArifmeticOperations.NumbersSubtraction(a, b));
                Console.Write("Division: ");
                Console.WriteLine(ArifmeticOperations.NumbersDivision(a, b));
                Console.Write("Multiplication: ");
                Console.WriteLine(ArifmeticOperations.NumbersMultiplication(a, b));
            }
        }
    }
}

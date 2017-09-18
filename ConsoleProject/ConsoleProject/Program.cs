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
            Console.WriteLine("Enter 2 numbers:");
            if ((Int32.TryParse(Console.ReadLine(), out int a)) && (Int32.TryParse(Console.ReadLine(), out int b)))
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
            else
            {
                 Console.WriteLine("One(both) values are not number(s)");
            }
        }
    }
}

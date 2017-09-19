using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryProject;
using System.Configuration;
using System.Resources;
using System.IO;

namespace ConsoleProject
{
    class Program
    {
        public static void LibraryPrint(int a, int b)
        {
            Console.WriteLine("Using library..");
            Console.Write("Sum: ");
            Console.WriteLine(ArifmeticOperations.NumbersSum(a, b));
            Console.Write("Substraction: ");
            Console.WriteLine(ArifmeticOperations.NumbersSubtraction(a, b));
            Console.Write("Division: ");
            Console.WriteLine(ArifmeticOperations.NumbersDivision(a, b));
            Console.Write("Multiplication: ");
            Console.WriteLine(ArifmeticOperations.NumbersMultiplication(a, b));
        }

        public static void StraightPrint(int a, int b)
        {
            Console.WriteLine($"Not using library.. \nSum: {a + b}");
        }

        static void Main(string[] args)
        {
            int a = 0, b = 0;
            bool flag = true;
            //Console or Resource file choice
            if (ConfigurationManager.AppSettings.Get("ConsoleInput").Equals("true")) 
            {
                //Console input
                Console.WriteLine("Enter 2 numbers:");
                flag = Int32.TryParse(Console.ReadLine(), out a);
                flag = Int32.TryParse(Console.ReadLine(), out b);
            }
            else
            {
                //Resource file input
                flag = Int32.TryParse(ConsoleProjectResources.a, out a);
                flag = Int32.TryParse(ConsoleProjectResources.b, out b);
                Console.WriteLine($"Using parameters {a} , {b}..");
            }
            //If input values are numbers
            if (flag)
            {
                //Print choice
                if (ConfigurationManager.AppSettings.Get("Library").Equals("true"))
                {
                    LibraryPrint(a, b);
                }
                else
                {
                    StraightPrint(a, b);
                }
            }
            else
            {
                Console.WriteLine("One(both) values are not number(s)");
            }
        }
    }
}

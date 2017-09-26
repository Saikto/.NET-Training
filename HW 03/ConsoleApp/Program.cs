using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using EquationsAndMatrixLibrary;

namespace ConsoleApp
{
    class Program
    {
        static string TypeInput()
        {
            int choice = 0;
            Console.WriteLine("Choose equation type [1]/[2]\n" +
                              "[1] Linear\n" +
                              "[2] Quadratic");
            for (;;)
            {
                choice = 0;
                if (!Int32.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2))
                {
                    Console.WriteLine("No such type. Try again.");
                }
                else
                {
                    break;
                }
            }
            if(choice == 1)
                return "Linear";
            return "Quadratic";
        }

        static void PrintResult(double root)
        {
            //int result;
            //Console.WriteLine(!Int32.TryParse(root.ToString(), out result) ? $"Root:\nx={root}" : $"Root:\nx={result}");
            Console.Write($"Root:\nx = ");
            Console.WriteLine("{0:0.##}", root);

        }

        static void PrintResult(double[] roots)
        {
            Console.WriteLine("Roots:");
            for (int i = 0; i < roots.Length; i++)
            {
                Console.Write($"x{i + 1} = ");
                Console.WriteLine("{0:0.##}" , roots[i]);
            }
        }

        static double[] CoefficientsInput(string type)
        {
            var coeffs = new double[2];
            if (type.Equals("Quadratic"))
            {
                coeffs = new double[3];
                Console.WriteLine("Enter coefficients for the quadratic equation ax^2 + bx + c = 0");
                for (; ; )
                {
                    Console.Write("a = ");
                    if (!Double.TryParse(Console.ReadLine(), out coeffs[2]))
                    {
                        Console.WriteLine("Unacceptable coefficient. Try again.");
                    }
                    else break;
                }
            }
            else if(type.Equals("Linear"))
            {
                coeffs = new double[2];
                Console.WriteLine("Enter coefficients for the linear equation bx + c = 0");
            }
            for (;;)
            {
                Console.Write("b = ");
                if (!Double.TryParse(Console.ReadLine(), out coeffs[1]))
                {
                    Console.WriteLine("Unacceptable coefficient. Try again.");
                }
                else break;
            }
            for (;;)
            {
                Console.Write("c = ");
                if (!Double.TryParse(Console.ReadLine(), out coeffs[0]))
                {
                    Console.WriteLine("Unacceptable coefficient. Try again.");
                }
                else break;
            }
            return coeffs.Reverse().ToArray();
        }

        static void Main(string[] args)
        {
            double[] coeffs = CoefficientsInput(TypeInput());
            if (coeffs.Length == 2)
            {
                LinearEquation linearEquation = new LinearEquation(coeffs);
                PrintResult(linearEquation.Solve());
            }
            else if (coeffs.Length == 3)
            {
                QuadraticEquation quadraticEquation = new QuadraticEquation(coeffs);
                if (!quadraticEquation.AreComplexRoots())
                {
                    PrintResult(quadraticEquation.Solve());
                }
                else 
                    Console.WriteLine("There are no real roots.");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using EquationsAndMatrixLibrary;
using NLog;

namespace ConsoleApp
{
    class Program
    {
        private static Logger logger = LogManager.GetLogger("Equations");
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
                    string a = Console.ReadLine();
                    if (!Double.TryParse(a, out coeffs[2]))
                    {
                        Console.WriteLine("Unacceptable coefficient. Try again.");
                        logger.Log(LogLevel.Info, $"User entered unacceptable coefficient A: {a}");
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
                string b = Console.ReadLine();
                if (!Double.TryParse(b, out coeffs[1]))
                {
                    Console.WriteLine("Unacceptable coefficient. Try again.");
                    logger.Log(LogLevel.Info, $"User entered unacceptable coefficient B: {b}");
                }
                else break;
            }
            for (;;)
            {
                Console.Write("c = ");
                string c = Console.ReadLine();
                if (!Double.TryParse(c, out coeffs[0]))
                {
                    Console.WriteLine("Unacceptable coefficient. Try again.");
                    logger.Log(LogLevel.Info, $"User entered unacceptable coefficient C: {c}");
                }
                else break;
            }
            return coeffs.Reverse().ToArray();
        }

        static void Main(string[] args)
        {
            string type = "";
            type = TypeInput();
            double[] coeffs = CoefficientsInput(type);
            if (type.Equals("Linear"))
            {
                LinearEquation linearEquation = new LinearEquation(coeffs);
                double result = linearEquation.Solve();
                PrintResult(result);
                logger.Log(LogLevel.Info, $"Equation type: {type}| Root: {result}");
            }
            else if (type.Equals("Quadratic"))
            {
                QuadraticEquation quadraticEquation = new QuadraticEquation(coeffs);
                if (!quadraticEquation.AreComplexRoots())
                {
                    double[] result = quadraticEquation.Solve();
                    PrintResult(result);
                    logger.Log(LogLevel.Info, $"Equation type: {type}| Roots: {result[0]} , {result[1]}");
                }
                else
                {
                    Console.WriteLine("There are no real roots.");
                    logger.Log(LogLevel.Info, $"Equation type: {type}| No roots");
                }
            }
        }
    }
}

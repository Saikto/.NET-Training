using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using EquationsAndMatrixLibrary;
using NLog;
using System.Configuration;

namespace ConsoleApp
{
    class Program
    {
        //Logger instance
        private static Logger logger = LogManager.GetLogger("Equations");
        //Method to get equation type from console
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
        //Method to print roots of linear equation
        static void PrintResult(double root)
        {
            Console.Write($"Root:\nx = ");
            Console.WriteLine("{0:0.##}", root);

        }
        //Method to print roots of quadratic equation
        static void PrintResult(double[] roots)
        {
            Console.WriteLine("Roots:");
            for (int i = 0; i < roots.Length; i++)
            {
                Console.Write($"x{i + 1} = ");
                Console.WriteLine("{0:0.##}" , roots[i]);
            }
        }
        //Method to get coeffs from console
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
            //Matrixes//////////////////////////////////////////////////////////////////////////////
            string inputFilePath = ConfigurationManager.AppSettings.Get("homepath");
            StreamReader reader = new StreamReader(inputFilePath);
            List<int> endIndexes= new List<int>(2);
            int index = 0;
            while (endIndexes.Count != 2)
            {
                if (reader.ReadLine().Equals("end"))
                {
                    endIndexes.Add(index);
                }
                index++;
            }
            int n = 3, m = 4;
            Matrix X = new Matrix(n, m);
            Matrix Z = new Matrix(m, n);
            X.FillMatrixFromFile(inputFilePath, 0, endIndexes[0] - 1);
            Z.FillMatrixFromFile(inputFilePath, endIndexes[0] + 1, endIndexes[1] - 1);
            X.PrintMatrix();
            Z.PrintMatrix();
            Matrix A = new Matrix(n, n);
            try
            {
                A = X * Z;
                A.PrintMatrix();
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Can't multiply matrices of such dimensions.");
            }
            //Equations///////////////////////////////////////////////////////////////////////////////
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

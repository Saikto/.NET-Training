using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using FibonacciAndFactorialUtils;
using System.Numerics;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(Fibonacci.FibonacciNSimple(22));
            for (;;)
            {
                Console.WriteLine("[1] Fibonacci\n[2] Factorial");
                bool flag = true;
                flag = int.TryParse(Console.ReadLine(), out int c);
                if (flag && c == 1)
                {
                    for (;;)
                    {
                        Console.WriteLine("Enter N:");
                        BigInteger.TryParse(Console.ReadLine(), out BigInteger n);
                        Console.WriteLine("Result: " + Fibonacci.FibonacciNFast(n));
                        break;
                    }
                }else if (flag && c == 2)
                {
                    for (;;)
                    {
                        Console.WriteLine("Enter N:");
                        BigInteger.TryParse(Console.ReadLine(), out BigInteger n);
                        Console.WriteLine("Result: " + Factorial.Calculate(n));
                        break;
                    }
                }
                else if (c != 1 && c != 2)
                {
                    Console.WriteLine("No such function, try again");
                }
            }
        }
    }
}
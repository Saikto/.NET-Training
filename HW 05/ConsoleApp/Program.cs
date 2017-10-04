using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using FibonacciAndFactorialUtils;
using HomeworkUtils;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int rowLength = 200;
            List<BigInteger> fibonacciRow = new List<BigInteger>();
            for (int i = 0; i < rowLength; i++)
            {
                fibonacciRow.Add(Fibonacci.FibonacciNFast(i));
                Console.WriteLine(fibonacciRow[i]); //Prints row into console
            }

            //Prime numbers count
            
            //int primeCount = fibonacciRow.Count(t => t.IsPrime());
            //Console.WriteLine($"Prime count in first {rowLength} members of fib row: {primeCount}");
            
            //Console.WriteLine($"{fibonacciRow[73]}");           //Freeze from here
            //Console.WriteLine($"{ fibonacciRow[73].IsPrime()}");

            //Divisible by sum of numbers
            var divisible = fibonacciRow.Where(t => t.IsDivisible(t.DigitsSum())).ToList();
            Console.WriteLine($"Divisible by the sum of their numbers: {divisible.Count}");
            
            //Divisible by 5
            bool divisibleBy5 = fibonacciRow.Any(t => t.IsDivisible(5));
            Console.WriteLine($"Any divisible by 5: {divisibleBy5}");
            
            //Sorted fib row by second digit
            var sortedBySecondDigit = fibonacciRow.OrderByDescending(t => t.GetDigitOnN(2)).ToList();
            //foreach (BigInteger number in sortedBySecondDigit)
            //{
            //    Console.WriteLine(number);
            //}
            
            //Average count of zeros in number
            double averageZerosCount = fibonacciRow.Average(t => t.GetZerosCount());
            Console.WriteLine($"Average zeros count = {averageZerosCount}");

            //Max digits square sum
            BigInteger numberWithMaxDigitsSquareSum =
                fibonacciRow.OrderBy(t => t.GetDigitsSquareSum()).ToList().LastOrDefault();
            Console.WriteLine($"Number with maximum sum of it's digits squares: {numberWithMaxDigitsSquareSum}");

            //Sqrt of all numbers that contain "2"
            var sqrtList = fibonacciRow.Where(t => t.Contains(2)).Select(t => t.SqrtDown()).ToList();
            //foreach (BigInteger number in mas)
            //{
            //    Console.WriteLine(number);
            //}

            //Reterns list of element whose +-5 members adre divisible by 5
            var b = Utils.LeftRightMembersDevisibleBy5(fibonacciRow);
            //Returns list of last 2 digits of numbers in list b that are divisible by 3
            var a = b.Where(t => t.IsDivisible(3)).Select(t => t.GetLastDigits(2)).ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace FibonacciAndFactorialUtils
{
    public class Fibonacci
    {
        public static int FibonacciNSimple(int n)
        {
            return n > 1 ? FibonacciNSimple(n - 1) + FibonacciNSimple(n - 2) : n;
        }

        public static BigInteger FibonacciNFast(BigInteger n)
        {
            if (n <= 1)
                return n;

            BigInteger[,] result =
            {
                { 1, 0 },
                { 0, 1 }
            };
            BigInteger[,] fiboM =
            {
                { 1, 1 },
                { 1, 0 }
            };

            while (n > 0)
            {
                if (n % 2 == 1)
                    MatrixMultForFib(result, fiboM);
                n /= 2;
                MatrixMultForFib(fiboM, fiboM);
            }

            return result[1, 0];
        }

        private static void MatrixMultForFib(BigInteger[,] m, BigInteger[,] n)
        {
            BigInteger a = m[0, 0] * n[0, 0] + m[0, 1] * n[1, 0];
            BigInteger b = m[0, 0] * n[0, 1] + m[0, 1] * n[1, 1];
            BigInteger c = m[1, 0] * n[0, 0] + m[1, 1] * n[0, 1];
            BigInteger d = m[1, 0] * n[0, 1] + m[1, 1] * n[1, 1];

            m[0, 0] = a;
            m[0, 1] = b;
            m[1, 0] = c;
            m[1, 1] = d;
        }
    }
}

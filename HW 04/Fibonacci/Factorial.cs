using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FibonacciAndFactorialUtils
{
    public class Factorial
    {
        public static BigInteger Calculate(BigInteger n)
        {
            BigInteger result = 1; 
            for (int i = 1; i <= n; i++)
            {
                result = result * i;
            }
            return result;
        }
    }
}

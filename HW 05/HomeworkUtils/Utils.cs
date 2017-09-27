using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkUtils
{
    public static class IntExtension
    {
        public static bool IsPrime(this int n)
        {
            if (n < 2)
                return false;
            for (int i = 2; i <= Math.Sqrt(n); i++)
            {
                if (n % i == 0)
                    return false;
            }
            return true;
        }
    }

    public static class BigIntegerExtension
    {
        public static bool Contains(this BigInteger n, int digit)
        {
            if (n.ToString().Contains(digit.ToString()))
            {
                return true;
            }
            return false;
        }

        public static BigInteger SqrtDown(this BigInteger n)
        {
            //Using Newton Raphson method we calculate the
            //square root (N/g + g)/2
            BigInteger rootN = n;
            int count = 0;
            int bitLength = 1; // There is a bug in finding bit length hence we start with 1 not 0
            while (rootN / 2 != 0)
            {
                rootN /= 2;
                bitLength++;
            }
            bitLength = (bitLength + 1) / 2;
            rootN = n >> bitLength;

            BigInteger lastRoot = BigInteger.Zero;
            do
            {
                if (lastRoot > rootN)
                {
                    if (count++ > 1000)                   // Work around for the bug where it gets into an infinite loop
                    {
                        return rootN;
                    }
                }
                lastRoot = rootN;
                rootN = (BigInteger.Divide(n, rootN) + rootN) >> 1;
            }
            while (!((rootN ^ lastRoot).ToString() == "0"));
            return rootN;
        }

        public static long GetDigitsSquareSum(this BigInteger n)
        {
            long sum = 0;
            int digit;
            string nString = n.ToString();
            foreach (char str in nString)
            {
                sum += Int32.Parse(str.ToString()) * Int32.Parse(str.ToString());
            }
            return sum;
        }

        public static int GetZerosCount(this BigInteger n)
        {
            string nString = n.ToString();
            int count = 0;
            foreach (char str in nString)
            {
                if (str.Equals('0'))
                    count++;
            }
            return count;
        }

        public static int GetDigitOnN(this BigInteger n, int position)
        {
            int digit;
            try
            {
                Int32.TryParse(n.ToString()[position - 1].ToString(), out digit);
            }
            catch (IndexOutOfRangeException)
            {
                return 0;
            }

            return digit;
        }

        public static bool IsPrime(this BigInteger n)
        {
            if (n < 2)
                return false;
            for (int i = 2; i <= n.SqrtDown(); i++)
            {
                if (n % i == 0)
                    return false;
            }
            return true;
        }

        public static int DigitsSum(this BigInteger n)
        {
            int length = n.ToString().Length;
            int[] arr = new int[length];
            for (int i = 0; i < length; i++)
            {
                arr[i] = Int32.Parse(n.ToString()[i].ToString());
            }
            int sum = 0;
            foreach (int j in arr)
            {
                sum += j;
            }
            return sum;
        }

        public static bool IsDivisible(this BigInteger n, int divider)
        {
            if (divider == 0)
                return false;
            if (n % new BigInteger(divider) != 0)
            {
                return false;
            }
            return true;
        }
    }

    public class Utils
    {
        public static int TotalZerosInListInMembers(List<BigInteger> list)
        {
            int count = 0;
            foreach (BigInteger n in list)
            {
                string nString = n.ToString();
                foreach (char str in nString)
                {
                    if (str.Equals('0'))
                        count++;
                }
            }
            return count;
        }
    }
}

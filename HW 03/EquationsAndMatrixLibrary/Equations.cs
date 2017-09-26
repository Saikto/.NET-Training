using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationsAndMatrixLibrary
{
    public class QuadraticEquation
    {
        private double CoeffA { get; set; }
        private double CoeffB { get; set; }
        private double CoeffC { get; set; }
        private double[] Roots {get; set; }

        public QuadraticEquation(double[] coeffs)
        {
            CoeffA = coeffs[0];
            CoeffB = coeffs[1];
            CoeffC = coeffs[2];
            Roots = new double [2];
        }

        public bool AreComplexRoots()
        {
            if ((CoeffB * CoeffB - 4 * CoeffA * CoeffC) < 0)
            {
                return true;
            }
                return false;
        }

        public double[] Solve()
        {
            Roots[0] = (-1 * CoeffB + Math.Sqrt(CoeffB * CoeffB - 4 * CoeffA * CoeffC)) / (2 * CoeffA);
            Roots[1] = (-1 * CoeffB - Math.Sqrt(CoeffB * CoeffB - 4 * CoeffA * CoeffC)) / (2 * CoeffA);
            return Roots;
        }

    }

    public class LinearEquation
    {
        private double CoeffB { get; set; }
        private double CoeffC { get; set; }
        private double Root { get; set; }

        public LinearEquation(double[] coeffs)
        {
            CoeffB = coeffs[0];
            CoeffC = coeffs[1];
        }

        public  double Solve()
        {
            Root = -1 * CoeffB / CoeffC;
            return Root;
        }
    }
}

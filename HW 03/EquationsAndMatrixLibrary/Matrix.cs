using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationsAndMatrixLibrary
{
    public class Matrix
    {
        public int N { get; set; }
        public int M { get; set; }
        public double[,] content;

        public Matrix(int n, int m)
        {
            N = n;
            M = m;
            content = new double[N,M];
        }

        public void FillMatrixFromFile(string path, int startLineIndex, int endLineIndex)
        {
            StreamReader reader = new StreamReader(path);
            int currLineIndex = 0;
            while (currLineIndex != startLineIndex)
            {
                reader.ReadLine();
                currLineIndex++;
            }
            for (int i = 0; i < N; i++)
            {
                string[] line = reader.ReadLine().Split();
                for (int j = 0; j < M; j++)
                {
                    Double.TryParse(line[j], out content[i, j]);
                }
                if (currLineIndex == endLineIndex)
                {
                    break;
                }
                currLineIndex++;
            }
        }

        public void PrintMatrix()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    Console.Write("{0:0.##}", content[i,j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }

        }

        public static Matrix operator *(Matrix A, Matrix B)
        {
            if (A.M != B.N)
                throw new System.ArgumentException("Thouse matrix can not be multiplied.");
            Matrix C = new Matrix(A.N, B.M);
            for (int i = 0; i < A.N; i++)
            {
                for (int j = 0; j < B.M; j++)
                {
                    C.content[i, j] = 0;
                    {
                        for (int k = 0; k < A.M; ++k)
                        {
                            C.content[i, j] += A.content[i, k] * B.content[k, j];
                        }
                    }
                }
            }
            return C;
        }
    }
}

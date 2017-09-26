using System;
using System.Collections.Generic;
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point
{
    public class Point
    {
        private double X { get; set; }
        private double Y { get; set; }

        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public Point()
        {
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Point equalPoint = obj as Point;
            if ((System.Object)equalPoint == null)
            {
                return false;
            }
            return (X == equalPoint.X) && (Y == equalPoint.Y);
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}

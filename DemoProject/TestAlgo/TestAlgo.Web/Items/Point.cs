using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NCRV
{
    public class Point
    {
        public double X {get ; set; }

        public double Y { get; set; }

        public Point()
        {
            X = 0;
            Y = 0;
        }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Point operator -(Point x, Point y)
        {
            return new Point(x.X - y.X, x.Y - y.Y);
        }

    }
}

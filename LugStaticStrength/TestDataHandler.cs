using System;
using System.Collections.Generic;

namespace LugStaticStrength
{
    public class DataPoint
    {
        public double X { get; set; }
        public double Y { get; set; }

        public DataPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"X = {X}; Y = {Y}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is DataPoint))
                return false;

            DataPoint dataPoint = obj as DataPoint;

            return X == dataPoint.X && Y == dataPoint.Y;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }

    public static class TestDataHandler
    {
        public static double GetPolynomialValue(List<double> polynomialСoefficients, double variable)
        {
            double value = 0.0;

            for (int i = 0; i < polynomialСoefficients.Count; i++)
            {
                value += polynomialСoefficients[i] * Math.Pow(variable, i);
            }

            return value;
        }

        public static double GetLinearInterpolatedValue(DataPoint a, DataPoint b, double xValue)
        {
            if (a.Equals(b))
            {
                return a.Y;
            }

            DataPoint top;
            DataPoint bottom;

            if (a.X > b.X)
            {
                top = a;
                bottom = b;
            }
            else
            {
                top = b;
                bottom = a;
            }

            var result = bottom.Y + (top.Y - bottom.Y) * (xValue - bottom.X) / (top.X - bottom.X);

            return result;
        }
    }
}

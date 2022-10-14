using System;
using System.Collections.Generic;

namespace LugStaticStrength
{
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
                return a.Y;

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

            return bottom.Y + (top.Y - bottom.Y) * (xValue - bottom.X) / (top.X - bottom.X);
        }
    }
}

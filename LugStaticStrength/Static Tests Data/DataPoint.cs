using System;

namespace LugStaticStrength
{
    public class DataPoint : IEquatable<DataPoint>
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

        public bool Equals(DataPoint other)
        {
            return X == other.X && Y == other.Y;
        }
    }
}

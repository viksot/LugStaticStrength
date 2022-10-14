using System;

namespace LugStaticStrength
{
    public class Load
    {
        public double AxialLoad { get; }

        public double TransverseLoad { get; }

        public double Total { get; }

        public double Angle { get; }

        public Load(double axialLoad, double transverseLoad)
        {
            AxialLoad = axialLoad;
            TransverseLoad = transverseLoad;
            Total = GetTotalLoad();
            Angle = GetAngle();
        }

        private double GetTotalLoad()
        {
            return Math.Sqrt(Math.Pow(AxialLoad, 2) + Math.Pow(TransverseLoad, 2));
        }

        private double GetAngle()
        {
            if (AxialLoad >= 0)
            {
                return Math.Atan2(TransverseLoad, AxialLoad);
            }
            else
            {
                return Math.Atan2(Math.Abs(AxialLoad), TransverseLoad) + Math.PI / 2;
            }
        }

        public override string ToString()
        {
            return $"Px = {AxialLoad: 0.#}; " +
                   $"Py = {TransverseLoad: 0.#}; " +
                   $"Total = {Total: 0.#}; " +
                   $"Angle = {Angle: 0.#}";
        }
    }
}

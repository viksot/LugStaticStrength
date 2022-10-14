using System;

namespace LugStaticStrength
{
    public class MarginOfSafety
    {
        public double Value { get; }

        public FailureMode FailureMode { get;}

        public Load Load { get;}

        public MarginOfSafety(FailureMode failureMode, Load load)
        {
            FailureMode = failureMode;
            Load = load;
            Value = GetValue();
        }

        private double GetValue()
        {
            if (Load.Total > 0)
            {
                if (FailureMode is Bearing
                    || (Load.Angle >= 0 && Load.Angle <= Math.PI / 2)
                    || (Load.Angle >= 3 * Math.PI / 2 && Load.Angle <= 2 * Math.PI))
                {
                    return FailureMode.AllowableLoad / Load.Total;
                }
                else
                {
                    return 100;
                }
            }
            else
            {
                return 100;
            }
        }

        public override string ToString()
        {
            return $"Failure Mode: {FailureMode.GetTitle()}; " +
                   $"Allowable Load: {FailureMode.AllowableLoad: 0.#}; " +
                   $"Applied Load: {Load.Total: 0.#}; " +
                   $"Value: {Value: 0.##}";
        }
    }
}

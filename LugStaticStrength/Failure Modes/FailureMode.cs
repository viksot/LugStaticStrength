namespace LugStaticStrength
{
    public abstract class FailureMode
    {
        public readonly double StrengthFactor;

        public readonly double AllowableLoad;

        public FailureMode(Lug lug)
        {
            StrengthFactor = GetStrengthFactor(lug);

            AllowableLoad = GetAllowableLoad(lug);
        }

        protected abstract double GetStrengthFactor(Lug lug);

        protected abstract double GetAllowableLoad(Lug lug);

        public abstract string GetTitle();

        public override string ToString()
        {
            return $"Failure Mode: {this.GetTitle()}; " +
                   $"Strength Factor: {StrengthFactor: 0.00}; " +
                   $"Allowable Load: {AllowableLoad: 0.#}";
        }
    }
}

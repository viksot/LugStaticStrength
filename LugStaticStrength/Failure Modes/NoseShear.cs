namespace LugStaticStrength
{
    public class NoseShear : FailureMode
    {
        public NoseShear(Lug lug)
            : base(lug)
        { }

        protected override double GetStrengthFactor(Lug lug)
        {
            if (lug.Material.Type == MaterialTypes.Steel || lug.Material.Type == MaterialTypes.Titanium)
            {
                if (lug.Width / lug.HoleDiameter >= 2.0)
                {
                    return 0.9 + 0.1 * (1.5 - (lug.EdgeMargin - 0.5 * lug.HoleDiameter) / (0.5 * (lug.Width - lug.HoleDiameter)));
                }
                else
                {
                    return 0.7;
                }
            }
            else
            {
                return 0.7 + 0.3 * (1.5 - (lug.EdgeMargin - 0.5 * lug.HoleDiameter) / (0.5 * (lug.Width - lug.HoleDiameter)));
            }          
        }

        protected override double GetAllowableLoad(Lug lug)
        {
            return GetStrengthFactor(lug)
                * lug.Material.UltimateShearStrength
                * 2 * lug.Thickness
                * (lug.EdgeMargin - 0.295 * lug.HoleDiameter);
        }

        public override string GetTitle()
        {
            return "Nose Shear";
        }
    }
}

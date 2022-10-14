namespace LugStaticStrength
{
    public class NoseRupture : FailureMode
    {
        public NoseRupture(Lug lug)
            : base(lug)
        { }

        protected override double GetStrengthFactor(Lug lug)
        {
            return 1.15 - 0.3 * (lug.EdgeMargin - 0.5 * lug.HoleDiameter) / (0.5 * (lug.Width - lug.HoleDiameter));
        }

        protected override double GetAllowableLoad(Lug lug)
        {
            return GetStrengthFactor(lug) * 
                   lug.Material.UltimateTensileStrength 
                   * 2 * (lug.EdgeMargin - 0.5 * lug.HoleDiameter)
                   * lug.Thickness;
        }

        public override string GetTitle()
        {
            return "Nose Rupture";
        }
    }
}

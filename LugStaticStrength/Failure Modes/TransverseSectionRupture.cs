namespace LugStaticStrength
{
    public class TransverseSectionRupture : FailureMode
    {
        public TransverseSectionRupture(Lug lug)
            : base(lug)
        { }

        protected override double GetStrengthFactor(Lug lug)
        {
            return 0.48 +
                   0.45 * (lug.EdgeMargin - 0.5 * lug.HoleDiameter) / (0.5 * (lug.Width - lug.HoleDiameter)) -
                   0.075 * lug.Width / lug.HoleDiameter;
        }

        protected override double GetAllowableLoad(Lug lug)
        {
            return GetStrengthFactor(lug) 
                   * lug.Material.UltimateTensileStrength
                   * (lug.Width - lug.HoleDiameter) 
                   * lug.Thickness;
        }

        public override string GetTitle()
        {
            return "Transverse Section Rupture";
        }
    }
}

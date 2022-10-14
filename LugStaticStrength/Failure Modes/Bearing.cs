namespace LugStaticStrength
{
    public class Bearing : FailureMode
    {
        public Bearing(Lug lug)
            : base(lug)
        {
        }

        protected override double GetStrengthFactor(Lug lug)
        {
            return BearingTestData.GetStrengthFactor(lug);
        }

        protected override double GetAllowableLoad(Lug lug)
        {
            return GetStrengthFactor(lug) 
                    * lug.Material.UltimateTensileStrength 
                    * lug.HoleDiameter 
                    * lug.Thickness;
        }

        public override string GetTitle()
        {
            return "Bearing";
        }
    }
}

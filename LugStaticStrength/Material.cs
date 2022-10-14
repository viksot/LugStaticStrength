namespace LugStaticStrength
{
    public class Material
    {
        public int ID { get; }
        public string Title { get; }

        public MaterialTypes Type { get; }

        public double UltimateTensileStrength { get; }

        public double UltimateShearStrength { get; }

        public Material(int iD, string title, MaterialTypes type, double ultimateTensileStrength, double ultimateShearStrength)
        {
            ID = iD;
            Title = title;
            Type = type;
            UltimateTensileStrength = ultimateTensileStrength;
            UltimateShearStrength = ultimateShearStrength;
        }

        public override string ToString()
        {
            return $"ID: {ID}; " +
                   $"Title: {Title}; " +
                   $"Type: {Type}; " +
                   $"Ftu = {UltimateTensileStrength: 0.#}; " +
                   $"Fsu = {UltimateShearStrength: 0.#}";
        }
    }
}

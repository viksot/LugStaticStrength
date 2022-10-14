using System.Collections.Generic;

namespace LugStaticStrength
{
    public class Lug
    {
        public int ID { get; }

        public Material Material { get; }

        public double HoleDiameter { get; }

        public double Thickness { get; }

        /// <summary>
        /// Edge margin from hole center to nose profile in axial direction
        /// </summary>
        public double EdgeMargin { get; }

        public double Width { get; }

        /// <summary>
        /// Body profile taper half angle
        /// </summary>
        public double TaperHalfAngle { get; }

        public List<FailureMode> FailureModes { get; }

        public Lug(int id, Material material, double holeDiameter, double thickness, double edgeMargin, double width, double taperHalfAngle)
        {
            ID = id;
            Material = material;
            HoleDiameter = holeDiameter;
            Thickness = thickness;
            EdgeMargin = edgeMargin;
            Width = width;
            TaperHalfAngle = taperHalfAngle;
            FailureModes = new List<FailureMode>()
                                                    {
                                                        new Bearing(this),
                                                        new NoseRupture(this),
                                                        new NoseShear(this),
                                                        new TransverseSectionRupture(this)
                                                    };
        }

        public override string ToString()
        {
            return $"ID: [{ID}];" +
                   $"Material: [{Material.ToString()}]; " +
                   $"Hole Diameter: {HoleDiameter: 0.#}; " +
                   $"Thickness: {Thickness: 0.#}; " +
                   $"Edge Margin: {EdgeMargin: 0.#}; " +
                   $"Width: {Width: 0.#}; " +
                   $"Taper Half Angle: {TaperHalfAngle: 0.#}";
        }
    }
}

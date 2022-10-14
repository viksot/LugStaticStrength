using NUnit.Framework;

namespace LugStaticStrength
{
    [TestFixture]
    public class FailureMode_Test
    {
        [Test]
        public void NoseShearStrengthFactorCheck()
        {
            Lug lug = new Lug(0,new Material(0, "Круг В1-32", MaterialTypes.Steel, 102.0, 61.2), 4, 2, 8, 12, 0);
            Assert.AreEqual(0.9, lug.FailureModes[2].StrengthFactor, 0.01);

            lug = new Lug(1,new Material(0, "Круг В1-32", MaterialTypes.Steel, 102.0, 61.2), 4, 2, 6, 12, 0);
            Assert.AreEqual(0.95, lug.FailureModes[2].StrengthFactor, 0.01);

            lug = new Lug(2,new Material(0, "Круг В1-32", MaterialTypes.Steel, 102.0, 61.2), 4, 2, 4, 12, 0);
            Assert.AreEqual(1, lug.FailureModes[2].StrengthFactor, 0.01);

            lug = new Lug(3,new Material(0, "Круг В1-32", MaterialTypes.Steel, 102.0, 61.2), 6, 2, 5, 10, 0);
            Assert.AreEqual(0.7, lug.FailureModes[2].StrengthFactor, 0.01);

            lug = new Lug(4,new Material(0, "Плита Д16Т", MaterialTypes.Aluminum, 102.0, 61.2), 4, 2, 8, 12, 0);
            Assert.AreEqual(0.7, lug.FailureModes[2].StrengthFactor, 0.01);

            lug = new Lug(5,new Material(0, "Плита Д16Т", MaterialTypes.Aluminum, 102.0, 61.2), 4, 2, 6, 12, 0);
            Assert.AreEqual(0.85, lug.FailureModes[2].StrengthFactor, 0.01);

            lug = new Lug(6,new Material(0, "Плита Д16Т", MaterialTypes.Aluminum, 102.0, 61.2), 4, 2, 4, 12, 0);
            Assert.AreEqual(1, lug.FailureModes[2].StrengthFactor, 0.01);
        }
    }
}

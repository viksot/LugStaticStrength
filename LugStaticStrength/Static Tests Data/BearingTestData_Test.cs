using System.Collections.Generic;
using NUnit.Framework;

namespace LugStaticStrength
{
    [TestFixture]
    internal class BearingTestData_Test
    {
        [TestCase(2.0, 1.0, 0.8724)]
        [TestCase(2.0, 3.0, 2.5181)]
        [TestCase(3.0, 1.0, 0.8782)]
        [TestCase(3.0, 3.0, 2.4424)]
        [TestCase(4.0, 1.0, 0.8724)]
        [TestCase(4.0, 3.0, 2.2864)]
        [TestCase(6.0, 2.0, 1.7098)]
        [TestCase(6.0, 3.0, 1.9649)]
        [TestCase(8.0, 1.0, 0.8724)]
        [TestCase(8.0, 3.0, 1.5864)]
        [TestCase(10.0, 1.0, 0.8607)]
        [TestCase(10.0, 3.0, 1.3)]
        [TestCase(20.0, 1.0, 0.4414)]
        [TestCase(20.0, 3.0, 0.65)]
        public void CheckOfTestDataCurvesLinearInterpolation(double dt, double ed, double expectedFactor)
        {
            Assert.AreEqual(expectedFactor, GetPolynomialValue(dt, ed), 0.015);
        }

        public double GetPolynomialValue(double dt, double ed)
        {
            List<double> dataCurvePolynomialСoefficients = BearingTestData._dataCurves[dt];

            return TestDataHandler.GetPolynomialValue(dataCurvePolynomialСoefficients, ed);
        }
    }
}

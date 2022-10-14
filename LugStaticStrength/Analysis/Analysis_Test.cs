using System.Collections.Generic;
using NUnit.Framework;

namespace LugStaticStrength
{
    [TestFixture]
    internal class Analysis_Test
    {
        [Test]
        public void StraightSteelLugTwoLoadCases()
        {
            Lug lug = new Lug(0, new Material(0, "Круг В1-32", MaterialTypes.Steel, 102.0, 61.2), 10, 4, 9, 18, 0);

            List<LoadCase> loadCases = new List<LoadCase>()
            {
                new LoadCase(0, "VerticalGust", new Load(136.001, 19.001)),
                new LoadCase(1, "Maneuver", new Load(-155, 32))
            };

            var actualMS = GetActualMSList(Analysis.GetAnalysisOutput(new AnalysisInput(lug, loadCases)));

            var expectedMS = new List<List<double>>()
            {
                new List<double>() {21.45, 20.20, 15.09, 18.90 },
                new List<double>() {18.61, 100, 100, 100 }
            };

            Assert.That(actualMS, Is.EqualTo(expectedMS).Within(0.05));
        }

        [Test]
        public void StraightTitaniumLugSixLoadCases()
        {
            Lug lug = new Lug(0, new Material(0, "Плита ВТ-6", MaterialTypes.Titanium, 90.27, 55.97), 8, 6, 11, 22, 0);

            List<LoadCase> loadCases = new List<LoadCase>()
            {
                new LoadCase(0, "0", new Load(-687.5, 13.75)),
                new LoadCase(1, "1", new Load(-2406, 220)),
                new LoadCase(2, "2", new Load(-3505, 296.25)),
                new LoadCase(3, "3", new Load(670, 73.75)),
                new LoadCase(4, "4", new Load(2353.75, 106.25)),
                new LoadCase(5, "5", new Load(3422.5, 165))
            };

            var actualMS = GetActualMSList(Analysis.GetAnalysisOutput(new AnalysisInput(lug, loadCases)));

            var expectedMS = new List<List<double>>()
            {
                new List<double>() { 8.42, 100, 100, 100},
                new List<double>() { 2.39, 100, 100, 100},
                new List<double>() { 1.65, 100, 100, 100},
                new List<double>() { 8.59, 9.56, 8.18, 8.14},
                new List<double>() { 2.46, 2.74, 2.34, 2.33},
                new List<double>() { 1.69, 1.88, 1.61, 1.60 }
            };

            Assert.That(actualMS, Is.EqualTo(expectedMS).Within(0.05));
        }

        private List<List<double>> GetActualMSList(AnalysisOutput analysisResult)
        {
            var actualMargins = new List<List<double>>();

            //load case 
            for (int i = 0; i < analysisResult.LoadCasesOutput.Count; i++)
            {
                var loadCaseOutput = analysisResult[i];

                actualMargins.Add(new List<double>());

                //failure modes
                for (int j = 0; j < analysisResult[i].FailureModesMargins.Count; j++)
                {
                    var margin = loadCaseOutput[j];

                    actualMargins[i].Add(margin.Value);
                }
            }

            return actualMargins;
        }
    }
}

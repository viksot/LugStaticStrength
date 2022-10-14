using System.Collections.Generic;

namespace LugStaticStrength
{
    public static class Analysis
    {
        public static AnalysisOutput GetAnalysisOutput(AnalysisInput analysisInput)
        {
            var analysisOutput = new AnalysisOutput(analysisInput.Lug, new List<LoadCaseOutput>());

            foreach (var loadCase in analysisInput.LoadCases)
            {             
                analysisOutput.LoadCasesOutput.Add(GetLoadCaseOutput(analysisInput, loadCase));
            }

            return analysisOutput;
        }

        private static LoadCaseOutput GetLoadCaseOutput(AnalysisInput analysisInput, LoadCase loadCase)
        {
            var loadCaseOutput = new LoadCaseOutput(loadCase, new List<MarginOfSafety>());

            foreach (var failureMode in analysisInput.Lug.FailureModes)
            {
                loadCaseOutput.FailureModesMargins.Add(new MarginOfSafety(failureMode, loadCase.Load));
            }

            return loadCaseOutput;
        }
    }
}

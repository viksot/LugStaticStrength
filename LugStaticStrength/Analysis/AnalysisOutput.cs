using System;
using System.Collections.Generic;

namespace LugStaticStrength
{
    public class AnalysisOutput
    {
        public Lug Lug { get; }

        public List<LoadCaseOutput> LoadCasesOutput { get; }

        public AnalysisOutput(Lug lug, List<LoadCaseOutput> loadCasesOutput)
        {
            Lug = lug;
            LoadCasesOutput = loadCasesOutput;
        }

        public LoadCaseOutput this[int index]
        {
            get
            {
                if (index < 0 || index >= LoadCasesOutput.Count)
                    throw new IndexOutOfRangeException();

                return LoadCasesOutput[index];
            }
            set
            {
                if (index < 0 || index >= LoadCasesOutput.Count)
                    throw new IndexOutOfRangeException();

                LoadCasesOutput[index] = value;
            }
        }
    }
}

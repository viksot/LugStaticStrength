using System;
using System.Collections.Generic;

namespace LugStaticStrength
{
    public class AnalysisInput
    {
        public Lug Lug { get; }

        public List<LoadCase> LoadCases { get; }

        public AnalysisInput(Lug lug, List<LoadCase> loadCases)
        {
            Lug = lug;
            LoadCases = loadCases;
        }

        public LoadCase this[int index]
        {
            get
            {
                if (index < 0 || index >= LoadCases.Count)
                    throw new IndexOutOfRangeException();

                return LoadCases[index];
            }
            set
            {
                if (index < 0 || index >= LoadCases.Count)
                    throw new IndexOutOfRangeException();

                LoadCases[index] = value;
            }
        }
    }
}


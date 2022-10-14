using System;
using System.Collections.Generic;

namespace LugStaticStrength
{
    public class LoadCaseOutput
    {
        public LoadCase LoadCase { get; }

        public List<MarginOfSafety> FailureModesMargins { get; }

        public LoadCaseOutput(LoadCase loadCase, List<MarginOfSafety> failureModeMargins)
        {
            LoadCase = loadCase;
            FailureModesMargins = failureModeMargins;
        }

        public MarginOfSafety this[int index]
        {
            get
            {
                if (index < 0 || index >= FailureModesMargins.Count)
                    throw new IndexOutOfRangeException();

                return FailureModesMargins[index];
            }
            set
            {
                if (index < 0 || index >= FailureModesMargins.Count)
                    throw new IndexOutOfRangeException();

                FailureModesMargins[index] = value;
            }
        }
    }
}

using System.Collections.Generic;

namespace LugStaticStrength
{
    internal class MarginOfSafetyByValueComparer : IComparer<MarginOfSafety>
    {
        public int Compare(MarginOfSafety x, MarginOfSafety y)
        {
            return x.Value.CompareTo(x.Value);
        }
    }
}

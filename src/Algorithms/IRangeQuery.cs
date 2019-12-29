using System;

namespace Algorithms
{
    public interface IRangeQuery
    {
        int GetMinimum(int rangeInclusiveLowerBound, int rangeInclusiveUpperBound);
    }
}

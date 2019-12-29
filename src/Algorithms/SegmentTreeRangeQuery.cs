using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Algorithms
{
    public class SegmentTreeRangeQuery : IRangeQuery
    {
        [DebuggerDisplay("[{" + nameof(LowerInclusiveBound) + "}; {" + nameof(UpperInclusiveBound) + "}] @ Index {" + nameof(Index) + "}")]
        private struct IntervalHelper
        {
            public int Index { get; }

            public int LowerInclusiveBound { get; }

            public int UpperInclusiveBound { get; }

            public bool ContainsSingleItem => LowerInclusiveBound == UpperInclusiveBound;

            public IntervalHelper(int index, int lowerInclusiveBound, int upperInclusiveBound)
            {
                Index = index;
                LowerInclusiveBound = lowerInclusiveBound;
                UpperInclusiveBound = upperInclusiveBound;
            }

            public IntervalHelper GetLeftSubsetInterval() => GetLeftSubsetInterval(Index, LowerInclusiveBound, UpperInclusiveBound);

            public IntervalHelper GetRightSubsetInterval() => GetRightSubsetInterval(Index, LowerInclusiveBound, UpperInclusiveBound);

            public (IntervalHelper Left, IntervalHelper Right) Split()
            {
                return (GetLeftSubsetInterval(), GetRightSubsetInterval());
            }

            public (IntervalHelper Left, IntervalHelper Right) SplitAt(int targetLeftIntervalUpperInclusiveBound)
            {
                if (LowerInclusiveBound <= targetLeftIntervalUpperInclusiveBound && targetLeftIntervalUpperInclusiveBound < UpperInclusiveBound)
                {
                    return (new IntervalHelper(0, LowerInclusiveBound, targetLeftIntervalUpperInclusiveBound), new IntervalHelper(0, targetLeftIntervalUpperInclusiveBound + 1, UpperInclusiveBound));
                }
                else
                {
                    return (this, this);
                }
            }

            public bool IsSubsetOf(IntervalHelper other) => other.LowerInclusiveBound <= LowerInclusiveBound && UpperInclusiveBound <= other.UpperInclusiveBound;

            public bool OverlapsWith(IntervalHelper other)
            {
                return IsSubsetOf(other)
                       || other.IsSubsetOf(this)
                       || (other.LowerInclusiveBound <= LowerInclusiveBound && LowerInclusiveBound <= other.UpperInclusiveBound) // this.LowerInclusiveBound is within other's range
                       || (LowerInclusiveBound <= other.LowerInclusiveBound && other.LowerInclusiveBound <= UpperInclusiveBound); // other's LowerInclusiveBound is within this' range
            }

            public static IntervalHelper GetLeftSubsetInterval(int currentIndex, int currentLowerInclusiveBound, int currentUpperInclusiveBound)
            {
                return new IntervalHelper(LeftIndex(currentIndex), currentLowerInclusiveBound, currentLowerInclusiveBound + (currentUpperInclusiveBound - currentLowerInclusiveBound) / 2);
            }

            public static IntervalHelper GetRightSubsetInterval(int currentIndex, int currentLowerInclusiveBound, int currentUpperInclusiveBound)
            {
                return new IntervalHelper(RightIndex(currentIndex), currentLowerInclusiveBound + (currentUpperInclusiveBound - currentLowerInclusiveBound) / 2 + 1, currentUpperInclusiveBound);
            }
        }

        private class HeapAccessHelper
        {
            private List<int> _storage = new List<int>();

            private void EnsureIndex(int index)
            {
                if (index > _storage.Count - 1)
                {
                    int delta = index - (_storage.Count - 1);
                    _storage.AddRange(Enumerable.Range(0, delta).Select(i => 0));
                }
            }

            public int this[int index]
            {
                get
                {
                    EnsureIndex(index);
                    return _storage[index];
                }

                set
                {
                    EnsureIndex(index);
                    _storage[index] = value;
                }
            }
        }

        private HeapAccessHelper _treeStorage = new HeapAccessHelper();
        private readonly int[] _originalValues;

        private static int LeftIndex(int n) => 2 * n + 1;

        private static int RightIndex(int n) => 2 * n + 2;

        private void Init(IntervalHelper interval)
        {
            if (interval.ContainsSingleItem)
            {
                _treeStorage[interval.Index] = _originalValues[interval.LowerInclusiveBound]; // LowerInclusiveBound represents the index of the original array
            }
            else
            {
                IntervalHelper leftSubset = interval.GetLeftSubsetInterval();
                IntervalHelper rightSubset = interval.GetRightSubsetInterval();

                Init(leftSubset);
                Init(rightSubset);

                _treeStorage[interval.Index] = Math.Min(_treeStorage[leftSubset.Index], _treeStorage[rightSubset.Index]);
            }
        }

        private IntervalHelper GetRootInterval() => new IntervalHelper(0, 0, _originalValues.Length - 1);

        private int GetMinimum(IntervalHelper currentFrameInterval, IntervalHelper targetInterval)
        {
            if (currentFrameInterval.IsSubsetOf(targetInterval))
            {
                return _treeStorage[currentFrameInterval.Index];
            }
            else
            {
                var newFrameIntervals = currentFrameInterval.Split();
                var newTargetIntervals = targetInterval.SplitAt(newFrameIntervals.Left.UpperInclusiveBound);

                if (!newFrameIntervals.Left.OverlapsWith(newTargetIntervals.Left))
                {
                    return GetMinimum(newFrameIntervals.Right, newTargetIntervals.Right);
                }

                if (!newFrameIntervals.Right.OverlapsWith(newTargetIntervals.Right))
                {
                    return GetMinimum(newFrameIntervals.Left, newTargetIntervals.Left);
                }

                return Math.Min(GetMinimum(newFrameIntervals.Left, newTargetIntervals.Left), GetMinimum(newFrameIntervals.Right, newTargetIntervals.Right));
            }
        }

        public int GetMinimum(int rangeInclusiveLowerBound, int rangeInclusiveUpperBound) => GetMinimum(
            currentFrameInterval: GetRootInterval(),
            targetInterval: new IntervalHelper(0, rangeInclusiveLowerBound, rangeInclusiveUpperBound));

        public SegmentTreeRangeQuery(IEnumerable<int> collection)
        {
            _originalValues = collection.ToArray();
            Init(GetRootInterval());
        }
    }
}

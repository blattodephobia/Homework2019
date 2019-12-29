using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Tests
{
    [TestFixture]
    public class SegmentTreeRangeQueryTests
    {
        private static readonly IEnumerable<int> TestData = new int[] { 2, -1, 4, 6, 1, 7 };

        [Test]
        public void Test1()
        {
            SegmentTreeRangeQuery testObj = new SegmentTreeRangeQuery(TestData);
            Assert.That(testObj.GetMinimum(0, 5), Is.EqualTo(-1));
        }

        [Test]
        public void Test2()
        {
            SegmentTreeRangeQuery testObj = new SegmentTreeRangeQuery(TestData);
            Assert.That(testObj.GetMinimum(2, 4), Is.EqualTo(1));
        }

        [Test]
        public void Test3()
        {
            SegmentTreeRangeQuery testObj = new SegmentTreeRangeQuery(TestData);
            Assert.That(testObj.GetMinimum(2, 2), Is.EqualTo(4));
        }

        [Test]
        public void Test4()
        {
            SegmentTreeRangeQuery testObj = new SegmentTreeRangeQuery(TestData);
            Assert.That(testObj.GetMinimum(5, 5), Is.EqualTo(7));
        }
    }
}

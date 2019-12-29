using NUnit.Framework;
using System.Collections.Generic;

namespace Algorithms.Tests
{
    public class SqrtRangeQueryTests
    {
        private static readonly IEnumerable<int> TestData = new int[] { 2, -1, 4, 6, 1, 7 };

        [Test]
        public void Test1()
        {
            SqrtRangeQuery testObj = new SqrtRangeQuery(TestData);
            Assert.That(testObj.GetMinimum(0, 5), Is.EqualTo(-1));
        }
    }
}
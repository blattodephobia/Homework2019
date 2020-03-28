using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Tests
{
    [TestFixture]
    public class AvlTreeTests
    {
        [Test]
        public void BalanceTest()
        {
            var tree = new AvlTree<int>();
            var testData = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int index;
            for (index = 0; index < 3; index++)
            {
                tree.Add(testData[index]);
            }
            Assert.That(tree.Root.Key, Is.EqualTo(testData[1]));

            for (; index < 6; index++)
            {
                tree.Add(testData[index]);
            }
            Assert.That(tree.Root.Right.Key, Is.EqualTo(testData[4]));

            tree.Add(testData[index++]);
            Assert.That(tree.Root.Key, Is.EqualTo(testData[3]));

            for (; index < testData.Length; index++)
            {
                tree.Add(testData[index]);
            }
            Assert.That(tree.Root.Key, Is.EqualTo(testData[3]));
        }

        [Test]
        public void ComplexityTest()
        {
            AvlTree<int> tree = new AvlTree<int>();

            tree.AddRange(Enumerable.Range(0, 128));

            Assert.That(tree.Root.Height, Is.EqualTo(8));
        }
    }
}

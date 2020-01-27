using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Tests
{
    [TestFixture]
    public class TreapTests
    {
        [Test]
        public void GeneratesTreeStructureCorrectly()
        {
            Stack<int> priorities = new Stack<int>(new int[] { 5, -1, 2, 4 });
            Treap<int> treap = new Treap<int>(() => priorities.Pop());

            treap.Add(5);
            treap.Add(7);
            treap.Add(8);
            treap.Add(12);

            int[] expectedFlattenedStructure = new int[] { 5, 12, 8, 7 };

            int[] actualFlattenedStructure = treap.Root.BreadthFirstSearch().Select(node => node.Key).ToArray();

            Assert.That(actualFlattenedStructure, Is.EquivalentTo(expectedFlattenedStructure));
        }

        [Test]
        public void GeneratesTreeStructureCorrectly_DegratedToLinkedList()
        {
            Stack<int> priorities = new Stack<int>(new int[] { 0, 1, 2, 3 });
            Treap<int> treap = new Treap<int>(() => priorities.Pop());

            var testData = new int[] { 5, 7, 8, 12 };
            treap.Add(testData[0]);
            treap.Add(testData[1]);
            treap.Add(testData[2]);
            treap.Add(testData[3]);

            int[] expectedFlattenedStructure = testData;

            int[] actualFlattenedStructure = new int[] { treap.Root.Key, treap.Root.Right.Key, treap.Root.Right.Right.Key, treap.Root.Right.Right.Right.Key };

            Assert.That(actualFlattenedStructure, Is.EquivalentTo(expectedFlattenedStructure));
        }
    }
}
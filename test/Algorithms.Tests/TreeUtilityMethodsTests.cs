using static Algorithms.TreeUtilityMethods;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Tests
{
    [TestFixture]
    public class TreeUtilityMethodsTests
    {
        [Test]
        public void RotateLeftWithNodeDescendant()
        {
            /*   R          Q
             *    \        /
             *     Q  ->  R
             *    /        \
             *   S          S
             */
            var R = new BinaryTreeNode<int, int>(4, 4);
            var Q = new BinaryTreeNode<int, int>(8, 8);
            R.Right = Q;
            var S = new BinaryTreeNode<int, int>(2, 2) { Left = new BinaryTreeNode<int, int>(4, 4), Right = new BinaryTreeNode<int, int>(5, 5) };
            Q.Left = S;

            BinaryTreeNode<int, int> newRoot = RotateLeft(R);

            Assert.That(newRoot, Is.SameAs(Q));
            Assert.That(Q.Left, Is.SameAs(R));
            Assert.That(R.Right, Is.SameAs(S));
        }

        [Test]
        public void RotateRightWithNodeDescendant()
        {
            /*   R      Q
             *  /        \
             * Q    ->    R
             *  \        /
             *   S      S
             */
            var R = new BinaryTreeNode<int, int>(4, 4);
            var Q = new BinaryTreeNode<int, int>(8, 8);
            R.Left = Q;
            var S = new BinaryTreeNode<int, int>(2, 2)
            {
                Left = new BinaryTreeNode<int, int>(4, 4),
                Right = new BinaryTreeNode<int, int>(5, 5)
            };
            Q.Right = S;

            BinaryTreeNode<int, int> newRoot = RotateRight(R);

            Assert.That(newRoot, Is.SameAs(Q));
            Assert.That(Q.Right, Is.SameAs(R));
            Assert.That(R.Left, Is.SameAs(S));
        }

        [Test]
        public void RotateLeftWithLeafDescendant()
        {
            /*    R          Q
             *     \   ->   /
             *      Q      R
             */
            var R = new BinaryTreeNode<int, int>(4, 4);
            var Q = new BinaryTreeNode<int, int>(8, 8);
            R.Right = Q;

            BinaryTreeNode<int, int> newRoot = RotateLeft(R);

            Assert.That(newRoot, Is.SameAs(Q));
            Assert.That(Q.Left, Is.SameAs(R));
        }

        [Test]
        public void RotateRightWithLeafDescendant()
        {
            /*    R      Q
             *   /   ->   \
             *  Q          R
             */
            var R = new BinaryTreeNode<int, int>(4, 4);
            var Q = new BinaryTreeNode<int, int>(8, 8);
            R.Left = Q;

            BinaryTreeNode<int, int> newRoot = RotateRight(R);

            Assert.That(newRoot, Is.SameAs(Q));
            Assert.That(Q.Right, Is.SameAs(R));
        }

        [Test]
        public void HeightTest()
        {
            /*   R
             *  / \
             * A   Q
             *    /
             *   S
             */
            var R = new BinaryTreeNode<int, int>(4, 4);
            var A = new BinaryTreeNode<int, int>(2, 2);
            var Q = new BinaryTreeNode<int, int>(8, 8);
            R.Left = A;
            R.Right = Q;
            var S = new BinaryTreeNode<int, int>(2, 2);
            Q.Left = S;

            Assert.That(R.Height(), Is.EqualTo(2));
        }

        [Test]
        public void HeightTest_RootOnly()
        {
            var R = new BinaryTreeNode<int, int>(4, 4);

            Assert.That(R.Height(), Is.EqualTo(0));
        }

        [Test]
        public void FullTest_FullTree()
        {
            var R = new BinaryTreeNode<int, int>(4, 4)
            {
                Left = new BinaryTreeNode<int, int>(8, 8)
                {
                    Left = new BinaryTreeNode<int, int>(0, 0),
                    Right = new BinaryTreeNode<int, int>(2, 2)
                    {
                        Left = new BinaryTreeNode<int, int>(4, 4),
                        Right = new BinaryTreeNode<int, int>(5, 5)
                    }
                },
                Right = new BinaryTreeNode<int, int>(0, 0)
            };

            Assert.That(R.IsFull(), Is.True);
        }

        [Test]
        public void FullTest_FullTree_RootOnly()
        {
            var R = new BinaryTreeNode<int, int>(4, 4);

            Assert.That(R.IsFull(), Is.True);
        }

        [Test]
        public void FullTest_IncompleteTree()
        {
            var R = new BinaryTreeNode<int, int>(4, 4)
            {
                Left = new BinaryTreeNode<int, int>(1, 1)
            };

            Assert.That(R.IsFull(), Is.False);
        }

        [Test]
        public void FullTest_IncompleteTree2()
        {
            var R = new BinaryTreeNode<int, int>(4, 4);
            var Q = new BinaryTreeNode<int, int>(8, 8);
            R.Left = Q;
            var S = new BinaryTreeNode<int, int>(2, 2)
            {
                Left = new BinaryTreeNode<int, int>(4, 4),
                Right = new BinaryTreeNode<int, int>(5, 5)
            };
            Q.Right = S;

            Assert.That(R.IsFull(), Is.False);
        }
    }
}

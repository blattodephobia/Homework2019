using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Tests
{
    [TestFixture]
    public class TraversalsTests
    {
        private BinaryTreeNode<int> X = new BinaryTreeNode<int>(4);
        private BinaryTreeNode<int> A = new BinaryTreeNode<int>(4);
        private BinaryTreeNode<int> B = new BinaryTreeNode<int>(4);
        private BinaryTreeNode<int> Y = new BinaryTreeNode<int>(4);
        private BinaryTreeNode<int> Z = new BinaryTreeNode<int>(4);
        private BinaryTreeNode<int> S = new BinaryTreeNode<int>(4);

        private void Given_BinaryTree()
        {
            /*
             *      X
             *    /    \
             *  A       Y
             *   \     / \
             *    B   Z   S
             */
            X = new BinaryTreeNode<int>(4);
            A = new BinaryTreeNode<int>(4);
            B = new BinaryTreeNode<int>(4);
            Y = new BinaryTreeNode<int>(4);
            Z = new BinaryTreeNode<int>(4);
            S = new BinaryTreeNode<int>(4);

            X.Left = A;
            X.Right = Y;

            A.Right = B;
            Y.Left = Z;
            Y.Right = S;
        }

        private IEnumerable<BinaryTreeNode<int>> ExpectedBfsCollection() => new[] { X, A, Y, B, Z, S };

        [Test]
        public void BreadthFirstSearch_ReturnCollectionInBfs()
        {
            Given_BinaryTree();

            // when
            var bfsResult = X.BreadthFirstSearch();

            // then
            Assert.That(bfsResult, Is.EquivalentTo(ExpectedBfsCollection()));
        }
    }
}

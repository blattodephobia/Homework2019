using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Tests
{
    [TestFixture]
    public class AvlTreeNodeTests
    {
        [Test]
        public void LeafHeightShouldBeZero()
        {
            AvlTreeNode<int> root = new AvlTreeNode<int>(5);
            Assert.That(root.Height, Is.EqualTo(0));
        }

        [Test]
        public void UpdatesHeightWhenNodeAdded()
        {
            var root = new AvlTreeNode<int>(5);
            int initialHeight = root.Height;

            var subTree = new AvlTreeNode<int>(6)
            {
                Left = new AvlTreeNode<int>(4),
                Right = new AvlTreeNode<int>(2)
                {
                    Right = new AvlTreeNode<int>(2)
                }
            };

            root.Right = subTree;
            int finalHeight = root.Height;

            Assert.That(initialHeight, Is.EqualTo(0));
            Assert.That(finalHeight, Is.EqualTo(3));
        }

        [Test]
        public void UpdatesHeightWhenNodeRemoved()
        {
            var root = new AvlTreeNode<int>(5)
            {
                Left = new AvlTreeNode<int>(6)
                {
                    Left = new AvlTreeNode<int>(4),
                    Right = new AvlTreeNode<int>(2)
                    {
                        Right = new AvlTreeNode<int>(2)
                    }
                }
            };

            int initialHeight = root.Height;

            root.Left.Right = null;

            int finalHeight = root.Height;

            Assert.That(initialHeight, Is.EqualTo(3));
            Assert.That(finalHeight, Is.EqualTo(2));
        }

        [Test]
        public void DetachedNodesHaveNoEffectOnRootHeight()
        {
            var root = new AvlTreeNode<int>(5)
            {
                Left = new AvlTreeNode<int>(6)
                {
                    Left = new AvlTreeNode<int>(4),
                    Right = new AvlTreeNode<int>(2)
                    {
                        Right = new AvlTreeNode<int>(2)
                    }
                }
            };
            
            AvlTreeNode<int> detachedNode = root.Left.Right;

            root.Left.Right = null;
            int detachedHeight = root.Height;

            detachedNode.Right.Right = new AvlTreeNode<int>(9);

            int finalHeight = root.Height;

            Assert.That(detachedHeight, Is.EqualTo(finalHeight));
        }
    }
}

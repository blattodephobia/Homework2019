using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    internal static class TreeUtilityMethods
    {
        public static TNode RotateLeft<TNode>(TNode subTreeRoot) where TNode : IBinaryTreeNode<TNode>
        {
            if (subTreeRoot == null)
            {
                throw new ArgumentNullException(nameof(subTreeRoot));
            }

            TNode newRoot = subTreeRoot;
            if (subTreeRoot.Right != null)
            {
                TNode oldRoot = subTreeRoot;
                newRoot = subTreeRoot.Right;
                oldRoot.Right = newRoot.Left;
                newRoot.Left = oldRoot;
            }

            return newRoot;
        }

        public static TNode RotateRight<TNode>(TNode subTreeRoot) where TNode : IBinaryTreeNode<TNode>
        {
            if (subTreeRoot == null)
            {
                throw new ArgumentNullException(nameof(subTreeRoot));
            }

            TNode newRoot = subTreeRoot;
            if (subTreeRoot.Left != null)
            {
                TNode oldRoot = subTreeRoot;
                newRoot = subTreeRoot.Left;
                oldRoot.Left = newRoot.Right;
                newRoot.Right = oldRoot;
            }

            return newRoot;
        }

        public static int Height<TNode>(this TNode subTreeRoot) where TNode : IBinaryTreeNode<TNode>
        {
            if (subTreeRoot == null)
            {
                throw new ArgumentNullException(nameof(subTreeRoot));
            }

            return subTreeRoot.Height_Checked();
        }

        private static int Height_Checked<TNode>(this TNode subTreeRoot) where TNode : IBinaryTreeNode<TNode>
        {
            return Math.Max(
                val1: (subTreeRoot.Left?.Height() + 1) ?? 0,
                val2: (subTreeRoot.Right?.Height() + 1) ?? 0);
        }

        public static bool IsFull<TNode>(this TNode subTreeRoot) where TNode : IBinaryTreeNode<TNode>
        {
            if (subTreeRoot == null)
            {
                throw new ArgumentNullException(nameof(subTreeRoot));
            }

            return subTreeRoot.IsFull_Checked();
        }

        private static bool IsFull_Checked<TNode>(this TNode subTreeRoot) where TNode : IBinaryTreeNode<TNode>
        {
            return
                (subTreeRoot.Left == null && subTreeRoot.Right == null) ||  // is leaf OR
                (subTreeRoot.Left != null && subTreeRoot.Right != null) &&  // is node AND
                subTreeRoot.Left.IsFull() && subTreeRoot.Right.IsFull();    // sub tree is also full
        }
    }
}

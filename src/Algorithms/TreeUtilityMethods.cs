using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    internal static class TreeUtilityMethods
    {
        public static BinaryTreeNode<TKey, TValue> RotateLeft<TKey, TValue>(BinaryTreeNode<TKey, TValue> subTreeRoot)
        {
            if (subTreeRoot == null)
            {
                throw new ArgumentNullException(nameof(subTreeRoot));
            }

            BinaryTreeNode<TKey, TValue> newRoot = subTreeRoot;
            if (subTreeRoot.Right != null)
            {
                BinaryTreeNode<TKey, TValue> oldRoot = subTreeRoot;
                newRoot = subTreeRoot.Right;
                oldRoot.Right = newRoot.Left;
                newRoot.Left = oldRoot;
            }

            return newRoot;
        }

        public static BinaryTreeNode<TKey, TValue> RotateRight<TKey, TValue>(BinaryTreeNode<TKey, TValue> subTreeRoot)
        {
            if (subTreeRoot == null)
            {
                throw new ArgumentNullException(nameof(subTreeRoot));
            }

            BinaryTreeNode<TKey, TValue> newRoot = subTreeRoot;
            if (subTreeRoot.Left != null)
            {
                BinaryTreeNode<TKey, TValue> oldRoot = subTreeRoot;
                newRoot = subTreeRoot.Left;
                oldRoot.Left = newRoot.Right;
                newRoot.Right = oldRoot;
            }

            return newRoot;
        }

        public static int Height<TKey, TValue>(this BinaryTreeNode<TKey, TValue> subTreeRoot)
        {
            if (subTreeRoot == null)
            {
                throw new ArgumentNullException(nameof(subTreeRoot));
            }

            return subTreeRoot.Height_Checked();
        }

        private static int Height_Checked<TKey, TValue>(this BinaryTreeNode<TKey, TValue> subTreeRoot)
        {
            return Math.Max(
                val1: (subTreeRoot.Left?.Height() + 1) ?? 0,
                val2: (subTreeRoot.Right?.Height() + 1) ?? 0);
        }

        public static bool IsFull<TKey, TValue>(this BinaryTreeNode<TKey, TValue> subTreeRoot)
        {
            if (subTreeRoot == null)
            {
                throw new ArgumentNullException(nameof(subTreeRoot));
            }

            return subTreeRoot.IsFull_Checked();
        }

        private static bool IsFull_Checked<TKey, TValue>(this BinaryTreeNode<TKey, TValue> subTreeRoot)
        {
            return
                (subTreeRoot.Left == null && subTreeRoot.Right == null) ||  // is leaf OR
                (subTreeRoot.Left != null && subTreeRoot.Right != null) &&  // is node AND
                subTreeRoot.Left.IsFull() && subTreeRoot.Right.IsFull();    // sub tree is also full
        }
    }
}

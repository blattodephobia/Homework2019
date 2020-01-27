using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    public static class Traversals
    {
        public static IEnumerable<BinaryTreeNode<TKey, TValue>> BreadthFirstSearch<TKey, TValue>(this BinaryTreeNode<TKey, TValue> subTreeRoot)
        {
            return new BreadthFirstSearchEnumerable<TKey, TValue>(subTreeRoot);
        }
    }
}

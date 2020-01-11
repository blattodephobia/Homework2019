using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Algorithms
{
    [DebuggerDisplay("{Key}: {Value}")]
    public class BinaryTreeNode<TKey, TValue> : TreeNode<TKey, TValue>
    {
        public BinaryTreeNode(TKey key, TValue value) :
            base(key, value)
        {
        }

        public BinaryTreeNode<TKey, TValue> Left { get; set; }

        public BinaryTreeNode<TKey, TValue> Right { get; set; }
    }
}

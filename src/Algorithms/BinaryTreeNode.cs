using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Algorithms
{
    [DebuggerDisplay("{Key}: {Value}")]
    public class BinaryTreeNode<TKey> : IBinaryTreeNode<TKey, BinaryTreeNode<TKey>>
    {
        public BinaryTreeNode(TKey key)
        {
            Key = key;
        }

        public TKey Key { get; }

        public BinaryTreeNode<TKey> Left { get; set; }

        public BinaryTreeNode<TKey> Right { get; set; }
    }
}

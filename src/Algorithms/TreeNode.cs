using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    public class TreeNode<TKey, TValue>
    {
        public TreeNode(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public TKey Key { get; }
        public TValue Value { get; }
    }
}

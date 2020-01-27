using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    public class BreadthFirstSearchEnumerable<TKey, TValue> : IEnumerable<BinaryTreeNode<TKey, TValue>>
    {
        private readonly BinaryTreeNode<TKey, TValue> _subTreeRoot;

        public BreadthFirstSearchEnumerable(BinaryTreeNode<TKey, TValue> subTreeRoot)
        {
            _subTreeRoot = subTreeRoot ?? throw new ArgumentNullException(nameof(subTreeRoot));
        }

        public IEnumerator<BinaryTreeNode<TKey, TValue>> GetEnumerator() => new BreadthFirstSearchEnumerator<TKey, TValue>(_subTreeRoot);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

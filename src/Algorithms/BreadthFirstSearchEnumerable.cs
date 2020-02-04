using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    public class BreadthFirstSearchEnumerable<TNode> : IEnumerable<TNode> where TNode : IBinaryTreeNode<TNode>
    {
        private readonly TNode _subTreeRoot;

        public BreadthFirstSearchEnumerable(TNode subTreeRoot)
        {
            if (subTreeRoot == null) throw new ArgumentNullException(nameof(subTreeRoot));

            _subTreeRoot = subTreeRoot;
        }

        public IEnumerator<TNode> GetEnumerator() => new BreadthFirstSearchEnumerator<TNode>(_subTreeRoot);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

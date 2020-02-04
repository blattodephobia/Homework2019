using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    public class BreadthFirstSearchEnumerator<TNode> : IEnumerator<TNode>
        where TNode : IBinaryTreeNode<TNode>
    {
        private readonly Queue<TNode> _bfsQueue = new Queue<TNode>();
        private readonly TNode _subTreeRoot;

        public BreadthFirstSearchEnumerator(TNode subTreeRoot)
        {
            if (subTreeRoot == null) throw new ArgumentNullException(nameof(subTreeRoot));

            _subTreeRoot = subTreeRoot;
            Init();
        }

        public TNode Current { get; private set; }

        object IEnumerator.Current => Current;
        
        public bool MoveNext()
        {
            if (_bfsQueue.Count == 0)
            {
                Current = default;
                return false;
            }

            Current = _bfsQueue.Dequeue();

            if (Current.Left != null) _bfsQueue.Enqueue(Current.Left);
            if (Current.Right != null) _bfsQueue.Enqueue(Current.Right);

            return true;
        }

        private void Init()
        {
            _bfsQueue.Clear();
            _bfsQueue.Enqueue(_subTreeRoot);

        }

        void IEnumerator.Reset()
        {
            Init();
        }

        void IDisposable.Dispose()
        {
            // Method intentionally left empty.
        }
    }
}

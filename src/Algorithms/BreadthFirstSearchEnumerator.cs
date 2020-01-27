using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    public class BreadthFirstSearchEnumerator<TKey, TValue> : IEnumerator<BinaryTreeNode<TKey, TValue>>
    {
        private readonly Queue<BinaryTreeNode<TKey, TValue>> _bfsQueue = new Queue<BinaryTreeNode<TKey, TValue>>();
        private readonly BinaryTreeNode<TKey, TValue> _subTreeRoot;

        public BreadthFirstSearchEnumerator(BinaryTreeNode<TKey, TValue> subTreeRoot)
        {
            _subTreeRoot = subTreeRoot ?? throw new ArgumentNullException(nameof(subTreeRoot));
            Init();
        }

        public BinaryTreeNode<TKey, TValue> Current { get; private set; }

        object IEnumerator.Current => Current;
        
        public bool MoveNext()
        {
            if (_bfsQueue.Count == 0)
            {
                Current = null;
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Algorithms
{
    [DebuggerDisplay("{Key} ({Height})")]
    public class AvlTreeNode<TKey> : IBinaryTreeNode<TKey, AvlTreeNode<TKey>>, IComparable<AvlTreeNode<TKey>>
        where TKey : IComparable<TKey>
    {
        public AvlTreeNode(TKey key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            Key = key;
        }

        public TKey Key { get; }

        private int _height = 1;
        public int Height
        {
            get => _height;

            private set
            {
                _height = value;
                HeightChanged?.Invoke();
            }
        }

        private AvlTreeNode<TKey> _left;
        public AvlTreeNode<TKey> Left
        {
            get => _left;

            set
            {
                DetachNode(ref _left);
                AttachNode(ref _left, value);
                OnHeightChanged();
            }
        }

        private AvlTreeNode<TKey> _right;
        public AvlTreeNode<TKey> Right
        {
            get => _right;

            set
            {
                DetachNode(ref _right);
                AttachNode(ref _right, value);
                OnHeightChanged();
            }
        }

        public int GetBalanceFactor() => (Left?.Height ?? 0) - (Right?.Height ?? 0);

        private void DetachNode(ref AvlTreeNode<TKey> storage)
        {
            if (storage != null)
            {
                storage.HeightChanged -= OnHeightChanged;
                storage = null;
            }
        }

        private void AttachNode(ref AvlTreeNode<TKey> storage, AvlTreeNode<TKey> newNode)
        {
            if (newNode != null)
            {
                newNode.HeightChanged += OnHeightChanged;
            }

            storage = newNode;
        }

        private void OnHeightChanged()
        {
            Height = this.IsLeaf()
                ? 1
                : Math.Max(Left?.Height ?? 0, Right?.Height ?? 0) + 1;
        }

        public int CompareTo(AvlTreeNode<TKey> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Key.CompareTo(other.Key);
        }

        private event Action HeightChanged;
    }
}

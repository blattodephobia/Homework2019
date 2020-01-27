using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    using static TreeUtilityMethods;

    public class Treap<TKey> where TKey : IComparable<TKey>
    {
        private static TreapNode<TKey> MinPriorityNode = new TreapNode<TKey>(default, int.MinValue);

        private class TreapNode<TKey> : BinaryTreeNode<TKey, int>, IComparable<TKey>
            where TKey : IComparable<TKey>
        {

            public TreapNode(TKey key, int priority) :
                base(key, priority)
            {
            }

            public int Priority => Value;

            public int CompareTo(Treap<TKey>.TreapNode<TKey> other) => Priority.CompareTo(other.Value);

            public int CompareTo(TKey other) => Key.CompareTo(other);

            public new TreapNode<TKey> Left
            {
                get => base.Left as TreapNode<TKey>;

                set => base.Left = value;
            }

            public new TreapNode<TKey> Right
            {
                get => base.Right as TreapNode<TKey>;

                set => base.Right = value;
            }
        }

        private static readonly Random PriorityGenerator = new Random();

        private readonly Func<int> _priorityGenerator;

        private int GeneratePriority()
        {
            return Math.Max(int.MinValue + 1, _priorityGenerator.Invoke());
        }

        private TreapNode<TKey> _root;

        private TreapNode<TKey> CreateNodeFor(TKey key) => new TreapNode<TKey>(key, GeneratePriority());

        private TreapNode<TKey> GetUpdatedSubTree(TreapNode<TKey> newNode, TreapNode<TKey> subTreeRoot = null)
        {
            if (subTreeRoot == null) return newNode;

            if (newNode.Key.CompareTo(subTreeRoot.Key) < 0)
            {
                subTreeRoot.Left = GetUpdatedSubTree(newNode, subTreeRoot.Left);
            }
            else
            {
                subTreeRoot.Right = GetUpdatedSubTree(newNode, subTreeRoot.Right);
            }

            return EnsureMaxHeapConstraint(subTreeRoot);
        }

        private TreapNode<TKey> RotateToMaxPriority(TreapNode<TKey> subTreeRoot)
        {
            switch (subTreeRoot.CompareTo(subTreeRoot.Left))
            {
                case -1: return (TreapNode<TKey>)RotateRight(subTreeRoot);
                case 0:  return subTreeRoot;
                case 1:  return (TreapNode<TKey>)RotateLeft(subTreeRoot);
                default: throw new InvalidOperationException();
            }
        }

        private TreapNode<TKey> Find(TreapNode<TKey> subTreeRoot, TKey item)
        {
            if (subTreeRoot == null) return null;

            switch (item.CompareTo(subTreeRoot.Key))
            {
                case -1: return Find(subTreeRoot.Left, item);
                case 0: return subTreeRoot;
                case 1: return Find(subTreeRoot.Right, item);
                default: throw new NotSupportedException();         
            }
        }
        
        private TreapNode<TKey> EnsureMaxHeapConstraint(TreapNode<TKey> subTreeRoot)
        {
            int leftComparisonResult = subTreeRoot.CompareTo(subTreeRoot?.Left ?? MinPriorityNode);
            int rightComparisonResult = subTreeRoot.CompareTo(subTreeRoot?.Right ?? MinPriorityNode);
            if (leftComparisonResult < 0 && rightComparisonResult < 0)
            {
                return subTreeRoot.Left.CompareTo(subTreeRoot.Right) >= 0
                    ? (TreapNode<TKey>)RotateRight(subTreeRoot)
                    : (TreapNode<TKey>)RotateLeft(subTreeRoot);
            }
            else if (rightComparisonResult < 0)
            {
                return (TreapNode<TKey>)RotateLeft(subTreeRoot);
            }
            else if (leftComparisonResult < 0)
            {
                return (TreapNode<TKey>)RotateRight(subTreeRoot);
            }
            else
            {
                return subTreeRoot;
            }
        }

        public BinaryTreeNode<TKey, int> Root => _root;

        public void Add(TKey key)
        {
            _root = GetUpdatedSubTree(CreateNodeFor(key), _root);
        }

        public bool Contains(TKey item)
        {
            TreapNode<TKey> foundNode = Find(_root, item);

            return foundNode != null;
        }

        public Treap() :
            this(() => PriorityGenerator.Next(int.MinValue + 1, int.MaxValue))
        {

        }

        internal Treap(Func<int> priorityGenerator)
        {
            _priorityGenerator = priorityGenerator;
        }
    }
}

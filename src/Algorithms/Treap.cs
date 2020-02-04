using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    using static TreeUtilityMethods;

    public class Treap<TKey> where TKey : IComparable<TKey>
    {
        private static TreapNode MinPriorityNode = new TreapNode(default, int.MinValue);

        public class TreapNode :
            IBinaryTreeNode<TKey, TreapNode>,
            IComparable<TKey>,
            IComparable<TreapNode>
        {

            public TreapNode(TKey key, int priority)
            {
                Key = key;
                Priority = priority;
            }

            public TKey Key { get; }

            public int Priority { get; }

            public int CompareTo(TreapNode other) => Priority.CompareTo(other.Priority);

            public int CompareTo(TKey other) => Key.CompareTo(other);

            public TreapNode Left { get; set; }

            public TreapNode Right { get; set; }
        }

        private static readonly Random PriorityGenerator = new Random();

        private readonly Func<int> _priorityGenerator;

        private int GeneratePriority()
        {
            return Math.Max(int.MinValue + 1, _priorityGenerator.Invoke());
        }

        private TreapNode _root;

        private TreapNode CreateNodeFor(TKey key) => new TreapNode(key, GeneratePriority());

        private TreapNode GetUpdatedSubTree(TreapNode newNode, TreapNode subTreeRoot = null)
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

        private TreapNode Find(TreapNode subTreeRoot, TKey item)
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
        
        private TreapNode EnsureMaxHeapConstraint(TreapNode subTreeRoot)
        {
            int leftComparisonResult = subTreeRoot.CompareTo(subTreeRoot?.Left ?? MinPriorityNode);
            int rightComparisonResult = subTreeRoot.CompareTo(subTreeRoot?.Right ?? MinPriorityNode);
            if (leftComparisonResult < 0 && rightComparisonResult < 0)
            {
                return subTreeRoot.Left.CompareTo(subTreeRoot.Right) >= 0
                    ? RotateRight(subTreeRoot)
                    : RotateLeft(subTreeRoot);
            }
            else if (rightComparisonResult < 0)
            {
                return RotateLeft(subTreeRoot);
            }
            else if (leftComparisonResult < 0)
            {
                return RotateRight(subTreeRoot);
            }
            else
            {
                return subTreeRoot;
            }
        }

        public TreapNode Root => _root;

        public void Add(TKey key)
        {
            _root = GetUpdatedSubTree(CreateNodeFor(key), _root);
        }

        public bool Contains(TKey item)
        {
            TreapNode foundNode = Find(_root, item);

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

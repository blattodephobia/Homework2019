using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using static Algorithms.TreeUtilityMethods;

namespace Algorithms
{
    public class AvlTree<TKey> where TKey : IComparable<TKey>
    {
        [DebuggerDisplay("{" + nameof(Name) + "}")]
        private class UnbalancedCase
        {
            private static readonly UnbalancedCase LeftRightCase = new UnbalancedCase(
                nameof(LeftRightCase),
                isMatchPredicate: subTree => subTree.Right == null && (subTree.Left?.Right?.IsLeaf() ?? false) && subTree.Left.Left == null,
                rotationCallback: subTree => RotateRight(RotateLeft(subTree)));

            private static readonly UnbalancedCase LeftLeftCase = new UnbalancedCase(
                nameof(LeftLeftCase),
                isMatchPredicate: subTree => subTree.Right == null && (subTree.Left?.Left?.IsLeaf() ?? false) && subTree.Left.Right == null,
                rotationCallback: subTree => RotateRight(subTree));

            private static readonly UnbalancedCase RightLeftCase = new UnbalancedCase(
                nameof(RightLeftCase),
                isMatchPredicate: subTree => subTree.Left == null && (subTree.Right?.Left?.IsLeaf() ?? false) && subTree.Right.Right == null,
                rotationCallback: subTree => RotateLeft(RotateRight(subTree)));

            private static readonly UnbalancedCase RightRightCase = new UnbalancedCase(
                nameof(RightRightCase),
                isMatchPredicate: subTree => subTree.Left == null && (subTree.Right?.Right?.IsLeaf() ?? false) && subTree.Right.Left == null,
                rotationCallback: subTree => RotateLeft(subTree));

            private static readonly UnbalancedCase[] UnbalancedCases = new[] { LeftRightCase, LeftLeftCase, RightLeftCase, RightRightCase };

            public static AvlTreeNode<TKey> EnsureBalanced(AvlTreeNode<TKey> subTreeNode)
            {
                UnbalancedCase @case = UnbalancedCases.FirstOrDefault(c => c.IsMatch(subTreeNode));
                return @case?.GetBalancedSubTree(subTreeNode) ?? subTreeNode;
            }

            private readonly Func<AvlTreeNode<TKey>, bool> _isMatchPredicate;
            private readonly Func<AvlTreeNode<TKey>, AvlTreeNode<TKey>> _rotationCallback;

            public string Name { get; }

            private UnbalancedCase(string name, Func<AvlTreeNode<TKey>, bool> isMatchPredicate, Func<AvlTreeNode<TKey>, AvlTreeNode<TKey>> rotationCallback)
            {
                Name = name;
                _isMatchPredicate = isMatchPredicate;
                _rotationCallback = rotationCallback;
            }

            public bool IsMatch(AvlTreeNode<TKey> subTreeNode) => _isMatchPredicate.Invoke(subTreeNode);

            public AvlTreeNode<TKey> GetBalancedSubTree(AvlTreeNode<TKey> subTreeNode) => _rotationCallback.Invoke(subTreeNode);
        }

        public AvlTreeNode<TKey> Root { get; private set; }

        public void Add(TKey key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            Root = GetUpdatedRoot(Root, new AvlTreeNode<TKey>(key));
        }

        public void AddRange(IEnumerable<TKey> collection)
        {
            foreach (TKey key in collection)
            {
                Add(key);
            }
        }

        private AvlTreeNode<TKey> GetUpdatedRoot(AvlTreeNode<TKey> subTreeRoot, AvlTreeNode<TKey> key)
        {
            if (subTreeRoot == null) return key;

            if (key.CompareTo(subTreeRoot) < 0)
            {
                subTreeRoot.Left = GetUpdatedRoot(subTreeRoot.Left, key);
            }
            else
            {
                subTreeRoot.Right = GetUpdatedRoot(subTreeRoot.Right, key);
            }

            AvlTreeNode<TKey> result = subTreeRoot;
            int leavesBalanceFactor = subTreeRoot.GetBalanceFactor();
            if (Math.Abs(leavesBalanceFactor) > 1)
            {
                var balanceOperation = leavesBalanceFactor < 0
                    ? (Func<AvlTreeNode<TKey>, AvlTreeNode<TKey>>)RotateLeft
                    : (Func<AvlTreeNode<TKey>, AvlTreeNode<TKey>>)RotateRight;

                result = balanceOperation.Invoke(result);
            }

            return result;
        }
    }
}

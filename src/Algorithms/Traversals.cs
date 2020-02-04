using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    public static class Traversals
    {
        public static IEnumerable<TNode> BreadthFirstSearch<TNode>(this TNode subTreeRoot) where TNode : IBinaryTreeNode<TNode>
        {
            return new BreadthFirstSearchEnumerable<TNode>(subTreeRoot);
        }
    }
}

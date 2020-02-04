using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    public interface IBinaryTreeNode<TKey, TNode> : IBinaryTreeNode<TNode> where TNode : IBinaryTreeNode<TKey, TNode>
    {
        TKey Key { get; }
    }

    public interface IBinaryTreeNode<TNode> where TNode : IBinaryTreeNode<TNode>
    {
        TNode Left { get; set; }

        TNode Right { get; set; }
    }
}

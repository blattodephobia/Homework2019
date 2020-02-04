using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    public interface INode<TKey>
    {
        TKey Key { get; }
    }
}

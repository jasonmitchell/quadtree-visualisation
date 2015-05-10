using System;
using System.Collections.Generic;

namespace SpatialMapping.QuadTree
{
    interface IQuadTreeElement<T> where T : IHasRectangle
    {
        void Insert(T item);
        void Remove(T item);
        void ForEach(Action<QuadTreeNode<T>> action);
        List<T> Intersects(FRectangle rectangle);
        int Count { get; }
        List<QuadTreeNode<T>> Nodes { get; }
        List<T> SubTreeContents { get; }
    }
}

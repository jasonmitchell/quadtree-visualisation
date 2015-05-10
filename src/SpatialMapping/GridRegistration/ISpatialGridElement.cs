using System.Collections.Generic;

namespace SpatialMapping.GridRegistration
{
    interface ISpatialGridElement<T> where T : IHasRectangle
    {
        void Insert(T item);
        void Remove(T item);
        void ForEach(SpatialGrid<T>.SpatialGridAction action);
        int Count { get; }
        List<SpatialGridCell<T>> Contents { get; }
    }
}

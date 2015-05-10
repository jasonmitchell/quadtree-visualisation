using System.Collections.Generic;
using System.Windows;

namespace SpatialMapping.GridRegistration
{
    public class SpatialGridCell<T> : ISpatialGridElement<T> where T : IHasRectangle
    {
        private readonly List<SpatialGridCell<T>> contents = new List<SpatialGridCell<T>>();

        public SpatialGridCell(Point location, float cellSize)
        {
            Location = location;
            Size = cellSize;
        }

        public void Insert(T item)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(T item)
        {
            throw new System.NotImplementedException();
        }

        public void ForEach(SpatialGrid<T>.SpatialGridAction action)
        {
            action(this);
        }

        public int Count
        {
            get { throw new System.NotImplementedException(); }
        }

        public List<SpatialGridCell<T>> Contents
        {
            get { return contents; }
        }

        public Point Location { get; private set; }
        public float Size { get; private set; }
    }
}

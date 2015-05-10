using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace SpatialMapping.GridRegistration
{
    public class SpatialGrid<T> : ISpatialGridElement<T> where T : IHasRectangle
    {
        public delegate void SpatialGridAction(SpatialGridCell<T> cell);

        private readonly List<SpatialGridCell<T>> cells = new List<SpatialGridCell<T>>();

        public SpatialGrid(Point location, int sceneWidth, int sceneHeight, int cellSize)
        {
            int columns = sceneWidth/cellSize;
            int rows = sceneHeight/cellSize;

            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    Point cellLocation = new Point(location.X + (x * cellSize), location.Y + (y * cellSize));
                    cells.Add(new SpatialGridCell<T>(cellLocation, cellSize));
                }
            }
        }

        public void Insert(T item)
        {
            throw new NotImplementedException();
        }

        public void Remove(T item)
        {
            throw new NotImplementedException();
        }

        public void ForEach(SpatialGridAction action)
        {
            foreach(SpatialGridCell<T> cell in Cells)
                cell.ForEach(action);
        }

        public List<SpatialGridCell<T>> Cells
        {
            get { return cells; }
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public List<SpatialGridCell<T>> Contents
        {
            get { throw new NotImplementedException(); }
        }
    }
}

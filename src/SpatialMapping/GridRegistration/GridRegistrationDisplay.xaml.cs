using System.Windows.Controls;
using System.Windows;
using System;

namespace SpatialMapping.GridRegistration
{
    public partial class GridRegistrationDisplay
    {
        public GridRegistrationDisplay()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            GenerateGridDiagram();
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            GenerateGridDiagram();
        }

        private void GenerateGridDiagram()
        {
            GridCanvas.Children.Clear();
            Random r = new Random();

            int totalItems;
            if (!Int32.TryParse(ItemCount.Text, out totalItems))
                totalItems = 5;

            int cellSize;
            if (!Int32.TryParse(CellSize.Text, out cellSize))
                cellSize = 100;

            var spatialGrid = new SpatialGrid<TestObject>(new Point(0, 0), (int)GridCanvas.Width, (int)GridCanvas.Height, cellSize);

            //for (int i = 0; i < totalItems; i++)
            //    quadTree.Insert(new TestObject(r, QuadCanvas));

            foreach (var cell in spatialGrid.Cells)
                VisualizationHelper.DrawRectangle(r, GridCanvas, cell.Location, cell.Size, cell.Size);
            //foreach (var node in quadTree.Nodes)
            //    VisualizationHelper.DrawRectangle(r, QuadCanvas, node.Location, node.Width, node.Height);
        }
    }
}

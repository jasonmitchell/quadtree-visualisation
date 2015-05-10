using System;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;

namespace SpatialMapping.QuadTree
{
    public partial class QuadTreeDisplay
    {
        private const int RectWidth = 100;
        private const int RectHeight = 100;

        private FRectangle intersectionRectangle;
        private readonly Rectangle visualisationRectangle;
        private QuadTree<TestObject> quadTree;

        public QuadTreeDisplay()
        {
            InitializeComponent();
            intersectionRectangle = new FRectangle(0, 0, RectWidth, RectHeight);

            visualisationRectangle = new Rectangle();
            visualisationRectangle.Width = intersectionRectangle.Width;
            visualisationRectangle.Height = intersectionRectangle.Height;
            visualisationRectangle.Fill = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0));
        }

        private void ControlLoaded(object sender, RoutedEventArgs e)
        {
            GenerateQuadTreeDiagram();
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            GenerateQuadTreeDiagram();
        }

        private void MouseClicked(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(QuadCanvas);
            Point bottomRight = position;
            bottomRight.X += RectWidth;
            bottomRight.Y += RectHeight;
            
            intersectionRectangle = new FRectangle(position, bottomRight);

            Canvas.SetTop(visualisationRectangle, intersectionRectangle.Top);
            Canvas.SetLeft(visualisationRectangle, intersectionRectangle.Left);

            IntersectingItems.Text = quadTree.Intersects(intersectionRectangle).Count.ToString();
        }

        private void GenerateQuadTreeDiagram()
        {
            QuadCanvas.Children.Clear();
            Random r = new Random();

            int propagationThreshold;
            if (!Int32.TryParse(Threshold.Text, out propagationThreshold))
                propagationThreshold = 2;

            int totalItems;
            if (!Int32.TryParse(ItemCount.Text, out totalItems))
                totalItems = 10;

            quadTree = new QuadTree<TestObject>(propagationThreshold, new Point(0, 0), (float)QuadCanvas.Width, (float)QuadCanvas.Height);

            for (int i = 0; i < totalItems; i++)
                quadTree.Insert(new TestObject(r, QuadCanvas));

            foreach (var node in quadTree.Nodes)
                VisualizationHelper.DrawRectangle(r, QuadCanvas, node.Location, node.Width, node.Height);

            QuadCanvas.Children.Add(visualisationRectangle);
        }
    }
}

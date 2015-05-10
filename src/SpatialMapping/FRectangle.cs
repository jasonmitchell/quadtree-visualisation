using System.Windows;

namespace SpatialMapping
{
    public struct FRectangle
    {
        private Point topLeft;
        private Point bottomRight;

        public FRectangle(Point topleft, Point bottomright)
        {
            topLeft = topleft;
            bottomRight = bottomright;
        }

        public FRectangle(float x, float y, float width, float height)
        {
            topLeft = new Point(x, y);
            bottomRight = new Point(x + width, y + height);
        }

        public bool Contains(Point point)
        {
            return (topLeft.X <= point.X && bottomRight.X >= point.X &&
                    topLeft.Y <= point.Y && bottomRight.Y >= point.Y);
        }

        public bool Contains(FRectangle rectangle)
        {
            return (rectangle.TopLeft.X >= TopLeft.X &&
                    rectangle.TopLeft.Y >= TopLeft.Y &&
                    rectangle.BottomRight.X <= BottomRight.X &&
                    rectangle.BottomRight.Y <= BottomRight.Y);
        }

        public bool Intersects(FRectangle rectangle)
        {
            return (!( Bottom < rectangle.Top ||
                       Top > rectangle.Bottom ||
                       Right < rectangle.Left ||
                       Left > rectangle.Right ));
        }

        public Point TopLeft
        {
            get { return topLeft; }
            set { topLeft = value; }
        }

        public Point TopRight
        {
            get { return new Point(bottomRight.X, topLeft.Y); }
            set
            {
                bottomRight.X = value.X;
                topLeft.Y = value.Y;
            }
        }

        public Point BottomRight
        {
            get { return bottomRight; }
            set { bottomRight = value; }
        }

        public Point BottomLeft
        {
            get { return new Point(topLeft.X, bottomRight.Y); }
            set
            {
                topLeft.X = value.X;
                bottomRight.Y = value.Y;
            }
        }

        public float Top
        {
            get { return (float)TopLeft.Y; }
            set { topLeft.Y = value; }
        }

        public float Left
        {
            get { return (float)TopLeft.X; }
            set { topLeft.X = value; }
        }

        public float Bottom
        {
            get { return (float)BottomRight.Y; }
            set { bottomRight.Y = value; }
        }

        public float Right
        {
            get { return (float)BottomRight.X; }
            set { bottomRight.X = value; }
        }

        public float Width
        {
            get { return (float)(bottomRight.X - topLeft.X); }
        }

        public float Height
        {
            get { return (float)(bottomRight.Y - topLeft.Y); }
        }
    }
}

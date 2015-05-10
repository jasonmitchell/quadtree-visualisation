using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SpatialMapping.QuadTree
{
    public class QuadTreeNode<T> : IQuadTreeElement<T> where T : IHasRectangle
    {
        private readonly int propagationThreshold;
        private readonly FRectangle rectangle;

        private readonly QuadTreeNode<T> parentNode;
        private readonly List<QuadTreeNode<T>> childNodes = new List<QuadTreeNode<T>>(4);

        public QuadTreeNode(int propagationThreshold, QuadTreeNode<T> parentNode, Point location, float width, float height)
        {
            Contents = new List<T>();

            this.propagationThreshold = propagationThreshold;
            this.parentNode = parentNode;

            rectangle = new FRectangle((float) location.X, (float) location.Y, width, height);

            Location = location;
            Width = width;
            Height = height;
        }

        public void Insert(T item)
        {
            if (InsertInChildNode(item))
                return;

            Contents.Add(item);

            if (!IsPartioned && Contents.Count >= propagationThreshold)
                PartitionNode();
        }

        public void Remove(T item)
        {
            if(Contents.Contains(item))
                Contents.Remove(item);
        }

        private void RemoveAt(int index)
        {
            if (index < Contents.Count)
                Contents.RemoveAt(index);
        }

        private bool InsertInChildNode(T item)
        {
            if (!IsPartioned)
                return false;

            foreach(QuadTreeNode<T> node in childNodes)
            {
                if(node.rectangle.Contains(item.Rectangle))
                {
                    node.Insert(item);
                    return true;
                }
            }

            return false;
        }

        private void PartitionNode()
        {
            float nodeWidth = Width/2;
            float nodeHeight = Height/2;

            childNodes.Add(new QuadTreeNode<T>(propagationThreshold, this, Location, nodeWidth, nodeHeight));
            childNodes.Add(new QuadTreeNode<T>(propagationThreshold, this, new Point(Location.X + nodeWidth, Location.Y), nodeWidth, nodeHeight));
            childNodes.Add(new QuadTreeNode<T>(propagationThreshold, this, new Point(Location.X, Location.Y + nodeHeight), nodeWidth, nodeHeight));
            childNodes.Add(new QuadTreeNode<T>(propagationThreshold, this, new Point(Location.X + nodeWidth, Location.Y + nodeHeight), nodeWidth, nodeHeight));

            int i = 0;
            while (i < Contents.Count)
            {
                if (!PushItemDown(i))
                    i++;
            }
        }

        public void ForEach(Action<QuadTreeNode<T>> action)
        {
            action(this);

            foreach (QuadTreeNode<T> node in childNodes)
                node.ForEach(action);
        }

        public List<T> Intersects(FRectangle intersectionRectangle)
        {
            List<T> objects = new List<T>();

            if(intersectionRectangle.Intersects(rectangle))
            {
                objects.AddRange(Contents.Where(item => intersectionRectangle.Intersects(item.Rectangle)));

                foreach (QuadTreeNode<T> node in childNodes)
                    objects.AddRange(node.Intersects(intersectionRectangle));
            }

            return objects;
        }

        public bool PushItemDown(int index)
        {
            if(InsertInChildNode(Contents[index]))
            {
                RemoveAt(index);
                return true;
            }

            return false;
        }

        public void PushItemUp(int index)
        {
            T item = Contents[index];

            RemoveAt(index);
            parentNode.Insert(item);
        }

        private bool IsPartioned
        {
            get { return childNodes.Count != 0; }
        }

        public List<T> Contents { get; private set; }

        public int Count
        {
            get
            {
                int count = Contents.Count;

                foreach (QuadTreeNode<T> node in childNodes)
                    count += node.Contents.Count;

                return count;
            }
        }

        public List<QuadTreeNode<T>> Nodes
        {
            get
            {
                List<QuadTreeNode<T>> nodes = new List<QuadTreeNode<T>>(childNodes);

                foreach (QuadTreeNode<T> node in childNodes)
                    nodes.AddRange(node.Nodes);

                return nodes;
            }
        }

        public List<T> SubTreeContents
        {
            get
            {
                List<T> results = new List<T>(Contents);

                foreach (QuadTreeNode<T> node in childNodes)
                    results.AddRange(node.Contents);

                return results;
            }
        }

        public Point Location { get; private set; }
        public float Width { get; private set; }
        public float Height { get; private set; }
    }
}

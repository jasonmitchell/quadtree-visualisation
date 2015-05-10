using System;
using System.Collections.Generic;
using System.Windows;

namespace SpatialMapping.QuadTree
{
    public class QuadTree<T> : IQuadTreeElement<T> where T : IHasRectangle
    {
        private readonly QuadTreeNode<T> rootNode;

        public QuadTree(int propagationThreshold, Point location, float width, float height)
        {
            PropagationThreshold = propagationThreshold;
            rootNode = new QuadTreeNode<T>(propagationThreshold, null, location, width, height);
        }

        public void Insert(T item)
        {
            rootNode.Insert(item);
        }

        public void Remove(T item)
        {
            rootNode.Remove(item);
        }

        public void ForEach(Action<QuadTreeNode<T>> action)
        {
            rootNode.ForEach(action);
        }

        public List<T> Intersects(FRectangle intersectionRectangle)
        {
            return rootNode.Intersects(intersectionRectangle);
        }

        public int PropagationThreshold { get; private set; }

        public int Count
        {
            get { return rootNode.Count; }
        }

        public List<QuadTreeNode<T>> Nodes
        {
            get
            {
                List<QuadTreeNode<T>> nodes = new List<QuadTreeNode<T>>(rootNode.Nodes);
                nodes.Add(rootNode);

                return nodes;
            }
        }

        public List<T> SubTreeContents
        {
            get { return rootNode.SubTreeContents; }
        }
    }
}

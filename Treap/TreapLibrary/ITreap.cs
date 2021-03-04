using System;

namespace TreapLibrary
{
    public interface ITreap<T> where T : IComparable
    {
        
        public T this[int position] { get; }
        
        public int Count { get; }
        
        private int SizeOf(Node<T> vertex) => vertex?.Size ?? 0;

        private void Upd(Node<T> vertex)
        {
            if (vertex != null)
            {
                vertex.Size = SizeOf(vertex.LeftChild) + SizeOf(vertex.RightChild) + 1;
            }
        }
        
        public Node<T> Merge(Node<T> firstRoot, Node<T> secondRoot)
        {
            if (firstRoot == null)
            {
                return secondRoot;
            }

            if (secondRoot == null)
            {
                return firstRoot;
            }

            if (firstRoot.Priority > secondRoot.Priority)
            {
                firstRoot.RightChild = Merge(firstRoot.RightChild, secondRoot);
                Upd(firstRoot);
                return firstRoot;
            }
            else
            {
                secondRoot.LeftChild = Merge(firstRoot, secondRoot.LeftChild);
                Upd(secondRoot);
                return secondRoot;
            }
        }

        public (Node<T> FirstRoot, Node<T> SecondRoot) Split(Node<T> root, T key)
        {
            if (root == null)
            {
                return (FirstRoot: null, SecondRoot: null);
            }

            if (root.Key.CompareTo(key) > 0)
            {
                var leftRoot = Split(root.LeftChild, key);
                root.LeftChild = leftRoot.SecondRoot;
                Upd(root);
                return (FirstRoot: leftRoot.FirstRoot, SecondRoot: root);
            }
            else
            {
                var rightRoot = Split(root.RightChild, key);
                root.RightChild = rightRoot.FirstRoot;
                Upd(root);
                return (FirstRoot: root, SecondRoot: rightRoot.SecondRoot);
            }
        }

        public (Node<T> FirstRoot, Node<T> SecondRoot) SplitSize(Node<T> root, int size)
        {
            if (root == null)
            {
                return (FirstRoot: null, SecondRoot: null);
            }

            if (SizeOf(root.LeftChild) < size)
            {
                var rightRoot = SplitSize(root.RightChild, size - SizeOf(root.LeftChild) - 1);
                root.RightChild = rightRoot.FirstRoot;
                Upd(root);
                return (FirstRoot: root, SecondRoot: rightRoot.SecondRoot);
            }
            else
            {
                var leftRoot = SplitSize(root.LeftChild, size);
                root.LeftChild = leftRoot.SecondRoot;
                Upd(root);
                return (FirstRoot: leftRoot.FirstRoot, SecondRoot: root);
            }
        }

        public void Add(T key);

        public void RemoveAt(int position);
    }
}
using System;
using System.Collections.Generic;

namespace TreapLibrary
{
    public class TreapExplicit<T> : ITreap<T> where T : IComparable
    {

        private Node<T> root;
        private List<string> items = new List<string>();

        public int Count => root?.Size ?? 0;

        public void Add(T key)
        {
            Node<T> node = new Node<T>(key);
            var iTreap = this as ITreap<T>;

            var roots = iTreap.Split(root, key);
            root = iTreap.Merge(iTreap.Merge(roots.FirstRoot, node), roots.SecondRoot);
        }

        public void RemoveAt(int position)
        {
            var iTreap = this as ITreap<T>;
            
            if (root == null)
            {
                throw new ArgumentException("Treap is not exist");
            }

            if (position >= Count || position < 0)
            {
                throw new IndexOutOfRangeException("Index out of the range");
            }

            var roots = iTreap.SplitSize(root, position);
            var rootSplit = iTreap.SplitSize(roots.SecondRoot, 1);

            root = iTreap.Merge(roots.FirstRoot, rootSplit.SecondRoot);
        }

        public T this[int position]
        {
            get
            {
                var iTreap = this as ITreap<T>;
                if (root == null)
                {
                    throw new ArgumentException("Treap is not exist");
                }

                if (position >= Count || position < 0)
                {
                    throw new IndexOutOfRangeException("Index out of the range");
                }

                var roots = iTreap.SplitSize(root, position);
                var rootSplit = iTreap.SplitSize(roots.SecondRoot, 1);
                T answer = rootSplit.FirstRoot.Key;

                root = iTreap.Merge(roots.FirstRoot, iTreap.Merge(rootSplit.FirstRoot, rootSplit.SecondRoot));
                
                return answer;
            }
        }

        public void Remove(T key)
        {
            
            var iTreap = this as ITreap<T>;

            var roots = iTreap.Split(root, key);
            if (roots.FirstRoot != null)
            {
                var rootsLeft = iTreap.SplitSize(roots.FirstRoot, roots.FirstRoot.Size - 1);
                if (rootsLeft.SecondRoot != null && rootsLeft.SecondRoot.CompareTo(key) == 0)
                {
                    root = iTreap.Merge(rootsLeft.FirstRoot, roots.SecondRoot);
                }
                else
                {
                    root = iTreap.Merge(iTreap.Merge(rootsLeft.FirstRoot, rootsLeft.SecondRoot), roots.SecondRoot);
                }
            }
            else
            {
                root = iTreap.Merge(roots.FirstRoot, roots.SecondRoot);
            }

        }

        public TreapExplicit()
        {
            root = null;
        }

        public TreapExplicit(T[] keys)
        {
            root = null;
            foreach (var item in keys)
            {
                Add(item);
            }
        }

        private void Print(Node<T> root)
        {
            if (root != null)
            {
                Print(root.LeftChild);
                items.Add(root.Key.ToString());
                Print(root.RightChild);
            }
        }

        public override string ToString()
        {
            items.Clear();
            Print(root);
            return String.Join(" ", items);
        }
    }
}
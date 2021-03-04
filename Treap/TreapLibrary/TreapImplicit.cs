using System;
using System.Collections.Generic;

namespace TreapLibrary
{
    public class TreapImplicit<T> : ITreap<T> where T : IComparable
    {
        private Node<T> root;
        private List<string> items = new List<string>();
        
        public int Count => root?.Size ?? 0;
        
        public void RemoveAt(int position)
        {
            var iTreap = this as ITreap<T>;
            
            if (root == null)
            {
                throw new ArgumentException("Treap is not exist");
            }

            if (position >= Count)
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

                if (position >= Count)
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

        public void Add(T key)
        {
            root = (this as ITreap<T>).Merge(root, new Node<T>(key));
        }

        public void Add(T key, int position)
        {
            if (position < 0 || Count <= position)
            {
                throw new IndexOutOfRangeException("Index out of the range");
            }
            
            var iTreap = this as ITreap<T>;
            var roots = iTreap.SplitSize(root, position);

            root = iTreap.Merge(iTreap.Merge(roots.FirstRoot, new Node<T>(key)), roots.SecondRoot);
        }

        public TreapImplicit(T[] keys)
        {
            foreach (var key in keys)
            {
                Add(key);
            }
        }

        public TreapImplicit()
        {
            root = null;
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
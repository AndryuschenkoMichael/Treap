using System;

namespace TreapLibrary
{
    public class Node<T> : IComparable where T : IComparable
    {
        public int Size { get; set; }
        public Node<T> LeftChild { get; set; }
        public Node<T> RightChild { get; set; }
        public T Key { get; }
        public int Priority { get; }
        
        public Node(T key)
        {
            Key = key;
            LeftChild = null;
            RightChild = null;
            Priority = (new Random()).Next();
            Size = 1;
        }

        public int CompareTo(object obj) => (obj is Node<T>) ? Key.CompareTo((obj as Node<T>).Key) : 0;
    }
}
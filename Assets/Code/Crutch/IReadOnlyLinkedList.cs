using System.Collections.Generic;

namespace Game
{
    public interface IReadOnlyLinkedList<T> : IReadOnlyCollection<T>
    {
        public LinkedListNode<T> Last { get; }
        public LinkedListNode<T> First { get; }

        public bool Contains(T value);

        public LinkedListNode<T> Find(T value);
        public LinkedListNode<T> FindLast(T value);
    }
}

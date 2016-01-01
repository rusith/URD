using System;
using System.Collections;
using System.Collections.Generic;

namespace URD
{
    public class DOStack<T> : IEnumerable<T>
    {
        private readonly T[] _items;

        private int _top;

        private int _count;

        public int Count { get { return _count; } }

        public DOStack(int capacity)
        {
            _items = new T[capacity];
        }

        public void Push(T item)
        {
            _count += 1;
            _count = _count > _items.Length ? _items.Length : _count;

            _items[_top] = item;
            _top = (_top + 1) % _items.Length;
        }

        public T Pop()
        {
            _count -= 1;
            _count = _count < 0 ? 0 : _count;

            _top = (_items.Length + _top - 1) % _items.Length;
            return _items[_top];
        }


        public T Peek()
        {
            return _items[(_items.Length + _top - 1) % _items.Length];
        }

        public T GetItem(int index)
        {
            if (index > Count) throw new InvalidOperationException("Index out of bounds");
            else return _items[(_items.Length + _top - (index + 1)) % _items.Length];
        }

        public void Clear()
        {
            _count = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++) yield return GetItem(i);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

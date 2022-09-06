using System;

namespace Nazio_LT.Core
{
    public interface IHeapableObject<T> : IComparable<T>
    {
        int HeapIndex { set; get; }
    }

    public class Heap<T> where T : IHeapableObject<T>
    {
        public Heap(int _size)
        {
            elements = new T[_size];
        }

        private T[] elements;
        public int count { private set; get; } = 0;

        public void Add(T _item)
        {
            _item.HeapIndex = count;
            elements[count] = _item;
            SortUp(_item);
            count++;
        }

        public T RemoveFirst()
        {
            T _result = elements[0];
            count--;
            elements[0] = elements[count];
            elements[0].HeapIndex = 0;
            SortDown(elements[0]);

            return _result;
        }

        public bool Contains(T _item) => Equals(elements[_item.HeapIndex], _item);
        public void UpdateItem(T _item) => SortUp(_item);

        private void SortDown(T _item)
        {
            while (true)
            {
                int _leftChildID = GetChildID(_item.HeapIndex, false);
                int _rightChildID = GetChildID(_item.HeapIndex, true);
                int _swapID = 0;

                if (_leftChildID >= count) return;

                _swapID = _leftChildID;

                if (_rightChildID < count)
                {
                    if (elements[_leftChildID].CompareTo(elements[_rightChildID]) < 0)
                    {
                        _swapID = _rightChildID;
                    }
                }

                if (_item.CompareTo(elements[_swapID]) >= 0) return;

                SwapItem(elements[_swapID], _item);
            }
        }

        /// <summary>
        /// Remonte un item jusqu'a ce que
        /// </summary>
        /// <param name="_item"></param>
        private void SortUp(T _item)
        {
            int _parentID = GetParentID(_item.HeapIndex);

            while (true)
            {
                T _parent = elements[_parentID];

                if (_item.CompareTo(_parent) <= 0) break;

                SwapItem(_parent, _item);
                _parentID = GetParentID(_item.HeapIndex);
            }
        }

        private void SwapItem(T _a, T _b)
        {
            elements[_a.HeapIndex] = _b;
            elements[_b.HeapIndex] = _a;

            int _aID = _a.HeapIndex;
            _a.HeapIndex = _b.HeapIndex;
            _b.HeapIndex = _aID;
        }

        public T GetItem(int _id) => elements[_id];

        private int GetParentID(int _id) => (_id - 1) / 2;
        private int GetChildID(int _id, bool _rightChild) => _id * 2 + (_rightChild ? 2 : 1);

        public T GetFirst()
        {
            if (elements.Length >= 1) return elements[0];
            return default(T);
        }

        public bool IsEmpty => count <= 0;
    }
}
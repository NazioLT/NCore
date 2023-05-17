using System;

namespace Nazio_LT.Tools.Core
{
    public interface IHeapableObject<T> : IComparable<T>
    {
        int HeapIndex { set; get; }
    }

    public class Heap<T> where T : IHeapableObject<T>
    {
        public Heap(int size)
        {
            m_elements = new T[size];
        }

        private T[] m_elements;
        private int m_count = 0;

        public void Add(T item)
        {
            item.HeapIndex = m_count;
            m_elements[m_count] = item;
            SortUp(item);
            m_count++;
        }

        public T RemoveFirst()
        {
            T result = m_elements[0];
            m_count--;
            m_elements[0] = m_elements[m_count];
            m_elements[0].HeapIndex = 0;
            SortDown(m_elements[0]);

            return result;
        }

        public bool Contains(T item) => Equals(m_elements[item.HeapIndex], item);
        public void UpdateItem(T item) => SortUp(item);


        public T GetItem(int id) => m_elements[id];

        public T GetFirst()
        {
            if (m_elements.Length >= 1) return m_elements[0];
            return default(T);
        }

        private void SortDown(T item)
        {
            while (true)
            {
                int leftChildID = GetChildID(item.HeapIndex, false);
                int rightChildID = GetChildID(item.HeapIndex, true);
                int swapID = 0;

                if (leftChildID >= m_count) return;

                swapID = leftChildID;

                if (rightChildID < m_count)
                {
                    if (m_elements[leftChildID].CompareTo(m_elements[rightChildID]) < 0)
                    {
                        swapID = rightChildID;
                    }
                }

                if (item.CompareTo(m_elements[swapID]) >= 0) return;

                SwapItem(m_elements[swapID], item);
            }
        }

        /// <summary>
        /// Remonte un item jusqu'a ce que
        /// </summary>
        /// <param name="item"></param>
        private void SortUp(T item)
        {
            int parentID = GetParentID(item.HeapIndex);

            while (true)
            {
                T parent = m_elements[parentID];

                if (item.CompareTo(parent) <= 0) break;

                SwapItem(parent, item);
                parentID = GetParentID(item.HeapIndex);
            }
        }

        private void SwapItem(T a, T b)
        {
            m_elements[a.HeapIndex] = b;
            m_elements[b.HeapIndex] = a;

            int aID = a.HeapIndex;
            a.HeapIndex = b.HeapIndex;
            b.HeapIndex = aID;
        }

        private int GetParentID(int id) => (id - 1) / 2;
        private int GetChildID(int id, bool rightChild) => id * 2 + (rightChild ? 2 : 1);

        public bool IsEmpty => m_count <= 0;
        public int Count => m_count;
    }
}
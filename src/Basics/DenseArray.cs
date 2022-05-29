using System;
using System.Collections;
using System.Collections.Generic;

namespace TrenchesRTS.Basics
{
    /// <summary>
    /// A list stored in a contiguous block of memory and ensuring the packing of data.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DenseArray<T> : IList<T>
    {

        private readonly int _initialSize;
        private T[] _array;
        private int?[] _indexLookUpTable;

        public int Count { get; private set; } = 0;
        public int Capacity { get; }
        public bool IsReadOnly => false;

        public DenseArray(int capacity, int initialSize = 16)
        {
            _array = new T[_initialSize = initialSize];
            _indexLookUpTable = new int?[capacity];
            Capacity = capacity;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_array).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public void Add(T item)
        {
            if (Count == Capacity)
                throw new ArgumentOutOfRangeException(item?.ToString(), "Error: Maximum number of components are created");
            int i;
            for (i = 0; i < _indexLookUpTable.Length; i++)
                if (_indexLookUpTable[i] == null)
                    break;
            _indexLookUpTable[i] = Count;
            _array[Count] = item;
            /*TODO if count > _array.length, resize*/
        }
        public bool Remove(T item)
        {
            int i;
            for (i = 0; i < _indexLookUpTable.Length; i++)
                if (_indexLookUpTable[i].Equals(item))
                    break;

            throw new NotImplementedException();
        }

        public void Clear()
        {
            _array = new T[_initialSize];
            _indexLookUpTable = new int?[Capacity];
        }

        public bool Contains(T item)
        {
            for (var i = 0; i < Count; i++)
                if (_array[i].Equals(item))
                    return true;

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }


        public int IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            var translatedIndex = _indexLookUpTable[index];
            if (translatedIndex == null)
                return;
            _indexLookUpTable[index] = null;
            _array[(int)translatedIndex] = default;
        }

        public T this[int index]
        {
            get
            {
                var translatedIndex = _indexLookUpTable[index];
                if (translatedIndex == null)
                    return this[index] = default;
                return _array[(int)translatedIndex];
            }
            set => Insert(index, value);
        }
    }
}

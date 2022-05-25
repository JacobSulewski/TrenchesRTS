using System;
using System.Collections;
using System.Collections.Generic;

namespace TrenchesRTS.Managers
{
    public class ComponentManager
    {
        private readonly int _maxEntityCount;
        private int _componentCount = 0;

        private readonly Dictionary<Type, int> _componentIdMap = new(sizeof(int));
        private readonly ComponentMap[] _componentMaps = new ComponentMap[sizeof(int)];

        public ComponentManager(int maxEntityCount)
        {
            _maxEntityCount = maxEntityCount;
        }

        public int GetComponentId<T>() where T : struct => GetComponentId(typeof(T));
        public int GetComponentId(Type type) => _componentIdMap.TryGetValue(type, out var id) ? id : _createAndGetNewComponentId(type);
        public T GetComponent<T>(int entityId) where T : struct => ((ComponentMap<T>)_componentMaps[GetComponentId<T>()])[entityId];
        public void AddComponent<T>(int entityId, T component) where T : struct
        {
            var componentId = GetComponentId<T>();
            _componentMaps[componentId] ??= new ComponentMap<T>(_maxEntityCount);
            ((ComponentMap<T>)_componentMaps[componentId])[entityId] = component;
        }

        private int _createAndGetNewComponentId(Type type)
        {
            if (_componentCount == _componentMaps.Length)
                throw new ArgumentOutOfRangeException(type.Name, "Error: Maximum number of components are created");
            _componentIdMap.Add(type, _componentCount);
            return _componentCount++;
        }
    }

    /// <summary>
    /// A list stored in a contiguous block of memory ensures packing of data.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PackedArray<T> : IList<T>
    {
        private T[] _array;
        private readonly int _initialSize;

        public int Count { get; private set; } = 0;
        public int Capacity { get; }
        public bool IsReadOnly => false;

        public PackedArray(int capacity, int initialSize = 16)
        {
            _array = new T[_initialSize = initialSize];
            Capacity = capacity;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return (IEnumerator<T>) _array.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            _array[Count++] = item;
        }
        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            _array = new T[_initialSize];
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public T this[int index]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
    }
}
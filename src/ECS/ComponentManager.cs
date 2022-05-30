using System;
using System.Collections.Generic;

namespace TrenchesRTS.ECS
{
    public class ComponentManager
    {
        private readonly int _maxEntityCount;
        private int _count = 0;

        private readonly Dictionary<Type, int> _idMap = new(sizeof(int));
        private readonly Array[] _arrays = new Array[sizeof(int)];
        // TODO replace array with a dense array

        public ComponentManager(int maxEntityCount)
        {
            _maxEntityCount = maxEntityCount;
        }

        public int GetId<T>() where T : struct => GetId(typeof(T));
        public int GetId(Type type) => _idMap.TryGetValue(type, out var id) ? id : _createNewId(type);
        public T Get<T>(int entityId) where T : struct => ((T[])(_arrays[GetId<T>()] ??= _createNewArray<T>()))[entityId];
        public void Add<T>(int entityId, T component) where T : struct => ((T[])_arrays[GetId<T>()])[entityId] = component;

        private int _createNewId(Type type)
        {
            if (_count == _arrays.Length)
                throw new ArgumentOutOfRangeException(type.Name, "Error: Maximum number of components are created");

            _idMap.Add(type, _count);
            return _count++;
        }
        private T[] _createNewArray<T>() where T : struct => new T[_maxEntityCount];
    }
}
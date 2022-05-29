using System;
using System.Collections.Generic;

namespace TrenchesRTS.ECS
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
}
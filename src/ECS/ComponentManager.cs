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

        /// <summary>
        /// Get the Id for a given component type.
        /// </summary>
        /// <typeparam name="T"> The type of component to get the Id of. </typeparam>
        /// <exception cref="ArgumentOutOfRangeException"> Reached the max amount of unique types for components. </exception>>
        /// <returns> Gets the Id associated with the given component type. </returns>
        public int GetId<T>() where T : struct => GetId(typeof(T));
        /// <summary>
        /// Get the Id for a given component type.
        /// </summary>
        /// <typeparam name="T"> The type of component to get the Id of. </typeparam>
        /// <exception cref="ArgumentOutOfRangeException"> Reached the max amount of unique types for components. </exception>>
        /// <returns> Gets the Id associated with the given component type. </returns>
        public int GetId(Type type) => _idMap.TryGetValue(type, out var id) ? id : _createNewId(type);
        /// <summary>
        /// Get the component for the given entity id supplied given the type of the component.
        /// </summary>
        /// <typeparam name="T"> The type of component to get. </typeparam>
        /// <typeparam entityId="int"> The entity id associated with the given component. </typeparam>
        /// <exception cref="ArgumentOutOfRangeException"> Reached the max amount of unique types for components. </exception>>
        /// <returns> The component associated with the supplied entityId. </returns>
        public T Get<T>(int entityId) where T : struct => ((T[])(_arrays[GetId<T>()] ??= _createNewArray<T>()))[entityId];
        /// <summary>
        /// Add the given component to the associated entityId.
        /// </summary>
        /// <typeparam name="T"> The type of the component. </typeparam>
        /// <param name="entityId"> The entityId to associate with the component. </param>
        /// <param name="component"> The component to add. </param>
        /// <exception cref="ArgumentOutOfRangeException"> Reached the max amount of unique types for components. </exception>>
        public void Add<T>(int entityId, T component) where T : struct => ((T[])_arrays[GetId<T>()])[entityId] = component;

        private int _createNewId(Type type)
        {
            // Check to see if there is room for a new type of component
            if (_count == _arrays.Length)
                throw new ArgumentOutOfRangeException(type.Name, "Error: Maximum number of components are created");

            _idMap.Add(type, _count);
            return _count++;
        }
        private T[] _createNewArray<T>() where T : struct => new T[_maxEntityCount];
    }
}
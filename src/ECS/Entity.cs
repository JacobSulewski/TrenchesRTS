using System;
using System.Diagnostics;
using TrenchesRTS.ECS.Interfaces;

namespace TrenchesRTS.ECS
{
    /// <summary>
    /// Entity is a wrapper around a unique public Id and ComponentBitMask.
    /// </summary>
    public sealed class Entity : IEntity
    {
        private readonly ComponentManager _componentManager;

        public int Id { get; }
        /// <summary>
        /// Bit mask to represent active components associated with @this.
        /// </summary>
        public int ComponentBitMask { get; private set; } = 0;

        /// <param name="id"> Unique int identifier for @this. </param>
        /// <param name="componentManager"> Stores components associated with @this. </param>
        public Entity(int id, ComponentManager componentManager)
        {
            Id = id;
            _componentManager = componentManager;
        }

        public IEntity AddComponent<T>(T component) where T : struct
        {
            try
            {
                _componentManager.Add(Id, component);
                ComponentBitMask += 1 << _componentManager.GetId<T>();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return this;
        }

        /// <summary>
        /// Try to get the component associated with @this.
        /// </summary>
        /// <typeparam name="T"> The type of the component to get. </typeparam>
        /// <param name="component"> The component returned. </param>
        /// <returns> Returns true if returned component is an active component to @this, false otherwise. </returns>
        public bool TryGetComponent<T>(out T component) where T : struct
        {
            try
            {
                component = _componentManager.Get<T>(Id);
                var componentId = _componentManager.GetId<T>();
                return componentId == (ComponentBitMask & componentId);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                component = default;
                return false;
            }
        }

        public void Dispose()
        {
            // Destroy all component associations with @this ('reset' @this)
            ComponentBitMask = 0;
        }
    }
}
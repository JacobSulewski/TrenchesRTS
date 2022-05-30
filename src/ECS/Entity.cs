using System;
using System.Diagnostics;
using TrenchesRTS.ECS.Interfaces;

namespace TrenchesRTS.ECS
{
    public sealed class Entity : IEntity
    {
        private readonly ComponentManager _componentManager;

        public int Id { get; }
        public int ComponentBitMask { get; private set; } = 0;

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

        public bool GetComponent<T>(out T component) where T : struct
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
            ComponentBitMask = 0;
        }
    }
}
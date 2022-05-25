using System;
using System.Diagnostics;
using TrenchesRTS.ECS.Interfaces;
using TrenchesRTS.Managers;

namespace TrenchesRTS.ECS
{
    public class Entity : IEntity
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
                _componentManager.AddComponent(Id, component);
                ComponentBitMask += 1 << _componentManager.GetComponentId<T>();
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
                component = _componentManager.GetComponent<T>(Id);
                var componentId = _componentManager.GetComponentId<T>();
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
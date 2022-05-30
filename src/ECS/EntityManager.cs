using System.Collections.Generic;
using TrenchesRTS.Basics;
using TrenchesRTS.ECS.Interfaces;

namespace TrenchesRTS.ECS
{
    public class EntityManager
    {
        private int _entityCount;
        private readonly Pool<IEntity> _entityPool;
        private readonly List<IEntity> _activeEntities = new();
        public IEnumerable<IEntity> ActiveEntities => _activeEntities.ToArray();

        /// <param name="maxEntityCount"> The max number of entities to create. </param>
        /// <param name="componentManager"> The component manager to hold all the components of the created entities</param>
        public EntityManager(int maxEntityCount, ComponentManager componentManager)
        {
            _entityPool = new Pool<IEntity>(
                () => new Entity(_entityCount++, componentManager),
                (e) => e.Dispose(),
                maximum: maxEntityCount);
        }

        public IEntity CreateEntity()
        {
            var entity = _entityPool.Obtain();
            _activeEntities.Add(entity);
            return entity;
        }
        public void DestroyEntity(IEntity entity)
        {
            _activeEntities.Remove(entity);
            _entityPool.Free(entity);
        }
    }
}
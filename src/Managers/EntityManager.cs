using System.Collections.Generic;
using TrenchesRTS.Basics;
using TrenchesRTS.ECS;
using TrenchesRTS.ECS.Interfaces;

namespace TrenchesRTS.Managers
{
    public class EntityManager
    {
        private int _entityCount;
        private readonly Pool<IEntity> _entityPool;
        private readonly List<IEntity> _activeEntities = new();
        public IEnumerable<IEntity> ActiveEntities => _activeEntities.ToArray();

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
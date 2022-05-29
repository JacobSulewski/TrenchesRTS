using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Trenches.ECS;
using Trenches.ECS.Interfaces;
using TrenchesRTS.ECS.Interfaces;

namespace TrenchesRTS.ECS
{
    public class World
    {
        private readonly ComponentManager _componentManager;
        private readonly EntityManager _entityManager;

        private readonly IList<IUpdateSystem> _updateSystems = new List<IUpdateSystem>();
        private readonly IList<IDrawSystem> _drawSystems = new List<IDrawSystem>();

        public World(int maxEntityCount = 128)
        {
            _componentManager = new ComponentManager(maxEntityCount);
            _entityManager = new EntityManager(maxEntityCount, _componentManager);
        }

        public World AttachSystem(System system)
        {
            system.Init(this, _componentManager);

            if (system is IUpdateSystem updateSystem)
                _updateSystems.Add(updateSystem);
            if (system is IDrawSystem drawSystem)
                _drawSystems.Add(drawSystem);

            return this;
        }
        public void Update(GameTime gameTime)
        {
            foreach (var system in _updateSystems)
                system.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var system in _drawSystems)
                system.Draw(spriteBatch);
        }

        public IEnumerable<IEntity> ActiveEntities => _entityManager.ActiveEntities;
        public IEntity CreateEntity() => _entityManager.CreateEntity();
        public void Destroy(IEntity entity) => _entityManager.DestroyEntity(entity);
    }
}

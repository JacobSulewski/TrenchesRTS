using Microsoft.Xna.Framework;
using System;
using TrenchesRTS.ECS.Components;
using TrenchesRTS.ECS.Interfaces;
using TrenchesRTS.Sprites.Factories;
using Transform = TrenchesRTS.ECS.Components.Transform;

namespace TrenchesRTS.ECS.EntityFactories
{
    public class GreenEntityFactory : IEntityFactory
    {
        private readonly Func<IEntity> _createEntity;
        private readonly Vector2 _spawnLocation;
        private readonly SpriteFactoryCache _spritePoolCache;

        public GreenEntityFactory(Func<IEntity> createEntity, Vector2 spawnLocation, SpriteFactoryCache spritePoolCache)
        {
            _createEntity = createEntity;
            _spawnLocation = spawnLocation;
            _spritePoolCache = spritePoolCache;
        }

        public IEntity SpawnInfantryman()
        {
            return GetUnit().AddComponent(new SpriteRenderer()
            {
                SpriteFactory = _spritePoolCache.GetFactory("GreenInfantryman.sf")
            });
        }

        public IEntity SpawnMachineGunner()
        {
            return GetUnit().AddComponent(new SpriteRenderer()
            {
                SpriteFactory = _spritePoolCache.GetFactory("GreenMachineGunner.sf")
            });
        }

        public IEntity SpawnEngineer()
        {
            return GetUnit().AddComponent(new SpriteRenderer()
            {
                SpriteFactory = _spritePoolCache.GetFactory("GreenEngineer.sf")
            });
        }

        public IEntity SpawnMortarMan()
        {
            return GetUnit().AddComponent(new SpriteRenderer()
            {
                SpriteFactory = _spritePoolCache.GetFactory("GreenMortarMan.sf")
            });
        }

        private IEntity GetUnit()
        {
            return _createEntity()
                .AddComponent(new Transform() { Position = _spawnLocation })
                .AddComponent(new Moveable() { Speed = 50 })
                .AddComponent(new BoxCollider() { Boundary = new Rectangle(0, 0, 16, 16) });
        }
    }
}
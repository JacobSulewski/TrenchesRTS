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
        private readonly SpriteFactoryCache _spriteFactoryCache;

        public GreenEntityFactory(Func<IEntity> createEntity, Vector2 spawnLocation, SpriteFactoryCache spriteFactoryCache)
        {
            _createEntity = createEntity;
            _spawnLocation = spawnLocation;
            _spriteFactoryCache = spriteFactoryCache;
        }

        public IEntity SpawnInfantryman()
        {
            return GetUnit().AddComponent(new SpriteRenderer()
            {
                SpriteFactory = _spriteFactoryCache.GetFactory("GreenInfantryman.sf")
            });
        }

        public IEntity SpawnMachineGunner()
        {
            return GetUnit().AddComponent(new SpriteRenderer()
            {
                SpriteFactory = _spriteFactoryCache.GetFactory("GreenMachineGunner.sf")
            });
        }

        public IEntity SpawnEngineer()
        {
            return GetUnit().AddComponent(new SpriteRenderer()
            {
                SpriteFactory = _spriteFactoryCache.GetFactory("GreenEngineer.sf")
            });
        }

        public IEntity SpawnMortarMan()
        {
            return GetUnit().AddComponent(new SpriteRenderer()
            {
                SpriteFactory = _spriteFactoryCache.GetFactory("GreenMortarMan.sf")
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
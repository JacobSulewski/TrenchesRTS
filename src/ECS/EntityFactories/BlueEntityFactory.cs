using Microsoft.Xna.Framework;
using System;
using Trenches.ECS.Components;
using TrenchesRTS.ECS.Components;
using TrenchesRTS.ECS.Interfaces;
using TrenchesRTS.Sprites.Factories;

namespace TrenchesRTS.ECS.EntityFactories
{
    public class BlueEntityFactory : IEntityFactory
    {
        private readonly Func<IEntity> _createEntity;
        private readonly Vector2 _spawnLocation;
        private readonly SpriteFactoryCache _spriteFactoryCache;

        public BlueEntityFactory(Func<IEntity> createEntity, Vector2 spawnLocation,
            SpriteFactoryCache spriteFactoryCache)
        {
            _createEntity = createEntity;
            _spawnLocation = spawnLocation;
            _spriteFactoryCache = spriteFactoryCache;
        }

        public IEntity SpawnInfantryman()
        {
            return GetUnit().AddComponent(new SpriteRenderer()
            {
                SpriteFactory = _spriteFactoryCache.GetFactory("BlueInfantryman.sf")
            });
        }

        public IEntity SpawnMachineGunner()
        {
            return GetUnit().AddComponent(new SpriteRenderer()
            {
                SpriteFactory = _spriteFactoryCache.GetFactory("BlueMachineGunner.sf")
            });
        }

        public IEntity SpawnEngineer()
        {
            return GetUnit().AddComponent(new SpriteRenderer()
            {
                SpriteFactory = _spriteFactoryCache.GetFactory("BlueEngineer.sf")
            });
        }

        public IEntity SpawnMortarMan()
        {
            return GetUnit().AddComponent(new SpriteRenderer()
            {
                SpriteFactory = _spriteFactoryCache.GetFactory("BlueMortarMan.sf")
            });
        }

        private IEntity GetUnit()
        {
            return _createEntity()
                .AddComponent(new Transform() { Position = _spawnLocation })
                .AddComponent(new Moveable() { Speed = 50 })
                .AddComponent(new UnitBoxCollider());
        }
    }
}
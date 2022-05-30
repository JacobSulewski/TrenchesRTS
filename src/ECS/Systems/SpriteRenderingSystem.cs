using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Trenches.ECS;
using Trenches.ECS.Interfaces;
using TrenchesRTS.ECS.Components;

namespace TrenchesRTS.ECS.Systems
{
    public class SpriteRenderingSystem : System, IUpdateSystem, IDrawSystem
    {
        public SpriteRenderingSystem()
        : base(new ComponentFilter(new[] { typeof(SpriteRenderer), typeof(Transform) }, Type.EmptyTypes))
        {

        }

        public void Update(GameTime gameTime)
        {
            foreach (var entity in World.ActiveEntities)
                if (Verify(entity))
                {
                    entity.TryGetComponent(out SpriteRenderer spriteRenderer);
                    spriteRenderer.Update(gameTime);
                }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var entity in World.ActiveEntities)
                if (Verify(entity))
                {
                    entity.TryGetComponent(out SpriteRenderer spriteRenderer);
                    entity.TryGetComponent(out Transform transform);
                    spriteRenderer.Draw(spriteBatch, transform);
                }
        }
    }
}
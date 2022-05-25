using Microsoft.Xna.Framework;
using System;
using Trenches.ECS;
using TrenchesRTS.ECS.Components;

namespace TrenchesRTS.ECS.Systems
{
    public class PhysicsSystem : System, IUpdateSystem
    {
        public PhysicsSystem()
            : base(new ComponentFilter(new[] { typeof(Transform), typeof(Moveable) }, Type.EmptyTypes))
        {

        }

        public void Update(GameTime gameTime)
        {
            foreach (var entity in World.ActiveEntities)
                if (Verify(entity))
                {
                    entity.GetComponent(out Transform transform);
                    entity.GetComponent(out Moveable moveable);
                    transform.Position += (moveable.Velocity * gameTime.ElapsedGameTime.Seconds) * moveable.Speed;
                    moveable.Velocity += (moveable.Acceleration * gameTime.ElapsedGameTime.Seconds);
                }
        }
    }
}
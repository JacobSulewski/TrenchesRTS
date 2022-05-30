using Microsoft.Xna.Framework;
using System;
using Trenches.ECS;
using TrenchesRTS.ECS.Components;

namespace TrenchesRTS.ECS.Systems
{
    public class CollisionSystem : System, IUpdateSystem
    {
        public CollisionSystem()
        : base(new ComponentFilter(new[] { typeof(Transform), typeof(BoxCollider) }, Type.EmptyTypes))
        {

        }

        public void Update(GameTime gameTime)
        {
            foreach (var entity in World.ActiveEntities)
                if (Verify(entity))
                {
                    entity.TryGetComponent(out Transform transform);
                    entity.TryGetComponent(out BoxCollider boxCollider);

                    /*TODO broad search, quad tree*/
                    /*TODO OnExit*/
                    /*TODO OnEnter*/
                    /*TODO Entered*/
                }
        }
    }
}
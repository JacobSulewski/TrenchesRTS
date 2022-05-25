using Microsoft.Xna.Framework;
using TrenchesRTS.ECS.Components;

namespace Trenches.ECS.Components
{
    public class UnitBoxCollider : BoxCollider
    {
        public UnitBoxCollider()
        {
            Boundary = new Rectangle(0, 0, 16, 16);
        }

        public override void OnCollisionEnter(/*TODO:CollisionInfo*/) { /*TODO*/ }
        public override void OnCollisionExit() { }
        public override void OnCollisionStay() { }

        public override void OnTriggerEnter(/*TODO:TriggerINFO*/) { /*TODO*/ }
        public override void OnTriggerExit() { }
        public override void OnTriggerStay() { }
    }
}
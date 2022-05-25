using Microsoft.Xna.Framework;

namespace TrenchesRTS.ECS.Components
{
    public struct Moveable
    {
        private Vector2 _velocity;

        public Vector2 Velocity
        {
            get => _velocity;
            set => _velocity = Vector2.Normalize(value);
        }
        public Vector2 Acceleration { get; set; }
        public int Speed { get; set; }
    }
}
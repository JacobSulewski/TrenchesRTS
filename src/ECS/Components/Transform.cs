using Microsoft.Xna.Framework;

namespace TrenchesRTS.ECS.Components
{
    public struct Transform
    {
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }
        public float LayerDepth { get; set; }
        public float Rotation { get; set; }
    }
}
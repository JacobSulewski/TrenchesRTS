#nullable enable
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TrenchesRTS.Sprites
{
    public interface ISprite
    {
        public Texture2D? Texture { get; set; }
        public Rectangle?[] Frames { get; set; }
        public double AnimationSpeed { get; set; }
        public Rectangle Boundary { get; }

        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color, float rotation = 0.0f,
            Vector2 origin = new(), Vector2 scale = new(), SpriteEffects effects = SpriteEffects.None, float layerDepth = 0.0f);
    }
}
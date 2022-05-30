using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TrenchesRTS.Sprites;
using TrenchesRTS.Sprites.Factories;

namespace TrenchesRTS.ECS.Components
{
    public struct SpriteRenderer
    {
        private ISprite _sprite;

        public ISpriteFactory SpriteFactory { get; set; }
        public SpriteEffects SpriteEffects { get; set; }
        public Rectangle SpriteBoundary => _sprite.Boundary;

        public void SetSprite(string spriteName) => _sprite = SpriteFactory.GetSprite(spriteName);

        public void Update(GameTime gameTime) => _sprite.Update(gameTime);
        public void Draw(SpriteBatch spriteBatch, Transform transform)
        {
            _sprite?.Draw(spriteBatch, transform.Position, Color.White, transform.Rotation, Vector2.Zero, transform.Scale, SpriteEffects, transform.LayerDepth);
        }
    }
}
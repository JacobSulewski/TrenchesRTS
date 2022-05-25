#nullable enable
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TrenchesRTS.Sprites
{
    public class Sprite : ISprite
    {
        private Texture2D? _texture = null;
        private Rectangle?[] _frames = new Rectangle?[] { null };
        private double _animationSpeed = 1;
        private double _animationCooldown = 1;
        private int _currentFrame = 0;
        private int CurrentFrame
        {
            get => _currentFrame;
            set
            {
                _currentFrame = value;
                if (_currentFrame >= Frames.Length)
                    _currentFrame = 0;
            }
        }

        public Texture2D? Texture
        {
            get => _texture;
            set
            {
                _texture = value;
                Frames = new Rectangle?[] { null };
            }
        }
        public Rectangle?[] Frames
        {
            get => _frames;
            set
            {
                _frames = value;
                _currentFrame = 0;
            }
        }
        public double AnimationSpeed
        {
            get => _animationSpeed;
            set
            {
                _animationSpeed = value;
                _animationCooldown = 1 / AnimationSpeed;
            }
        }

        public Rectangle Boundary => Frames[CurrentFrame] ?? new Rectangle();

        public Sprite() { }
        public Sprite(Texture2D texture, Rectangle? frame = null)
            : this(texture, new[] { frame }, 1) { }
        public Sprite(Texture2D texture, Rectangle?[] frames, double animationSpeed = 5f)
        {
            Texture = texture;
            Frames = frames;
            AnimationSpeed = animationSpeed;
        }

        public void Update(GameTime gameTime)
        {
            if (Texture == null)
                return;
            _animationCooldown -= gameTime.ElapsedGameTime.TotalSeconds;
            if (_animationCooldown < 0)
            {
                CurrentFrame++;
                _animationCooldown = 1 / AnimationSpeed;
            }
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color, float rotation = 0.0f,
            Vector2 origin = new(), Vector2 scale = new(), SpriteEffects effects = SpriteEffects.None, float layerDepth = 0.0f)
        {
            if (Texture == null)
                return;
            spriteBatch.Draw(_texture, position, Frames[CurrentFrame], Color.White, rotation, Vector2.Zero, scale, effects, layerDepth);
        }
    }
}
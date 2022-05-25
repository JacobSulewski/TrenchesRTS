using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TrenchesRTS
{
    public class TrenchesRTS : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public TrenchesRTS()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO
            // Step 1: Get input
            // Step 2: Send input to network
            // Step 3: Get commands from network
            // Step 4: Process commands
            // Step 5: Update world and its systems

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            base.Draw(gameTime);
        }
    }
}

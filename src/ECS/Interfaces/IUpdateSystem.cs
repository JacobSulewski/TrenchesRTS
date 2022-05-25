using Microsoft.Xna.Framework;

namespace Trenches.ECS
{
    public interface IUpdateSystem
    {
        public void Update(GameTime gameTime);
    }
}
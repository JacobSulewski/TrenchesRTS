namespace TrenchesRTS.Sprites.Factories
{
    public interface ISpriteFactory
    {
        /// <summary>
        /// Get a sprite of a given name.
        /// </summary>
        /// <param name="name"> The name of the sprite to get. </param>
        /// <returns> Returns the sprite of the given name if the name is valid, null otherwise. </returns>
        public ISprite GetSprite(string name);
    }
}

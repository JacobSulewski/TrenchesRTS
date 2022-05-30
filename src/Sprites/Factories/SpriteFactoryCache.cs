using System.Collections.Generic;
using System.IO;
using TrenchesRTS.Basics;

namespace TrenchesRTS.Sprites.Factories
{
    /// <summary>
    /// Caches the sprite factories that are read from a sprite factory (.sf) Json file.
    /// </summary>
    /*
     * Parsing Json is expensive, and since the sprite factories are stateless objects, this cache
     * is to reduce the need for parsing a the same Json file multiple times.
     */
    public class SpriteFactoryCache
    {
        private readonly IDictionary<string, ISpriteFactory> _factories = new Dictionary<string, ISpriteFactory>();
        private readonly TexturePool _textures;

        public SpriteFactoryCache(TexturePool textures)
        {
            _textures = textures;
        }

        public ISpriteFactory GetFactory(string factoryFile)
        {
            // Check formatting
            if (!factoryFile.EndsWith(".sf"))
                throw new FileNotFoundException("Sprite factory file missing '.sf' extension: " + factoryFile);

            // Return factory if it exists
            if (_factories.TryGetValue(factoryFile, out var factory))
                return factory;

            // Parse new factory if it doesn't
            factory = new JsonSpriteFactory(factoryFile, _textures);
            _factories.Add(factoryFile, factory);
            return factory;
        }
    }
}
using System.Collections.Generic;
using System.IO;
using TrenchesRTS.Basics;

namespace TrenchesRTS.Sprites.Factories
{
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
            if (!factoryFile.EndsWith(".sf"))
                throw new FileNotFoundException("Sprite factory file missing '.sf' extension: " + factoryFile);

            if (_factories.TryGetValue(factoryFile, out var factory))
                return factory;

            factory = new JsonSpriteFactory(factoryFile, _textures);
            _factories.Add(factoryFile, factory);
            return factory;
        }
    }
}
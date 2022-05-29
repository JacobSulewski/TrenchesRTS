#nullable enable
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using TrenchesRTS.Basics;
namespace TrenchesRTS.Sprites.Factories
{
    public class JsonSpriteFactory : ISpriteFactory
    {
        private readonly IDictionary<string, (Texture2D texture, Rectangle?[] frames, double animationSpeed)> _spriteData = new Dictionary<string, (Texture2D, Rectangle?[], double)>();
        private readonly Pool<ISprite> _sprites = new(() => new Sprite());

        public JsonSpriteFactory(string factoryFile, TexturePool textures)
        {
            var schema = JSchema.Parse(File.ReadAllText("SpriteFactorySchema.json"));
            var jsonObject = JObject.Parse(File.ReadAllText(factoryFile));
            if (!jsonObject.IsValid(schema, out IList<string> errorMessages))
            {
                Debug.WriteLine("ERROR: Invalid '.sf' file, does not match schema:" + errorMessages);
                return;
            }

            foreach (var (spriteName, tuple) in jsonObject.ToObject<Dictionary<string, JObject>>()!)
            {
                if (_spriteData.ContainsKey(spriteName))
                    continue;
                var textureName = Convert.ToString(tuple["Texture2D"]);
                var animationSpeed = Convert.ToDouble(tuple["AnimationSpeed"]);
                var frames = ParseRectangles((JArray)tuple["Frames"]!);
                _spriteData.Add(spriteName, (textures.Get(textureName), frames, animationSpeed));
            }

        }

        public ISprite? GetSprite(string spriteName)
        {
            var sprite = _sprites.Obtain();
            if (_spriteData.TryGetValue(spriteName, out var spriteInfo))
            {
                sprite.Texture = spriteInfo.texture;
                sprite.Frames = spriteInfo.frames;
                sprite.AnimationSpeed = spriteInfo.animationSpeed;
            }
            else
            {
                sprite.Texture = null;
            }
            return sprite;
        }
        public void Free(ISprite sprite) => _sprites.Free(sprite);

        private static Rectangle?[] ParseRectangles(JArray jsonRectangleArray)
        {
            var frames = new List<Rectangle?>();

            foreach (var rect in jsonRectangleArray)
            {
                var x = Convert.ToInt32(rect["x"]);
                var y = Convert.ToInt32(rect["y"]);
                var width = Convert.ToInt32(rect["width"]);
                var height = Convert.ToInt32(rect["height"]);
                frames.Add(new Rectangle(x, y, width, height));
            }

            if (frames.Count == 0)
                frames.Add(null);

            return frames.ToArray();
        }
    }
}
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace TrenchesRTS.Basics
{
    /// <summary>
    /// Texture pool for texture reuse.
    /// </summary>
    public sealed class TexturePool : IDisposable
    {
        private readonly ContentManager _content;
        private IDictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();

        public TexturePool(ContentManager content)
        {
            _content = content;
        }

        public Texture2D Get(string name)
        {
            if (_textures.TryGetValue(name, out var texture))
                return texture;
            texture = _content.Load<Texture2D>(name);
            _textures.Add(name, texture);
            return texture;
        }

        public void Dispose() => _textures = new Dictionary<string, Texture2D>();
    }
}
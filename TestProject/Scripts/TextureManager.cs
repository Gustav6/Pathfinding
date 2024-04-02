using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class TextureManager
    {
        private readonly int tileWidth = 48;
        private readonly int tileHeight = 48;

        public static SpriteFont Font { get; private set; }
        public static Dictionary<Texture, Texture2D> Textures { get; private set; }
        public static Dictionary<TileTexture, Texture2D> TileTextures { get; private set; }

        public void LoadTextures(ContentManager content)
        {
            #region Initialize dictionaries
            Textures = new();
            TileTextures = new();
            #endregion

            #region Regular textures
            //Textures.TryAdd(Texture.playerTexture, content.Load<Texture2D>(""));
            //Textures.TryAdd(Texture.enemyTexture, content.Load<Texture2D>(""));
            Textures.TryAdd(Texture.arrowTexture, content.Load<Texture2D>("arrow"));
            Textures.TryAdd(Texture.playerTexture, Library.CreateTexture(tileWidth, tileHeight, pixel => Color.DarkGreen));
            Textures.TryAdd(Texture.enemyTexture, Library.CreateTexture(tileWidth, tileHeight, pixel => Color.White));
            Textures.TryAdd(Texture.hitboxTexture, Library.CreateTexture(tileWidth, tileHeight, pixel => Color.Green));
            #endregion

            #region Tile textures
            TileTextures.TryAdd(TileTexture.solid, Library.CreateTexture(tileWidth, tileHeight, pixel => Color.Black));
            TileTextures.TryAdd(TileTexture.empty, Library.CreateTexture(tileWidth, tileHeight, pixel => Color.White));
            #endregion

            Font = content.Load<SpriteFont>("spritefont");
        }
    }

    public enum Texture
    {
        playerTexture,
        enemyTexture,
        hitboxTexture,
        arrowTexture,
    }

    public enum TileTexture
    {
        empty,
        solid,
    }
}

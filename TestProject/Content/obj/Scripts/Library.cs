using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public static class Library
    {
        public static List<GameObject> gameObjects = new();
        public static Player PlayerInstance { get; set; }
        public static TileMap TileMap { get; set; }

        public static Tile previousTarget;

        public static VectorFieldPathfinding fieldPathfinding = new();

        public static Texture2D CreateTexture(GraphicsDevice device, int width, int height, Func<int, Color> paint)
        {
            Texture2D texture = new(device, width, height);

            Color[] data = new Color[width * height];
            for (int pixel = 0; pixel < data.Length; pixel++)
            {
                data[pixel] = paint(pixel);
            }

            texture.SetData(data);

            return texture;
        }

        public static void DrawHitbox(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(TextureManager.Textures[Texture.hitboxTexture], position, Color.White * 0.5f);
        }
    }
}

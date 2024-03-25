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

        public static GraphicsDeviceManager graphics;
        public static Player PlayerInstance { get; set; }
        public static TileMap TileMap { get; set; }

        public static VectorFieldPathfinding fieldPathfinding = new();

        public static bool showArrows1;
        public static bool showArrows2;

        public static bool showDistance;

        public static Texture2D CreateTexture(int width, int height, Func<int, Color> paint)
        {
            Texture2D texture = new(graphics.GraphicsDevice, width, height);

            Color[] data = new Color[width * height];
            for (int pixel = 0; pixel < data.Length; pixel++)
            {
                data[pixel] = paint(pixel);
            }

            texture.SetData(data);

            return texture;
        }

        public static void DrawHitbox(SpriteBatch spriteBatch, Vector2 size, Vector2 position, Color color, float alpha)
        {
            spriteBatch.Draw(CreateTexture((int)size.X, (int)size.Y, pixel => color * alpha), position, Color.White);
        }
    }
}

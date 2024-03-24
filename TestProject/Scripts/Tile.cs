using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class Tile
    {
        private readonly Texture2D texture;
        public readonly Rectangle hitbox;

        private Vector2 position;
        public Color color = Color.White;

        public bool IsSolid { get; private set; }
        public bool occupied;

        public float distanceFromTarget;
        public Vector2 direction;
        public List<Tile> AdjacentNeighbors { get; set; }
        public List<Tile> VerticalNeighbors { get; set; }

        public Tile(Vector2 _position, Texture2D tileTexture)
        {
            texture = tileTexture;
            position = _position;

            if (tileTexture == TextureManager.TileTextures[TileTexture.empty])
            {
                hitbox = new Rectangle((int)position.X, (int)position.Y, tileTexture.Width, tileTexture.Height);
                IsSolid = false;
            }
            else
            {
                IsSolid = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, color);

            if (!IsSolid)
            {
                
                
                spriteBatch.DrawString(TextureManager.Font, distanceFromTarget.ToString(), new Vector2(position.X + 12, position.Y + 4), Color.Green);
            }
        }
    }
}

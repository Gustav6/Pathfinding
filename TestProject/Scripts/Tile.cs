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
        public readonly Texture2D texture;
        public readonly Rectangle hitbox;
        private Vector2 origin;

        public Vector2 Position { get; private set; }
        public Color color = Color.White;

        public bool IsSolid { get; private set; }
        public bool occupied;

        public float distanceFromTarget;
        public Vector2 direction;
        public List<Tile> AdjacentNeighbors { get; set; }
        public List<Tile> VerticalNeighbors { get; set; }
        public bool hasSolidNeighbor;

        public Tile(Vector2 _position, Texture2D tileTexture)
        {
            texture = tileTexture;
            Position = _position;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            hitbox = new Rectangle((int)Position.X - (int)origin.X, (int)Position.Y - (int)origin.Y, tileTexture.Width, tileTexture.Height);

            if (tileTexture != TextureManager.TileTextures[TileTexture.solid])
            {
                IsSolid = false;
            }
            else
            {
                IsSolid = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (direction != Vector2.Zero && !Library.showArrows1)
            {
                spriteBatch.Draw(texture, Position, null, color, 0, origin, 1, SpriteEffects.None, 0);
            }
            else if (direction == Vector2.Zero && IsSolid)
            {
                spriteBatch.Draw(texture, Position, null, color, 0, origin, 1, SpriteEffects.None, 0);
            }

            if (!IsSolid)
            {
                if (Library.showArrows1)
                {
                    if (!hasSolidNeighbor)
                    {
                        spriteBatch.Draw(texture, Position, null, color, 0, origin, 1, SpriteEffects.None, 0);
                    }
                }
                else if (Library.showArrows2)
                {
                }
                else if (Library.showDistance)
                {
                    spriteBatch.DrawString(TextureManager.Font, distanceFromTarget.ToString(), new Vector2(Position.X - 12, Position.Y - 16), Color.Green);
                }
            }
        }
    }
}

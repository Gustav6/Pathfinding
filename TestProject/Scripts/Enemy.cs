using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class Enemy : Moveable, ICollidable
    {
        public Rectangle tileHitbox;

        public Enemy(Vector2 spawnLocation)
        {
            Texture = TextureManager.Textures[TestProject.Texture.enemyTexture];
            Position = spawnLocation;
            MovementSpeed = 50;
            Color = Color.White;
            IsRemoved = false;
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Update(GameTime gameTime)
        {
            //Move(gameTime);

            tileHitbox.Location = new Vector2(Position.X + Texture.Width / 2, Position.Y + Texture.Height / 2).ToPoint();

            Pathfind();

            Move(gameTime);

            base.Update(gameTime);
        }

        public void Pathfind()
        {
            for (int i = 0; i < Library.TileMap.tiles.Count; i++)
            {
                if (tileHitbox.Intersects(Library.TileMap.tiles[i].hitbox))
                {
                    direction = Library.TileMap.tiles[i].direction;
                    break;
                }
            }

        }

        public void OnCollision(GameObject gameObject)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}

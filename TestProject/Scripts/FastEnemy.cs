using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Scripts
{
    public class FastEnemy : Enemy
    {
        public FastEnemy(Vector2 spawnLocation)
        {
            Texture = TextureManager.Textures[TestProject.Texture.enemyTexture];
            Start();

            Position = spawnLocation;
            MovementSpeed = 150;
            tileHitbox = new Rectangle((int)Position.X, (int)Position.Y, 10, 10);
            hitbox = new Rectangle((int)Position.X - (int)origin.X, (int)Position.Y - (int)origin.Y, Texture.Width, Texture.Height);
            Color = Color.White;
            SetStartingValues(Vector2.One, 0);
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}

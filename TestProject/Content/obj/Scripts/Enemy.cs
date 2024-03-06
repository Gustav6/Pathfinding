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
        public Enemy(Vector2 spawnLocation)
        {
            Texture = TextureManager.Textures[TestProject.Texture.enemyTexture];
            Position = spawnLocation;
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

            Pathfinding();

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public void Pathfinding()
        {

        }

        public void FindPath(Vector2 startPosition, Vector2 targetPosition)
        {

        }

        public void OnCollision(GameObject gameObject)
        {

        }
    }
}

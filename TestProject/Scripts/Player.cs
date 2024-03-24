using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class Player : Moveable, ICollidable
    {
        public Rectangle hitbox;

        public Rectangle tileHitbox;

        public Player(Vector2 startingPosition)
        {
            Texture = TextureManager.Textures[TestProject.Texture.playerTexture];
            MovementSpeed = 500;
            Position = startingPosition;
            Color = Color.White;
            hitbox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            tileHitbox = new Rectangle((int)Position.X + Texture.Width / 2, (int)Position.Y + Texture.Height / 2, 1, 1);
        }

        public override void Update(GameTime gameTime)
        {
            Control();

            Move(gameTime);

            if (direction != Vector2.Zero)
            {
                hitbox.Location = Position.ToPoint();
                tileHitbox.Location = new Vector2(Position.X + Texture.Width / 2, Position.Y + Texture.Height / 2).ToPoint();
            }

            base.Update(gameTime);
        }


        private void Control()
        {
            #region X Control
            direction.X = 0;

            if (Input.IsPressed(Keys.A))
            {
                direction.X -= 1;
            }
            if (Input.IsPressed(Keys.D))
            {
                direction.X += 1;
            }
            #endregion

            #region Y Control
            direction.Y = 0;

            if (Input.IsPressed(Keys.W))
            {
                direction.Y -= 1;
            }
            if (Input.IsPressed(Keys.S))
            {
                direction.Y += 1;
            }
            #endregion
        }

        public void OnCollision(GameObject gameObject)
        {

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            //Library.DrawHitbox(spriteBatch, hitbox.Location.ToVector2());
        }
    }
}

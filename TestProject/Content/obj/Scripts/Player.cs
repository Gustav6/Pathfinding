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
        private Rectangle hitbox;

        public Player(Vector2 startingPosition)
        {
            Texture = TextureManager.Textures[TestProject.Texture.playerTexture];
            MovementSpeed = 200;
            Position = startingPosition;
            Color = Color.White;
            hitbox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            IsRemoved = false;
        }

        public override void Update(GameTime gameTime)
        {
            Control();

            Move(gameTime);

            hitbox.Location = Position.ToPoint();

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            //Library.DrawHitbox(spriteBatch, hitbox.Location.ToVector2());
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
    }
}

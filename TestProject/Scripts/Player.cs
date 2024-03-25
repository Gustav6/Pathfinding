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
            Start();

            Library.TileMap.OnEnterTile += Player_EnteredNewTile;

            MovementSpeed = 500;
            Position = startingPosition;
            Color = Color.White;
            hitbox = new Rectangle((int)Position.X - (int)origin.X, (int)Position.Y - (int)origin.Y, Texture.Width, Texture.Height);
            tileHitbox = new Rectangle((int)Position.X, (int)Position.Y, 12, 12);
            SetStartingValues(Vector2.One, 0);
        }

        private void Player_EnteredNewTile(object sender, OnEnterEventArgs e)
        {
            if (e.entered == this)
            {
                Library.fieldPathfinding.target = e.tile;
            }
        }

        public override void Start()
        {
            base.Start();
        }


        public override void Update(GameTime gameTime)
        {
            Control();

            if (direction != Vector2.Zero)
            {
                Move(gameTime);
                hitbox.Location = new Vector2(Position.X - origin.X, Position.Y - origin.Y).ToPoint();
                tileHitbox.Location = new Vector2(Position.X - tileHitbox.Width / 2, Position.Y - tileHitbox.Height / 2).ToPoint();
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

            Library.DrawHitbox(spriteBatch, new Vector2(tileHitbox.Width, tileHitbox.Height), tileHitbox.Location.ToVector2(), Color.Black, 1);
        }
    }
}

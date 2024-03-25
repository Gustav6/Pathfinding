using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public abstract class Enemy : Moveable
    {
        public Rectangle tileHitbox;
        public Rectangle hitbox;

        public override void Start()
        {
            Library.TileMap.OnEnterTile += Enemy_EnteredNewTile;

            base.Start();
        }


        public override void Update(GameTime gameTime)
        {
            if (direction != Vector2.Zero)
            {
                Move(gameTime);

                tileHitbox.Location = new Vector2(Position.X - tileHitbox.Width / 2, Position.Y - tileHitbox.Height / 2).ToPoint();
                hitbox.Location = new Vector2(Position.X - origin.X, Position.Y - origin.Y).ToPoint();
            }

            base.Update(gameTime);
        }

        private void Enemy_EnteredNewTile(object sender, OnEnterEventArgs e)
        {
            if (e.entered == this)
            {
                direction = e.tile.direction;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            Library.DrawHitbox(spriteBatch, new Vector2(tileHitbox.Width, tileHitbox.Height), tileHitbox.Location.ToVector2(), Color.Black, 1);
        }
    }
}

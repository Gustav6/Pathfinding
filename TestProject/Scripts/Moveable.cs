using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public abstract class Moveable : GameObject
    {
        protected Vector2 direction;
        protected float MovementSpeed { get; set; }

        public void Move(GameTime gameTime)
        {
            if (direction != Vector2.Zero)
            {
                direction.Normalize();

                Position += direction * MovementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}

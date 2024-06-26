﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public abstract class Moveable : GameObject
    {
        public Vector2 direction;
        protected float movementSpeed;

        public void Move(GameTime gameTime)
        {
            if (direction != Vector2.Zero)
            {
                direction.Normalize();

                Position += direction * movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}

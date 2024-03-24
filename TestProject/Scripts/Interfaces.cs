using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    interface IDamageable
    {
        void TakeDamage(float amount);
    }

    interface ICollidable
    {
        void OnCollision(GameObject gameObject);
    }
}

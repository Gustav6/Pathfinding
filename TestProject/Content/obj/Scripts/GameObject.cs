using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public abstract class GameObject
    {
        public Texture2D Texture { get; protected set; }
        public Color Color { get; protected set; }
        public Vector2 Position { get; set; }
        public bool IsRemoved { get; set; }

        public virtual void Start()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color);
        }

        public void Destroy(GameObject gameObject)
        {
            IsRemoved = true;
        }
    }
}

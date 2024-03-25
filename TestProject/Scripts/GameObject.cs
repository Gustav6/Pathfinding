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
        protected Vector2 scale;
        protected Vector2 origin;
        protected float rotation;

        public Vector2 Position { get; set; }
        public bool IsRemoved { get; set; }

        public virtual void Start()
        {
            SetOrigin();
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color, rotation, origin, scale, SpriteEffects.None, 0);
        }

        private void SetOrigin()
        {
            origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
        }

        protected void SetStartingValues(Vector2 _scale, float _rotation)
        {
            scale = _scale;
            rotation = _rotation;
        }

        public void Destroy()
        {
            IsRemoved = true;
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace TestProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            GameManager.Instance = new GameManager();

            base.Initialize();

            GameManager.Instance.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            GameManager.Instance.LoadContent(Content, GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            GameManager.Instance.Update(gameTime);

            SetTile();

            base.Update(gameTime);
        }

        private void SetTile()
        {
            if (Input.MouseHasBeenPressed(Input.currentMS.LeftButton, Input.prevMS.LeftButton))
            {
                for (int i = 0; i < Library.TileMap.tiles.Count; i++)
                {
                    if (Input.GetMouseBounds(true).Intersects(Library.TileMap.tiles[i].hitbox) && !Library.TileMap.tiles[i].IsSolid)
                    {
                        if (Library.previousTarget != null)
                        {
                            Library.previousTarget.occupied = false;
                            Library.previousTarget.color = Color.White;

                            Library.previousTarget = null;
                        }

                        Library.TileMap.tiles[i].occupied = true;
                        Library.TileMap.tiles[i].color = Color.Green;

                        Library.fieldPathfinding.targetTile = Library.TileMap.tiles[i];
                        Library.fieldPathfinding.T();
                    }
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            GameManager.Instance.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
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
        public Rectangle hitbox;
        private Vector2 desiredDirection;
        public Tile currentTile;

        protected float hitboxSizeMultiplier = 1;
        private Vector2 tempDirection;
        private Tile hasShortestPath;
        bool canMoveTowardsDesired = true;


        public override void Start()
        {
            Library.TileMap.OnEnterTile += Enemy_EnteredNewTile;

            base.Start();
        }


        public override void Update(GameTime gameTime)
        {
            if (currentTile != null)
            {
                if (currentTile.direction != Vector2.Zero)
                {
                    desiredDirection = currentTile.direction;

                    if (!currentTile.hitbox.Intersects(hitbox))
                    {
                        GetIntersectingTile();
                    }

                    CheckForObstacle(gameTime);
                }
                else if (direction != Vector2.Zero)
                {
                    direction = Vector2.Zero;
                }
            }

            if (direction != Vector2.Zero)
            {
                Move(gameTime);

                hitbox.Location = new Vector2(Position.X - (origin.X * hitboxSizeMultiplier), Position.Y - (origin.Y * hitboxSizeMultiplier)).ToPoint();
            }

            base.Update(gameTime);
        }

        private void Enemy_EnteredNewTile(object sender, OnEnterEventArgs e)
        {
            if (e.entered == this)
            {
                currentTile = e.tile;
            }
        }

        private void CheckForObstacle(GameTime gameTime)
        {
            if (canMoveTowardsDesired)
            {
                if (CanMoveInDirection(desiredDirection, gameTime))
                {
                    direction = desiredDirection;
                }
                else
                {
                    canMoveTowardsDesired = false;

                    MoveTowardsNewTile();
                }
            }
            else if (!canMoveTowardsDesired)
            {
                if (hasShortestPath != null)
                {
                    if (CanMoveInDirection(tempDirection, gameTime))
                    {
                        if (tempDirection.X != 0)
                        {
                            if (Position.X + 2 >= hasShortestPath.Position.X && Position.X - 2 <= hasShortestPath.Position.X)
                            {
                                if (CanMoveInDirection(desiredDirection, gameTime))
                                {
                                    canMoveTowardsDesired = true;
                                    tempDirection = Vector2.Zero;
                                }

                                Position = new Vector2(hasShortestPath.Position.X, Position.Y);
                                direction = Vector2.Zero;
                            }
                            else
                            {
                                direction = tempDirection;
                            }
                        }

                        if (tempDirection.Y != 0)
                        {
                            if (Position.Y + 2 >= hasShortestPath.Position.Y && Position.Y - 2 <= hasShortestPath.Position.Y)
                            {
                                if (CanMoveInDirection(desiredDirection, gameTime))
                                {
                                    canMoveTowardsDesired = true;
                                    tempDirection = Vector2.Zero;
                                }

                                Position = new Vector2(Position.X, hasShortestPath.Position.Y);
                                direction = Vector2.Zero;
                            }
                            else
                            {
                                direction = tempDirection;
                            }
                        }
                    }
                    else
                    {
                        direction = Vector2.Zero;

                        if (tempDirection.Y != 0)
                        {
                            Position = currentTile.Position;
                        }

                        if (tempDirection.X != 0)
                        {
                            Position = currentTile.Position;
                        }

                        if (CanMoveInDirection(desiredDirection, gameTime))
                        {
                            canMoveTowardsDesired = true;
                            tempDirection = Vector2.Zero;
                        }
                    }
                }
            }
        }

        private bool CanMoveInDirection(Vector2 dir, GameTime gameTime)
        {
            Vector2 desiredPosition = Position + (dir * movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);

            Rectangle tempHitbox = new(new Vector2(desiredPosition.X - origin.X, desiredPosition.Y - origin.Y).ToPoint(), new Point(Texture.Width, Texture.Height));

            for (int i = 0; i < Library.TileMap.SolidTiles.Count; i++)
            {
                if (tempHitbox.Intersects(Library.TileMap.SolidTiles[i].hitbox))
                {
                    direction = Vector2.Zero;

                    return false;
                }
            }

            return true;
        }

        private void MoveTowardsNewTile()
        {
            if (currentTile != null)
            {
                for (int i = 0; i < currentTile.AdjacentNeighbors.Count; i++)
                {
                    if (hasShortestPath != null)
                    {
                        if (!currentTile.AdjacentNeighbors[i].IsSolid)
                        {
                            if (hasShortestPath.distanceFromTarget > currentTile.AdjacentNeighbors[i].distanceFromTarget)
                            {
                                hasShortestPath = currentTile.AdjacentNeighbors[i];
                            }
                        }
                    }
                    else
                    {
                        if (!currentTile.AdjacentNeighbors[i].IsSolid)
                        {
                            hasShortestPath = currentTile.AdjacentNeighbors[i];
                        }
                    }
                }

                if (hasShortestPath.Position.X > Position.X)
                {
                    tempDirection = new Vector2(1, 0);
                }
                else if (hasShortestPath.Position.X < Position.X)
                {
                    tempDirection = new Vector2(-1, 0);
                }

                if (hasShortestPath.Position.Y > Position.Y)
                {
                    tempDirection = new Vector2(0, 1);
                }
                else if (hasShortestPath.Position.Y < Position.Y)
                {
                    tempDirection = new Vector2(0, -1);
                }
            }
        }

        protected void GetIntersectingTile()
        {
            for (int i = 0; i < Library.TileMap.NotSolidTiles.Count; i++)
            {
                Tile tile = Library.TileMap.NotSolidTiles[i];

                if (hitbox.Intersects(tile.hitbox))
                {
                    Library.TileMap.OnEnter(new OnEnterEventArgs { entered = this, tile = tile });
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            //Library.DrawHitbox(spriteBatch, new Vector2(hitbox.Width, hitbox.Height), hitbox.Location.ToVector2(), Color.Black, 0.6f);
        }
    }
}

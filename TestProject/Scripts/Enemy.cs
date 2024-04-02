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

                    MoveTowardsNewTile(gameTime);
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
                            Vector2 nextPosition = Position + (tempDirection * movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);

                            if (tempDirection.X < 0 && nextPosition.X <= hasShortestPath.Position.X || tempDirection.X > 0 && nextPosition.X >= hasShortestPath.Position.X)
                            {
                                Position = new Vector2(hasShortestPath.Position.X, Position.Y);
                                direction = Vector2.Zero;

                                if (CanMoveInDirection(desiredDirection, gameTime))
                                {
                                    hasShortestPath = null;
                                    canMoveTowardsDesired = true;
                                    tempDirection = Vector2.Zero;
                                }
                            }
                            else
                            {
                                direction = tempDirection;
                            }
                        }

                        if (tempDirection.Y != 0)
                        {
                            Vector2 nextPosition = Position + (tempDirection * movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);

                            if (tempDirection.Y > 0 && nextPosition.Y >= hasShortestPath.Position.Y || tempDirection.Y < 0 && nextPosition.Y <= hasShortestPath.Position.Y)
                            {
                                Position = new Vector2(Position.X, hasShortestPath.Position.Y);
                                direction = Vector2.Zero;

                                if (CanMoveInDirection(desiredDirection, gameTime))
                                {
                                    hasShortestPath = null;
                                    canMoveTowardsDesired = true;
                                    tempDirection = Vector2.Zero;
                                }
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

                        if (CanMoveInDirection(desiredDirection, gameTime))
                        {
                            canMoveTowardsDesired = true;
                            tempDirection = Vector2.Zero;
                        }
                        else
                        {
                            Vector2 nextPosition = Position + (tempDirection * movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);

                            if (tempDirection.Y != 0)
                            {
                                if (nextPosition.Y != hasShortestPath.Position.Y)
                                {
                                    Position = new Vector2(Position.X, currentTile.Position.Y);
                                }
                            }
                            else if (tempDirection.X != 0)
                            {
                                if (nextPosition.Y != hasShortestPath.Position.Y)
                                {
                                    Position = new Vector2(currentTile.Position.X, Position.Y);
                                }
                            }
                        }
                    }
                }

                if (tempDirection == Vector2.Zero && !canMoveTowardsDesired)
                {
                    MoveTowardsNewTile(gameTime);
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

        private void MoveTowardsNewTile(GameTime gameTime)
        {
            if (currentTile != null)
            {
                List<Tile> safety = new();

                for (int i = 0; i < currentTile.AdjacentNeighbors.Count; i++)
                {
                    if (hasShortestPath != null)
                    {
                        if (!currentTile.AdjacentNeighbors[i].IsSolid)
                        {
                            if (hasShortestPath.distanceFromTarget > currentTile.AdjacentNeighbors[i].distanceFromTarget)
                            {
                                safety.Clear();
                                safety.Add(currentTile.AdjacentNeighbors[i]);
                                hasShortestPath = currentTile.AdjacentNeighbors[i];
                            }
                            else if (hasShortestPath.distanceFromTarget == currentTile.AdjacentNeighbors[i].distanceFromTarget)
                            {
                                safety.Add(currentTile.AdjacentNeighbors[i]);
                            }
                        }
                        //currentTile.AdjacentNeighbors[i].color = Color.Blue;
                    }
                    else
                    {
                        if (!currentTile.AdjacentNeighbors[i].IsSolid)
                        {
                            hasShortestPath = currentTile.AdjacentNeighbors[i];
                            safety.Add(currentTile.AdjacentNeighbors[i]);
                            //currentTile.AdjacentNeighbors[i].color = Color.Blue;
                        }
                    }
                }

                if (safety.Count > 1)
                {
                    for (int i = 0; i < currentTile.VerticalNeighbors.Count; i++)
                    {
                        if (!currentTile.VerticalNeighbors[i].IsSolid)
                        {
                            if (currentTile.VerticalNeighbors[i].distanceFromTarget < safety.First().distanceFromTarget)
                            {
                                hasShortestPath = currentTile.VerticalNeighbors[i];
                            }
                        }
                    }

                    if (safety.Contains(hasShortestPath))
                    {
                        hasShortestPath = safety.First();
                        safety.Clear();
                    }
                }
                else
                {
                    safety.Clear();
                }

                //hasShortestPath.color = Color.Green;

                if (safety.Count == 0)
                {
                    //Color = Color.Yellow;

                    if (hasShortestPath.Position.Y == currentTile.Position.Y)
                    {
                        if (Position.Y == hasShortestPath.Position.Y)
                        {
                            if (hasShortestPath.Position.X > currentTile.Position.X)
                            {
                                tempDirection = new Vector2(1, 0);
                            }
                            else if (hasShortestPath.Position.X < currentTile.Position.X)
                            {
                                tempDirection = new Vector2(-1, 0);
                            }
                        }
                        else
                        {
                            if (Position.Y > hasShortestPath.Position.Y)
                            {
                                tempDirection = new Vector2(0, -1);
                            }
                            else if (Position.Y < hasShortestPath.Position.Y)
                            {
                                tempDirection = new Vector2(0, 1);
                            }
                        }
                    }
                    else if (hasShortestPath.Position.X == currentTile.Position.X)
                    {
                        if (Position.X == hasShortestPath.Position.X)
                        {
                            if (hasShortestPath.Position.Y > currentTile.Position.Y)
                            {
                                tempDirection = new Vector2(0, 1);
                            }
                            else if (hasShortestPath.Position.Y < currentTile.Position.Y)
                            {
                                tempDirection = new Vector2(0, -1);
                            }
                        }
                        else
                        {
                            if (Position.X > hasShortestPath.Position.X)
                            {
                                tempDirection = new Vector2(-1, 0);
                            }
                            else if (Position.X < hasShortestPath.Position.X)
                            {
                                tempDirection = new Vector2(1, 0);
                            }
                        }
                    }
                    else
                    {
                        Position = currentTile.Position;
                        hasShortestPath = null;
                    }
                }
                else
                {
                    //Color = Color.Blue;

                    Vector2 desiredVerticalDirection = Vector2.Zero;

                    if (hasShortestPath.Position.Y > currentTile.Position.Y && hasShortestPath.Position.X > currentTile.Position.X)
                    {
                        desiredVerticalDirection = new Vector2(1, 1);
                    }
                    else if (hasShortestPath.Position.Y < currentTile.Position.Y && hasShortestPath.Position.X < currentTile.Position.X)
                    {
                        desiredVerticalDirection = new Vector2(-1, -1);
                    }
                    else if (hasShortestPath.Position.Y > currentTile.Position.Y && hasShortestPath.Position.X < currentTile.Position.X)
                    {
                        desiredVerticalDirection = new Vector2(-1, 1);
                    }
                    else if (hasShortestPath.Position.Y < currentTile.Position.Y && hasShortestPath.Position.X > currentTile.Position.X)
                    {
                        desiredVerticalDirection = new Vector2(1, -1);
                    }

                    float testXDir = desiredVerticalDirection.X, testYDir = desiredVerticalDirection.Y;

                    if (!CanMoveInDirection(new Vector2(testXDir, 0), gameTime))
                    {
                        desiredVerticalDirection.X = 0;
                    }
                    else if (!CanMoveInDirection(new Vector2(0, testYDir), gameTime))
                    {
                        desiredVerticalDirection.Y = 0;
                    }

                    tempDirection = desiredVerticalDirection;
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
    }
}

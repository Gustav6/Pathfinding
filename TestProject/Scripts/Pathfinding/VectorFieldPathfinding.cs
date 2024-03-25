using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class VectorFieldPathfinding
    {
        public Tile target;
        private Tile previousTarget;
        public readonly float distanceFromTarget;


        public void CreatePathfindingField()
        {
            if (Library.PlayerInstance != null)
            {
                if (previousTarget != null)
                {
                    previousTarget.occupied = false;

                    if (target != previousTarget)
                    {
                        CreateHeatMap(target);
                        GiveDirection();
                    }
                }

                if (target != null)
                {
                    target.occupied = true;
                    previousTarget = target;
                }
            }
        }

        private void CreateHeatMap(Tile targetTile)
        {
            List<Tile> openTiles = new();
            HashSet<Tile> closedTiles = new();

            for (int i = 0; i < Library.TileMap.AllTiles.Count; i++)
            {
                Library.TileMap.AllTiles[i].distanceFromTarget = 0;
            }

            Tile selectedTile = targetTile;

            openTiles.Add(selectedTile);

            while (openTiles.Count != 0)
            {
                #region Clear neighbors

                List<Tile> totalNeighbors = new();

                totalNeighbors.AddRange(selectedTile.AdjacentNeighbors);
                #endregion

                foreach (Tile neighboringTile in totalNeighbors)
                {
                    #region Give distance from target
                    if (!neighboringTile.IsSolid)
                    {
                        if (closedTiles.Contains(neighboringTile))
                        {
                            if (selectedTile.distanceFromTarget + 1 < neighboringTile.distanceFromTarget)
                            {
                                neighboringTile.distanceFromTarget = selectedTile.distanceFromTarget + 1;
                            }
                        }
                        else
                        {
                            neighboringTile.distanceFromTarget = selectedTile.distanceFromTarget + 1;

                            openTiles.Add(neighboringTile);
                            closedTiles.Add(neighboringTile);
                        }
                    }
                    #endregion
                }

                if (selectedTile != null)
                {
                    #region Get new selected tile
                    openTiles.Remove(selectedTile);
                    closedTiles.Add(selectedTile);

                    if (openTiles.Count != 0)
                    {
                        selectedTile = openTiles.First();
                    }
                    #endregion
                }
            }
        }

        private void GiveDirection()
        {
            foreach (Tile tile in Library.TileMap.AllTiles)
            {
                if (tile != target)
                {
                    List<Tile> neighbors = new();
                    List<Tile> adjacentNeighbors = new();

                    neighbors.AddRange(tile.VerticalNeighbors);
                    neighbors.AddRange(tile.AdjacentNeighbors);
                    adjacentNeighbors.AddRange(tile.AdjacentNeighbors);

                    tile.direction = LowestNumberDirection(neighbors, tile);
                }
            }
        }

        private Vector2 CombinedVectorDirection(List<Tile> neighbors, Tile currentTile)
        {
            Vector2 direction = Vector2.Zero;

            for (int i = 0; i < neighbors.Count; i++)
            {
                if (!neighbors[i].IsSolid)
                {
                    float xDirection, yDirection;

                    if (neighbors[i].Position.X > currentTile.Position.X)
                    {
                        xDirection = neighbors[i].distanceFromTarget;
                    }
                    else if (neighbors[i].Position.X < currentTile.Position.X)
                    {
                        xDirection = -neighbors[i].distanceFromTarget;
                    }
                    else
                    {
                        xDirection = 0;
                    }

                    if (neighbors[i].Position.Y > currentTile.Position.Y)
                    {
                        yDirection = neighbors[i].distanceFromTarget;
                    }
                    else if (neighbors[i].Position.Y < currentTile.Position.Y)
                    {
                        yDirection = -neighbors[i].distanceFromTarget;
                    }
                    else
                    {
                        yDirection = 0;
                    }

                    if (yDirection != 0)
                    {
                        yDirection = 1 / yDirection;
                    }
                    if (xDirection != 0)
                    {
                        xDirection = 1 / xDirection;
                    }

                    direction += new Vector2(xDirection, yDirection);
                }
            }

            if (direction.X > 1)
            {
                direction.X = 1;
            }
            else if (direction.X < -1)
            {
                direction.X = -1;
            }

            if (direction.Y > 1)
            {
                direction.Y = 1;
            }
            else if (direction.Y < -1)
            {
                direction.Y = -1;
            }

            return direction;
        }

        private Vector2 LowestNumberDirection(List<Tile> neighbors, Tile currentTile)
        {
            float xDirection = 0, yDirection = 0;
            List<Tile> temp = new();
            Tile temp2 = null;
            Vector2 combinedDirection = Vector2.Zero;

            for (int i = 0; i < neighbors.Count; i++)
            {
                if (!neighbors[i].IsSolid)
                {
                    if (temp2 != null)
                    {
                        if (neighbors[i].distanceFromTarget < temp2.distanceFromTarget)
                        {
                            temp2 = neighbors[i];
                        }
                    }
                    else
                    {
                        temp2 = neighbors[i];
                    }

                    //if (temp.Count == 0)
                    //{
                    //    temp.Add(neighbors[i]);
                    //}
                    //else
                    //{
                    //    bool lowerNumberDetected = false;

                    //    for (int y = 0; y < temp.Count; y++)
                    //    {
                    //        if (temp[y].distanceFromTarget > neighbors[i].distanceFromTarget)
                    //        {
                    //            temp.Clear();
                    //            temp.Add(neighbors[i]);
                    //            lowerNumberDetected = true;
                    //        }
                    //    }

                    //    if (!lowerNumberDetected)
                    //    {
                    //        temp.Add(neighbors[i]);
                    //    }
                    //}
                }
            }

            //if (temp.Count > 1)
            //{
            //    combinedDirection = CombinedVectorDirection(temp, currentTile);
            //}
            //else if (temp.Count == 1)
            //{
            //    if (temp.First().Position.X > currentTile.Position.X)
            //    {
            //        xDirection = 1;
            //    }
            //    else if (temp.First().Position.X < currentTile.Position.X)
            //    {
            //        xDirection = -1;
            //    }
            //    else
            //    {
            //        xDirection = 0;
            //    }

            //    if (temp.First().Position.Y > currentTile.Position.Y)
            //    {
            //        yDirection = 1;
            //    }
            //    else if (temp.First().Position.Y < currentTile.Position.Y)
            //    {
            //        yDirection = -1;
            //    }
            //    else
            //    {
            //        yDirection = 0;
            //    }
            //}

            if (temp2.Position.X > currentTile.Position.X)
            {
                xDirection = 1;
            }
            else if (temp2.Position.X < currentTile.Position.X)
            {
                xDirection = -1;
            }
            else
            {
                xDirection = 0;
            }

            if (temp2.Position.Y > currentTile.Position.Y)
            {
                yDirection = 1;
            }
            else if (temp2.Position.Y < currentTile.Position.Y)
            {
                yDirection = -1;
            }
            else
            {
                yDirection = 0;
            }

            if (temp2 == target)
            {
                xDirection = 0;
                yDirection = 0;
            }

            return new Vector2(xDirection, yDirection);

            //if (combinedDirection != Vector2.Zero)
            //{
            //    return combinedDirection;
            //}
            //else
            //{
            //    return new Vector2(xDirection, yDirection);
            //}
        }
    }
}

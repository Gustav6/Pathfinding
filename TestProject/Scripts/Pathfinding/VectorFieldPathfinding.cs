using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class VectorFieldPathfinding
    {
        public Tile target;
        private Tile previousTarget;

        public void SetTarget()
        {
            if (Library.PlayerInstance != null)
            {
                for (int i = 0; i < Library.TileMap.tiles.Count; i++)
                {
                    if (Library.PlayerInstance.tileHitbox.Intersects(Library.TileMap.tiles[i].hitbox))
                    {
                        target = Library.TileMap.tiles[i];
                        break;
                    }
                }

                if (previousTarget != null)
                {
                    previousTarget.occupied = false;

                    if (target != previousTarget)
                    {
                        CreateHeatMap(target);
                        GiveVector();
                    }
                }

                target.occupied = true;
                previousTarget = target;
            }
        }

        private void CreateHeatMap(Tile targetTile)
        {
            List<Tile> openTiles = new();
            HashSet<Tile> closedTiles = new();

            for (int i = 0; i < Library.TileMap.tiles.Count; i++)
            {
                Library.TileMap.tiles[i].distanceFromTarget = 0;
            }

            Tile selectedTile = targetTile;

            openTiles.Add(selectedTile);

            while (openTiles.Count != 0)
            {
                #region Clear neighbors
                List<Tile> neighbors = new();

                neighbors.AddRange(selectedTile.VerticalNeighbors);
                neighbors.AddRange(selectedTile.AdjacentNeighbors);
                #endregion

                // Check neighboring tiles

                foreach (Tile neighboringTile in neighbors)
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

        private void GiveVector()
        {
            foreach (Tile tile in Library.TileMap.tiles)
            {
                Vector2 direction = Vector2.Zero;

                List<Tile> neighbors = new();

                neighbors.AddRange(tile.VerticalNeighbors);
                neighbors.AddRange(tile.AdjacentNeighbors);

                for (int i = 0; i < neighbors.Count; i++)
                {
                    Vector2 neighborsDirection = new();

                    if (neighbors[i].distanceFromTarget != 0)
                    {
                        neighborsDirection = new(1 / neighbors[i].distanceFromTarget, 1 / neighbors[i].distanceFromTarget);
                    }

                    direction += neighborsDirection;
                }

                tile.direction = direction;
            }
        }
    }
}

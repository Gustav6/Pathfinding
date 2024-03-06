using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class VectorFieldPathfinding
    {
        public List<Tile> openTiles;
        public HashSet<Tile> closedTiles;

        public Tile targetTile;

        public void T()
        {
            openTiles = new List<Tile>();
            closedTiles = new HashSet<Tile>();

            for (int i = 0; i < Library.TileMap.tiles.Count; i++)
            {
                Library.TileMap.tiles[i].distanceFromTarget = 0;
            }

            List<Tile> totalNeighbors = new();

            Tile selectedTile = targetTile;
            Library.previousTarget = selectedTile;

            openTiles.Add(selectedTile);

            while (openTiles.Count != 0)
            {
                totalNeighbors.Clear();
                totalNeighbors.AddRange(selectedTile.VerticalNeighbors);
                totalNeighbors.AddRange(selectedTile.AdjacentNeighbors);

                // Check neighboring tiles

                foreach (Tile neighboringTile in totalNeighbors)
                {
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
                }

                if (selectedTile != null)
                {
                    openTiles.Remove(selectedTile);
                    closedTiles.Add(selectedTile);

                    if (openTiles.Count != 0)
                    {
                        selectedTile = openTiles.First();
                    }
                }
            }
        }
    }
}

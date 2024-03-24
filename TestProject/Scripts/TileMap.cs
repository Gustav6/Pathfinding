using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class TileMap
    {
        private Tile[,] currentTileMap = new Tile[0, 0];
        public List<Tile> tiles = new();

        public TileMap(int[,] map)
        {
            CreateTileMap(map);
            SetNeighbors(currentTileMap);
        }

        private void CreateTileMap(int[,] map)
        {
            currentTileMap = new Tile[map.GetLength(0), map.GetLength(1)];

            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    Texture2D tileTexture = TextureManager.TileTextures[(TileTexture)map[x, y]];

                    float positionX = y * tileTexture.Height + 65;
                    float positionY = x * tileTexture.Width + 50;

                    Tile temp = new(new Vector2(positionX, positionY), tileTexture);

                    currentTileMap[x, y] = temp;
                    tiles.Add(temp);
                }
            }
        }

        public void SetNeighbors(Tile[,] tileMap)
        {
            for (int x = 0; x < tileMap.GetLength(0); x++)
            {
                for (int y = 0; y < tileMap.GetLength(1); y++)
                {
                    Tile currentTile = tileMap[x, y];
                    List<Tile> verticalNeighborTiles = new();
                    List<Tile> adjacentNeighborTiles = new();

                    #region Adjacent
                    if (x + 1 < tileMap.GetLength(0))
                    {
                        Tile temp = tileMap[x + 1, y];
                        adjacentNeighborTiles.Add(temp);
                    }
                    if (x - 1 >= 0)
                    {
                        Tile temp = tileMap[x - 1, y];
                        adjacentNeighborTiles.Add(temp);
                    }
                    if (y + 1 < tileMap.GetLength(1))
                    {
                        Tile temp = tileMap[x, y + 1];
                        adjacentNeighborTiles.Add(temp);
                    }
                    if (y - 1 >= 0)
                    {
                        Tile temp = tileMap[x, y - 1];
                        adjacentNeighborTiles.Add(temp);
                    }
                    #endregion

                    #region Vertical
                    if (x + 1 < tileMap.GetLength(0) && y + 1 < tileMap.GetLength(1))
                    {
                        Tile temp = tileMap[x + 1, y + 1];
                        verticalNeighborTiles.Add(temp);
                    }
                    if (x - 1 >= 0 && y + 1 < tileMap.GetLength(1))
                    {
                        Tile temp = tileMap[x - 1, y + 1];
                        verticalNeighborTiles.Add(temp);
                    }
                    if (x - 1 >= 0 && y - 1 >= 0)
                    {
                        Tile temp = tileMap[x - 1, y - 1];
                        verticalNeighborTiles.Add(temp);
                    }
                    if (x + 1 < tileMap.GetLength(0) && y - 1 >= 0)
                    {
                        Tile temp = tileMap[x + 1, y - 1];
                        verticalNeighborTiles.Add(temp);
                    }
                    #endregion

                    currentTile.AdjacentNeighbors = adjacentNeighborTiles;
                    currentTile.VerticalNeighbors = verticalNeighborTiles;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < currentTileMap.GetLength(0); x++)
            {
                for (int y = 0; y < currentTileMap.GetLength(1); y++)
                {
                    currentTileMap[x, y].Draw(spriteBatch);
                }
            }
        }
    }
}

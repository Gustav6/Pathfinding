using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class OnEnterEventArgs : EventArgs
    {
        public Tile tile;
        public GameObject entered;
    }

    public class TileMap
    {
        private Tile[,] currentMap;
        public List<Tile> AllTiles { get; private set; }
        public List<Tile> NotSolidTiles { get; private set; }
        public List<Tile> SolidTiles { get; private set; }

        public event EventHandler<OnEnterEventArgs> OnEnterTile;

        public TileMap(int[,] map)
        {
            currentMap = new Tile[0, 0];
            AllTiles = new List<Tile>();
            SolidTiles = new List<Tile>();
            NotSolidTiles = new List<Tile>();

            CreateTileMap(map);
            SetNeighbors(currentMap);
        }

        public void OnEnter(OnEnterEventArgs e)
        {
            OnEnterTile?.Invoke(this, e);
        }

        private void CreateTileMap(int[,] map)
        {
            currentMap = new Tile[map.GetLength(0), map.GetLength(1)];

            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    Texture2D tileTexture = TextureManager.TileTextures[(TileTexture)map[x, y]];

                    float positionX = y * tileTexture.Height + 65;
                    float positionY = x * tileTexture.Width + 50;

                    Tile temp = new(new Vector2(positionX, positionY), tileTexture);

                    currentMap[x, y] = temp;

                    if (tileTexture != TextureManager.TileTextures[TileTexture.solid])
                    {
                        NotSolidTiles.Add(temp);
                    }
                    else
                    {
                        SolidTiles.Add(temp);
                    }

                    AllTiles.Add(temp);
                }
            }
        }

        private void SetNeighbors(Tile[,] tileMap)
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

                        if (temp.IsSolid)
                        {
                            tileMap[x, y].hasSolidNeighbor = true;
                        }

                        adjacentNeighborTiles.Add(temp);
                    }
                    if (x - 1 >= 0)
                    {
                        Tile temp = tileMap[x - 1, y];

                        if (temp.IsSolid)
                        {
                            tileMap[x, y].hasSolidNeighbor = true;
                        }

                        adjacentNeighborTiles.Add(temp);
                    }
                    if (y + 1 < tileMap.GetLength(1))
                    {
                        Tile temp = tileMap[x, y + 1];

                        if (temp.IsSolid)
                        {
                            tileMap[x, y].hasSolidNeighbor = true;
                        }

                        adjacentNeighborTiles.Add(temp);
                    }
                    if (y - 1 >= 0)
                    {
                        Tile temp = tileMap[x, y - 1];

                        if (temp.IsSolid)
                        {
                            tileMap[x, y].hasSolidNeighbor = true;
                        }

                        adjacentNeighborTiles.Add(temp);
                    }
                    #endregion

                    #region Vertical
                    if (x + 1 < tileMap.GetLength(0) && y + 1 < tileMap.GetLength(1))
                    {
                        Tile temp = tileMap[x + 1, y + 1];

                        if (temp.IsSolid)
                        {
                            tileMap[x, y].hasSolidNeighbor = true;
                        }

                        verticalNeighborTiles.Add(temp);
                    }
                    if (x - 1 >= 0 && y + 1 < tileMap.GetLength(1))
                    {
                        Tile temp = tileMap[x - 1, y + 1];

                        if (temp.IsSolid)
                        {
                            tileMap[x, y].hasSolidNeighbor = true;
                        }

                        verticalNeighborTiles.Add(temp);
                    }
                    if (x - 1 >= 0 && y - 1 >= 0)
                    {
                        Tile temp = tileMap[x - 1, y - 1];

                        if (temp.IsSolid)
                        {
                            tileMap[x, y].hasSolidNeighbor = true;
                        }

                        verticalNeighborTiles.Add(temp);
                    }
                    if (x + 1 < tileMap.GetLength(0) && y - 1 >= 0)
                    {
                        Tile temp = tileMap[x + 1, y - 1];

                        if (temp.IsSolid)
                        {
                            tileMap[x, y].hasSolidNeighbor = true;
                        }

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
            for (int x = 0; x < currentMap.GetLength(0); x++)
            {
                for (int y = 0; y < currentMap.GetLength(1); y++)
                {
                    currentMap[x, y].Draw(spriteBatch);
                }
            }
        }
    }
}

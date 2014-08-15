using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame8
{
    public class TileMapLayer
    {
        Texture2D tileTexture;
        int[,] map;
        int tileWidth, tileHeight;
        int spacing;

        public TileMapLayer(Texture2D tex,int width, int height, int tileWidth, int tileHeight, int spacing)
        {
            tileTexture = tex;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            this.spacing = spacing;

            map = new int[height, width];
        }

        public Texture2D TileSet { get { return tileTexture; } }

        public int TileMapWidth { get { return map.GetLength(1); } }
        public int TileMapHeight { get { return map.GetLength(0); } }

        public int TileWidth { get { return tileWidth; } }
        public int TileHeight { get { return tileHeight; } }

        public int TileSpacing { get { return spacing; } }

        public void SetTile(int x, int y, int tileIndex)
        {
            map[y, x] = tileIndex;
        }

        public int GetTile(int x, int y)
        {
            return map[y, x];
        }
        
    }
}

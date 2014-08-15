using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame8
{    
    public class Texture
    {
        Texture2D texture;
        int tileWidth;
        int tileHeight;
        int tileSpace;

        List<Point> tiles = new List<Point>();

        public Texture(Texture2D texture, int width, int height, int space)
        {
            this.texture = texture;
            tileWidth = width;
            tileHeight = height;
            tileSpace = space;

            CreateTiles();
        }

        public int TileSpace { get { return tileSpace; } }
        public int TileWidth { get { return tileWidth; } }
        public int TileHeight { get { return tileHeight; } }

        public int Width { get { return texture.Width; } }
        public int Height { get { return texture.Height; } }

        public Texture2D TileAtlas { get { return texture; } }

        public Rectangle GetTile(int index)
        {
            return new Rectangle(tiles[index].X, tiles[index].Y, tileWidth, tileHeight);
        }


        private void CreateTiles()
        {
            for (int y = 0; y < Height; y += tileHeight + tileSpace)
            {
                for (int x = 0; x < Width; x += tileWidth + tileSpace)
                {
                    tiles.Add(new Point(x, y));
                }
            }   
        }
    }
}

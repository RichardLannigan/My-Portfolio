using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame8
{
    public class Map
    {
        int[,] map;
        float depth;

        public Map(int width, int height, float depth)
        {
            map = new int[height, width];
            this.depth = depth;
        }

        public int GetTile(int x, int y)
        {
            return map[y, x];
        }

        public int GetTile(Point point)
        {
            return map[point.Y, point.X];
        }

        public void SetTile(int x, int y, int cellIndex)
        {
            map[y, x] = cellIndex;
        }

        public int Width
        {
            get { return map.GetLength(1); }
        }

        public int Height
        {
            get { return map.GetLength(0); }
        }

        public float Depth
        {
            get { return depth; }
        }
    }
}

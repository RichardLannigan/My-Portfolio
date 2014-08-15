using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame8
{
    class CollisionLayer
    {        
        int[,] map;

        public CollisionLayer(int width, int height)
        {
            map = new int[height, width];            
        } 


        public int GetCellIndex(int x, int y)
        {
            return map[y, x];
        }

        public void SetCellIndex(int x, int y, int cellIndex)
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

        
    }
}

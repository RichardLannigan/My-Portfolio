using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame8
{
    class MapsLayer
    {
        int[,] map;

        public CollisionLayer(int width, int height)
        {
            map = new int[height, width];            
        } 
    }
}

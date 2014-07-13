using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Assignment2
{
    class Grid
    {
        public static List<Node> grid = new List<Node>();

        public static void LoadContent()
        {
            for (int i = 0; i < 21; i++)
                for (int j = 0; j < 15; j++)
                    grid.Add(new Node(i, j, false));
        }

    }
}

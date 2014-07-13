using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System.IO;

namespace Pathfinder
{
    class Dijkstra
    {
        public bool[,] closed; //Whether or not location is closed 
        public float[,] cost; //Cost value for each location 
        public Coord2[,] link; //Link for each location = coords of a neighbouring location 
        public bool[,] inPath; //Whether or not a location is in the final path
        public Coord2[] path;

        public int pathLength = 0;
        public int timerAStar;
        int gridSize = 0;
        public bool complete = false;
        public bool runAStar = false;
        public bool runDijkstra = false;

        public TimeSpan runTime = TimeSpan.Zero;

        public Dijkstra(Level level)
        {
            gridSize = level.GridSize;
            closed = new bool[gridSize, gridSize];
            cost = new float[gridSize, gridSize];
            link = new Coord2[gridSize, gridSize];
            inPath = new bool[gridSize, gridSize];
            path = new Coord2[300];
        }

        //Reset arrays when a new map is selected.
        public void Reset(Level level)
        {
            gridSize = level.GridSize;
            closed = new bool[gridSize, gridSize];
            cost = new float[gridSize, gridSize];
            link = new Coord2[gridSize, gridSize];
            inPath = new bool[gridSize, gridSize];
            path = new Coord2[300];
        }

        public void Build(Level level, Bot bot, Player player, GameTime gameTime)
        {
            runTime = gameTime.TotalGameTime;
            for (int i = 0; i < gridSize; i++)
                for (int j = 0; j < gridSize; j++)
                {
                    closed[j, i] = false;
                    cost[j, i] = 10000;
                    link[j, i] = new Coord2(1, 1);
                    inPath[j, i] = false;
                }
            //closed[bot.GridPosition.X, bot.GridPosition.Y] = false;
            cost[bot.GridPosition.X, bot.GridPosition.Y] = 0;
            Coord2 start = (new Coord2(bot.GridPosition.X, bot.GridPosition.Y));
            timerAStar = 0;

            while (bot.GridPosition != player.GridPosition)
            {
                float lowestCost = 10000;
                int PosX = 0;
                int PosY = 0;
                for (int i = 0; i < gridSize; i++)
                {
                    for (int j = 0; j < gridSize; j++)
                    {
                        if (closed[j, i] == false && level.ValidPosition(new Coord2(j, i)) == true)
                        {
                            if (cost[j, i] < lowestCost)
                            {
                                lowestCost = cost[j, i];
                                PosX = j;
                                PosY = i;
                            }
                        }
                    }
                }
                bot.gridPosition = new Coord2(PosX, PosY);
                closed[PosX, PosY] = true;
                setAdjacentNodes(level, bot, player);
            }

            bool done = false;
            Coord2 nextClosed = player.GridPosition;
            int counter = 0;
            start = new Coord2(5, 5);
            while (!done)
            {
                inPath[nextClosed.X, nextClosed.Y] = true;
                nextClosed = link[nextClosed.X, nextClosed.Y];
                path[counter] = new Coord2(nextClosed.X, nextClosed.Y);
                counter++;
                if (nextClosed == start)
                {
                    pathLength = counter;
                    done = true;
                }
            }
            complete = true;
        }
        public void setAdjacentNodes(Level level, AiBotBase bot, Player player)
        {
            //Top
            if (cost[bot.GridPosition.X, bot.GridPosition.Y] + 1 < cost[bot.GridPosition.X, bot.GridPosition.Y - 1]
                && level.ValidPosition(new Coord2(bot.GridPosition.X, bot.GridPosition.Y - 1)) == true)
            {
                cost[bot.GridPosition.X, bot.GridPosition.Y - 1] = cost[bot.GridPosition.X, bot.GridPosition.Y] + 1;
                link[bot.GridPosition.X, bot.GridPosition.Y - 1] = new Coord2(bot.GridPosition.X, bot.GridPosition.Y);
            }
            //Top right
            if (cost[bot.GridPosition.X, bot.GridPosition.Y] + 1.4 < cost[bot.GridPosition.X + 1, bot.GridPosition.Y - 1]
                && level.ValidPosition(new Coord2(bot.GridPosition.X + 1, bot.GridPosition.Y - 1)) == true)
            {
                cost[bot.GridPosition.X + 1, bot.GridPosition.Y - 1] = cost[bot.GridPosition.X, bot.GridPosition.Y] + 1.4f;
                link[bot.GridPosition.X + 1, bot.GridPosition.Y - 1] = new Coord2(bot.GridPosition.X, bot.GridPosition.Y);
            }
            //Right
            if (cost[bot.GridPosition.X, bot.GridPosition.Y] + 1 < cost[bot.GridPosition.X + 1, bot.GridPosition.Y]
                && level.ValidPosition(new Coord2(bot.GridPosition.X + 1, bot.GridPosition.Y)) == true)
            {
                cost[bot.GridPosition.X + 1, bot.GridPosition.Y] = cost[bot.GridPosition.X, bot.GridPosition.Y] + 1;
                link[bot.GridPosition.X + 1, bot.GridPosition.Y] = new Coord2(bot.GridPosition.X, bot.GridPosition.Y);
            }
            //Bottom right
            if (cost[bot.GridPosition.X, bot.GridPosition.Y] + 1.4 < cost[bot.GridPosition.X + 1, bot.GridPosition.Y + 1]
                && level.ValidPosition(new Coord2(bot.GridPosition.X + 1, bot.GridPosition.Y + 1)) == true)
            {
                cost[bot.GridPosition.X + 1, bot.GridPosition.Y + 1] = cost[bot.GridPosition.X, bot.GridPosition.Y] + 1.4f;
                link[bot.GridPosition.X + 1, bot.GridPosition.Y + 1] = new Coord2(bot.GridPosition.X, bot.GridPosition.Y);
            }
            //Bottom
            if (cost[bot.GridPosition.X, bot.GridPosition.Y] + 1 < cost[bot.GridPosition.X, bot.GridPosition.Y + 1]
                && level.ValidPosition(new Coord2(bot.GridPosition.X, bot.GridPosition.Y + 1)) == true)
            {
                cost[bot.GridPosition.X, bot.GridPosition.Y + 1] = cost[bot.GridPosition.X, bot.GridPosition.Y] + 1;
                link[bot.GridPosition.X, bot.GridPosition.Y + 1] = new Coord2(bot.GridPosition.X, bot.GridPosition.Y);
            }
            //Bottom left
            if (cost[bot.GridPosition.X, bot.GridPosition.Y] + 1.4 < cost[bot.GridPosition.X - 1, bot.GridPosition.Y + 1]
                && level.ValidPosition(new Coord2(bot.GridPosition.X - 1, bot.GridPosition.Y + 1)) == true)
            {
                cost[bot.GridPosition.X - 1, bot.GridPosition.Y + 1] = cost[bot.GridPosition.X, bot.GridPosition.Y] + 1.4f;
                link[bot.GridPosition.X - 1, bot.GridPosition.Y + 1] = new Coord2(bot.GridPosition.X, bot.GridPosition.Y);
            }
            //Left
            if (cost[bot.GridPosition.X, bot.GridPosition.Y] + 1 < cost[bot.GridPosition.X - 1, bot.GridPosition.Y]
                && level.ValidPosition(new Coord2(bot.GridPosition.X - 1, bot.GridPosition.Y)) == true)
            {
                cost[bot.GridPosition.X - 1, bot.GridPosition.Y] = cost[bot.GridPosition.X, bot.GridPosition.Y] + 1;
                link[bot.GridPosition.X - 1, bot.GridPosition.Y] = new Coord2(bot.GridPosition.X, bot.GridPosition.Y);
            }
            //Top left
            if (cost[bot.GridPosition.X, bot.GridPosition.Y] + 1.4 < cost[bot.GridPosition.X - 1, bot.GridPosition.Y - 1]
                && level.ValidPosition(new Coord2(bot.GridPosition.X - 1, bot.GridPosition.Y - 1)) == true)
            {
                cost[bot.GridPosition.X - 1, bot.GridPosition.Y - 1] = cost[bot.GridPosition.X, bot.GridPosition.Y] + 1.4f;
                link[bot.GridPosition.X - 1, bot.GridPosition.Y - 1] = new Coord2(bot.GridPosition.X, bot.GridPosition.Y);
            }
        }
    }
}

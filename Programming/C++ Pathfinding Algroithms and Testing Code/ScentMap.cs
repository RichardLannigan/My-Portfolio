using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pathfinder
{
    class ScentMap
    {
        int gridSize = 0;
        public float[,] buffer1;
        public float[,] buffer2;
        int sourceValue;
        public float lowestValue;

        public int maxScent = 100;

        public bool complete = false;

        public Coord2 newPosition = new Coord2(0, 0);

        public ScentMap(Level level)
        {
            gridSize = level.GridSize;
            buffer1 = new float[gridSize, gridSize];
            buffer2 = new float[gridSize, gridSize];
        }
        public void build(Level level, Bot bot, Player player)
        {
            sourceValue = 0;
            gridSize = level.GridSize;
            buffer1 = new float[gridSize, gridSize];
            buffer2 = new float[gridSize, gridSize];
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    buffer1[i, j] = 0;
                    buffer2[i, j] = 0;
                }
            }
            sourceValue++;
            buffer2 = buffer1;
            int counter = 0;

            buffer1[player.GridPosition.X, player.GridPosition.Y] = maxScent; //Set the scent to begin at the player's position.
            while (counter < maxScent) //Ensures that the scent has dispersed throughout the map.
            {
                for (int i = 0; i < gridSize; i++)
                {
                    for (int j = 0; j < gridSize; j++)
                    {
                        if (buffer1[i, j] > 0)
                        {
                            for (int x = -1; x <= 1; x++)
                            {
                                for (int y = -1; y <= 1; y++)
                                {
                                    if (level.ValidPosition(new Coord2(i + x, j + y))
                                        && (level.ValidPosition(new Coord2(i + x, j)) && level.ValidPosition(new Coord2(i, j + y))))
                                    {
                                        if (buffer1[i + x, j + y] < buffer1[i, j])
                                        {
                                            buffer1[i + x, j + y] = buffer1[i, j] - 1;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                counter++;
            }
        }

        public void Run(Level level, Bot bot, Player player)
        {
            while (bot.gridPosition != player.GridPosition)
            {
                GetLowestValue();
                bot.gridPosition = FindBestLocation(level, bot, buffer1);
            }
            complete = true;
            newPosition = bot.gridPosition;
        }

        public void Update(Level level, Player player)
        {
            sourceValue++;
            buffer2 = buffer1;
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    float manhattanX = (j - player.GridPosition.X);
                    float manhattanY = (i - player.GridPosition.Y);
                    if (manhattanX < 0)
                        manhattanX = -manhattanX;
                    if (manhattanY < 0)
                        manhattanY = -manhattanY;
                    float distance = (manhattanX + manhattanY)/2;
                    buffer1[j, i] = 100 - distance;
                }
            }
            GetLowestValue();
        }
        
        public void GetLowestValue()
        {
            lowestValue = 100;
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (buffer1[i, j] < lowestValue)
                        lowestValue = buffer1[i, j];
                }
            }
        }

        public Coord2 FindBestLocation(Level level, Bot bot, float[,] buffer)
        {
            float highestScent = 0;
            int PosX = 0;
            int PosY = 0;
            Coord2 top = new Coord2(bot.GridPosition.X, bot.GridPosition.Y - 1);
            Coord2 topRight = new Coord2(bot.GridPosition.X + 1, bot.GridPosition.Y - 1);
            Coord2 right = new Coord2(bot.GridPosition.X + 1, bot.GridPosition.Y);
            Coord2 bottomRight = new Coord2(bot.GridPosition.X + 1, bot.GridPosition.Y + 1);
            Coord2 bottom = new Coord2(bot.GridPosition.X, bot.GridPosition.Y + 1);
            Coord2 bottomLeft = new Coord2(bot.GridPosition.X - 1, bot.GridPosition.Y + 1);
            Coord2 left = new Coord2(bot.GridPosition.X - 1, bot.GridPosition.Y);
            Coord2 topLeft = new Coord2(bot.GridPosition.X - 1, bot.GridPosition.Y - 1);
            float rightScent = buffer[right.X, right.Y];

            if (buffer[top.X, top.Y] > highestScent && level.ValidPosition(new Coord2(top.X, top.Y)) == true)
            {
                highestScent = buffer[top.X, top.Y];
                PosX = top.X;
                PosY = top.Y;
            }
            if (buffer[topRight.X, topRight.Y] > highestScent && level.ValidPosition(new Coord2(topRight.X, topRight.Y)) == true)
            {
                highestScent = buffer[topRight.X, topRight.Y];
                PosX = topRight.X;
                PosY = topRight.Y;
            }
            if (buffer[right.X, right.Y] > highestScent && level.ValidPosition(new Coord2(right.X, right.Y)) == true)
            {
                highestScent = buffer[right.X, right.Y];
                PosX = right.X;
                PosY = right.Y;
            }
            if (buffer[bottomRight.X, bottomRight.Y] > highestScent && level.ValidPosition(new Coord2(bottomRight.X, bottomRight.Y)) == true)
            {
                highestScent = buffer[bottomRight.X, bottomRight.Y];
                PosX = bottomRight.X;
                PosY = bottomRight.Y;
            }
            if (buffer[bottom.X, bottom.Y] > highestScent && level.ValidPosition(new Coord2(bottom.X, bottom.Y)) == true)
            {
                highestScent = buffer[bottom.X, bottom.Y];
                PosX = bottom.X;
                PosY = bottom.Y;
            }
            if (buffer[bottomLeft.X, bottomLeft.Y] > highestScent && level.ValidPosition(new Coord2(bottomLeft.X, bottomLeft.Y)) == true)
            {
                highestScent = buffer[bottomLeft.X, bottomLeft.Y];
                PosX = bottomLeft.X;
                PosY = bottomLeft.Y;
            }
            if (buffer[left.X, left.Y] > highestScent && level.ValidPosition(new Coord2(left.X, left.Y)) == true)
            {
                highestScent = buffer[left.X, left.Y];
                PosX = left.X;
                PosY = left.Y;
            }
            if (buffer[topLeft.X, topLeft.Y] > highestScent && level.ValidPosition(new Coord2(topLeft.X, topLeft.Y)) == true)
            {
                highestScent = buffer[topLeft.X, topLeft.Y];
                PosX = topLeft.X;
                PosY = topLeft.Y;
            }
            return new Coord2(PosX, PosY);
        }
    }
}

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame8
{
    static class Collisions
    {
        public static Map collisionMap;


        static public bool WalkableTile(AniminatedSprite sprite, Vector2 motion)
        {
            Vector2 nextMove = sprite.Position + motion;

            Rectangle nextRectangle = new Rectangle();
            nextRectangle.X = (int)nextMove.X + 10;
            nextRectangle.Y = (int)nextMove.Y + 62;
            nextRectangle.Width = 44;
            nextRectangle.Height = 2;


            //Rectangle nextRectangle = sprite.Bounds;
            //nextRectangle.X = (int)nextMove.X;
            //nextRectangle.Y = (int)nextMove.Y;
            

            if(motion.Y < 0 && motion.X < 0)
            {
                return CheckUpAndLeft(nextRectangle);
            }
            else if(motion.Y < 0 && motion.X == 0)
            {
                return CheckUp(nextRectangle);
            }
            else if (motion.Y < 0 && motion.X > 0)
            {
                return CheckUpAndRight(nextRectangle);
            }
            else if (motion.Y == 0 && motion.X < 0)
            {
                return CheckLeft(nextRectangle);
            }
            else if (motion.Y == 0 && motion.X > 0)
            {
                return CheckRight(nextRectangle);
            }
            else if (motion.Y > 0 && motion.X < 0)
            {
                return CheckDownAndLeft(nextRectangle);
            }
            else if (motion.Y > 0 && motion.X == 0)
            {
                return CheckDown(nextRectangle);
            }
            return CheckDownAndRight(nextRectangle);
        }


        static private bool CheckUpAndLeft(Rectangle nextRectangle)
        {
            Point tile1 = ConvertPositionToCell(new Vector2(nextRectangle.X, nextRectangle.Y));
            Point tile2 = ConvertPositionToCell(new Vector2(nextRectangle.X + nextRectangle.Width, nextRectangle.Y + nextRectangle.Height));
            bool doesCollide = false;

            for (int y = tile1.Y; y <= tile2.Y; y++)
            {
                for (int x = tile1.X; x <= tile2.X; x++)
                {
                    if (collisionMap.GetTile(x, y) == 0)
                    {
                        doesCollide = true;
                        break;
                    }
                }
            }
            return doesCollide;
        }

        static private bool CheckUp(Rectangle nextRectangle)
        {
            Point tile1 = ConvertPositionToCell(new Vector2(nextRectangle.X, nextRectangle.Y));
            Point tile2 = ConvertPositionToCell(new Vector2(nextRectangle.X + nextRectangle.Width - 1, nextRectangle.Y + nextRectangle.Height));
            bool doesCollide = false;

            int y = tile1.Y;
            for (int x = tile1.X; x <= tile2.X; x++)
            {
                if (collisionMap.GetTile(x, y) == 0)
                {
                    doesCollide = true;
                    break;
                }
            }
            return doesCollide;
        }

        static private bool CheckUpAndRight(Rectangle nextRectangle)
        {
            Point tile1 = ConvertPositionToCell(new Vector2(nextRectangle.X, nextRectangle.Y));
            Point tile2 = ConvertPositionToCell(new Vector2(nextRectangle.X + nextRectangle.Width + 1, nextRectangle.Y + nextRectangle.Height));
            bool doesCollide = false;

            for (int y = tile1.Y; y <= tile2.Y; y++)
            {
                for (int x = tile1.X; x <= tile2.X; x++)
                {
                    if (collisionMap.GetTile(x, y) == 0)
                    {
                        doesCollide = true;
                        break;
                    }
                }
            }
            return doesCollide;
        }

        static private bool CheckLeft(Rectangle nextRectangle)
        {
            Point tile1 = ConvertPositionToCell(new Vector2(nextRectangle.X, nextRectangle.Y));
            Point tile2 = ConvertPositionToCell(new Vector2(nextRectangle.X + nextRectangle.Width, nextRectangle.Y + nextRectangle.Height - 1));
            bool doesCollide = false;
            
            int x = tile1.X;
            for (int y = tile1.Y; y <= tile2.Y; y++)
            {
                if (collisionMap.GetTile(x, y) == 0)
                {
                    doesCollide = true;
                    break;
                }
            }
            return doesCollide;
        }

        static private bool CheckRight(Rectangle nextRectangle)
        {
            Point tile1 = ConvertPositionToCell(new Vector2(nextRectangle.X, nextRectangle.Y));
            Point tile2 = ConvertPositionToCell(new Vector2(nextRectangle.X + nextRectangle.Width, nextRectangle.Y + nextRectangle.Height - 1));
            bool doesCollide = false;
                        
            int x = tile2.X;
            for (int y = tile1.Y; y <= tile2.Y; y++)
            {
                if (collisionMap.GetTile(x, y) == 0)
                {
                    doesCollide = true;
                    break;
                }
            }
            return doesCollide;
        }


        static private bool CheckDownAndLeft(Rectangle nextRectangle)
        {
            Point tile1 = ConvertPositionToCell(new Vector2(nextRectangle.X, nextRectangle.Y));
            Point tile2 = ConvertPositionToCell(new Vector2(nextRectangle.X + nextRectangle.Width, nextRectangle.Y + nextRectangle.Height));
            bool doesCollide = false;

            for (int y = tile1.Y; y <= tile2.Y; y++)
            {
                for (int x = tile1.X; x <= tile2.X; x++)
                {
                    if (collisionMap.GetTile(x, y) == 0)
                    {
                        doesCollide = true;
                        break;
                    }
                }
            }
            return doesCollide;
        }


        static private bool CheckDown(Rectangle nextRectangle)
        {
            Point tile1 = ConvertPositionToCell(new Vector2(nextRectangle.X, nextRectangle.Y));
            Point tile2 = ConvertPositionToCell(new Vector2(nextRectangle.X + nextRectangle.Width - 1, nextRectangle.Y + nextRectangle.Height));
            bool doesCollide = false;
            
            int y = tile2.Y;
            for (int x = tile1.X; x <= tile2.X; x++)
            {
                if (collisionMap.GetTile(x, y) == 0)
                {
                    doesCollide = true;
                    break;
                }
            }
            return doesCollide;
        }


        static private bool CheckDownAndRight(Rectangle nextRectangle)
        {
            Point tile1 = ConvertPositionToCell(new Vector2(nextRectangle.X, nextRectangle.Y));
            Point tile2 = ConvertPositionToCell(new Vector2(nextRectangle.X + nextRectangle.Width, nextRectangle.Y + nextRectangle.Height));
            bool doesCollide = false;

            for (int y = tile1.Y; y <= tile2.Y; y++)
            {
                for (int x = tile1.X; x <= tile2.X; x++)
                {
                    if (collisionMap.GetTile(x, y) == 0)
                    {
                        doesCollide = true;
                        break;
                    }
                }
            }
            return doesCollide;
        }


        public static Point ConvertPositionToCell(Vector2 position)
        {
            return new Point((int)(position.X / (float)TileEngine.tileSize.X), (int)(position.Y / (float)TileEngine.tileSize.Y));
        }



        public static Rectangle CreateRectForCell(Point cell)
        {
            return new Rectangle(cell.X * TileEngine.tileSize.X,
                                 cell.Y * TileEngine.tileSize.Y,
                                 TileEngine.tileSize.X,
                                 TileEngine.tileSize.Y);
        }
    }
}

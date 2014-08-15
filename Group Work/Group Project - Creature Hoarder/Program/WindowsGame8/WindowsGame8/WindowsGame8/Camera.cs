using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame8
{
    static public class Camera
    {
        static public Vector2 Position = Vector2.Zero;
        static float speed = 2.0f;
        
        static public Matrix TransformMatrix
        {
            get { return Matrix.CreateTranslation(new Vector3(-Position, 0.0f)); }
        }

        static public float Speed
        {
            get { return speed; }
            set { speed = MathHelper.Clamp(value, 0.5f, 50); }
        }

        static public void LockToTarget(Player player, int screenWidth, int screenHeight)
        {
            Position.X = player.Position.X + player.CurrentAnimation.CurrentRect.Width - (screenWidth / 2);
            Position.Y = player.Position.Y + player.CurrentAnimation.CurrentRect.Height - (screenHeight / 2);
        }

        static public void LockCamera(Vector2 position, int width, int height)
        {
            Position.X = MathHelper.Clamp(Position.X, 0, width);
            Position.Y = MathHelper.Clamp(Position.Y, 0, height);
        }

        static public void ClampToArea(int width, int height)
        {
            Position.X = MathHelper.Clamp(Position.X, 0, width);
            Position.Y = MathHelper.Clamp(Position.Y, 0, height);
        }
    }
}




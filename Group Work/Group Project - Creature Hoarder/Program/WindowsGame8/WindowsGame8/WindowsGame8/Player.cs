using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame8
{
    public class Player : AniminatedSprite
    {
        InputManager inputManager;
        public int Money { get; set; }


        public Player(Game game,Texture2D texture, int newMoney) : base(texture)
        {
            inputManager = game.Services.GetService(typeof(InputManager)) as InputManager;
            OriginOffset = new Vector2(32, 64);
            Money = newMoney;
            Position = new Vector2(4224, 4672);
            FrameAnimation down = new FrameAnimation(3, 64, 64, 0, 0);
            down.FramesPerSecond = 10;
            Animations.Add("Down", down);
            FrameAnimation left = new FrameAnimation(3, 64, 64, 0, 64);
            left.FramesPerSecond = 10;
            Animations.Add("Left", left);
            FrameAnimation right = new FrameAnimation(3, 64, 64, 0, 128);
            right.FramesPerSecond = 10;
            Animations.Add("Right", right);
            FrameAnimation up = new FrameAnimation(3, 64, 64, 0, 192);
            up.FramesPerSecond = 10;
            Animations.Add("Up", up);

            CurrentAnimationName = "Down";
        }



        public override void Update(GameTime gameTime)
        {
            Vector2 motion = Vector2.Zero;
            if (inputManager.IsKeyUp(Keys.M))
            {
                TileEngine.tileSize.X = (int)(TileEngine.tileSize.X * 0.5f);
                TileEngine.tileSize.Y = (int)(TileEngine.tileSize.Y * 0.5f);
                TileEngine.mapWidthInPixels = (int)(TileEngine.tileMapWidth * TileEngine.tileSize.X);
                TileEngine.mapHeightInPixels = (int)(TileEngine.tileMapHeight * TileEngine.tileSize.Y);
            }

            if (inputManager.IsKeyPressed(Keys.Up)) motion.Y--;

            if (inputManager.IsKeyPressed(Keys.Down)) motion.Y++;

            if (inputManager.IsKeyPressed(Keys.Left)) motion.X--;

            if (inputManager.IsKeyPressed(Keys.Right)) motion.X++;

            if (motion != Vector2.Zero)
            {
                motion.Normalize();
                if(inputManager.IsKeyPressed(Keys.LeftShift))
                    motion *= Camera.Speed * 2;
                motion *= Camera.Speed;
                if (!Collisions.WalkableTile(this, motion))
                {
                    Position += motion;
                }
                UpdateSpriteAnimation(motion);
                animation = true;

                
            }
            else
            {
                animation = false;
            }

            

            
            ClampToArea(TileEngine.mapWidthInPixels, TileEngine.mapHeightInPixels);
        
            Camera.LockToTarget(this, TileEngine.screenWidth, TileEngine.screenHeight);
            Camera.ClampToArea(TileEngine.mapWidthInPixels - TileEngine.screenWidth, TileEngine.mapHeightInPixels - TileEngine.screenHeight);
            

            base.Update(gameTime);
        }





        private void UpdateSpriteAnimation(Vector2 motion)
        {
            float motionAngle = (float)Math.Atan2(motion.Y, motion.X);

            if (motionAngle >= -MathHelper.PiOver4 && motionAngle <= MathHelper.PiOver4)
            {
                CurrentAnimationName = "Right";
                //motion = new Vector2(1.0f, 0.0f);
            }
            else if (motionAngle >= MathHelper.PiOver4 && motionAngle < 3.0f * MathHelper.PiOver4)
            {
                CurrentAnimationName = "Down";
                //motion = new Vector2(0.0f, 1.0f);
            }
            else if (motionAngle <= -MathHelper.PiOver4 && motionAngle > -3.0f * MathHelper.PiOver4)
            {
                CurrentAnimationName = "Up";
                //motion = new Vector2(0.0f, -1.0f);
            }
            else
            {
                CurrentAnimationName = "Left";
                //motion = new Vector2(-1.0f, 0.0f);
            }


        }

        
    }
}

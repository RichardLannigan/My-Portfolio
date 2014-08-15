using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame8
{
    public class FrameAnimation
    {
        Rectangle[] frames;
        int currentFrame;

        float frameLength = 0.5f;
        float timer = 0;

        public FrameAnimation(int numberOfFrames, int frameWidth, int frameHeight, int xOffset, int yOffset)
        {
            frames = new Rectangle[numberOfFrames];

            for (int i = 0; i < numberOfFrames; i++)
            {                
                frames[i] = new Rectangle(xOffset + (i * frameWidth), yOffset, frameWidth, frameHeight);
            }
        }

        public int FramesPerSecond
        {
            get { return (int)(1.0f / frameLength); }
            set { frameLength = (float)Math.Max(1.0f / (float)value, 0.01f); }
        }

        public Rectangle CurrentRect
        {
            get { return frames[currentFrame]; }
        }

        public int CurrentFrame
        {
            get { return currentFrame; }
            set { currentFrame = (int)MathHelper.Clamp(value, 0, frames.Length - 1); }
        }

        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer >= frameLength)
            {
                currentFrame = (currentFrame + 1) % frames.Length;
                timer = 0;
            }
        }
    }
}

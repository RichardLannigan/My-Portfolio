using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace WindowsGame8
{
    public class AniminatedSprite
    {
        string currentAnimation = null;
        public Dictionary<string, FrameAnimation> Animations = new Dictionary<string, FrameAnimation>();
        Texture2D texture;
        public Vector2 Position = Vector2.Zero;
        public Vector2 OriginOffset = Vector2.Zero;

        protected bool animation = true;


        public bool IsAnimating
        {
            get { return animation; }
            set { animation = value; }
        }

        public Vector2 Center
        {
            get
            {
                return Position + new Vector2(CurrentAnimation.CurrentRect.Width / 2, CurrentAnimation.CurrentRect.Height / 2);
            }
        }

        public Rectangle Bounds
        {
            get
            {
                Rectangle rect = CurrentAnimation.CurrentRect;
                rect.X = (int)Position.X;
                rect.Y = (int)Position.Y;
                return rect;
            }
        }


        public Vector2 Origin
        {
            get { return Position + OriginOffset; }
        }

        public AniminatedSprite(Texture2D texture)
        {
            this.texture = texture;
        }

        public string CurrentAnimationName
        {
            get { return currentAnimation; }
            set
            {
                currentAnimation = value;
            }
        }

        public FrameAnimation CurrentAnimation
        {
            get
            {
                if (!string.IsNullOrEmpty(currentAnimation))
                    return Animations[currentAnimation];
                else
                    return null;
            }
        }



        public void ClampToArea(int width, int height)
        {
            Position.X = MathHelper.Clamp(Position.X, 0, width - CurrentAnimation.CurrentRect.Width);
            Position.Y = MathHelper.Clamp(Position.Y, 0, height - CurrentAnimation.CurrentRect.Height);
        }





        public virtual  void Update(GameTime gameTime)
        {
            if (!IsAnimating) return;

            FrameAnimation animation = CurrentAnimation;

            if (animation == null)
            {
                if (Animations.Count > 0)
                {
                    string[] keys = new string[Animations.Count];
                    Animations.Keys.CopyTo(keys, 0);

                    currentAnimation = keys[0];

                    animation = CurrentAnimation;
                }
                else
                {
                    return;
                }
            }
            animation.Update(gameTime);
        }







        public void Draw(SpriteBatch spriteBatch)
        {
            FrameAnimation animation = Animations[currentAnimation];

            if (animation != null)
            {
                spriteBatch.Draw(texture,
                                 Position,
                                 animation.CurrentRect,
                                 Color.White,
                                 0,
                                 Vector2.Zero,
                                 1,
                                 SpriteEffects.None,
                                 0.75f );
            }
        }
    }
}

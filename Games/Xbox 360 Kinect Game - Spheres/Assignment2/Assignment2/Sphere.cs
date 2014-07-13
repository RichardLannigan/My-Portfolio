using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Assignment2
{
    class Sprite
    {
        public Texture2D texture;
        public Rectangle rectangle;
        public Vector2 position;
        public Vector2 velocity;

        public int BackgroundWidth;
        public int BackgroundHeight;

        public Vector2 cameraPosition;
        public Matrix viewMatrix;

        public Random Rnd = new Random(DateTime.Now.Millisecond);

        public virtual void update()
        {
            //viewMatrix = Matrix.CreateTranslation(new Vector3(-cameraPosition, 0));
        }
        public virtual void LoadContent()
        {

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public int ScreenWidth 
        {
            get { return GraphicsDeviceManager.DefaultBackBufferWidth; }
        }

        public int ScreenHeight
        {
            get { return GraphicsDeviceManager.DefaultBackBufferHeight; }
        }
    }

    class Player : Sprite
    {
        #region
        public bool Visible;
        public bool Merged;
        public bool squished;
        public bool squishedX = false;
        public bool squishedY = false;

        public Vector2 size;
        public Vector2 origin;
        public Vector2 savedVelocity;

        public int currentFrame;
        public int currentFrameX = 1;
        public int currentFrameY = 1;
        public int frameHeight;
        public int frameWidth;
        public int sphereSize = 1;
        public int stuckTime;

        public float timer;
        public float interval = 25;

        public TimeSpan timeSpan;
        public Viewport Viewport;

        public Vector2 center { get { return (position + (size / 2)); } } // Sprite center
        public float radius { get { return size.X / 2; } } // Sprite radius

        #endregion

        public override void update()
        {

        }
        public override void LoadContent()
        {

        }
        public override void Draw(SpriteBatch playerBatch)
        {
            playerBatch.Draw(texture, position, Color.White);
        }

    }
    class Collector : Player
    {
        public Collector(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
            size.X = 100;
            size.Y = 100;
        }
        //////////////-----Collision Logic-----//////////////
        public bool CircleCollides(Sphere otherSprite)
        { // Check if two circle sprites collided
            return (Vector2.Distance(this.center, otherSprite.center) <
            this.radius + otherSprite.radius);
        }
    }
    class Blocker : Player
    {
        public Blocker(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
            size.X = 300;
            size.Y = 300;
        }
        //////////////-----Collision Logic-----//////////////
        public bool CircleCollides(Sphere otherSprite)
        { // Check if two circle sprites collided
            return (Vector2.Distance(this.center, otherSprite.center) <
            this.radius + otherSprite.radius);
        }
    }

    class Sphere : Sprite
    {
#region
        public bool Visible;
        public bool Merged;
        public bool squished;
        public bool squishedX = false;
        public bool squishedY = false;
        public bool placed = false;
        public bool collideRight = false;
        public bool collideLeft = false;
        public bool enemyRebounded = false;

        public Vector2 size;
        public Vector2 origin;
        public Vector2 savedVelocity;

        public int currentFrame;
        public int currentFrameX = 1;
        public int currentFrameY = 1;
        public int frameHeight;
        public int frameWidth;
        public int sphereSize = 1;

        public float timer;
        public float interval = 12.5f;
        public float stuckTime = 0;

        public TimeSpan timeSpan;
        public Viewport Viewport;

        public Vector2 center { get { return (position + (size / 2)); } } //Sprite center
        public float radius { get { return size.X / 2; } } //Sprite radius

#endregion
        public Sphere(Texture2D newTexture, Vector2 newPosition, int newFrameHeight, int newFrameWidth)
        {
            texture = newTexture;
            position = newPosition;
            frameHeight = newFrameHeight;
            frameWidth = newFrameWidth;
            Visible = true;
            Merged = false;
            size.X = texture.Width / 10;
            size.Y = texture.Height / 10;
        }

        public override void update()
        {
            rectangle = new Rectangle(currentFrameX * frameWidth, currentFrameY * frameHeight, frameWidth, frameHeight);
            origin = new Vector2(rectangle.Width / 2, rectangle.Height / 2);

            position += velocity;

            if (squishedX == true)
            {
                AnimateX(Game1.TS);
            }
            if (squishedY == true)
            {
                AnimateY(Game1.TS);
            }
        }
        public override void LoadContent()
        {

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, rectangle, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
        }

        //////////////-----Collision Logic-----//////////////
        public bool CircleCollides(Sphere otherSprite)
        { //Checks if two spheres have collided
            return (Vector2.Distance(this.center, otherSprite.center) <
            this.radius + otherSprite.radius);
        }
        public virtual void Boundaries(Sphere sphere, GameTime gameTime)
        {
            //if (sphere.position.X < 0)
            //{
            //    sphere.velocity.X = -sphere.velocity.X;
            //}
            //if (sphere.position.Y < 0)
            //{
            //    sphere.velocity.Y = -sphere.velocity.Y;
            //}
            //if (sphere.position.X > Game1.BackgroundWidth - size.X)
            //{
            //    sphere.velocity.X = -sphere.velocity.X;
            //}
            //if (sphere.position.Y > Game1.BackgroundHeight - size.X)
            //{
            //    sphere.velocity.Y = -sphere.velocity.Y;
            //}
            if (sphere.position.X < 0)
            {
                if (squishedX == false)
                    savedVelocity = velocity;
                velocity.X = 0;
                squishedX = true;
                currentFrameY = 2;
                //Game1.Bounce.Play();
            }
            if (sphere.position.Y < 0)
            {
                if (squishedY == false)
                    savedVelocity = velocity;
                velocity.Y = 0;
                squishedY = true;
                currentFrameY = 3;
                //Game1.Bounce.Play();
            }
            if (sphere.position.X > Game1.BackgroundWidth - size.X)
            {
                if (squishedX == false)
                    savedVelocity = velocity;
                velocity.X = 0;
                squishedX = true;
                currentFrameY = 1;
                //Game1.Bounce.Play();
            }
            if (sphere.position.Y > Game1.BackgroundHeight - size.X)
            {
                if (squishedY == false)
                    savedVelocity = velocity;
                velocity.Y = 0;
                squishedY = true;
                currentFrameY = 4;
                //Game1.Bounce.Play();
            }
        }
        public void OffScreenCheck()
        {
            if (position.X < -100)
            {
                position.X = 100;
            }
            if (position.Y < -100)
            {
                position.Y = 100;
            }
            if (position.X > Game1.BackgroundWidth)
            {
                position.X = 100;
            }
            if (position.Y > Game1.BackgroundHeight)
            {
                position.Y = 100;
            }
        }
        public bool Collides(Sphere otherSprite)
        {
            if (this.position.X + 100 > otherSprite.position.X &&
                this.position.Y + 100 > otherSprite.position.Y &&
                this.position.X < otherSprite.position.X + 100 &&
                this.position.Y < otherSprite.position.Y + 100)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CollidesTop(Sphere otherSprite)
        {
            if (this.position.X + 100 > otherSprite.position.X &&
                this.position.Y + 1 > otherSprite.position.Y &&
                this.position.X < otherSprite.position.X + 100 &&
                this.position.Y < otherSprite.position.Y + 100)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CollidesBottom(Sphere otherSprite)
        {
            if (this.position.X + 100 > otherSprite.position.X &&
                this.position.Y + 100 > otherSprite.position.Y &&
                this.position.X < otherSprite.position.X + 100 &&
                this.position.Y + 99 < otherSprite.position.Y + 100)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CollidesLeft(Sphere otherSprite)
        {
            if (this.position.X + 1 > otherSprite.position.X &&
                this.position.Y + 100 > otherSprite.position.Y &&
                this.position.X < otherSprite.position.X + 100 &&
                this.position.Y < otherSprite.position.Y + 100)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CollidesRight(Sphere otherSprite)
        {
            if (this.position.X + 100 > otherSprite.position.X &&
                this.position.Y + 100 > otherSprite.position.Y &&
                this.position.X + 99 < otherSprite.position.X + 100 &&
                this.position.Y < otherSprite.position.Y + 100)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Stuck()
        {
            if (stuckTime == 500)
            {
                stuckTime = 0;
                if (velocity.X < 0)
                    position.X += 150;
                else
                    position.X -= 150;
            }
        }

        //////////////-----Sprite Animation-----//////////////
        public void AnimateX(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (timer > interval)
            {
                timer = 0;
                if (squished == false)
                {
                    if (currentFrameX >= 8)
                    {
                        squished = true;
                        velocity.X = -savedVelocity.X;
                        if (savedVelocity.X > 0)
                            position.X -= 10;
                        else
                            position.X += 10;
                        position += velocity;
                    }
                    else
                        currentFrameX++;
                }
                else
                    if (currentFrameX <= 1)
                    {
                        squishedX = false;
                        squished = false;
                        collideRight = false;
                        collideLeft = false;
                    }
                    else
                    {
                        currentFrameX--;
                        position += velocity;
                    }
            }
        }
        public void AnimateY(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (timer > interval)
            {
                timer = 0;
                if (squished == false)
                {
                    if (currentFrameX >= 8)
                    {
                        squished = true;
                        velocity.Y = -savedVelocity.Y;
                        if (savedVelocity.Y > 0)
                            position.Y -= 10;
                        else
                            position.Y += 10;
                        position += velocity;
                    }
                    else
                        currentFrameX++;
                }
                else
                    if (currentFrameX <= 1)
                    {
                        squishedY = false;
                        squished = false;
                    }
                    else
                    {
                        currentFrameX--;
                        position += velocity;
                    }
            }
        }
    }
    class ScoreSphere : Sprite
    {
        public ScoreSphere(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
        }

        public override void update()
        {

        }
        public override void LoadContent()
        {

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
    class Background
    {
        public Texture2D texture;
        public Vector2 position;

        public Background(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
        }

        public void update()
        {

        }
        public void LoadContent()
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }

    class Node
    {
        public int x, y;
        public bool used;

        public Node(int newX, int newY, bool newUsed)
        {
            x = newX;
            y = newY;
            used = newUsed;
        }
    }
}
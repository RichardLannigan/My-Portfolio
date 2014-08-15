using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame8
{
    class Sprite : DrawableGameComponent
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected Vector2 origin;
        protected float scale;
        protected float rotation;
        protected float zLayer;
        protected Rectangle bounds;
        protected Rectangle? currrentFrame;
        protected SpriteBatch spriteBatch;
        protected Color colour;


        public Vector2 Origin
        {
            set
            {
                origin = value;
                bounds = new Rectangle((int)(position.X - origin.X), (int)(position.Y - origin.Y), texture.Width, texture.Height);
            }
        }
        public Point TextureSize
        {
            get { return new Point(texture.Width, texture.Height); }
        }

        public Vector2 Position
        {
            set { position = value; }
        }

        public Sprite(Game game, Vector2 position, Texture2D texture) : base(game)
        {
            
            spriteBatch = game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            
            this.position = position;
            this.texture = texture;

            colour = Color.White;
            
            currrentFrame = null;
            rotation = 0;
            zLayer = 1;
            scale = 1;
            origin = Vector2.Zero;
            bounds = new Rectangle((int)(position.X - origin.X), (int)(position.Y - origin.Y), texture.Width, texture.Height);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(texture, position, currrentFrame, colour, rotation, origin, scale, SpriteEffects.None, zLayer);
            base.Draw(gameTime);
        }

        

        
    }
}

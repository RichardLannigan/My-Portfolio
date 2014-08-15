using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame8
{
    class BackgroundComponent : DrawableGameComponent
    {
        Texture2D texture;
        SpriteBatch spritBatch;
        Rectangle bounds;
        Color colour;

        public BackgroundComponent(Game game, Texture2D texture) : base(game)
        {
            spritBatch = game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;

            this.texture = texture;
            bounds = game.GraphicsDevice.Viewport.Bounds;
            colour = Color.White;
        }
        

        //public override void Update(GameTime gameTime)
        //{
        //    base.Update(gameTime);
        //}

        public override void Draw(GameTime gameTime)
        {
            spritBatch.Draw(texture, bounds, colour);

            base.Draw(gameTime);
        }
    }
}

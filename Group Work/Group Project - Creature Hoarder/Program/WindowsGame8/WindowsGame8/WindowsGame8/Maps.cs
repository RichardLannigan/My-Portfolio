using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame8
{
    public class Maps : DrawableGameComponent
    {
        //Point gameTileSize = new Point(64, 64);
        List<Map> mapLayers = new List<Map>();
        Texture mapTexture;
        

       
        public static Player sprite;

        SpriteBatch spriteBatch;

       
       
        public Maps(Game game, Texture mapTexture, List<Map> mapLayers) : base(game) //, Camera camera) : base(game)
        {
            sprite = Game1.player;

            spriteBatch = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            
            this.mapTexture = mapTexture;
            this.mapLayers = mapLayers;
            
        }

        public override void Initialize()
        {
            

            base.Initialize();
        }


        public override void Update(GameTime gameTime)
        {
            Game1.player.Update(gameTime);

            base.Update(gameTime);
        }




        public override void Draw(GameTime gameTime)
        {

            Point min = ConvertPositionToCell(Camera.Position);
            Point max = ConvertPositionToCell(Camera.Position +
                                                     new Vector2(spriteBatch.GraphicsDevice.Viewport.Width + TileEngine.tileSize.X,
                                                                 spriteBatch.GraphicsDevice.Viewport.Height + TileEngine.tileSize.Y));


            min.X = (int)Math.Max(min.X, 0);
            min.Y = (int)Math.Max(min.Y, 0);
            max.X = (int)Math.Min(max.X, mapLayers[0].Width);
            max.Y = (int)Math.Min(max.Y, mapLayers[0].Height);


            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Camera.TransformMatrix);

            Game1.player.Draw(spriteBatch);

            foreach (var layer in mapLayers)
            {
                for (int y = min.Y; y < max.Y; y++)
                {
                    for (int x = min.X; x < max.X; x++)
                    {
                        if (layer.GetTile(x, y) != -1)
                        {
                            spriteBatch.Draw(mapTexture.TileAtlas,
                                             new Rectangle((int)(x * TileEngine.tileSize.X),
                                                           (int)(y * TileEngine.tileSize.Y),
                                                           (int)TileEngine.tileSize.X,
                                                           (int)TileEngine.tileSize.Y),
                                             mapTexture.GetTile(layer.GetTile(x, y)),
                                             Color.White,
                                             0,
                                             Vector2.Zero,
                                             SpriteEffects.None,
                                             layer.Depth 
                                             );
                        }
                    }
                }
            }


            spriteBatch.End();
            base.Draw(gameTime);
        }

        public virtual void Hide()
        {
            Visible = false;
            Enabled = false;
        }

        public virtual void Show()
        {
            Visible = true;
            Enabled = true;
        }


        public Point ConvertPositionToCell(Vector2 position)
        {
            return new Point((int)(position.X / TileEngine.tileSize.X), (int)(position.Y / TileEngine.tileSize.Y));
        }


    }
}

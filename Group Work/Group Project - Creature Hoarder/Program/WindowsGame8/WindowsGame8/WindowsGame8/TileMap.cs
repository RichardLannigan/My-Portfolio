using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WindowsGame8
{
    public class TileMap : DrawableGameComponent
    {
        

        //Camera camera;
        Point tileSize;
        public List<TileMapLayer> tileMapLayers = new List<TileMapLayer>();
        SpriteBatch spriteBatch;
        List<Rectangle> tiles = new List<Rectangle>();



        public TileMap(Game game, List<TileMapLayer> tileMapLayers, Point tileSize) : base(game)
        {
            this.tileSize = tileSize;
           // this.camera = camera;
            spriteBatch = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;

            this.tileMapLayers = tileMapLayers;

            int tw = tileMapLayers[0].TileWidth;
            int th = tileMapLayers[0].TileHeight;

            for (int i = 0; i < tileMapLayers[0].TileSet.Height; i += tileMapLayers[0].TileHeight + tileMapLayers[0].TileSpacing)
            {
                for (int j = 0; j < tileMapLayers[0].TileSet.Width; j += tileMapLayers[0].TileWidth + tileMapLayers[0].TileSpacing)
                {
                    tiles.Add(new Rectangle(j, i, tw, th));
                }
            }        
        }


        public override void Initialize()
        {
            base.Initialize();
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            
            Point min = ConvertPositionToCell(camera.Position);
            Point max = ConvertPositionToCell(camera.Position +
                                                     new Vector2(spriteBatch.GraphicsDevice.Viewport.Width + tileSize.X,
                                                                 spriteBatch.GraphicsDevice.Viewport.Height + tileSize.Y));


            min.X = (int)Math.Max(min.X, 0);
            min.Y = (int)Math.Max(min.Y, 0);
            max.X = (int)Math.Min(max.X, tileMapLayers[0].TileMapWidth);
            max.Y = (int)Math.Min(max.Y, tileMapLayers[0].TileMapHeight);


            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, camera.TransformMatrix);

            foreach (var layer in tileMapLayers)
            {
                for (int y = min.Y; y < max.Y; y++)
                {
                    for (int x = min.X; x < max.X; x++)
                    {
                        if (layer.GetTile(x, y) != -1)
                        {
                            spriteBatch.Draw(layer.TileSet,
                                             new Rectangle(x * tileSize.X,
                                                           y * tileSize.Y,
                                                           tileSize.X,
                                                           tileSize.Y),
                                             tiles[layer.GetTile(x, y)],
                                             Color.White);
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
            return new Point((int)(position.X / tileSize.X), (int)(position.Y / tileSize.Y));
        }

    }
}

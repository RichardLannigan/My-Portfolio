using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
//12
namespace WindowsGame8
{
    public class TileEngine : DrawableGameComponent
    {
        Maps map;
        InputManager inputManager;
        
        List<GameComponent> childComponents = new List<GameComponent>();

        public static Point tileSize = new Point(64, 64);

        public static int tileMapWidth;
        public static int tileMapHeight;

        public static int screenWidth;
        public static int screenHeight;

        public static int mapWidthInPixels;
        public static int mapHeightInPixels;
        
       
        public TileEngine(Game game, Texture myTexture, List<Map> myMaps) : base(game)
        {
            inputManager = Game.Services.GetService(typeof(InputManager)) as InputManager;

            screenWidth = game.Window.ClientBounds.Width;
            screenHeight = game.Window.ClientBounds.Height;

            tileMapWidth = myMaps[0].Width; // width in tiles
            tileMapHeight = myMaps[0].Height;

            mapWidthInPixels = (int)(tileMapWidth * tileSize.X);
            mapHeightInPixels = (int)(tileMapHeight * tileSize.Y);   


            map = new Maps(game, myTexture, myMaps);//, camera);
            childComponents.Add(map);              
        }        


        public override void Initialize()
        {
            base.Initialize();
        }


        public override void Update(GameTime gameTime)
        {
            foreach (var child in childComponents)
            {
                child.Update(gameTime);
            }

            base.Update(gameTime);
                           
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var child in childComponents)
            {
                var drawableChild = child as DrawableGameComponent;
                if (drawableChild != null && drawableChild.Visible)
                {
                    drawableChild.Draw(gameTime);
                }
            }

            base.Draw(gameTime);
        }

        public virtual void Hide()
        {
            Enabled = false;
            Visible = false;
            foreach (var child in childComponents)
            {
                child.Enabled = false;
                var drawableChild = child as DrawableGameComponent;
                if (drawableChild != null)
                {
                    drawableChild.Visible = false;
                }
            }
        }

        public virtual void Show()
        {
            Enabled = true;
            Visible = true;
            foreach (var child in childComponents)
            {
                child.Enabled = true;
                var drawableChild = child as DrawableGameComponent;
                if (drawableChild != null)
                {
                    drawableChild.Visible = true;
                }
            }
        }

        public virtual void Pause()
        {
            Enabled = !Enabled;
            foreach (var child in childComponents)
            {
                child.Enabled = !child.Enabled;
            }
        }

        
    }
}

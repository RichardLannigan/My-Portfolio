using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame8
{
    class MenuComponent : DrawableGameComponent
    {
        InputManager inputManager;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        string[] menuItems;
        int selectedIndex;
        Color normal = Color.Black;
        Color hilite = Color.Yellow;

        float height = 0.0f;
        float width = 0.0f;

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                if (selectedIndex < 0) selectedIndex = 0;
                if (selectedIndex >= menuItems.Length) selectedIndex = menuItems.Length - 1;
            }
        }

        public Vector2 Position { get; set; }
        public float Width { get { return width; } }
        public float Height { get { return height; } }


        public MenuComponent(Game game, SpriteFont spriteFont, string[] menuItems) : base(game)
        {
            inputManager = game.Services.GetService(typeof(InputManager)) as InputManager;
            spriteBatch = game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;            
            this.spriteFont = spriteFont;
            this.menuItems = menuItems;

            MeasureMenu();
        }


        private void MeasureMenu()
        {
            width = height = 0.0f;

            foreach (var item in menuItems)
            {
                Vector2 size = spriteFont.MeasureString(item);
                if (size.X > width)
                {
                    width = size.X;
                }
                height += spriteFont.LineSpacing + 5;
            }
            Position = new Vector2((Game.Window.ClientBounds.Width - width) / 2, (Game.Window.ClientBounds.Height - height) / 2);
        }


        public override void Initialize()
        {
            base.Initialize();
        }


        


        public override void Update(GameTime gameTime)
        {
            if (inputManager.IsKeyUp(Keys.Down))
            {
                selectedIndex = (selectedIndex + 1) % menuItems.Length;                
            }

            if (inputManager.IsKeyUp(Keys.Up))
            {
                selectedIndex = (selectedIndex + menuItems.Length - 1) % menuItems.Length;                
            }

            base.Update(gameTime);            
        }


        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Vector2 location = Position;
            Color tint;

            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == selectedIndex)
                {
                    tint = hilite;
                }
                else
                {
                    tint = normal;
                }
                spriteBatch.DrawString(spriteFont, menuItems[i], location, tint);
                location.Y += spriteFont.LineSpacing + 5;
            }
        }
    }
}


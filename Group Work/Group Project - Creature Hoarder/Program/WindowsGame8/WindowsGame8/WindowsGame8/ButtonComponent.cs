using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace WindowsGame8
{
    class ButtonComponent : DrawableGameComponent
    {
        InputManager inputManager;
        SpriteFont spriteFont;
        SpriteBatch spriteBatch;
        Texture2D texture;

        Color normal = Color.Black;
        Color hilite = Color.Red;

        Vector2 position;
        int selectedIndex = 0;
        StringCollection menuItems;
        int width, height;

        public ButtonComponent(Game game, SpriteFont font, Texture2D texture) : base(game)
        {
            this.spriteFont = font;
            this.texture = texture;
            inputManager = game.Services.GetService(typeof(InputManager)) as InputManager;
            spriteBatch = game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
        }

        public int Width { get { return width; } }
        public int Height { get { return height; } }

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = (int)MathHelper.Clamp(value, 0, menuItems.Count - 1); }
        }

        public Color Normal
        {
            get { return normal; }
            set { normal = value; }
        }

        public Color Hilite
        {
            get { return hilite; }
            set { hilite = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public void SetMenuItems(string[] items)
        {
            menuItems = new StringCollection();

            menuItems.Clear();
            menuItems.AddRange(items);
            CalculateBounds();
        }

        private void CalculateBounds()
        {
            width = texture.Width;
            height = 0;
            foreach (string item in menuItems)
            {
                Vector2 size = spriteFont.MeasureString(item);
                height += 5;
                height += texture.Height;
            }
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (inputManager.IsKeyUp(Keys.Down))
            {
                selectedIndex = (selectedIndex + 1) % menuItems.Count;                
            }

            if (inputManager.IsKeyUp(Keys.Up))
            {
                selectedIndex = (selectedIndex + menuItems.Count - 1) % menuItems.Count;                
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 textPosition = Position;
            Rectangle buttonBounds = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
            Color myColor;

            for (int i = 0; i < menuItems.Count; i++)
            {
                if (i == selectedIndex)
                    myColor = hilite;
                else
                    myColor = normal;

                spriteBatch.Draw(texture, buttonBounds, Color.White);

                textPosition = new Vector2(buttonBounds.X + (texture.Width / 2), buttonBounds.Y + (texture.Height / 2));
                Vector2 textSize = spriteFont.MeasureString(menuItems[i]);
                textPosition.X -= textSize.X / 2;
                textPosition.Y -= spriteFont.LineSpacing / 2;
                spriteBatch.DrawString(spriteFont, menuItems[i], textPosition, myColor);

                buttonBounds.Y += texture.Height;
                buttonBounds.Y += 5;
            }
            base.Draw(gameTime);
        }
    }
}

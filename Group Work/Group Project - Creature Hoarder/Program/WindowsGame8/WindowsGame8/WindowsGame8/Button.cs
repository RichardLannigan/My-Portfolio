using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame8
{
    class Button : Sprite
    {        
        Color textColour;
        Color textShadowColour;
        string name;
        bool isPressed = false;
        int states;

        InputManager inputManager;
        SpriteFont spriteFont;

        public string Name { get { return name; } }

        public bool IsPressed
        {
            get { return isPressed; }
            set { isPressed = value; }
        }

        public Button(Game game, string name, Vector2 position, Texture2D texture, int states, SpriteFont spriteFont) : base(game, position, texture)
        {
            inputManager = game.Services.GetService(typeof(InputManager)) as InputManager;

            this.states = states;
            origin = new Vector2(texture.Width / 2, (texture.Height / states) / 2);
            bounds = new Rectangle((int)(position.X - origin.X), (int)(position.Y - origin.Y), texture.Width, texture.Height/states);

            this.spriteFont = spriteFont;

            this.name = name;            
        }

        public override void Update(GameTime gameTime)
        {
            if (bounds.Contains(inputManager.MousePosition))
            {
                textShadowColour = new Color(49, 115, 173);
                textColour = Color.White;
                currrentFrame = new Rectangle(0, texture.Height / states, texture.Width, texture.Height / states);

                if (inputManager.IsLeftButtonUp())
                {
                    isPressed = true;
                }
                if (inputManager.IsLeftButtonPressed())
                {                  
                    currrentFrame = new Rectangle(0, (texture.Height / states) * 2, texture.Width, texture.Height / states);
                }
                
            }
            else
            {                
                textColour = Color.Black;
                textShadowColour = new Color(248, 248, 248);
                currrentFrame = new Rectangle(0, 0, texture.Width, texture.Height / states);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            
            spriteBatch.DrawString(spriteFont,
                                   name,
                                   position + Vector2.UnitY,
                                   textShadowColour,
                                   0,
                                   spriteFont.MeasureString(name) / 2,
                                   1,
                                   SpriteEffects.None,
                                   0.81f);

            spriteBatch.DrawString(spriteFont,
                                   name,
                                   position,
                                   textColour,
                                   0,
                                   spriteFont.MeasureString(name) / 2,
                                   1,
                                   SpriteEffects.None,
                                   0.8f);            
        }
                
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame8
{
    class HelpScreen : Scene
    {
        SpriteBatch spriteBatch;
        SpriteFont helpFont;
        Button arrowButton;
        Texture2D arrowTexture;
        Texture2D spriteSheet;
        int pageView = 1;

        public string SelectedName
        {
            get
            {
                Button button;
                foreach (var component in SceneComponents)
                {
                    button = component as Button;
                    if (button != null)
                    {
                        if (button.IsPressed)
                        {
                            button.IsPressed = false;
                            return button.Name;
                        }
                    }
                }
                return null;
            }
        }

        public HelpScreen(Game game, string name)
        {
            spriteBatch = game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            this.game = game;
            this.name = name;
        }

        public override void LoadContent()
        {
            SceneComponents.Add(new BackgroundComponent(game, game.Content.Load<Texture2D>(@"Backgrounds\help")));
            
            arrowTexture = game.Content.Load<Texture2D>("GUI\\arrow");
            spriteSheet = game.Content.Load<Texture2D>("Tiles\\Sprite_sheet_05");
            helpFont = game.Content.Load<SpriteFont>("Fonts\\helpfont");
            arrowButton = new Button(game, "", new Vector2(1100, 620), arrowTexture, 3, helpFont);
            
            Button button = new Button(game,
                                "Back",
                                new Vector2(game.Window.ClientBounds.Width / 2,
                                            game.Window.ClientBounds.Height - 80),
                                game.Content.Load<Texture2D>(@"GUI\buttonall"),
                                3,
                                game.Content.Load<SpriteFont>(@"Fonts\menufont"));
            //button.Origin = new Vector2(button.TextureSize.X / 2, button.TextureSize.Y / 2);
            SceneComponents.Add(button);
            //SceneComponents.Add(arrowButton);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (arrowButton.IsPressed && pageView < 2)
                pageView++;
            else if (arrowButton.IsPressed)
                pageView = 1;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            base.Draw(gameTime);

            if (pageView == 1)
            {
                spriteBatch.DrawString(helpFont, "So, keen adventurer! You wish to enter the world of Creatures, do you? Here are some things you'll need to know:", new Vector2(100, 200), Color.White);
                spriteBatch.DrawString(helpFont, "The objective of the game is to breed Creatures. Find the best ones, put them together and see what comes out!", new Vector2(100, 240), Color.White);
                spriteBatch.DrawString(helpFont, "You can go to the Ranch to manage your creatures. You can train and nurture them there.", new Vector2(100, 280), Color.White);
                spriteBatch.DrawString(helpFont, "Once you're ready, head out to the Arena and see if you've trained hard enough.", new Vector2(100, 320), Color.White);
                spriteBatch.DrawString(helpFont, "Not ready for the arena? Explore the world and find more creatures to breed!", new Vector2(100, 360), Color.White);
            }
            else if (pageView == 2)
            {
                spriteBatch.DrawString(helpFont, "This will teleport you:", new Vector2(100, 200), Color.White);
                spriteBatch.Draw(spriteSheet, new Vector2(100, 240), new Rectangle(512, 383, 256, 190), Color.White);
            }

            spriteBatch.End();
        }       
    }
}

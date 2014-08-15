using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame8
{
    class BreedScene : Scene
    {
        SpriteBatch spriteBatch;

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

        public BreedScene(Game game, string name)
        {
            spriteBatch = game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            this.game = game;
            this.name = name;
        }

        public override void LoadContent()
        {
            Button tempButton;
            SceneComponents.Add(new BackgroundComponent(game, game.Content.Load<Texture2D>(@"Backgrounds\status")));
            
            string[] menuItems = { "Choose Mother","Choose Father"};//first line
            for (int count = 0; count < menuItems.Length; count++)
            {
                tempButton = new Button(game,
                                        menuItems[count],
                                        new Vector2(300f * (count + 1), game.Window.ClientBounds.Height - 80),
                                        game.Content.Load<Texture2D>(@"GUI\buttonall"),
                                        3,
                                        game.Content.Load<SpriteFont>(@"Fonts\menufont"));
                SceneComponents.Add(tempButton);
            }
            if (Game1.NoCreatures == 30)
            menuItems = new string[] { "Back", "No space" };//second line
            else
                menuItems = new string[] { "Back", "Begin Breeding" };
            for (int count = 0; count < menuItems.Length; count++)
            {
                tempButton = new Button(game,
                                        menuItems[count],
                                        new Vector2(300f * (count + 1), game.Window.ClientBounds.Height - 160),
                                        game.Content.Load<Texture2D>(@"GUI\buttonall"),
                                        3,
                                        game.Content.Load<SpriteFont>(@"Fonts\menufont"));
                SceneComponents.Add(tempButton);

            }


            base.LoadContent();
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            base.Draw(gameTime);
            string[] input = { "mother", Convert.ToString(SceneManager.mother), "father", Convert.ToString(SceneManager.father), "target", Convert.ToString(SceneManager.breedTarget) };
            list(game.Content.Load<SpriteFont>(@"Fonts\menufont"), new Vector2(100, 100), new Vector2(120, 0), input, Color.White);

            spriteBatch.End();
        }

        public void list(SpriteFont font, Vector2 position, Vector2 offset, string[] entries, Color textC)
        {
            for (int count = 0; count < entries.Length; count++)
            {
                spriteBatch.DrawString(font, entries[count], position, textC);
                position += offset;
            }
        }

    }
}

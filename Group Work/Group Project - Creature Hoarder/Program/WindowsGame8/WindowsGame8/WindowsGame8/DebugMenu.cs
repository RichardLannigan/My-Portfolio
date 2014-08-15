using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame8
{
    class DebugMenu : Scene
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

        public DebugMenu(Game game, string name)
        {
            spriteBatch = game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            this.game = game;
            this.name = name;
        }

        public override void LoadContent()
        {
            Button tempButton;
            Sprite tempSprite;

            SceneComponents.Add(new BackgroundComponent(game, game.Content.Load<Texture2D>(@"Backgrounds\Title")));

            //tempSprite = new Sprite(game, new Vector2(game.Window.ClientBounds.Width / 2, 25), game.Content.Load<Texture2D>(@"Sprite\Title"));
           // tempSprite.Origin = new Vector2(tempSprite.TextureSize.X / 2, 0);
            //SceneComponents.Add(tempSprite);
            string[] menuItems = { "Back", "Cstatus","Breed","battle","end day"};
            for (int count = 0; count < menuItems.Length; count++)
            {
                tempButton = new Button(game,
                                        menuItems[count],
                                        new Vector2(game.Window.ClientBounds.Width / 2, game.Window.ClientBounds.Height - (count*80 + 80)),
                                        game.Content.Load<Texture2D>(@"GUI\buttonall"),
                                        3,
                                        game.Content.Load<SpriteFont>(@"Fonts\menufont"));
                SceneComponents.Add(tempButton);
            }
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            base.Draw(gameTime);
            spriteBatch.End();
        }

    }
}

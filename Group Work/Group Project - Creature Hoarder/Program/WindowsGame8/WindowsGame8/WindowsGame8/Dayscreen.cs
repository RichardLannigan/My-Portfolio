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
    class Dayscreen: Scene
    {
        static List<double> statusEntry = new List<double>();
        static List<string> output = new List<string>();
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
        static public void EndDay()
        {
            statusEntry = new List<double>();
            int food = 0;
            double outputs;
            for (int count = 0; count < Game1.NoCreatures; count++)
            {
                outputs = Jobs.DoJob(Game1.codes[count], ref Game1.ranch[count]);
                if (Game1.codes[count] == 0)
                    output.Add((count+1) + " Rested");
                else if (Game1.codes[count] > 99 && Game1.codes[count] < 200)
                    output.Add((count + 1) + " Earned " + outputs + " at work");
                else if (Game1.codes[count] > 199 && Game1.codes[count] < 300)
                    output.Add((count + 1) + " Trained its stat by " + outputs + " today");
            }
            for (int count = 0; count < Game1.NoCreatures; count++)
            {
                Game1.ranch[count].age++;
                if (Game1.ranch[count].age >= Game1.ranch[count].maxAge)
                {
                    statusEntry.Add(count + 1);
                }
                else
                {
                    food += (int)Game1.ranch[count].BST / 40;
                    if (Game1.ranch[count].happiness == 0)
                    {
                        statusEntry.Add(-count - 1);
                    }
                }
            }
            for (int count = statusEntry.Count; count > 0; count--)
            {
                if (statusEntry[count-1] >= 0)
                    Game1.kill((int)statusEntry[count-1] - 1);
                else
                    Game1.kill((int)-statusEntry[count-1] + 1);
            }
            if (Game1.player.Money >= food)
            {
                Game1.player.Money -= food;
                statusEntry.Add(food);
            }
            else
            {
                double percent = Game1.player.Money / food;
                Game1.player.Money = 0;
                statusEntry.Add(-percent*100);
                for (int count = 0; count < Game1.NoCreatures; count++)
                {
                    Game1.ranch[count].happiness += (25 * percent)-25; 
                }
            }
              for (int count = 0; count < statusEntry.Count - 1; count++)
            {
                if (statusEntry.Count >= 0)
                {
                    output.Add("creature " + statusEntry[count] + " has died");
                }
                else
                {
                    output.Add("creature " + statusEntry[count] + " has ran away");
                    
                }


            }
            if (statusEntry[statusEntry.Count-1] >= 0)
            {
                output.Add("you spent " + statusEntry[statusEntry.Count - 1] + " on food");
            
            }
            else
            {
                output.Add("you spent " + statusEntry[statusEntry.Count - 1] + " on food, but it wasn't enough");
            }
       
            for (int count = 0; count < Game1.NoCreatures; count++)
            {
                Game1.ranch[count].retrain();
            }
            
        }
        public Dayscreen(Game game, string name)
        {
            spriteBatch = game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            this.game = game;
            this.name = name;
        }

        public override void LoadContent()
        {
            Button tempButton;
            SceneComponents.Add(new BackgroundComponent(game, game.Content.Load<Texture2D>(@"Backgrounds\status")));

            string[] menuItems = { "Back" };
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


            base.LoadContent();
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            base.Draw(gameTime);

            list(game.Content.Load<SpriteFont>(@"Fonts\menufont"), new Vector2(100, 100), new Vector2(0, 30), output, Color.White);

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
        public void list(SpriteFont font, Vector2 position, Vector2 offset, List<string> entries, Color textC)
        {
            for (int count = 0; count < entries.Count; count++)
            {
                spriteBatch.DrawString(font, entries[count], position, textC);
                position += offset;
            }
        }
            
          

    }
}

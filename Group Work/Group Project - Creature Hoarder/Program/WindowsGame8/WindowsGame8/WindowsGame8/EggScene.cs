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
    class EggScene : Scene
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

        public EggScene(Game game, string name)
        {
            spriteBatch = game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            this.game = game;
            this.name = name;
        }

        public override void LoadContent()
        {
            Button tempButton;
            TButton tbutton;
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
            menuItems = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12","13","14","15","16"};
            for (int counter = 0; counter < (Game1.BreedNo/4); counter++)
            {
                for (int count = 0; count < (Game1.BreedNo % 4); count++)
                {
                    tbutton = new TButton(game,
                                             menuItems[counter * 4 + count],
                                             new Vector2(100f * (count + 1) + 100, game.Window.ClientBounds.Height - (200 + counter * 50)),
                                             game.Content.Load<Texture2D>(@"GUI\buttonsmall"),
                                             3,
                                             game.Content.Load<SpriteFont>(@"Fonts\menufont"));
                    SceneComponents.Add(tbutton);
                }
            }

            base.LoadContent();
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            base.Draw(gameTime);
            string[] statusEntry = { "STR", "HP", "STK", "BLO", "DEF", "CON", "SPD", "aggressive", "courage", "mammal" };
            list(game.Content.Load<SpriteFont>(@"Fonts\menufont"), new Vector2(100, 100), new Vector2(0, 30), statusEntry, Color.White);
            statusEntry = new string[] { Convert.ToString(Game1.ranch[Game1.targetCreature - 1].STR.total), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].HP.total), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].STK.total), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].BLO.total), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].DEF.total), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].CON.total), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].SPD.total), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].aggressive.total), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].courage.total), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].mammal.total) };
            list(game.Content.Load<SpriteFont>(@"Fonts\menufont"), new Vector2(240, 100), new Vector2(0, 30), statusEntry, Color.White);
            statusEntry = new string[] { "reptile", "bird", "wings", "insect", "amphibious", "stance", "lifespan" };
            list(game.Content.Load<SpriteFont>(@"Fonts\menufont"), new Vector2(320, 100), new Vector2(0, 30), statusEntry, Color.White);
            statusEntry = new string[] { Convert.ToString(Game1.ranch[Game1.targetCreature - 1].reptile.total), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].bird.total), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].wings.total), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].insect.total), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].amphibious.total), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].stance.total), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].lifespan.total) };
            list(game.Content.Load<SpriteFont>(@"Fonts\menufont"), new Vector2(460, 100), new Vector2(0, 30), statusEntry, Color.White);
            statusEntry = new string[] { "atkPerf", "defPerf", "stamina", "tech", "litter", "fickle", "vanity", "eggHappy", "tallness" };
            list(game.Content.Load<SpriteFont>(@"Fonts\menufont"), new Vector2(540, 100), new Vector2(0, 30), statusEntry, Color.White);
            statusEntry = new string[] { Convert.ToString(Game1.ranch[Game1.targetCreature - 1].atkPerf.total), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].defPerf.total), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].stamina.total), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].tech.total), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].litter.total), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].fickle.total), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].vanity.total), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].eggHappy.total), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].tallness.total) };
            list(game.Content.Load<SpriteFont>(@"Fonts\menufont"), new Vector2(680, 100), new Vector2(0, 30), statusEntry, Color.White);
            statusEntry = new string[] { "limb", "bulk", "feet", "thickness", "cuteness", "metabolism", "BST", "age", "happiness", "spoilage" };
            list(game.Content.Load<SpriteFont>(@"Fonts\menufont"), new Vector2(760, 100), new Vector2(0, 30), statusEntry, Color.White);
            statusEntry = new string[] { Convert.ToString(Game1.ranch[Game1.targetCreature - 1].limb.total), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].bulk.total), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].feet.total), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].thickness.total), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].cuteness.total), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].metabolism.total), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].BST), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].age), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].happiness), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].spoilage) };
            list(game.Content.Load<SpriteFont>(@"Fonts\menufont"), new Vector2(900, 100), new Vector2(0, 30), statusEntry, Color.White);
            statusEntry = new string[] { "curStam", "maxStam", "stamRate", "happyRate", "spoilRate", "restRate", "maxAge", "equips1", "equips2", "equips3" };
            list(game.Content.Load<SpriteFont>(@"Fonts\menufont"), new Vector2(980, 100), new Vector2(0, 30), statusEntry, Color.White);
            statusEntry = new string[] { Convert.ToString(Game1.ranch[Game1.targetCreature - 1].curStam), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].maxStam), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].stamRate), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].happyRate), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].spoilRate), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].restRate), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].maxAge), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].equips[0]), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].equips[1]), Convert.ToString(Game1.ranch[Game1.targetCreature - 1].equips[2]) };
            list(game.Content.Load<SpriteFont>(@"Fonts\menufont"), new Vector2(1120, 100), new Vector2(0, 30), statusEntry, Color.White);

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

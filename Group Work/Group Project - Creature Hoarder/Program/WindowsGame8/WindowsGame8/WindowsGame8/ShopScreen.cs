using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame8
{
    class ShopScreen: Scene
    {
        SpriteBatch spriteBatch;
        SpriteFont font;
        bool[] bought = new bool[16];
        int amountFood = 0;
        int amountPotion = 0;
        bool armourBought;
        bool weaponBought;
        Texture2D armour;
        Texture2D food;
        Texture2D potion;
        Texture2D weapon;
        //Player player = Maps.sprite; moved to game1
        Button armourButton;
        Button foodButton;
        Button potionButton;
        Button weaponButton;

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

        public ShopScreen(Game game, string name)
        {
            spriteBatch = game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            this.game = game;
            this.name = name;
        }

        public override void LoadContent()
        {
            SceneComponents.Add(new BackgroundComponent(game, game.Content.Load<Texture2D>(@"Shop\Shop")));
            font = game.Content.Load<SpriteFont>(@"Fonts\helpfont");
            
            armour = game.Content.Load<Texture2D>(@"Shop\Shop_Buttons_Armour");
            food = game.Content.Load<Texture2D>(@"Shop\Shop_Buttons_Food");
            potion = game.Content.Load<Texture2D>(@"Shop\Shop_Buttons_Potions");
            weapon = game.Content.Load<Texture2D>(@"Shop\Shop_Buttons_Weapons");
            armourButton = new Button(game, "", new Vector2(866, 107), armour, 3, font);
            foodButton = new Button(game, "", new Vector2(866, 235), food, 3, font);
            potionButton = new Button(game, "", new Vector2(866, 363), potion, 3, font);
            weaponButton = new Button(game, "", new Vector2(866, 491), weapon, 3, font);

            SceneComponents.Add(armourButton);
            SceneComponents.Add(foodButton);
            SceneComponents.Add(potionButton);
            SceneComponents.Add(weaponButton);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (armourButton.IsPressed)
            {
                if (Game1.player.Money >= 150 && !armourBought)
                {
                    Game1.player.Money -= 150;
                    bought[1] = true;
                    armourBought = true;
                }
            }
            armourButton.IsPressed = false;

            if (foodButton.IsPressed)
                if (Game1.player.Money >= 20)
                {
                    Game1.player.Money -= 20;
                    amountFood++;
                    bought[4] = true;
                }
            foodButton.IsPressed = false;

            if (potionButton.IsPressed)
                if (Game1.player.Money >= 80)
                {
                    Game1.player.Money -= 80;
                    amountPotion++;
                    bought[8] = true;
                }
            potionButton.IsPressed = false;

            if (weaponButton.IsPressed)
                if (Game1.player.Money >= 120 && !weaponBought)
                {
                    Game1.player.Money -= 120;
                    bought[12] = true;
                    weaponBought = true;
                }
            weaponButton.IsPressed = false;

        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            base.Draw(gameTime);
            spriteBatch.DrawString(font, "" + Game1.player.Money, new Vector2(50, 650), Color.White);
            //spriteBatch.Draw(

            for (int i = 3; i >= 0; i--)
            {
                if (bought[i])
                {
                    spriteBatch.Draw(armour, new Vector2(805, 565), new Rectangle(0, 10, 85, 100), Color.White);
                }
            }

            for (int i = 7; i > 3; i--)
            {
                if (bought[i])
                {
                    spriteBatch.Draw(food, new Vector2(917, 568), new Rectangle(0, 10, 85, 100), Color.White);
                    if (amountFood > 1)
                        spriteBatch.DrawString(font, "" + amountFood, new Vector2(990, 655), Color.White);
                }
            }

            for (int i = 11; i > 7; i--)
            {
                if (bought[i])
                {
                    spriteBatch.Draw(potion, new Vector2(1031, 571), new Rectangle(0, 10, 85, 100), Color.White);
                    if (amountPotion > 1)
                        spriteBatch.DrawString(font, "" + amountPotion, new Vector2(1100, 655), Color.White);
                }
            }

            for (int i = 15; i > 11; i--)
            {
                if (bought[i])
                {
                    spriteBatch.Draw(weapon, new Vector2(1142, 565), new Rectangle(0, 10, 85, 100), Color.White);
                }
            }

            spriteBatch.End();
        }       
    }
}

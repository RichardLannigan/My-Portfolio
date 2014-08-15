using System;
using System.Collections;
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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public static Random rand = new Random();
        public static creature[] ranch = new creature[30];
        public static creature[] clutch = new creature[16];
        public static int NoCreatures = 15;
        public static int BreedNo;
        public static int targetCreature = 1;//what creature to show, -1 before using
        public static Player player = Maps.sprite;
        public static Stack menustack = new Stack();
        public static int battleStatus = 1;//0 = in battle, 1 = player win, 2 = player lose
        public static double HPmultiplyer = 5;
        public static int[] codes = new int[30];
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            for (int count = 0; count < 30; count++)
                ranch[count] = new creature();
        }


        //public void list(SpriteFont font, Vector2 position, Vector2 offset, string[] entries, Color textC,int repeats,Vector2 Roffset)
        //{
        //    for (int counter = -1; counter < repeats; counter++)
        //    {
        //        for (int count = 0; count < entries.Length; count++)
        //        {
        //            spriteBatch.DrawString(font, entries[count], position, textC);
        //            position += offset;
        //        }
        //        position += Roffset;
        //    }
        //}
        static public void kill(int target)
        {
            for (int count = target; count < NoCreatures; count++)
            {
                ranch[target] = ranch[target + 1];
            }
            NoCreatures--;
            if (NoCreatures == 0)
            {
                NoCreatures++;
                    ranch[1].generate(210);
            }
        }
        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Services.AddService(typeof(SpriteBatch), spriteBatch);
            Services.AddService(typeof(InputManager), new InputManager(this));
            //Services.AddService(typeof(Game), this);
            //Services.AddService(typeof(ContentManager), Game);
            Components.Add(Services.GetService(typeof(InputManager)) as InputManager);
            Components.Add(new SceneManager(this));
            for (int count = 0; count < 15; count++)
            {
                Game1.ranch[count].generate(2100);
                Game1.ranch[count].initilize();
            }


            base.Initialize();
        }


        protected override void LoadContent() 
        {

            player = new Player(this, Content.Load<Texture2D>("Sprite/Player_Sprites"), 14000);
        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //spriteBatch.Begin();

            base.Draw(gameTime);

            //spriteBatch.End();
        }
    }
}

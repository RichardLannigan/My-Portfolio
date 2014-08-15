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
using Microsoft.Kinect;

namespace Assignment2
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
#region
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteBatch playerBatch;
        Random Rnd = new Random(DateTime.Now.Millisecond);
        Random randX = new Random();
        Random randY = new Random();

        public static GameTime TS;

        //Menu
        int MenuControllerIndex = 1;
        int MenuPlayGame = 1;
        int MenuOptions = 2;
        int MenuQuit = 3;

        bool pause = false;
        bool escapeUp = true;
        bool gameOver = false;
        bool gameWin = false;

        Texture2D MenuPlayButton;
        Texture2D MenuOptionsButton;
        Texture2D MenuBackground;
        Texture2D OptionsBackground;

        SpriteFont MenuPlayText;
        SpriteFont MenuOptionsText;
        SpriteFont GameOverText;

        bool UpButtonUp;
        bool DownButtonUp;

        //HUD
        Texture2D HUDBar;
        Texture2D PlayerHealthBar;
        Texture2D EnemyHealthBar;
        Texture2D BlockersBar;

        Rectangle HUDBarRectangle;
        Rectangle BlockersBarRectangle;
        Rectangle PlayerHealthBarRectangle;
        Rectangle EnemyHealthBarRectangle;

        int blockerDepletion = 0;
        int EnemyScore = 0;
        bool blockerDepleted = false;

        //Sphere
        Texture2D sphereTexture;
        Vector2 spherePosition;
        Vector2 sphereVelocity;
        bool Capture;
        public static int redSphereCount = 8;
        public static int blueSphereCount = 8;
        public static int greenSphereCount = 8;
        public static int yellowSphereCount = 8;
        public static int enemySphereCount = 8;
        public static int ScoreBoardCount = 8;

        bool checkRedPlacement = false;
        bool checkBluePlacement = false;
        bool checkGreenPlacement = false;
        bool checkYellowPlacement = false;
        bool checkEnemyPlacement = false;

        public bool CollectionMode = false;
        public bool CollectionComplete = false;
        public bool RedCollected = false;
        public bool BlueCollected = false;
        public bool GreenCollected = false;
        public bool YellowCollected = false;

        public bool mergeComplete = false;
        public bool mergeRed = false;
        public bool mergeBlue = false;
        public bool mergeGreen = false;
        public bool mergeYellow = false;

        bool CheckRedCollision = false;
        bool CheckBlueCollision = false;
        bool CheckGreenCollision = false;
        bool CheckYellowCollision = false;

        bool menuScreen = false;
        bool gameScreen = false;

        bool buttonReady = true;
        bool buttonReady2 = true;

        int collectorSwitch = 0;
        int collectorIndex = 0;
        int addIndex = 0;

        //ScoreBoard
        public static int redScore = 8;
        public static int blueScore = 8;
        public static int greenScore = 8;
        public static int yellowScore = 8;

        //public static int ScreenWidth = ;
        //public static int ScreenHeight;

        //Camera
        public static Vector2 cameraPosition;
        Matrix viewMatrix;
        public static int ScreenWidth = 1080;
        public static int ScreenHeight = 600;

        //Background
        Texture2D backgroundTexture;
        public static int BackgroundWidth = 2400;
        public static int BackgroundHeight = 1800;
        
        Background background;

        //Kinect
        KinectSensor kinectSensor;
        Texture2D kinectRGBVideo;
        //Rectangle kinectRGBVideo;
        string connectedStatus;

        Vector2 rightHandPosition;
        Vector2 leftHandPosition;
        Vector2 rightElbowPosition;
        Vector2 leftElbowPosition;
        Vector3 hipPosition;

        //Blockers
        Collector rightElbowBlocker;
        Collector leftElbowBlocker;
        Blocker Blocker;

        bool convertRed = false;
        bool convertBlue = false;
        bool convertGreen = false;
        bool convertYellow = false;

        int temp = 0;
        
        bool RightBlockerActive = false;
        bool LeftBlockerActive = false;

        //Collectors
        Collector MainCollecter;
        Collector leftHandCollector;
        Collector rightHandCollector;

        bool enemyConvert = false;
        bool reboundEnemies = false;

        //Sounds
        public static SoundEffect Bounce;
        SoundEffect SphereMerge;
        SoundEffect enemyConversion;
        //Song backgroundMusic;
        AudioEngine audioEngine;
        WaveBank waveBank;
        SoundBank soundBank;
        Cue MergeSound;
        Cue BackgroundMusic;
        Cue ConvertSound;

        int soundIndex = 1;

        enum GameState
        {
            MainMenu,
            Options,
            Help,
            Playing,
        }
        GameState CurrentGameState = GameState.MainMenu;

        ButtonClass buttonPlay;

#endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1080;
            graphics.PreferredBackBufferHeight = 600;
            //if (!graphics.IsFullScreen)
            //    graphics.ToggleFullScreen();
        }

        protected override void Initialize()
        {
            KinectSensor.KinectSensors.StatusChanged += new
                    EventHandler<StatusChangedEventArgs>(KinectSensors_StatusChanged);
            DiscoverKinectSensor();
            kinectRGBVideo = new Texture2D(GraphicsDevice, 160, 120);

            //InitialCollision();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            #region
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Menu
            MenuPlayButton = Content.Load<Texture2D>("Menu/PlayButton");
            MenuOptionsButton = Content.Load<Texture2D>("Menu/PlayButton");
            MenuBackground = Content.Load<Texture2D>("MenuScreen");
            OptionsBackground = Content.Load<Texture2D>("OptionsScreen");

            MenuPlayText = Content.Load<SpriteFont>("GameFont");
            MenuOptionsText = Content.Load<SpriteFont>("GameFont");
            GameOverText = Content.Load<SpriteFont>("GameFont");

            //HUD
            HUDBar = Content.Load<Texture2D>("HUD/HUD_Bar"); 
            PlayerHealthBar = Content.Load<Texture2D>("HUD/Health_Bar");
            EnemyHealthBar = Content.Load<Texture2D>("HUD/Enemy_Bar");
            BlockersBar = Content.Load<Texture2D>("Blockers_Bar");

            //Grid
            Grid.LoadContent();

            sphereTexture = Content.Load<Texture2D>("Collectors/Sphere");
            spherePosition = new Vector2(10, 10);

            //Collectors
            MainCollecter = new Collector(Content.Load<Texture2D>("Collectors/Sphere"), rightHandPosition + cameraPosition);
            rightHandCollector = new Collector(Content.Load<Texture2D>("Collectors/Sphere"), rightHandPosition + cameraPosition);
            leftHandCollector = new Collector(Content.Load<Texture2D>("Collectors/Sphere"), leftHandPosition + cameraPosition);

            //Blockers
            rightElbowBlocker = new Collector(Content.Load<Texture2D>("Collectors/Sphere"), rightElbowPosition + cameraPosition);
            leftElbowBlocker = new Collector(Content.Load<Texture2D>("Collectors/Sphere"), leftElbowPosition + cameraPosition);
            Blocker = new Blocker(Content.Load<Texture2D>("Collectors/Blocker"), rightHandPosition + cameraPosition);

            backgroundTexture = Content.Load<Texture2D>("Background");
            background = new Background(Content.Load<Texture2D>("Background"), new Vector2(0,0));

            SphereList.LoadContent(Content, randX, randY);
            SphereList.CreateScoreBoard(Content);

            checkRedPlacement = true;
            SpherePlacement(SphereList.RedSphereList);
            checkRedPlacement = false;
            
            checkBluePlacement = true;
            SpherePlacement(SphereList.BlueSphereList);
            checkBluePlacement = false;

            checkGreenPlacement = true;
            SpherePlacement(SphereList.GreenSphereList);
            checkGreenPlacement = false;

            checkYellowPlacement = true;
            SpherePlacement(SphereList.YellowSphereList);
            checkYellowPlacement = false;

            checkEnemyPlacement = true;
            SpherePlacement(SphereList.EnemySphereList);
            checkEnemyPlacement = false;

            foreach (ScoreSphere s in SphereList.ScoreBoardRedList)
            {
                s.LoadContent();
            }

            //Sounds
            //Bounce = Content.Load<SoundEffect>("Sounds/Bounce");
            //SphereMerge = Content.Load<SoundEffect>("Sounds/Sphere_Merge");
            //enemyConversion = Content.Load<SoundEffect>("Sounds/Eye_Poke");
            //backgroundMusic = Content.Load<Song>("Sounds/Ambient_Music");
            //MediaPlayer.Play(backgroundMusic);            
            audioEngine = new AudioEngine(Content.RootDirectory + "//GameMusic.xgs");
            waveBank = new WaveBank(audioEngine, Content.RootDirectory + "//Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, Content.RootDirectory + "//Sound Bank.xsb");
            MergeSound = soundBank.GetCue("Merge_Cue");
            BackgroundMusic = soundBank.GetCue("Music_Cue");
            ConvertSound = soundBank.GetCue("Convert_Cue");
            Settings.musicVolume = 1.0f;
            Settings.soundEffectsVolume = 1.0f;

            BackgroundMusic.Play();

            IsMouseVisible = true;

            //buttonPlay = new ButtonClass(Content.Load<Texture2D>("PlayButton"), graphics.GraphicsDevice);
            //buttonPlay.setPosition(new Vector2(350, 300));

            #endregion
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (pause == false)
            {
                #region
                MouseState mouse = Mouse.GetState();

                switch (CurrentGameState)
                {
                    case GameState.MainMenu:
                        graphics.PreferredBackBufferWidth = 1080;
                        graphics.PreferredBackBufferHeight = 600;
                        graphics.ApplyChanges();
                        if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed)
                            this.Exit();
                        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                            this.Exit();
                        //buttonPlay.Update(mouse);
                        break;

                    case GameState.Playing:
                        Playing(gameTime);
                        break;

                    case GameState.Options:
                        //if (Keyboard.GetState().IsKeyDown(Keys.Up))
                        if (Keyboard.GetState().IsKeyDown(Keys.Right) || GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed)
                        {
                            if (soundIndex == 1)
                            {
                                if (buttonReady == true)
                                {
                                    Settings.musicVolume += 0.25f;
                                    audioEngine.GetCategory("Music").SetVolume(Settings.musicVolume);
                                }
                            }
                            else if (buttonReady == true)
                            {
                                Settings.soundEffectsVolume += 0.25f;
                                audioEngine.GetCategory("SoundEffects").SetVolume(Settings.soundEffectsVolume);
                                MergeSound = soundBank.GetCue("Merge_Cue");
                                MergeSound.Play();
                            }
                            buttonReady = false;
                        }
                        if (Keyboard.GetState().IsKeyUp(Keys.Right) && GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Released)
                        {
                            buttonReady = true;
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Left) || GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed)
                        {
                                if (soundIndex == 1)
                                {
                                    if (Settings.musicVolume > 1f)
                                    {
                                        if (buttonReady2 == true)
                                        {
                                            Settings.musicVolume -= 0.25f;
                                            audioEngine.GetCategory("Music").SetVolume(Settings.musicVolume);
                                        }
                                    }
                                }
                                else if (buttonReady2 == true)
                                {
                                    if (Settings.soundEffectsVolume > 1f)
                                    {
                                        Settings.soundEffectsVolume -= 0.25f;
                                        audioEngine.GetCategory("SoundEffects").SetVolume(Settings.soundEffectsVolume);
                                        MergeSound = soundBank.GetCue("Merge_Cue");
                                        MergeSound.Play();
                                    }
                                }

                            buttonReady2 = false;
                        }
                        if (Keyboard.GetState().IsKeyUp(Keys.Left) && GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Released)
                        {
                            buttonReady2 = true;
                        }

                        if (Keyboard.GetState().IsKeyDown(Keys.Up) || GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed)
                            soundIndex = 1;

                        if (Keyboard.GetState().IsKeyDown(Keys.Down) || GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed)
                            soundIndex = 2;

                        if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed)
                            CurrentGameState = GameState.MainMenu;
                        if (Keyboard.GetState().IsKeyDown(Keys.Back))
                            CurrentGameState = GameState.MainMenu;
                        break;
                }

                #endregion
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    pause = false;
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    this.Exit();
                if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed)
                    this.Exit();
                if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
                {
                    pause = false;
                    if (gameOver == true)
                    {
                        gameOver = false;
                        LoadContent();
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    spriteBatch.Begin();
                        spriteBatch.Draw(MenuBackground, new Vector2(0, 0), Color.White);
                        spriteBatch.Draw(MenuPlayButton, new Rectangle(405, 250, 225, 80), Color.White);
                        spriteBatch.Draw(MenuOptionsButton, new Rectangle(405, 425, 225, 80), Color.White);
                        HandleMenu();
                    spriteBatch.End();
                    break;

                case GameState.Help:

                    break;

                case GameState.Playing:
                        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
                            null, null, null, null, viewMatrix);
                            background.Draw(spriteBatch);
                            spriteBatch.Draw(sphereTexture, spherePosition, null, Color.White,
                                0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
#region                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           #region
                        foreach (Sphere s in SphereList.RedSphereList)
                        {
                            if (s.Visible == true)
                                s.Draw(spriteBatch);
                        }
                        foreach (Sphere s in SphereList.BlueSphereList)
                        {
                            if (s.Visible == true)
                                s.Draw(spriteBatch);
                        }
                        foreach (Sphere s in SphereList.GreenSphereList)
                        {
                            if (s.Visible == true)
                                s.Draw(spriteBatch);
                        }
                        foreach (Sphere s in SphereList.YellowSphereList)
                        {
                            if (s.Visible == true)
                                s.Draw(spriteBatch);
                        }

                        foreach (Sphere s in SphereList.EnemySphereList)
                        {
                            if (s.Visible == true)
                                s.Draw(spriteBatch);
                        }
                        foreach (ScoreSphere s in SphereList.ScoreBoardRedList)
                        {
                            for (int i = 0; i < redScore; i++)
                            {
                                s.Draw(spriteBatch);
                            }
                        }
                        foreach (ScoreSphere s in SphereList.ScoreBoardBlueList)
                        {
                            for (int i = 0; i < blueScore; i++)
                            {
                                s.Draw(spriteBatch);
                            }
                        }
                        foreach (ScoreSphere s in SphereList.ScoreBoardGreenList)
                        {
                            for (int i = 0; i < greenScore; i++)
                            {
                                s.Draw(spriteBatch);
                            }
                        }
                        foreach (ScoreSphere s in SphereList.ScoreBoardYellowList)
                        {
                            for (int i = 0; i < yellowScore; i++)
                            {
                                s.Draw(spriteBatch);
                            }
                        }
                #endregion
                            spriteBatch.Draw(kinectRGBVideo, new Rectangle((int)cameraPosition.X + (ScreenWidth - 160), (int)cameraPosition.Y + 60, 160, 120), Color.White);
                
                            if (CollectionMode == true)
                                MainCollecter.Draw(spriteBatch);
                            else
                            {
                                rightHandCollector.Draw(spriteBatch);
                                leftHandCollector.Draw(spriteBatch);
                            }
                            if (RightBlockerActive == true && blockerDepleted == false)
                                rightElbowBlocker.Draw(spriteBatch);
                            if (LeftBlockerActive == true && blockerDepleted == false)
                                leftElbowBlocker.Draw(spriteBatch);
                            spriteBatch.Draw(HUDBar, new Vector2((int)cameraPosition.X, (int)cameraPosition.Y), Color.White);
                            spriteBatch.Draw(BlockersBar, BlockersBarRectangle, Color.White);
                            spriteBatch.Draw(EnemyHealthBar, EnemyHealthBarRectangle, Color.White);
                            spriteBatch.Draw(PlayerHealthBar, PlayerHealthBarRectangle, Color.White);
                            if (EnemyScore == 300)
                            {
                                spriteBatch.DrawString(MenuPlayText, "Game Over", new Vector2(425 + cameraPosition.X, 300 + cameraPosition.Y), Color.Red);
                                pause = true;
                                gameOver = true;
                            }
                            if (SphereList.ScoreBoardRedList.Count == 15 || SphereList.ScoreBoardBlueList.Count == 15 || SphereList.ScoreBoardGreenList.Count == 15 || SphereList.ScoreBoardYellowList.Count == 15)
                            {
                                spriteBatch.DrawString(MenuPlayText, "You Win!", new Vector2(500 + cameraPosition.X, 300 + cameraPosition.Y), Color.Green);
                            }
                        spriteBatch.End();
                    break;

                case GameState.Options:
                    spriteBatch.Begin();
                        spriteBatch.Draw(OptionsBackground, new Vector2(0, -100), Color.White);
                        if (soundIndex == 1)
                        {
                            spriteBatch.DrawString(MenuPlayText, "Music volume: " + (int)(Settings.musicVolume * 100), new Vector2(320, 250), Color.Blue);
                            spriteBatch.DrawString(MenuPlayText, "Effects volume: " + (int)(Settings.soundEffectsVolume * 100), new Vector2(320, 350), Color.White);
                        }
                        else
                        {
                            spriteBatch.DrawString(MenuPlayText, "Music volume: " + (int)(Settings.musicVolume * 100), new Vector2(320, 250), Color.White);
                            spriteBatch.DrawString(MenuPlayText, "Effects volume: " + (int)(Settings.soundEffectsVolume * 100), new Vector2(320, 350), Color.Blue);
                        }
                        spriteBatch.End();
                    break;
                }

            

            base.Draw(gameTime);
        }

        public void HandleMenu()
        {
            DrawText();
            MenuInput();

        }

        public void MenuInput()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Down) || GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed)
            {
                if (DownButtonUp == true)
                    if (MenuControllerIndex < 2)
                        MenuControllerIndex++;
                DownButtonUp = false;
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Down))
            {
                DownButtonUp = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up) || GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed)
            {
                if (UpButtonUp == true)
                    if (MenuControllerIndex > 1)
                        MenuControllerIndex--;
                UpButtonUp = false;
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Up))
            {
                UpButtonUp = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) || GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
                switch (MenuControllerIndex)
                {
                    case 1:
                        CurrentGameState = GameState.Playing;
                        break;

                    case 2:
                        CurrentGameState = GameState.Options;
                        break;

                }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
        }

        private void DrawText()
        {
            if (MenuControllerIndex == 1)
                spriteBatch.DrawString(MenuPlayText, "PLAY", new Vector2(455, 265), Color.Red);
            else
                spriteBatch.DrawString(MenuPlayText, "PLAY", new Vector2(455, 265), Color.White);

            if (MenuControllerIndex == 2)
                spriteBatch.DrawString(MenuPlayText, "Options", new Vector2(435, 435), Color.Red);
            else
                spriteBatch.DrawString(MenuPlayText, "Options", new Vector2(435, 435), Color.White);
        }

        public void Playing(GameTime gameTime) 
        {
            ////////////-----Checks For Xbox Controller Input----////////////

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                pause = true;

            //Checks For Right Trigger Pull (Activates Right Blocker)
            if (GamePad.GetState(PlayerIndex.One).Triggers.Right == 1 && BlockersBarRectangle.Width > 0)
                RightBlockerActive = true;
            else
                RightBlockerActive = false;

            //Checks For Left Trigger Pull (Activates Left Blocker)
            if (GamePad.GetState(PlayerIndex.One).Triggers.Left == 1 && BlockersBarRectangle.Width > 0)
                LeftBlockerActive = true;
            else
                LeftBlockerActive = false;

            ////////////-----Controller Input End----////////////

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                pause = true;
                escapeUp = false;
            }

            TS = gameTime;

            ///////////////////----ScrollingScreen Start----///////////////////
            ScrollingScreen();
            ///////////////////----ScrollingScreen End----///////////////////

            ///////////////////----HUD Start----///////////////////

            PlayerHealthBarRectangle = new Rectangle(425 + (int)cameraPosition.X, 20 + (int)cameraPosition.Y, 300 - EnemyScore, 20);

            //EnemyHealthBarRectangle.X += (int)cameraPosition.X;
            //EnemyHealthBarRectangle.Y += (int)cameraPosition.Y;

            HUDBarRectangle.X += (int)cameraPosition.X;
            HUDBarRectangle.Y += (int)cameraPosition.Y;

            //BlockersBarRectangle.X = (int)cameraPosition.X;
            //BlockersBarRectangle.Y = (int)cameraPosition.Y;

            EnemyHealthBarRectangle = new Rectangle(425 + (int)cameraPosition.X, 20 + (int)cameraPosition.Y, 600, 20);
            BlockersBarRectangle = new Rectangle(75 + (int)cameraPosition.X, 20 + (int)cameraPosition.Y, 300 - blockerDepletion, 20);
            ///////////////////----HUD End----///////////////////

            ///////////////////----Spheres Start----///////////////////
            //Collision
            CollisionEfficient(gameTime, SphereList.RedSphereList);
            CollisionEfficient(gameTime, SphereList.BlueSphereList);  
            CollisionEfficient(gameTime, SphereList.GreenSphereList);  
            CollisionEfficient(gameTime, SphereList.YellowSphereList);
            CollisionEfficient(gameTime, SphereList.EnemySphereList);

            ////Animation
            //CheckRedCollision = true;
            //AnimationLogic(gameTime, SphereList.RedSphereList);
            //CheckRedCollision = false;

            //CheckBlueCollision = true;
            //AnimationLogic(gameTime, SphereList.BlueSphereList);
            //CheckBlueCollision = false;

            //CheckGreenCollision = true;
            //AnimationLogic(gameTime, SphereList.GreenSphereList);
            //CheckGreenCollision = false;

            //CheckYellowCollision = true;
            //AnimationLogic(gameTime, SphereList.YellowSphereList);
            //CheckYellowCollision = false;

            //AnimationLogic(gameTime, SphereList.EnemySphereList);
            ///////////////////----Spheres End----///////////////////

            ///////////////////----Collectors Start----///////////////////
            CollectorsEfficient(gameTime, SphereList.RedSphereList);
            CollectorsEfficient(gameTime, SphereList.BlueSphereList);
            CollectorsEfficient(gameTime, SphereList.GreenSphereList);
            CollectorsEfficient(gameTime, SphereList.YellowSphereList);
            ///////////////////----Collectors End----///////////////////

            ///////////////////----Blockers Start----///////////////////
            if (RightBlockerActive == true && blockerDepleted == false)
            {
                if (blockerDepletion < 300)
                    blockerDepletion++;
                Blockers(gameTime);
            }

            if (LeftBlockerActive == true && blockerDepleted == false)
            {
                if (blockerDepletion < 300)
                    blockerDepletion++;
                Blockers(gameTime);
            }

            if ((RightBlockerActive == false && LeftBlockerActive == false) || blockerDepleted == true)
                if (blockerDepletion > 0)
                    blockerDepletion--;

            if (blockerDepletion == 299)
                blockerDepleted = true;

            if (blockerDepleted == true && blockerDepletion == 1)
                blockerDepleted = false;

            Blocker.position.X = leftHandPosition.X + cameraPosition.X;
            Blocker.position.Y = (leftHandPosition.Y + cameraPosition.Y) - 150;
            //Blocker.position = rightHandPosition + cameraPosition;
            ///////////////////----Blockers End----///////////////////

            ///////////////////----Score Board Start----///////////////////
            #region
            for (int i = 0; i < SphereList.ScoreBoardRedList.Count; i++)
            {
                SphereList.ScoreBoardRedList[i].position.X = Game1.cameraPosition.X + 10;
                SphereList.ScoreBoardRedList[i].position.Y = (Game1.cameraPosition.Y + (Game1.ScreenHeight - 50)) - (i * 30) + 15;
            }
            for (int i = 0; i < SphereList.ScoreBoardBlueList.Count; i++)
            {
                SphereList.ScoreBoardBlueList[i].position.X = Game1.cameraPosition.X + 40;
                SphereList.ScoreBoardBlueList[i].position.Y = (Game1.cameraPosition.Y + (Game1.ScreenHeight - 50)) - (i * 30) + 15;
            }
            for (int i = 0; i < SphereList.ScoreBoardGreenList.Count; i++)
            {
                SphereList.ScoreBoardGreenList[i].position.X = Game1.cameraPosition.X + (Game1.ScreenWidth - 65);
                SphereList.ScoreBoardGreenList[i].position.Y = (Game1.cameraPosition.Y + (Game1.ScreenHeight - 50)) - (i * 30) + 15;
            }
            for (int i = 0; i < SphereList.ScoreBoardYellowList.Count; i++)
            {
                SphereList.ScoreBoardYellowList[i].position.X = Game1.cameraPosition.X + (Game1.ScreenWidth - 30);
                SphereList.ScoreBoardYellowList[i].position.Y = (Game1.cameraPosition.Y + (Game1.ScreenHeight - 50)) - (i * 30) + 15;
            }

            if (SphereList.ScoreBoardRedList.Count == 15 || SphereList.ScoreBoardBlueList.Count == 15 || SphereList.ScoreBoardGreenList.Count == 15 || SphereList.ScoreBoardYellowList.Count == 15)
            {
                gameWin = true;
                pause = true;
            }

            #endregion
            ///////////////////----Score Board End----///////////////////
        }

        //////////////////////////----------Physics----------//////////////////////////
        #region
        bool CollectorCollision(Vector2 rightPosition, Vector2 leftPosition)
        {
            if (rightPosition.X + 150 > leftPosition.X &&
                rightPosition.Y + 150 > leftPosition.Y &&
                rightPosition.X < leftPosition.X + 150 &&
                rightPosition.Y < leftPosition.Y + 150)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        void ScrollingScreen()
        {
            int ScreenWidth = GraphicsDevice.Viewport.Width;
            int ScreenHeight = GraphicsDevice.Viewport.Height;

            spherePosition += sphereVelocity;

            sphereVelocity *= 0.95f;

            if (hipPosition.X < 0)
                hipPosition.X = 0;
            if (hipPosition.Y < 0)
                hipPosition.Y = 0;
            if (hipPosition.X < backgroundTexture.Width - (ScreenWidth / 2))
                cameraPosition.X = hipPosition.X - (ScreenWidth / 2);
            if (-hipPosition.Z < backgroundTexture.Height - (ScreenHeight / 2))
                cameraPosition.Y = -hipPosition.Z - (ScreenHeight / 2);

            if (cameraPosition.X < 0)
                cameraPosition.X = 0;
            if (cameraPosition.Y < 0)
                cameraPosition.Y = 0;
            if (spherePosition.X + (ScreenWidth / 2) > backgroundTexture.Width)
                cameraPosition.X = backgroundTexture.Width - (ScreenWidth);
            if (spherePosition.Y + (ScreenHeight / 2) > backgroundTexture.Height)
                cameraPosition.Y = backgroundTexture.Height - (ScreenHeight);

            viewMatrix = Matrix.CreateTranslation(new Vector3(-cameraPosition, 0));
        }
        void SpherePlacement(List<Sphere> ListOfSpheres)
        {
            int newX, newY;
            for (int i = 0; i < ListOfSpheres.Count; i++)
            {
                ListOfSpheres[i].LoadContent();
                while (ListOfSpheres[i].placed == false)
                {
                    newX = (Rnd.Next(0, 21));
                    newY = (Rnd.Next(0, 14));
                    if (Grid.grid[newY * 21 + newX].used == false)
                    {
                        ListOfSpheres[i].position = new Vector2((newX * 100), (newY * 100));
                        ListOfSpheres[i].placed = true;
                        Grid.grid[newY * 21 + newX].used = true;
                    }
                }

                ListOfSpheres[i].velocity.X = (float)Rnd.NextDouble() * randX.Next(-4, 4);
                ListOfSpheres[i].velocity.Y = (float)Rnd.NextDouble() * randX.Next(-4, 4);
            }
        }
        void CollisionEfficient(GameTime gameTime, List<Sphere> ListOfSpheres)
        {
            foreach (Sphere s in ListOfSpheres)
            //for (int i = 0; i < ListOfSpheres.Count; i++)
            {
                s.update();
                s.Boundaries(s, gameTime);
                s.OffScreenCheck();
                //s.Stuck();
                if (s.velocity.X == 0 && s.velocity.Y == 0)
                {
                    s.velocity.X = randX.Next(-4, 4);
                    s.velocity.Y = randY.Next(-4, 4);
                }

                for (int i = 0; i < redSphereCount; i++)
                {

                    if (s.CircleCollides(SphereList.RedSphereList[i]) == true)
                    {
                        Vector2 tempVelocity = s.velocity;
                        s.velocity = SphereList.RedSphereList[i].velocity;
                        SphereList.RedSphereList[i].velocity = tempVelocity;
                        s.stuckTime++;
                        //if (s.squishedX == false)
                        //    s.savedVelocity = s.velocity;
                        //s.velocity.X = 0;
                        //s.squishedX = true;
                        //s.currentFrameY = 2;
                        //Game1.Bounce.Play();
                    }

                    //if (s.collide == true)
                    //{
                    //    if (s.position.X < SphereList.RedSphereList[i].position.X && s.position.Y > SphereList.RedSphereList[i].position.Y && s.position.Y < SphereList.RedSphereList[i].position.Y + SphereList.RedSphereList[i].size.Y)
                    //    {
                    //        if (s.squishedX == false)
                    //        {
                    //            s.savedVelocity = s.velocity;
                    //            SphereList.RedSphereList[i].savedVelocity = SphereList.RedSphereList[i].velocity;
                    //        }
                    //        s.velocity.X = 0;
                    //        SphereList.RedSphereList[i].velocity.X = 0;
                    //        s.squishedX = true;
                    //        SphereList.RedSphereList[i].squishedX = true;
                    //        s.currentFrameY = 1;
                    //        SphereList.RedSphereList[i].currentFrameY = 2;
                    //    }
                    //    if (s.position.X > SphereList.RedSphereList[i].position.X && s.position.Y > SphereList.RedSphereList[i].position.Y && s.position.Y < SphereList.RedSphereList[i].position.Y + SphereList.RedSphereList[i].size.Y)
                    //    {
                    //        if (s.squishedX == false)
                    //        {
                    //            s.savedVelocity = s.velocity;
                    //            SphereList.RedSphereList[i].savedVelocity = SphereList.RedSphereList[i].velocity;
                    //        }
                    //        s.velocity.X = 0;
                    //        SphereList.RedSphereList[i].velocity.X = 0;
                    //        s.squishedX = true;
                    //        SphereList.RedSphereList[i].squishedX = true;
                    //        s.currentFrameY = 2;
                    //        SphereList.RedSphereList[i].currentFrameY = 1;
                    //    }
                    //}
                    //if (s.CollidesRight(SphereList.RedSphereList[i]) == true)
                    //{
                    //    if (s.squishedX == false)
                    //    {
                    //        s.savedVelocity = s.velocity;
                    //        SphereList.RedSphereList[i].savedVelocity = SphereList.RedSphereList[i].velocity;
                    //    }
                    //    s.velocity.X = 0;
                    //    SphereList.RedSphereList[i].velocity.X = 0;
                    //    s.squishedX = true;
                    //    SphereList.RedSphereList[i].squishedX = true;
                    //    s.currentFrameY = 1;
                    //    SphereList.RedSphereList[i].currentFrameY = 2;
                    //    //if (s.squishedX == false)
                    //    //    s.savedVelocity = s.velocity;
                    //    //s.velocity.X = 0;
                    //    //s.squishedX = true;
                    //    //s.currentFrameY = 2;
                    //    //s.AnimateX(gameTime);
                    //    //if (SphereList.RedSphereList[i].squishedX == false)
                    //    //    SphereList.RedSphereList[i].savedVelocity = SphereList.RedSphereList[i].velocity;
                    //    //SphereList.RedSphereList[i].velocity.X = 0;
                    //    //SphereList.RedSphereList[i].squishedX = true;
                    //    //SphereList.RedSphereList[i].currentFrameY = 1;
                    //    //SphereList.RedSphereList[i].AnimateX(gameTime);
                    //}


                    //if (s.CollidesLeft(SphereList.RedSphereList[i]) == true)
                    //{
                    //    if (s.squishedX == false)
                    //    {
                    //        s.savedVelocity = s.velocity;
                    //        SphereList.RedSphereList[i].savedVelocity = SphereList.RedSphereList[i].velocity;
                    //    }
                    //    s.velocity.X = 0;
                    //    SphereList.RedSphereList[i].velocity.X = 0;
                    //    s.squishedX = true;
                    //    SphereList.RedSphereList[i].squishedX = true;
                    //    s.currentFrameY = 2;
                    //    SphereList.RedSphereList[i].currentFrameY = 1;
                    //    //s.currentFrameY = 1;
                    //    //s.AnimateX(gameTime);
                    //    //SphereList.RedSphereList[i].currentFrameY = 2;
                    //    //SphereList.RedSphereList[i].AnimateX(gameTime);
                    //}

                    //if (s.CollidesTop(SphereList.RedSphereList[i]) == true)
                    //{
                    //    s.currentFrameY = 4;
                    //    //s.AnimateY(gameTime);
                    //    SphereList.RedSphereList[i].currentFrameY = 3;
                    //    //SphereList.RedSphereList[i].AnimateX(gameTime);
                    //}
                    //if (s.CollidesBottom(SphereList.RedSphereList[i]) == true)
                    //{
                    //    s.currentFrameY = 3;
                    //    s.AnimateY(gameTime);
                    //    SphereList.RedSphereList[i].currentFrameY = 4;
                    //    SphereList.RedSphereList[i].AnimateX(gameTime);
                    //}
                    //if (s.Collides(SphereList.RedSphereList[i]) == true)
                    //{
                    //    SphereList.RedSphereList[i].stuckTime++;
                    //    if (s.stuckTime == 100)
                    //    {
                    //        s.stuckTime = 0;
                    //        if (s.velocity.X < 0)
                    //            s.position.X += 100;
                    //        else
                    //            s.position.X -= 100;
                    //    }
                    //}
                }
                for (int i = 0; i < blueSphereCount; i++)
                {
                    if (s.CircleCollides(SphereList.BlueSphereList[i]) == true)
                    {
                        Vector2 tempVelocity = s.velocity;
                        s.velocity = SphereList.BlueSphereList[i].velocity;
                        SphereList.BlueSphereList[i].velocity = tempVelocity;
                        s.stuckTime++;
                        //Game1.Bounce.Play();
                    }
                    //if (s.Collides(SphereList.BlueSphereList[i]) == true)
                    //{
                    //    s.stuckTime++;
                    //    if (s.stuckTime == 100)
                    //    {
                    //        s.stuckTime = 0;
                    //        if (s.velocity.X < 0)
                    //            s.position.X += 100;
                    //        else
                    //            s.position.X -= 100;
                    //    }
                    //}
                }
                for (int i = 0; i < greenSphereCount; i++)
                {
                    if (s.CircleCollides(SphereList.GreenSphereList[i]) == true)
                    {
                        Vector2 tempVelocity = s.velocity;
                        s.velocity = SphereList.GreenSphereList[i].velocity;
                        SphereList.GreenSphereList[i].velocity = tempVelocity;
                        s.stuckTime++;
                        //Game1.Bounce.Play();
                    }
                    //if (s.Collides(SphereList.GreenSphereList[i]) == true)
                    //{
                    //    s.stuckTime++;
                    //    if (s.stuckTime == 100)
                    //    {
                    //        s.stuckTime = 0;
                    //        if (s.velocity.X < 0)
                    //            s.position.X += 100;
                    //        else
                    //            s.position.X -= 100;
                    //    }
                    //}
                }
                for (int i = 0; i < yellowSphereCount; i++)
                {
                    if (s.CircleCollides(SphereList.YellowSphereList[i]) == true)
                    {
                        Vector2 tempVelocity = s.velocity;
                        s.velocity = SphereList.YellowSphereList[i].velocity;
                        SphereList.YellowSphereList[i].velocity = tempVelocity;
                        s.stuckTime++;
                        //Game1.Bounce.Play();
                    }
                    //if (s.Collides(SphereList.YellowSphereList[i]) == true)
                    //{
                    //    s.stuckTime++;
                    //    if (s.stuckTime == 100)
                    //    {
                    //        s.stuckTime = 0;
                    //        if (s.velocity.X < 0)
                    //            s.position.X += 100;
                    //        else
                    //            s.position.X -= 100;
                    //    }
                    //}
                }
                for (int i = 0; i < enemySphereCount; i++)
                {
                    if (s.CircleCollides(SphereList.EnemySphereList[i]) == true)
                    {
                        Vector2 tempVelocity = s.velocity;
                        s.velocity = SphereList.EnemySphereList[i].velocity;
                        SphereList.EnemySphereList[i].velocity = tempVelocity;
                        s.stuckTime++;
                        //Game1.Bounce.Play();
                        //s.velocity = -s.velocity;
                    }
                    //if (s.Collides(SphereList.EnemySphereList[i]) == true)
                    //{
                    //    s.stuckTime++;
                    //    if (s.stuckTime == 100)
                    //    {
                    //        s.stuckTime = 0;
                    //        if (s.velocity.X < 0)
                    //            s.position.X += 100;
                    //        else
                    //            s.position.X -= 100;
                    //    }
                    //}
                }
            }
        }
        void AnimationLogic(GameTime gameTime, List<Sphere> ListOfSpheres)
        {
            foreach (Sphere s in ListOfSpheres)
            {
                if (CheckRedCollision == false)
                {
                    for (int i = 0; i < redSphereCount; i++)
                    {
                        if (s.CollidesRight(SphereList.RedSphereList[i]) == true)
                        {
                            if (s.squishedX == false)
                            {
                                s.savedVelocity = s.velocity;
                                SphereList.RedSphereList[i].savedVelocity = SphereList.RedSphereList[i].velocity;
                            }
                            s.velocity.X = 0;
                            SphereList.RedSphereList[i].velocity.X = 0;
                            s.squishedX = true;
                            SphereList.RedSphereList[i].squishedX = true;
                            s.currentFrameY = 1;
                            SphereList.RedSphereList[i].currentFrameY = 2;
                        }
                        if (s.CollidesLeft(SphereList.RedSphereList[i]) == true)
                        {
                            if (s.squishedX == false)
                            {
                                s.savedVelocity = s.velocity;
                                SphereList.RedSphereList[i].savedVelocity = SphereList.RedSphereList[i].velocity;
                            }
                            s.velocity.X = 0;
                            SphereList.RedSphereList[i].velocity.X = 0;
                            s.squishedX = true;
                            SphereList.RedSphereList[i].squishedX = true;
                            s.currentFrameY = 2;
                            SphereList.RedSphereList[i].currentFrameY = 1;
                        }
                        if (s.CollidesTop(SphereList.RedSphereList[i]) == true)
                        {
                            if (s.squishedX == false)
                            {
                                s.savedVelocity = s.velocity;
                                SphereList.RedSphereList[i].savedVelocity = SphereList.RedSphereList[i].velocity;
                            }
                            s.velocity.Y = 0;
                            SphereList.RedSphereList[i].velocity.Y = 0;
                            s.squishedY = true;
                            SphereList.RedSphereList[i].squishedY = true;
                            s.currentFrameY = 3;
                            SphereList.RedSphereList[i].currentFrameY = 4;
                        }
                        if (s.CollidesBottom(SphereList.RedSphereList[i]) == true)
                        {
                            if (s.squishedX == false)
                            {
                                s.savedVelocity = s.velocity;
                                SphereList.RedSphereList[i].savedVelocity = SphereList.RedSphereList[i].velocity;
                            }
                            s.velocity.Y = 0;
                            SphereList.RedSphereList[i].velocity.Y = 0;
                            s.squishedY = true;
                            SphereList.RedSphereList[i].squishedY = true;
                            s.currentFrameY = 4;
                            SphereList.RedSphereList[i].currentFrameY = 3;
                        }
                    }
                }
                if (CheckBlueCollision == false)
                {
                    for (int i = 0; i < blueSphereCount; i++)
                    {
                        if (s.CollidesRight(SphereList.BlueSphereList[i]) == true)
                        {
                            if (s.squishedX == false)
                            {
                                s.savedVelocity = s.velocity;
                                SphereList.BlueSphereList[i].savedVelocity = SphereList.BlueSphereList[i].velocity;
                            }
                            s.velocity.X = 0;
                            SphereList.BlueSphereList[i].velocity.X = 0;
                            s.squishedX = true;
                            SphereList.BlueSphereList[i].squishedX = true;
                            s.currentFrameY = 3;
                            SphereList.BlueSphereList[i].currentFrameY = 4;
                        }
                        if (s.CollidesLeft(SphereList.BlueSphereList[i]) == true)
                        {
                            if (s.squishedX == false)
                            {
                                s.savedVelocity = s.velocity;
                                SphereList.BlueSphereList[i].savedVelocity = SphereList.BlueSphereList[i].velocity;
                            }
                            s.velocity.X = 0;
                            SphereList.BlueSphereList[i].velocity.X = 0;
                            s.squishedX = true;
                            SphereList.BlueSphereList[i].squishedX = true;
                            s.currentFrameY = 2;
                            SphereList.BlueSphereList[i].currentFrameY = 1;
                        }
                        if (s.CollidesTop(SphereList.BlueSphereList[i]) == true)
                        {
                            if (s.squishedX == false)
                            {
                                s.savedVelocity = s.velocity;
                                SphereList.BlueSphereList[i].savedVelocity = SphereList.BlueSphereList[i].velocity;
                            }
                            s.velocity.Y = 0;
                            SphereList.BlueSphereList[i].velocity.Y = 0;
                            s.squishedY = true;
                            SphereList.BlueSphereList[i].squishedY = true;
                            s.currentFrameY = 3;
                            SphereList.BlueSphereList[i].currentFrameY = 4;
                        }
                        if (s.CollidesBottom(SphereList.BlueSphereList[i]) == true)
                        {
                            if (s.squishedX == false)
                            {
                                s.savedVelocity = s.velocity;
                                SphereList.BlueSphereList[i].savedVelocity = SphereList.BlueSphereList[i].velocity;
                            }
                            s.velocity.Y = 0;
                            SphereList.BlueSphereList[i].velocity.Y = 0;
                            s.squishedY = true;
                            SphereList.BlueSphereList[i].squishedY = true;
                            s.currentFrameY = 4;
                            SphereList.BlueSphereList[i].currentFrameY = 3;
                        }
                    }
                }
                if (CheckGreenCollision == false)
                {
                    for (int i = 0; i < greenSphereCount; i++)
                    {
                        if (s.CollidesRight(SphereList.GreenSphereList[i]) == true)
                        {
                            if (s.squishedX == false)
                            {
                                s.savedVelocity = s.velocity;
                                SphereList.GreenSphereList[i].savedVelocity = SphereList.GreenSphereList[i].velocity;
                            }
                            s.velocity.X = 0;
                            SphereList.GreenSphereList[i].velocity.X = 0;
                            s.squishedX = true;
                            SphereList.GreenSphereList[i].squishedX = true;
                            s.currentFrameY = 3;
                            SphereList.GreenSphereList[i].currentFrameY = 4;
                        }
                        if (s.CollidesLeft(SphereList.GreenSphereList[i]) == true)
                        {
                            if (s.squishedX == false)
                            {
                                s.savedVelocity = s.velocity;
                                SphereList.GreenSphereList[i].savedVelocity = SphereList.GreenSphereList[i].velocity;
                            }
                            s.velocity.X = 0;
                            SphereList.GreenSphereList[i].velocity.X = 0;
                            s.squishedX = true;
                            SphereList.GreenSphereList[i].squishedX = true;
                            s.currentFrameY = 2;
                            SphereList.GreenSphereList[i].currentFrameY = 1;
                        }
                        if (s.CollidesTop(SphereList.GreenSphereList[i]) == true)
                        {
                            if (s.squishedX == false)
                            {
                                s.savedVelocity = s.velocity;
                                SphereList.GreenSphereList[i].savedVelocity = SphereList.GreenSphereList[i].velocity;
                            }
                            s.velocity.Y = 0;
                            SphereList.GreenSphereList[i].velocity.Y = 0;
                            s.squishedY = true;
                            SphereList.GreenSphereList[i].squishedY = true;
                            s.currentFrameY = 3;
                            SphereList.GreenSphereList[i].currentFrameY = 4;
                        }
                        if (s.CollidesBottom(SphereList.GreenSphereList[i]) == true)
                        {
                            if (s.squishedX == false)
                            {
                                s.savedVelocity = s.velocity;
                                SphereList.GreenSphereList[i].savedVelocity = SphereList.GreenSphereList[i].velocity;
                            }
                            s.velocity.Y = 0;
                            SphereList.GreenSphereList[i].velocity.Y = 0;
                            s.squishedY = true;
                            SphereList.GreenSphereList[i].squishedY = true;
                            s.currentFrameY = 4;
                            SphereList.GreenSphereList[i].currentFrameY = 3;
                        }
                    }
                }
                if (CheckYellowCollision == false)
                {
                    for (int i = 0; i < yellowSphereCount; i++)
                    {
                        if (s.CollidesRight(SphereList.YellowSphereList[i]) == true)
                        {
                            if (s.squishedX == false)
                            {
                                s.savedVelocity = s.velocity;
                                SphereList.YellowSphereList[i].savedVelocity = SphereList.YellowSphereList[i].velocity;
                            }
                            s.velocity.X = 0;
                            SphereList.YellowSphereList[i].velocity.X = 0;
                            s.squishedX = true;
                            SphereList.YellowSphereList[i].squishedX = true;
                            s.currentFrameY = 3;
                            SphereList.YellowSphereList[i].currentFrameY = 4;
                        }
                        if (s.CollidesLeft(SphereList.YellowSphereList[i]) == true)
                        {
                            if (s.squishedX == false)
                            {
                                s.savedVelocity = s.velocity;
                                SphereList.YellowSphereList[i].savedVelocity = SphereList.YellowSphereList[i].velocity;
                            }
                            s.velocity.X = 0;
                            SphereList.YellowSphereList[i].velocity.X = 0;
                            s.squishedX = true;
                            SphereList.YellowSphereList[i].squishedX = true;
                            s.currentFrameY = 2;
                            SphereList.YellowSphereList[i].currentFrameY = 1;
                        }
                        if (s.CollidesTop(SphereList.YellowSphereList[i]) == true)
                        {
                            if (s.squishedX == false)
                            {
                                s.savedVelocity = s.velocity;
                                SphereList.YellowSphereList[i].savedVelocity = SphereList.YellowSphereList[i].velocity;
                            }
                            s.velocity.Y = 0;
                            SphereList.YellowSphereList[i].velocity.Y = 0;
                            s.squishedY = true;
                            SphereList.YellowSphereList[i].squishedY = true;
                            s.currentFrameY = 3;
                            SphereList.YellowSphereList[i].currentFrameY = 4;
                        }
                        if (s.CollidesBottom(SphereList.YellowSphereList[i]) == true)
                        {
                            if (s.squishedX == false)
                            {
                                s.savedVelocity = s.velocity;
                                SphereList.YellowSphereList[i].savedVelocity = SphereList.YellowSphereList[i].velocity;
                            }
                            s.velocity.Y = 0;
                            SphereList.YellowSphereList[i].velocity.Y = 0;
                            s.squishedY = true;
                            SphereList.YellowSphereList[i].squishedY = true;
                            s.currentFrameY = 4;
                            SphereList.YellowSphereList[i].currentFrameY = 3;
                        }
                    }
                }
                for (int i = 0; i < enemySphereCount; i++)
                {
                    if (s.CollidesRight(SphereList.EnemySphereList[i]) == true)
                    {
                        if (s.squishedX == false)
                        {
                            s.savedVelocity = s.velocity;
                            SphereList.EnemySphereList[i].savedVelocity = SphereList.EnemySphereList[i].velocity;
                        }
                        s.velocity.X = 0;
                        SphereList.EnemySphereList[i].velocity.X = 0;
                        s.squishedX = true;
                        SphereList.EnemySphereList[i].squishedX = true;
                        s.currentFrameY = 3;
                        SphereList.EnemySphereList[i].currentFrameY = 4;
                    }
                    if (s.CollidesLeft(SphereList.EnemySphereList[i]) == true)
                    {
                        if (s.squishedX == false)
                        {
                            s.savedVelocity = s.velocity;
                            SphereList.EnemySphereList[i].savedVelocity = SphereList.EnemySphereList[i].velocity;
                        }
                        s.velocity.X = 0;
                        SphereList.EnemySphereList[i].velocity.X = 0;
                        s.squishedX = true;
                        SphereList.EnemySphereList[i].squishedX = true;
                        s.currentFrameY = 2;
                        SphereList.EnemySphereList[i].currentFrameY = 1;
                    }
                    if (s.CollidesTop(SphereList.EnemySphereList[i]) == true)
                    {
                        if (s.squishedX == false)
                        {
                            s.savedVelocity = s.velocity;
                            SphereList.EnemySphereList[i].savedVelocity = SphereList.EnemySphereList[i].velocity;
                        }
                        s.velocity.Y = 0;
                        SphereList.EnemySphereList[i].velocity.Y = 0;
                        s.squishedY = true;
                        SphereList.EnemySphereList[i].squishedY = true;
                        s.currentFrameY = 3;
                        SphereList.EnemySphereList[i].currentFrameY = 4;
                    }
                    if (s.CollidesBottom(SphereList.EnemySphereList[i]) == true)
                    {
                        if (s.squishedX == false)
                        {
                            s.savedVelocity = s.velocity;
                            SphereList.EnemySphereList[i].savedVelocity = SphereList.EnemySphereList[i].velocity;
                        }
                        s.velocity.Y = 0;
                        SphereList.EnemySphereList[i].velocity.Y = 0;
                        s.squishedY = true;
                        SphereList.EnemySphereList[i].squishedY = true;
                        s.currentFrameY = 4;
                        SphereList.EnemySphereList[i].currentFrameY = 3;
                    }
                }
            }
        }
        void CollectorsEfficient(GameTime gameTime, List<Sphere> ListOfSpheres)
        {
            MainCollecter.position = rightHandPosition + cameraPosition;
            rightHandCollector.position = rightHandPosition + cameraPosition;
            leftHandCollector.position = leftHandPosition + cameraPosition;
            rightElbowBlocker.position = rightElbowPosition + cameraPosition;
            leftElbowBlocker.position = leftElbowPosition + cameraPosition;

            if (CollectorCollision(rightHandPosition, leftHandPosition) == true)
                CollectionMode = true;
            else
            {
                CollectionMode = false;
                MainCollecter.texture = Content.Load<Texture2D>("Collectors/Sphere");
                collectorSwitch = 0;
                collectorIndex = 0;
                addIndex = 0;
                mergeComplete = false;
                mergeRed = false;
                mergeBlue = false;
                mergeGreen = false;
                mergeYellow = false;
                foreach (Sphere e in SphereList.EnemySphereList)
                    e.enemyRebounded = false;
            }

            //Check for main collector/coloured sphere collision.
            #region
            if (CollectionMode == true)
            {
                //Collecting Red Spheres
                if (collectorSwitch == 0)
                {
                    for (int i = 0; i < redSphereCount; i++)
                    {
                        if (MainCollecter.CircleCollides(SphereList.RedSphereList[i]) == true)
                        {
                            SphereList.RedSphereList[i].position = MainCollecter.position;
                            MainCollecter.texture = Content.Load<Texture2D>("Red Spheres/Red_Sphere");
                            collectorSwitch = 1;
                            collectorIndex = i;
                            temp = i;
                        }
                    }
                }
                if (collectorSwitch == 0)
                {
                    for (int i = 0; i < blueSphereCount; i++)
                    {
                        if (MainCollecter.CircleCollides(SphereList.BlueSphereList[i]) == true)
                        {
                            SphereList.BlueSphereList[i].position = MainCollecter.position;
                            MainCollecter.texture = Content.Load<Texture2D>("Blue Spheres/Blue_Sphere");
                            collectorSwitch = 2;
                            collectorIndex = i;
                            temp = i;
                        }
                    }
                }
                if (collectorSwitch == 0)
                {
                    for (int i = 0; i < greenSphereCount; i++)
                    {
                        if (MainCollecter.CircleCollides(SphereList.GreenSphereList[i]) == true)
                        {
                            SphereList.GreenSphereList[i].position = MainCollecter.position;
                            MainCollecter.texture = Content.Load<Texture2D>("Green Spheres/Green_Sphere");
                            collectorSwitch = 3;
                            collectorIndex = i;
                            temp = i;
                        }
                    }
                }
                if (collectorSwitch == 0)
                {
                    for (int i = 0; i < yellowSphereCount; i++)
                    {
                        if (MainCollecter.CircleCollides(SphereList.YellowSphereList[i]) == true)
                        {
                            SphereList.YellowSphereList[i].position = MainCollecter.position;
                            MainCollecter.texture = Content.Load<Texture2D>("Yellow Spheres/Yellow_Sphere");
                            collectorSwitch = 4;
                            collectorIndex = i;
                            temp = i;
                        }
                    }
                }
                #endregion
                //Run mergeLogic function and pass appropriate list (i.e. the list that the collected sphere belongs to).
                #region
                switch (collectorSwitch)
                {
                    case 1:
                        mergeRed = true;
                        if (mergeComplete == false)
                            MergeLogic(SphereList.RedSphereList, redSphereCount, redScore);
                        break;
                    case 2:
                        mergeBlue = true;
                        if (mergeComplete == false)
                            MergeLogic(SphereList.BlueSphereList, blueSphereCount, blueScore);
                        break;
                    case 3:
                        mergeGreen = true;
                        if (mergeComplete == false)
                            MergeLogic(SphereList.GreenSphereList, greenSphereCount, greenScore);
                        break;
                    case 4:
                        mergeYellow = true;
                        if (mergeComplete == false)
                            MergeLogic(SphereList.YellowSphereList, yellowSphereCount, yellowScore);
                        break;
                }
                #endregion
            }
        }
        void MergeLogic(List<Sphere> ListOfSpheres, int sphereCount, int sphereScore)
        {
            ListOfSpheres[collectorIndex].position = MainCollecter.position;
            //Merge spheres
            #region
            if (mergeComplete != true)
            {
                for (int i = 0; i < sphereCount; i++)
                {
                    if (collectorIndex != i)
                    {
                        if (i < sphereCount && collectorIndex < sphereCount)
                            if (ListOfSpheres[collectorIndex].CircleCollides(ListOfSpheres[i]) == true)
                            {
                                if (i < sphereCount && mergeComplete == false)
                                    if (ListOfSpheres[collectorIndex].sphereSize == 1 && ListOfSpheres[i].sphereSize == 1)
                                    {
                                        SetMediumTexture();
                                        ListOfSpheres[collectorIndex].frameWidth = 150;
                                        ListOfSpheres[collectorIndex].frameHeight = 150;
                                        ListOfSpheres[collectorIndex].sphereSize += 1;
                                        ListOfSpheres.Remove(ListOfSpheres[i]);
                                        DecreaseSphereCount();
                                        sphereCount--;
                                        mergeComplete = true;
                                        MergeSound = soundBank.GetCue("Merge_Cue");
                                        MergeSound.Play();
                                    }
                                if (i < sphereCount && mergeComplete == false)
                                    if (ListOfSpheres[collectorIndex].sphereSize == 2 && ListOfSpheres[i].sphereSize == 2)
                                    {
                                        SetLargeTexture();
                                        ListOfSpheres[collectorIndex].frameWidth = 200;
                                        ListOfSpheres[collectorIndex].frameHeight = 200;
                                        ListOfSpheres[collectorIndex].sphereSize += 1;
                                        ListOfSpheres.Remove(ListOfSpheres[i]);
                                        DecreaseSphereCount();
                                        sphereCount--;
                                        mergeComplete = true;
                                        MergeSound = soundBank.GetCue("Merge_Cue");
                                        MergeSound.Play();
                                    }
                                if (i < sphereCount && mergeComplete == false)
                                    if (ListOfSpheres[collectorIndex].sphereSize == 3 && ListOfSpheres[i].sphereSize == 3)
                                    {
                                        SetGiantTexture();
                                        ListOfSpheres[collectorIndex].frameWidth = 300;
                                        ListOfSpheres[collectorIndex].frameHeight = 300;
                                        ListOfSpheres[collectorIndex].sphereSize += 1;
                                        ListOfSpheres.Remove(ListOfSpheres[i]);
                                        DecreaseSphereCount();
                                        sphereCount--;
                                        mergeComplete = true;
                                        MergeSound = soundBank.GetCue("Merge_Cue");
                                        MergeSound.Play();
                                    }
                                if (mergeRed == true)
                                    sphereCount = redSphereCount;
                                if (mergeBlue == true)
                                    sphereCount = blueSphereCount;
                                if (mergeGreen == true)
                                    sphereCount = greenSphereCount;
                                if (mergeYellow == true)
                                    sphereCount = yellowSphereCount;
                            }
                    }
                }
            }
            #endregion
            //Convert red sphere
            #region
            if (mergeComplete != true)
            {
                if (mergeRed != true)
                    for (int i = 0; i < redSphereCount; i++)
                    {
                        if (ListOfSpheres[collectorIndex].CircleCollides(SphereList.RedSphereList[i]) == true)
                        {
                            if (ListOfSpheres[collectorIndex].sphereSize > SphereList.RedSphereList[i].sphereSize)
                            {
                                addIndex = i;
                                if (SphereList.RedSphereList[i].sphereSize == 1)
                                    AddSmallSphere();
                                if (SphereList.RedSphereList[i].sphereSize == 2)
                                    AddMediumSphere();
                                if (SphereList.RedSphereList[i].sphereSize == 3)
                                    AddLargeSphere();

                                redScore--;
                                redSphereCount--;
                                SphereList.ScoreBoardRedList.Remove(SphereList.ScoreBoardRedList[i]);
                                SphereList.RedSphereList.Remove(SphereList.RedSphereList[i]);
                                IncreaseScore();
                                IncreaseSphereCount();
                                AddScoreSphere();
                                MergeSound = soundBank.GetCue("Merge_Cue");
                                MergeSound.Play();
                            }
                        }
                    }
            }
            #endregion
            //Convert blue sphere
            #region
            if (mergeComplete != true)
            {
                if (mergeBlue != true)
                    for (int i = 0; i < blueSphereCount; i++)
                    {
                        if (ListOfSpheres[collectorIndex].CircleCollides(SphereList.BlueSphereList[i]) == true)
                        {
                            if (ListOfSpheres[collectorIndex].sphereSize > SphereList.BlueSphereList[i].sphereSize)
                            {
                                addIndex = i;
                                if (SphereList.BlueSphereList[i].sphereSize == 1)
                                    AddSmallSphere();
                                if (SphereList.BlueSphereList[i].sphereSize == 2)
                                    AddMediumSphere();
                                if (SphereList.BlueSphereList[i].sphereSize == 3)
                                    AddLargeSphere();

                                blueScore--;
                                blueSphereCount--;
                                SphereList.ScoreBoardBlueList.Remove(SphereList.ScoreBoardBlueList[i]);
                                SphereList.BlueSphereList.Remove(SphereList.BlueSphereList[i]);
                                IncreaseScore();
                                IncreaseSphereCount();
                                AddScoreSphere();
                                MergeSound = soundBank.GetCue("Merge_Cue");
                                MergeSound.Play();
                            }
                        }
                    }
            }
            #endregion
            //Convert green sphere
            #region
            if (mergeComplete != true)
            {
                if (mergeGreen != true)
                    for (int i = 0; i < greenSphereCount; i++)
                    {
                        if (ListOfSpheres[collectorIndex].CircleCollides(SphereList.GreenSphereList[i]) == true)
                        {
                            if (ListOfSpheres[collectorIndex].sphereSize > SphereList.GreenSphereList[i].sphereSize)
                            {
                                addIndex = i;
                                if (SphereList.GreenSphereList[i].sphereSize == 1)
                                    AddSmallSphere();
                                if (SphereList.GreenSphereList[i].sphereSize == 2)
                                    AddMediumSphere();
                                if (SphereList.GreenSphereList[i].sphereSize == 3)
                                    AddLargeSphere();

                                greenScore--;
                                greenSphereCount--;
                                SphereList.ScoreBoardGreenList.Remove(SphereList.ScoreBoardGreenList[i]);
                                SphereList.GreenSphereList.Remove(SphereList.GreenSphereList[i]);
                                IncreaseScore();
                                IncreaseSphereCount();
                                AddScoreSphere();
                                MergeSound = soundBank.GetCue("Merge_Cue");
                                MergeSound.Play();
                            }
                        }
                    }
            }
            #endregion
            //Convert yellow sphere
            #region
            if (mergeComplete != true)
            {
                if (mergeYellow != true)
                    for (int i = 0; i < yellowSphereCount; i++)
                    {
                        if (ListOfSpheres[collectorIndex].CircleCollides(SphereList.YellowSphereList[i]) == true)
                        {
                            if (ListOfSpheres[collectorIndex].sphereSize > SphereList.YellowSphereList[i].sphereSize)
                            {
                                addIndex = i;
                                if (SphereList.YellowSphereList[i].sphereSize == 1)
                                    AddSmallSphere();
                                if (SphereList.YellowSphereList[i].sphereSize == 2)
                                    AddMediumSphere();
                                if (SphereList.YellowSphereList[i].sphereSize == 3)
                                    AddLargeSphere();

                                yellowScore--;
                                yellowSphereCount--;
                                SphereList.ScoreBoardYellowList.Remove(SphereList.ScoreBoardYellowList[i]);
                                SphereList.YellowSphereList.Remove(SphereList.YellowSphereList[i]);
                                IncreaseScore();
                                IncreaseSphereCount();
                                AddScoreSphere();
                                //SphereMerge.Play();
                                MergeSound = soundBank.GetCue("Merge_Cue");
                                MergeSound.Play();
                            }
                        }
                    }
            }
            #endregion
            //Enemies move to player and attempt to convert sphere
            #region
            if (mergeComplete == false)
            {
                reboundEnemies = true;
                foreach (Sphere e in SphereList.EnemySphereList)
                {
                    for (int j = 0; j < enemySphereCount; j++)
                    {
                        if (mergeRed == true)
                            if (e.CircleCollides(SphereList.RedSphereList[temp]) == true && RightBlockerActive == false && LeftBlockerActive == false && blockerDepleted == false)
                            {
                                mergeComplete = true;
                                convertRed = true;
                                enemyConvert = true;
                                enemyConversion.Play();
                            }
                        if (mergeBlue == true)
                            if (e.CircleCollides(SphereList.BlueSphereList[temp]) == true && RightBlockerActive == false && LeftBlockerActive == false && blockerDepleted == false)
                            {
                                mergeComplete = true;
                                convertBlue = true;
                                enemyConvert = true;
                                enemyConversion.Play();
                            }
                        if (mergeGreen == true)
                            if (e.CircleCollides(SphereList.GreenSphereList[temp]) == true && RightBlockerActive == false && LeftBlockerActive == false && blockerDepleted == false)
                            {
                                mergeComplete = true;
                                convertGreen = true;
                                enemyConvert = true;
                                enemyConversion.Play();
                            }
                        if (mergeYellow == true)
                            if (e.CircleCollides(SphereList.YellowSphereList[temp]) == true && RightBlockerActive == false && LeftBlockerActive == false && blockerDepleted == false)
                            {
                                mergeComplete = true;
                                convertYellow = true;
                                enemyConvert = true;
                                enemyConversion.Play();
                            }

                        if (e.position.X > MainCollecter.position.X && e.enemyRebounded == false)
                        {
                            if (e.velocity.X > 0)
                                e.velocity.X = -e.velocity.X;
                        }
                        if (e.position.X < MainCollecter.position.X && e.enemyRebounded == false)
                        {
                            if (e.velocity.X < 0)
                                e.velocity.X = -e.velocity.X;
                        }
                        if (e.position.Y > MainCollecter.position.Y && e.enemyRebounded == false)
                        {
                            if (e.velocity.Y > 0)
                                e.velocity.Y = -e.velocity.Y;
                        }
                        if (e.position.Y < MainCollecter.position.Y && e.enemyRebounded == false)
                        {
                            if (e.velocity.Y < 0)
                                e.velocity.Y = -e.velocity.Y;
                        }
                        //newDirection.X = SphereList.EnemySphereList[j].position.X - SphereList.RedSphereList[i].position.X;
                        //newDirection.Y = SphereList.EnemySphereList[j].position.Y - SphereList.RedSphereList[i].position.Y;

                        //SphereList.EnemySphereList[j].position.X = newDirection.X;
                        //SphereList.EnemySphereList[j].position.Y = newDirection.Y;
                    }
                }
            }
            if (enemyConvert == true)
            {
                if (convertRed == true)
                    SphereList.EnemySphereList.Add(new Sphere(Content.Load<Texture2D>("Black_Sphere_SpriteSheet"), new Vector2(SphereList.RedSphereList[temp].position.X + 100, SphereList.RedSphereList[temp].position.Y + 100), 100, 100));
                if (convertBlue == true)
                    SphereList.EnemySphereList.Add(new Sphere(Content.Load<Texture2D>("Black_Sphere_SpriteSheet"), new Vector2(SphereList.BlueSphereList[temp].position.X + 100, SphereList.BlueSphereList[temp].position.Y + 100), 100, 100));
                if (convertGreen == true)
                    SphereList.EnemySphereList.Add(new Sphere(Content.Load<Texture2D>("Black_Sphere_SpriteSheet"), new Vector2(SphereList.GreenSphereList[temp].position.X + 100, SphereList.GreenSphereList[temp].position.Y + 100), 100, 100));
                if (convertYellow == true)
                    SphereList.EnemySphereList.Add(new Sphere(Content.Load<Texture2D>("Black_Sphere_SpriteSheet"), new Vector2(SphereList.YellowSphereList[temp].position.X + 100, SphereList.YellowSphereList[temp].position.Y + 100), 100, 100));
                enemyConvert = false;
                mergeComplete = true;
                EnemyScore += 60;
            }
        }
            #endregion
        void SetMediumTexture()
        {
            if (mergeRed == true)
                SphereList.RedSphereList[collectorIndex].texture = Content.Load<Texture2D>("Red Spheres/RedSphere_SpriteSheet_Medium");
            if (mergeBlue == true)
                SphereList.BlueSphereList[collectorIndex].texture = Content.Load<Texture2D>("Blue Spheres/BlueSphere_SpriteSheet_Medium");
            if (mergeGreen == true)
                SphereList.GreenSphereList[collectorIndex].texture = Content.Load<Texture2D>("Green Spheres/GreenSphere_SpriteSheet_Medium");
            if (mergeYellow == true)
                SphereList.YellowSphereList[collectorIndex].texture = Content.Load<Texture2D>("Yellow Spheres/YellowSphere_SpriteSheet_Medium");
        }
        void SetLargeTexture()
        {
            if (mergeRed == true)
                SphereList.RedSphereList[collectorIndex].texture = Content.Load<Texture2D>("Red Spheres/RedSphere_SpriteSheet_Large");
            if (mergeBlue == true)
                SphereList.BlueSphereList[collectorIndex].texture = Content.Load<Texture2D>("Blue Spheres/BlueSphere_SpriteSheet_Large");
            if (mergeGreen == true)
                SphereList.GreenSphereList[collectorIndex].texture = Content.Load<Texture2D>("Green Spheres/GreenSphere_SpriteSheet_Large");
            if (mergeYellow == true)
                SphereList.YellowSphereList[collectorIndex].texture = Content.Load<Texture2D>("Yellow Spheres/YellowSphere_SpriteSheet_Large");
        }
        void SetGiantTexture()
        {
            if (mergeRed == true)
                SphereList.RedSphereList[collectorIndex].texture = Content.Load<Texture2D>("Red Spheres/RedSphere_SpriteSheet_Giant");
            if (mergeBlue == true)
                SphereList.BlueSphereList[collectorIndex].texture = Content.Load<Texture2D>("Blue Spheres/BlueSphere_SpriteSheet_Giant");
            if (mergeGreen == true)
                SphereList.GreenSphereList[collectorIndex].texture = Content.Load<Texture2D>("Green Spheres/GreenSphere_SpriteSheet_Giant");
            if (mergeYellow == true)
                SphereList.YellowSphereList[collectorIndex].texture = Content.Load<Texture2D>("Yellow Spheres/YellowSphere_SpriteSheet_Giant");
        }

        void AddSmallSphere()
        {
            if (mergeRed == true)
                SphereList.RedSphereList.Add(new Sphere(Content.Load<Texture2D>("Red Spheres/RedSphere_SpriteSheet_Small"), new Vector2(SphereList.BlueSphereList[addIndex].position.X + 100, SphereList.BlueSphereList[addIndex].position.Y + 100), 100, 100));
            if (mergeBlue == true)
                SphereList.BlueSphereList.Add(new Sphere(Content.Load<Texture2D>("Blue Spheres/BlueSphere_SpriteSheet_Small"), new Vector2(SphereList.BlueSphereList[addIndex].position.X + 100, SphereList.BlueSphereList[addIndex].position.Y + 100), 100, 100));
            if (mergeGreen == true)
                SphereList.GreenSphereList.Add(new Sphere(Content.Load<Texture2D>("Green Spheres/GreenSphere_SpriteSheet_Small"), new Vector2(SphereList.BlueSphereList[addIndex].position.X + 100, SphereList.BlueSphereList[addIndex].position.Y + 100), 100, 100));
            if (mergeYellow == true)
                SphereList.YellowSphereList.Add(new Sphere(Content.Load<Texture2D>("Yellow Spheres/YellowSphere_SpriteSheet_Small"), new Vector2(SphereList.BlueSphereList[addIndex].position.X + 100, SphereList.BlueSphereList[addIndex].position.Y + 100), 100, 100));
            mergeComplete = true;
        }
        void AddMediumSphere()
        {
            if (mergeRed == true)
                SphereList.RedSphereList.Add(new Sphere(Content.Load<Texture2D>("Red Spheres/RedSphere_SpriteSheet_Medium"), new Vector2(SphereList.BlueSphereList[addIndex].position.X + 100, SphereList.BlueSphereList[addIndex].position.Y + 100), 150, 150));
            if (mergeBlue == true)
                SphereList.BlueSphereList.Add(new Sphere(Content.Load<Texture2D>("Blue Spheres/BlueSphere_SpriteSheet_Medium"), new Vector2(SphereList.BlueSphereList[addIndex].position.X + 100, SphereList.BlueSphereList[addIndex].position.Y + 100), 150, 150));
            if (mergeGreen == true)
                SphereList.GreenSphereList.Add(new Sphere(Content.Load<Texture2D>("Green Spheres/GreenSphere_SpriteSheet_Medium"), new Vector2(SphereList.BlueSphereList[addIndex].position.X + 100, SphereList.BlueSphereList[addIndex].position.Y + 100), 150, 150));
            if (mergeYellow == true)
                SphereList.YellowSphereList.Add(new Sphere(Content.Load<Texture2D>("Yellow Spheres/YellowSphere_SpriteSheet_Medium"), new Vector2(SphereList.BlueSphereList[addIndex].position.X + 100, SphereList.BlueSphereList[addIndex].position.Y + 100), 150, 150));
            mergeComplete = true;
        }
        void AddLargeSphere()
        {
            if (mergeRed == true)
                SphereList.RedSphereList.Add(new Sphere(Content.Load<Texture2D>("Red Spheres/RedSphere_SpriteSheet_Large"), new Vector2(SphereList.BlueSphereList[addIndex].position.X + 100, SphereList.BlueSphereList[addIndex].position.Y + 100), 200, 200));
            if (mergeBlue == true)
                SphereList.BlueSphereList.Add(new Sphere(Content.Load<Texture2D>("Blue Spheres/BlueSphere_SpriteSheet_Large"), new Vector2(SphereList.BlueSphereList[addIndex].position.X + 100, SphereList.BlueSphereList[addIndex].position.Y + 100), 200, 200));
            if (mergeGreen == true)
                SphereList.GreenSphereList.Add(new Sphere(Content.Load<Texture2D>("Green Spheres/GreenSphere_SpriteSheet_Large"), new Vector2(SphereList.BlueSphereList[addIndex].position.X + 100, SphereList.BlueSphereList[addIndex].position.Y + 100), 200, 200));
            if (mergeYellow == true)
                SphereList.YellowSphereList.Add(new Sphere(Content.Load<Texture2D>("Yellow Spheres/YellowSphere_SpriteSheet_Large"), new Vector2(SphereList.BlueSphereList[addIndex].position.X + 100, SphereList.BlueSphereList[addIndex].position.Y + 100), 200, 200));
            mergeComplete = true;
        }
        void AddGiantSphere()
        {
            if (mergeRed == true)
                SphereList.RedSphereList.Add(new Sphere(Content.Load<Texture2D>("Red Spheres/RedSphere_SpriteSheet_Giant"), new Vector2(SphereList.BlueSphereList[addIndex].position.X + 100, SphereList.BlueSphereList[addIndex].position.Y + 100), 300, 300));
            if (mergeBlue == true)
                SphereList.BlueSphereList.Add(new Sphere(Content.Load<Texture2D>("Blue Spheres/BlueSphere_SpriteSheet_Giant"), new Vector2(SphereList.BlueSphereList[addIndex].position.X + 100, SphereList.BlueSphereList[addIndex].position.Y + 100), 300, 300));
            if (mergeGreen == true)
                SphereList.GreenSphereList.Add(new Sphere(Content.Load<Texture2D>("Green Spheres/GreenSphere_SpriteSheet_Giant"), new Vector2(SphereList.BlueSphereList[addIndex].position.X + 100, SphereList.BlueSphereList[addIndex].position.Y + 100), 300, 300));
            if (mergeYellow == true)
                SphereList.YellowSphereList.Add(new Sphere(Content.Load<Texture2D>("Yellow Spheres/YellowSphere_SpriteSheet_Giant"), new Vector2(SphereList.BlueSphereList[addIndex].position.X + 100, SphereList.BlueSphereList[addIndex].position.Y + 100), 300, 300));
            mergeComplete = true;
        }
        void AddScoreSphere()
        {
            if (mergeRed == true)
                SphereList.ScoreBoardRedList.Add(new ScoreSphere(Content.Load<Texture2D>("Red Spheres/Red_Sphere_Score"), new Vector2(5, (Game1.cameraPosition.Y + Game1.ScreenHeight) - (redScore * 30))));
            if (mergeBlue == true)
                SphereList.ScoreBoardBlueList.Add(new ScoreSphere(Content.Load<Texture2D>("Blue Spheres/Blue_Sphere_Score"), new Vector2(35, (Game1.cameraPosition.Y + Game1.ScreenHeight) - (blueScore * 30))));
            if (mergeGreen == true)
                SphereList.ScoreBoardGreenList.Add(new ScoreSphere(Content.Load<Texture2D>("Green Spheres/Green_Sphere_Score"), new Vector2(Game1.ScreenWidth - 65, (Game1.cameraPosition.Y + Game1.ScreenHeight) - (greenScore * 30))));
            if (mergeYellow == true)
                SphereList.ScoreBoardYellowList.Add(new ScoreSphere(Content.Load<Texture2D>("Yellow Spheres/Yellow_Sphere_Score"), new Vector2(Game1.ScreenWidth - 30, (Game1.cameraPosition.Y + Game1.ScreenHeight) - (yellowScore * 30))));
        }

        void IncreaseSphereCount()
        {
            if (mergeRed == true)
                redSphereCount++;
            if (mergeBlue == true)
                blueSphereCount++;
            if (mergeGreen == true)
                greenSphereCount++;
            if (mergeYellow == true)
                yellowSphereCount++;
        }
        void DecreaseSphereCount()
        {
            if (mergeRed == true)
                redSphereCount--;
            if (mergeBlue == true)
                blueSphereCount--;
            if (mergeGreen == true)
                greenSphereCount--;
            if (mergeYellow == true)
                yellowSphereCount--;
        }

        void DecreaseScore()
        {
            if (mergeRed == true)
                redScore--;
            if (mergeBlue == true)
                blueScore--;
            if (mergeGreen == true)
                greenScore--;
            if (mergeYellow == true)
                yellowScore--;
        }
        void IncreaseScore()
        {
            if (mergeRed == true)
                redScore++;
            if (mergeBlue == true)
                blueScore++;
            if (mergeGreen == true)
                greenScore++;
            if (mergeYellow == true)
                yellowScore++;
        }

        void Blockers(GameTime gameTime)
        {
            //rightElbowBlocker.Boundaries(rightElbowBlocker, gameTime);
            //leftElbowBlocker.Boundaries(leftElbowBlocker, gameTime);

            //rightElbowBlocker.position = rightElbowPosition + cameraPosition;
            //leftElbowBlocker.position = leftElbowPosition + cameraPosition;

            //for (int i = 0; i < redSphereCount; i++)
            //{
            //    if (RightBlockerActive == true)
            //        if (rightElbowBlocker.CircleCollides(SphereList.RedSphereList[i]) == true)
            //        {
            //            SphereList.RedSphereList[i].velocity.X = -SphereList.RedSphereList[i].velocity.X;
            //            SphereList.RedSphereList[i].velocity.Y = -SphereList.RedSphereList[i].velocity.Y;
            //        }
            //    if (LeftBlockerActive == true)
            //        if (leftElbowBlocker.CircleCollides(SphereList.RedSphereList[i]) == true)
            //        {
            //            SphereList.RedSphereList[i].velocity.X = -SphereList.RedSphereList[i].velocity.X;
            //            SphereList.RedSphereList[i].velocity.Y = -SphereList.RedSphereList[i].velocity.Y;
            //        }
            //}
            //for (int i = 0; i < blueSphereCount; i++)
            //{
            //    if (RightBlockerActive == true)
            //        if (rightElbowBlocker.CircleCollides(SphereList.BlueSphereList[i]) == true)
            //        {
            //            SphereList.BlueSphereList[i].velocity.X = -SphereList.BlueSphereList[i].velocity.X;
            //            SphereList.BlueSphereList[i].velocity.Y = -SphereList.BlueSphereList[i].velocity.Y;
            //        }
            //    if (LeftBlockerActive == true)
            //        if (leftElbowBlocker.CircleCollides(SphereList.BlueSphereList[i]) == true)
            //        {
            //            SphereList.BlueSphereList[i].velocity.X = -SphereList.BlueSphereList[i].velocity.X;
            //            SphereList.BlueSphereList[i].velocity.Y = -SphereList.BlueSphereList[i].velocity.Y;
            //        }
            //}
            //for (int i = 0; i < greenSphereCount; i++)
            //{
            //    if (RightBlockerActive == true)
            //        if (rightElbowBlocker.CircleCollides(SphereList.GreenSphereList[i]) == true)
            //        {
            //            SphereList.GreenSphereList[i].velocity.X = -SphereList.GreenSphereList[i].velocity.X;
            //            SphereList.GreenSphereList[i].velocity.Y = -SphereList.GreenSphereList[i].velocity.Y;
            //        }
            //    if (LeftBlockerActive == true)
            //        if (leftElbowBlocker.CircleCollides(SphereList.GreenSphereList[i]) == true)
            //        {
            //            SphereList.GreenSphereList[i].velocity.X = -SphereList.GreenSphereList[i].velocity.X;
            //            SphereList.GreenSphereList[i].velocity.Y = -SphereList.GreenSphereList[i].velocity.Y;
            //        }
            //}
            //for (int i = 0; i < yellowSphereCount; i++)
            //{
            //    if (RightBlockerActive == true)
            //        if (rightElbowBlocker.CircleCollides(SphereList.YellowSphereList[i]) == true)
            //        {
            //            SphereList.YellowSphereList[i].velocity.X = -SphereList.YellowSphereList[i].velocity.X;
            //            SphereList.YellowSphereList[i].velocity.Y = -SphereList.YellowSphereList[i].velocity.Y;
            //        }
            //    if (LeftBlockerActive == true)
            //        if (leftElbowBlocker.CircleCollides(SphereList.YellowSphereList[i]) == true)
            //        {
            //            SphereList.YellowSphereList[i].velocity.X = -SphereList.YellowSphereList[i].velocity.X;
            //            SphereList.YellowSphereList[i].velocity.Y = -SphereList.YellowSphereList[i].velocity.Y;
            //        }
            //}
            for (int i = 0; i < enemySphereCount; i++)
            {
                if (RightBlockerActive == true)
                    if (rightElbowBlocker.CircleCollides(SphereList.EnemySphereList[i]) == true)
                    {
                        SphereList.EnemySphereList[i].velocity.X = -SphereList.EnemySphereList[i].velocity.X;
                        SphereList.EnemySphereList[i].velocity.Y = -SphereList.EnemySphereList[i].velocity.Y;
                        if (reboundEnemies == true)
                            SphereList.EnemySphereList[i].enemyRebounded = true;
                    }
                if (LeftBlockerActive == true)
                    if (leftElbowBlocker.CircleCollides(SphereList.EnemySphereList[i]) == true)
                    {
                        SphereList.EnemySphereList[i].velocity.X = -SphereList.EnemySphereList[i].velocity.X;
                        SphereList.EnemySphereList[i].velocity.Y = -SphereList.EnemySphereList[i].velocity.Y;
                        if (reboundEnemies == true)
                            SphereList.EnemySphereList[i].enemyRebounded = true;
                    }

                //if (RightBlockerActive == true)
                //    if (Blocker.CircleCollides(SphereList.EnemySphereList[i]) == true)
                //    {
                //        SphereList.EnemySphereList[i].velocity.X = -SphereList.EnemySphereList[i].velocity.X;
                //        SphereList.EnemySphereList[i].velocity.Y = -SphereList.EnemySphereList[i].velocity.Y;
                //        if (reboundEnemies == true)
                //            SphereList.EnemySphereList[i].enemyRebounded = true;
                //    }
            }
            //}
        }
        #endregion

        //////////////////////////----------Kinect----------//////////////////////////
        #region
        void KinectSensors_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            if (this.kinectSensor == e.Sensor)
            {
                if (e.Status == KinectStatus.Disconnected ||
                        e.Status == KinectStatus.NotPowered)
                {
                    this.kinectSensor = null;
                    this.DiscoverKinectSensor();
                }
            }
        }
        private void DiscoverKinectSensor()
        {
            foreach (KinectSensor sensor in KinectSensor.KinectSensors)
            {
                if (sensor.Status == KinectStatus.Connected)
                {
                    kinectSensor = sensor;
                    break;
                }
            }
            if (this.kinectSensor == null)
            {
                connectedStatus = "Found none Kinect Sensors connected to USB";
                return;
            }
            // Init the kinect
            if (kinectSensor.Status == KinectStatus.Connected)
            {
                InitializeKinect();
            }
        }
        private bool InitializeKinect()
        {
            // Color stream
            kinectSensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
            kinectSensor.ColorFrameReady += new
                  EventHandler<ColorImageFrameReadyEventArgs>(kinectSensor_ColorFrameReady);

            // Skeleton Stream
            kinectSensor.SkeletonStream.Enable();
            kinectSensor.SkeletonFrameReady += new
                 EventHandler<SkeletonFrameReadyEventArgs>(kinectSensor_SkeletonFrameReady);

            kinectSensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
            kinectSensor.DepthFrameReady += new EventHandler<DepthImageFrameReadyEventArgs>(kinectSensor_DepthFrameReady);
 

            //kinectSensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
            //kinectSensor.ColorFrameReady += new
            //        EventHandler<ColorImageFrameReadyEventArgs>(kinectSensor_ColorFrameReady);
            try
            {
                kinectSensor.Start();
            }
            catch
            {
                connectedStatus = "Unable to start the Kinect Sensor";
                return false;
            }
            return true;
        }
        void kinectSensor_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            using (ColorImageFrame colorImageFrame = e.OpenColorImageFrame())
            {
                if (colorImageFrame != null)
                {
                    byte[] pixelsFromFrame = new byte[colorImageFrame.PixelDataLength];
                    colorImageFrame.CopyPixelDataTo(pixelsFromFrame);
                    Color[] color = new Color[colorImageFrame.Height * colorImageFrame.Width];
                    kinectRGBVideo = new Texture2D(graphics.GraphicsDevice,
                                                   colorImageFrame.Width,
              colorImageFrame.Height);
                    // Go through each pixel and set the bytes correctly
                    // Remember, each pixel has Red, Green and Blue
                    int index = 0;
                    for (int y = 0; y < colorImageFrame.Height; y++)
                    {
                        for (int x = 0; x < colorImageFrame.Width; x++, index += 4)
                        {
                            color[y * colorImageFrame.Width + x] = new Color(pixelsFromFrame[index + 2],
                                             pixelsFromFrame[index + 1], pixelsFromFrame[index + 0]);
                        }
                    }
                    // Set pixeldata from the ColorImageFrame to a Texture2D
                    kinectRGBVideo.SetData(color);
                }
            }
        }
        void kinectSensor_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    Skeleton[] skeletonData = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletonData);
                    Skeleton playerSkeleton = (from s in skeletonData
                                               where s.TrackingState == SkeletonTrackingState.Tracked
                                               select s).FirstOrDefault();
                    if (playerSkeleton != null)
                    {
                        //Camera Controller
                        Joint hipCentre = playerSkeleton.Joints[JointType.HipCenter];
                        hipPosition = new Vector3((((0.5f * hipCentre.Position.X) + 0.5f) * BackgroundWidth), 0,
                                                  (((-0.5f * hipCentre.Position.Z) + 0.5f) * BackgroundHeight));

                        //Blockers
                        Joint righElbow = playerSkeleton.Joints[JointType.ElbowRight];
                        rightElbowPosition = new Vector2((((0.5f * righElbow.Position.X) + 0.5f) * ScreenWidth),
                                                        (((-0.5f * righElbow.Position.Y) + 0.5f) * ScreenHeight));

                        Joint leftElbow = playerSkeleton.Joints[JointType.ElbowLeft];
                        leftElbowPosition = new Vector2((((0.5f * leftElbow.Position.X) + 0.5f) * ScreenWidth),
                                                       (((-0.5f * leftElbow.Position.Y) + 0.5f) * ScreenHeight));

                        //Collectors
                        Joint leftHand = playerSkeleton.Joints[JointType.HandLeft];
                        leftHandPosition = new Vector2((((0.5f * leftHand.Position.X) + 0.5f) * ScreenWidth),
                                                      (((-0.5f * leftHand.Position.Y) + 0.5f) * ScreenHeight));

                        Joint rightHand = playerSkeleton.Joints[JointType.HandRight];
                        rightHandPosition = new Vector2((((0.5f * rightHand.Position.X) + 0.5f) * ScreenWidth),
                                                       (((-0.5f * rightHand.Position.Y) + 0.5f) * ScreenHeight));
                        //Console.WriteLine(hipCentre.Position.Z);
                    }
                }
            }
        }
        void kinectSensor_DepthFrameReady(object sender, DepthImageFrameReadyEventArgs e)
        {
            using (DepthImageFrame depthImageFrame = e.OpenDepthImageFrame())
            {
                if (depthImageFrame != null)
                {
                    short[] pixelsFromFrame = new short[depthImageFrame.PixelDataLength];

                    depthImageFrame.CopyPixelDataTo(pixelsFromFrame);
                    byte[] convertedPixels = ConvertDepthFrame(pixelsFromFrame, ((KinectSensor)sender).DepthStream, 640 * 480 * 4);

                    Color[] color = new Color[depthImageFrame.Height * depthImageFrame.Width];
                }
            }
        }
        // Converts a 16-bit grayscale depth frame which includes player indexes into a 32-bit frame
        // that displays different players in different colors
        private byte[] ConvertDepthFrame(short[] depthFrame, DepthImageStream depthStream, int depthFrame32Length)
        {
            int tooNearDepth = depthStream.TooNearDepth;
            int tooFarDepth = depthStream.TooFarDepth;
            int unknownDepth = depthStream.UnknownDepth;
            byte[] depthFrame32 = new byte[depthFrame32Length];

            for (int i16 = 0, i32 = 0; i16 < depthFrame.Length && i32 < depthFrame32.Length; i16++, i32 += 4)
            {
                int player = depthFrame[i16] & DepthImageFrame.PlayerIndexBitmask;
                int realDepth = depthFrame[i16] >> DepthImageFrame.PlayerIndexBitmaskWidth;

                // transform 13-bit depth information into an 8-bit intensity appropriate
                // for display (we disregard information in most significant bit)
                byte intensity = (byte)(~(realDepth >> 4));
            }
            return depthFrame32;
        }
        #endregion
    }
}
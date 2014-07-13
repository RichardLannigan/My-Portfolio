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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace Pathfinder
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region  Global Variables
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //sprite texture for tiles, player, and ai bot
        Texture2D tile1Texture;
        Texture2D tile2Texture;
        Texture2D aiTexture;
        Texture2D playerTexture;

        //Objects representing the level map, bot, and player 
        private Level level;
        private Dijkstra dijkstra;
        private AStar aStar;
        private ScentMap scentMap;
        private Player player;
        private Bot bot;

        //Screen size and frame rate
        private const int TargetFrameRate = 50;
        private const int BackBufferWidth = 600; //600
        private const int BackBufferHeight = 600; //600
        int displaySize = 15;

        bool keyIsUpD = true;
        bool keyIsUpA = true;
        bool keyIsUpS = true;
        bool keyIsUpR = true;
        bool runDijkstra = false;
        bool runAStar = false;
        bool runScentMap = false;
        bool mapSelected = false;
        bool scentMapFirstRun = true;
        Coord2 originalBotPos = new Coord2(5,5);

        //Timing Variables
        TimeSpan startTime = TimeSpan.Zero;
        static int timesToRunAlgorithm = 100;
        const int timesToRunTest = 20;
        long[] dijkstraRunDuration = new long[timesToRunAlgorithm];
        long[] aStarRunDuration = new long[timesToRunAlgorithm];
        long[] scentMapRunDuration = new long[timesToRunAlgorithm];
        long dijkstraAverage = 0;
        long aStarAverage = 0;
        long scentMapAverage = 0;
        System.Diagnostics.Stopwatch time = new System.Diagnostics.Stopwatch();
        System.Diagnostics.Stopwatch testingTime = new System.Diagnostics.Stopwatch();
        int levelIndex = 1;
        int algorithmIndex = 1;

        //Create arrays where the averages will be stored (the '3' providing room for the shortest, longest and range values).
        long[] dijkstraAveragesArray = new long[timesToRunTest + 3];
        long[] aStarAveragesArray = new long[timesToRunTest + 3];
        long[] scentMapAveragesArray = new long[timesToRunTest + 3];
        bool dijkstraTestComplete = false;
        bool aStarTestComplete = false;
        bool scentMapTestComplete = false;

        //Printing to text files
        string[] lines = new string[72];
        int stringIndex = 0;
        int runIndex = 1;
        string aStarString;
        string dijkstraString;
        string scentMapString;
        bool rawData = false;

        #endregion

        public Game1()
        {
            //Constructor
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = BackBufferHeight;
            graphics.PreferredBackBufferWidth = BackBufferWidth;
            Window.Title = "Pathfinder";
            Content.RootDirectory = "Content";

            //Set frame rate.
            TargetElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / TargetFrameRate);

            //Load level map.
            level = new Level();
            level.Loadmap("Content/0.txt");

            //Initiate algorthms
            dijkstra = new Dijkstra(level);
            aStar = new AStar(level);
            scentMap = new ScentMap(level);

            //Instantiate bot and player objects.
            bot = new Bot(5, 5);
            player = new Player(36, 35);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            //Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Load the sprite textures.
            tile1Texture = Content.Load<Texture2D>("tile1");
            tile2Texture = Content.Load<Texture2D>("tile2");
            aiTexture = Content.Load<Texture2D>("ai");
            playerTexture = Content.Load<Texture2D>("target");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            //Player movement
            KeyboardState keyState = Keyboard.GetState();
            Coord2 currentPos = new Coord2();
            Coord2 botPos = new Coord2();
            currentPos = player.GridPosition;
            botPos = bot.gridPosition;

            //User input
            UserInput(keyState, player, level, currentPos, gameTime);

            //Update enemy and player
            bot.Update(gameTime, level, player);
            player.Update(gameTime, level);

            //Automatically run tests
            RunTests(gameTime);

            //Detect when each set is complete and calculate the shortest, longest and the range of the run times.
            if (dijkstraTestComplete == true)
            {
                TestComplete(dijkstraAveragesArray);
                dijkstraTestComplete = false;
            }
            if (aStarTestComplete == true)
            {
                TestComplete(aStarAveragesArray);
                aStarTestComplete = false;
            }
            if (scentMapTestComplete == true)
            {
                TestComplete(scentMapAveragesArray);
                scentMapTestComplete = false;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            //Draw level map
            DrawGrid();

            if (levelIndex < 3)
            {
                spriteBatch.Draw(aiTexture, bot.ScreenPosition, Color.White);
                spriteBatch.Draw(playerTexture, player.ScreenPosition, Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawGrid()
        {
            //draws the map grid
            int gridSize = level.GridSize;
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    Coord2 pos = new Coord2((x * displaySize), (y * displaySize));
                    if (level.tiles[x, y] == 0)
                    {
                        if (runScentMap == true)
                        {
                            float scale = (100 - scentMap.lowestValue);
                            scale = 100 / scale;
                            float diff = scentMap.buffer1[x, y] - scentMap.lowestValue;
                            float red = diff * scale;
                            spriteBatch.Draw(tile2Texture, pos, new Color(red / 100, 0, 0));
                            float colourValue = scentMap.buffer1[x, y] / 255;
                        }
                        else if (runAStar == true)
                        {
                            if (aStar.inPath[x, y])
                            {
                                spriteBatch.Draw(tile1Texture, pos, Color.Red);
                            }
                            else
                            {
                                if (aStar.closed[x, y])
                                    spriteBatch.Draw(tile1Texture, pos, Color.Blue);
                                else
                                    spriteBatch.Draw(tile1Texture, pos, Color.White);

                                if (aStar.cost[x, y] < 10000 && aStar.closed[x, y] == false)
                                    spriteBatch.Draw(tile1Texture, pos, Color.LightBlue);
                            }
                        }
                        else if (runDijkstra == true)
                        {
                            if (dijkstra.inPath[x, y])
                            {
                                spriteBatch.Draw(tile1Texture, pos, Color.Red);
                            }
                            else
                            {
                                if (dijkstra.closed[x, y])
                                    spriteBatch.Draw(tile1Texture, pos, Color.Blue);
                                else
                                    spriteBatch.Draw(tile1Texture, pos, Color.White);

                                if (dijkstra.cost[x, y] < 10000 && dijkstra.closed[x, y] == false)
                                    spriteBatch.Draw(tile1Texture, pos, Color.LightBlue);
                            }
                        }
                        else
                        {
                            if (level.tiles[x, y] == 0) spriteBatch.Draw(tile1Texture, pos, Color.White);
                            else spriteBatch.Draw(tile2Texture, pos, Color.White);
                        }
                    }
                    else spriteBatch.Draw(tile2Texture, pos, Color.White);
                }
            }
        }

        private void UserInput(KeyboardState keyState, Player player, Level level, Coord2 currentPos, GameTime gameTime)
        {
            //Handle player movement.
            PlayerMovement(keyState, player, level, currentPos, gameTime);

            //Run all tests
            if (keyState.IsKeyDown(Keys.R) && keyIsUpR == true)
            {
                keyIsUpR = false;
                RunTests(gameTime);
            }
            if (keyState.IsKeyUp(Keys.R) && keyIsUpR == false)
                keyIsUpR = true;

            //Run Dijkstra
            if (keyState.IsKeyDown(Keys.D) && keyIsUpD == true)
            {
                keyIsUpD = false;
                algorithmIndex = 1;
                SelectLevel(levelIndex);
                dijkstra.Reset(level);
                HandleDijkstraTests(gameTime);
            }
            if (keyState.IsKeyUp(Keys.D) && keyIsUpD == false)
                keyIsUpD = true;

            //Run AStar
            if (keyState.IsKeyDown(Keys.A) && keyIsUpA == true)
            {
                keyIsUpA = false;
                algorithmIndex = 2;
                SelectLevel(levelIndex);
                aStar.Reset(level);
                HandleAstarTests(gameTime);
            }
            if (keyState.IsKeyUp(Keys.A) && keyIsUpA == false)
                keyIsUpA = true;


            //Run Scent Map
            if (keyState.IsKeyDown(Keys.S) && keyIsUpS == true)
            {
                keyIsUpS = false;
                algorithmIndex = 3;
                SelectLevel(levelIndex);
                HandleScentMapTests(gameTime);
            }
            if (keyState.IsKeyUp(Keys.S) && keyIsUpS == false)
                keyIsUpS = true;
                        
            //Handele map selection
            MapSelection(keyState);

            if (keyState.IsKeyDown(Keys.P)) //Print results to text file.
            {
                System.IO.File.WriteAllLines(@"content\Text Files\test1.txt", lines);
            }

            if (keyState.IsKeyDown(Keys.Escape))
                Exit();
        }

        private void PlayerMovement(KeyboardState keyState, Player player, Level level, Coord2 currentPos, GameTime gameTime)
        {
            if (keyState.IsKeyDown(Keys.Up))
            {
                currentPos.Y -= 1;
                player.SetNextLocation(currentPos, level);
            }
            if (keyState.IsKeyDown(Keys.Down))
            {
                currentPos.Y += 1;
                player.SetNextLocation(currentPos, level);
            }
            if (keyState.IsKeyDown(Keys.Left))
            {
                currentPos.X -= 1;
                player.SetNextLocation(currentPos, level);
            }
            if (keyState.IsKeyDown(Keys.Right))
            {
                currentPos.X += 1;
                player.SetNextLocation(currentPos, level);
            }
        }

        void MapSelection(KeyboardState keyState) //Handle user input to switch between the grids.
        {
            if (keyState.IsKeyDown(Keys.D1))
            {
                levelIndex = 1;
                mapSelected = true;
            }

            if (keyState.IsKeyDown(Keys.D2))
            {
                levelIndex = 2;
                mapSelected = true;
            }

            if (keyState.IsKeyDown(Keys.D3))
            {
                levelIndex = 3;
                mapSelected = true;
            }
            if (keyState.IsKeyDown(Keys.D4))
            {
                levelIndex = 4;
                mapSelected = true;
            }
            if (keyState.IsKeyDown(Keys.D5))
            {
                levelIndex = 5;
                mapSelected = true;
            }

            //Handle map selection
            if (mapSelected == true)
            {
                SelectLevel(levelIndex);
                mapSelected = false;
                aStar.Reset(level);
                dijkstra.Reset(level);
            }
        }

        void SelectLevel(int levelIndex) //Define grid and algorithm operating parameters.
        {
            switch (levelIndex)
            {
                case 1:
                    level.gridSize = 40;
                    displaySize = 15; //Set the size of the displayed grid locations.
                    scentMap.maxScent = 100; //Set max scent to account for grid size.
                    player = new Player(36, 35); //Move player/target dependant on grid size
                    level.Loadmap("Content/0.txt"); //Load blank 40x40 grid.
                    switch (algorithmIndex)
                    {
                        case 1: //Dijkstra
                            timesToRunAlgorithm = 100;
                            break;
                        case 2: //AStar
                            timesToRunAlgorithm = 100;
                            break;
                        case 3: //Scent Map
                            timesToRunAlgorithm = 100;
                            break;
                    }
                    //Re-define array to correspond to the number of times run.
                    dijkstraRunDuration = new long[timesToRunAlgorithm];
                    aStarRunDuration = new long[timesToRunAlgorithm];
                    scentMapRunDuration = new long[timesToRunAlgorithm];
                    break;

                case 2:
                    level.gridSize = 40;
                    displaySize = 15; //Set the size of the displayed grid locations.
                    scentMap.maxScent = 100; //Set max scent to account for grid size.
                    player = new Player(36, 35); //Move player/target dependant on grid size
                    level.Loadmap("Content/maze.txt"); //Load pre-built 40x40 maze.
                    switch (algorithmIndex)
                    {
                        case 1: //Dijkstra
                            timesToRunAlgorithm = 100;
                            break;
                        case 2: //AStar
                            timesToRunAlgorithm = 100;
                            break;
                        case 3: //Scent Map
                            timesToRunAlgorithm = 100;
                            break;
                    }
                    //Re-define array to correspond to the number of times run.
                    dijkstraRunDuration = new long[timesToRunAlgorithm];
                    aStarRunDuration = new long[timesToRunAlgorithm];
                    scentMapRunDuration = new long[timesToRunAlgorithm];
                    break;

                case 3:
                    level.gridSize = 100;
                    level.obstacles = 750;
                    level.gridBorder = 99; //Used to place a border around the grid.
                    displaySize = 6; //Set the size of the displayed grid locations.
                    scentMap.maxScent = 100; //Set max scent to account for grid size.
                    player = new Player(90, 90); //Move player/target dependant on grid size
                    level.ResetLevel();
                    level.BuildLevel(player, bot);
                    switch (algorithmIndex)
                    {
                        case 1: //Dijkstra
                            timesToRunAlgorithm = 10;
                            break;
                        case 2: //AStar
                            timesToRunAlgorithm = 100;
                            break;
                        case 3: //Scent Map
                            timesToRunAlgorithm = 100;
                            break;
                    }
                    //Re-define array to correspond to the number of times run.
                    dijkstraRunDuration = new long[timesToRunAlgorithm];
                    aStarRunDuration = new long[timesToRunAlgorithm];
                    scentMapRunDuration = new long[timesToRunAlgorithm];
                    break;

                case 4:
                    level.gridSize = 150;
                    level.obstacles = 3500;
                    level.gridBorder = 149; //Used to place a border around the grid.
                    displaySize = 4; //Set the size of the displayed grid locations.
                    scentMap.maxScent = 200; //Set max scent to account for grid size.
                    player = new Player(140, 140); //Move player/target dependant on grid size
                    level.ResetLevel();
                    level.BuildLevel(player, bot);
                    switch (algorithmIndex)
                    {
                        case 1: //Dijkstra
                            timesToRunAlgorithm = 2;
                            break;
                        case 2: //AStar
                            timesToRunAlgorithm = 100;
                            break;
                        case 3: //Scent Map
                            timesToRunAlgorithm = 100;
                            break;
                    }
                    //Re-define array to correspond to the number of times run.
                    dijkstraRunDuration = new long[timesToRunAlgorithm];
                    aStarRunDuration = new long[timesToRunAlgorithm];
                    scentMapRunDuration = new long[timesToRunAlgorithm];
                    break;

                case 5:
                    level.gridSize = 250;
                    level.obstacles = 10000;
                    level.gridBorder = 249; //Used to place a border around the grid.
                    displaySize = 2; //Set the size of the displayed grid locations.
                    scentMap.maxScent = 300; //Set max scent to account for grid size.
                    player = new Player(240, 240); //Move player/target dependant on grid size
                    level.ResetLevel();
                    level.BuildLevel(player, bot);
                    switch (algorithmIndex)
                    {
                        case 1: //Dijkstra
                            timesToRunAlgorithm = 1;
                            break;
                        case 2: //AStar
                            timesToRunAlgorithm = 25;
                            break;
                        case 3: //Scent Map
                            timesToRunAlgorithm = 25;
                            break;
                    }
                    //Re-define array to correspond to the number of times run.
                    dijkstraRunDuration = new long[timesToRunAlgorithm];
                    aStarRunDuration = new long[timesToRunAlgorithm];
                    scentMapRunDuration = new long[timesToRunAlgorithm];
                    break;
            }
        }

        void RunTests(GameTime gameTime)
        {
            testingTime.Restart();
            for (int i = 1; i < 6; i++) //Cycle through each grid
            {
                Console.WriteLine("Grid: " + i);
                //Run Dijkstra
                algorithmIndex = 1;
                SelectLevel(levelIndex);
                dijkstra.Reset(level);
                HandleDijkstraTests(gameTime);
                TestComplete(dijkstraAveragesArray);

                //Run AStar
                algorithmIndex = 2;
                SelectLevel(levelIndex);
                aStar.Reset(level);
                HandleAstarTests(gameTime);
                TestComplete(aStarAveragesArray);

                //Run Scent Map
                algorithmIndex = 3;
                SelectLevel(levelIndex);
                HandleScentMapTests(gameTime);
                TestComplete(scentMapAveragesArray);

                //Prints run times to console to allow the progress of the testing to be easily followed.
                long testRunTime = testingTime.ElapsedMilliseconds;
                testRunTime = testRunTime / 1000;
                Console.WriteLine("Test run time: " + testRunTime);

                levelIndex++;
                stringIndex = 0;
            }
        }

        void HandleDijkstraTests(GameTime gameTime) //Handle Dijkstra testing sets.
        {
            for (int i = 0; i < timesToRunTest; i++)
            {
                testingTime.Restart();
                RunDijkstraTest(gameTime);
                dijkstraAveragesArray[i] = dijkstraAverage;
                //Console.WriteLine("Average runtime for the Dijkstra algorithm: " + dijkstraAverage + " ticks");
                Console.WriteLine("Dijkstra algorithm run" + i + ": " + dijkstraAverage + " ticks");
                long testRunTime = testingTime.ElapsedMilliseconds;
                testRunTime = testRunTime / 1000;
                Console.WriteLine("Test run time: " + testRunTime);
                testingTime.Reset();
                dijkstraAverage = 0;
            }
            dijkstraTestComplete = true;
            runIndex = 1; //Reset index for printing to text file.
        }

        void HandleAstarTests(GameTime gameTime) //Handle AStar testing sets.
        {
            for (int i = 0; i < timesToRunTest; i++)
            {
                testingTime.Restart();
                RunAStarTest(gameTime);
                aStarAveragesArray[i] = aStarAverage;
                Console.WriteLine("AStar algorithm run" + i + ": " + aStarAverage + " ticks");
                long testRunTime = testingTime.ElapsedMilliseconds;
                testRunTime = testRunTime / 1000;
                Console.WriteLine("Test run time: " + testRunTime);
                testingTime.Reset();
                aStarAverage = 0;
            }
            aStarTestComplete = true;
            runIndex = 1; //Reset index for printing to text file.
        }

        void HandleScentMapTests(GameTime gameTime) //Handle Scent Map testing sets.
        {
            for (int i = 0; i < timesToRunTest; i++)
            {
                testingTime.Restart();
                RunScentMap(gameTime);
                scentMapAveragesArray[i] = scentMapAverage;
                //Console.WriteLine("Average runtime for the Scent Map algorithm: " + scentMapAverage + " ticks");
                Console.WriteLine("Scent Map algorithm run" + i + ": " + scentMapAverage + " ticks");
                long testRunTime = testingTime.ElapsedMilliseconds;
                testRunTime = testRunTime / 1000;
                Console.WriteLine("Test run time: " + testRunTime);
                testingTime.Reset();
                scentMapAverage = 0;
            }
            scentMapTestComplete = true;
            runIndex = 1; //Reset index for printing to text file.
        }

        void RunDijkstraTest(GameTime gameTime) //Handle Dijkstra tests.
        {
            runAStar = false;
            runScentMap = false;
            int counter = 0;
            while (counter < timesToRunAlgorithm)
            {
                bot.gridPosition = originalBotPos;
                time.Restart();
                dijkstra.Build(level, bot, player, gameTime);
                dijkstraRunDuration[counter] = time.ElapsedTicks;
                counter++;
                time.Reset();
            }
            for (int i = 0; i < timesToRunAlgorithm; i++)
                dijkstraAverage += dijkstraRunDuration[i];
            dijkstraAverage = dijkstraAverage / timesToRunAlgorithm;
            runDijkstra = true;

            for (int i = 0; i < counter; i++)
            {
                dijkstraRunDuration[i] = 0;
            }

            //Print to text file
            dijkstraString = dijkstraAverage.ToString();
            if (rawData == false)
            {
                if (runIndex < 10)
                    lines[stringIndex] = "Dijkstra - run " + runIndex + ":  " + dijkstraString;
                else
                    lines[stringIndex] = "Dijkstra - run " + runIndex + ": " + dijkstraString;
            }
            else
                lines[stringIndex] = dijkstraString;
            stringIndex++;
            runIndex++;
        }

        void RunAStarTest(GameTime gameTime) //Handle AStar tests.
        {
            runDijkstra = false;
            runScentMap = false;
            int counter = 0;
            while (counter < timesToRunAlgorithm)
            {
                bot.gridPosition = originalBotPos;
                time.Restart();
                aStar.Build(level, bot, player, gameTime);
                aStarRunDuration[counter] = time.ElapsedTicks;
                counter++;
                time.Reset();
            }
            for (int i = 0; i < timesToRunAlgorithm; i++)
                aStarAverage += aStarRunDuration[i];
            aStarAverage = aStarAverage / timesToRunAlgorithm;
            runAStar = true;

            //Print to text file
            aStarString = aStarAverage.ToString();
            if (rawData == false)
            {
                if (runIndex < 10)
                    lines[stringIndex] = "AStar - run " + runIndex + ":  " + aStarString;
                else
                    lines[stringIndex] = "AStar - run " + runIndex + ": " + aStarString;
            }
            else
                lines[stringIndex] = aStarString;

            stringIndex++;
            runIndex++;
        }

        void RunScentMap(GameTime gameTime) //Handle Scent Map tests.
        {
            runDijkstra = false;
            runAStar = false;
            int counter = 0;
            while (counter < timesToRunAlgorithm)
            {
                bot.gridPosition = originalBotPos;
                time.Restart();
                if (scentMapFirstRun == true)
                {
                    scentMap.build(level, bot, player);
                    scentMapFirstRun = false;
                }
                scentMap.Run(level, bot, player);
                scentMapRunDuration[counter] = time.ElapsedTicks;
                counter++;
                time.Reset();
            }
            for (int i = 0; i < timesToRunAlgorithm; i++)
                scentMapAverage += scentMapRunDuration[i];
            scentMapAverage = scentMapAverage / timesToRunAlgorithm;

            bot.SetNextGridPosition(scentMap.newPosition, level);
            runScentMap = true;
            scentMapFirstRun = true;

            //Printing to text file
            scentMapString = scentMapAverage.ToString();
            if (rawData == false)
            {
                if (runIndex < 10)
                    lines[stringIndex] = "ScentMap - run " + runIndex + ":  " + scentMapString;
                else
                    lines[stringIndex] = "ScentMap - run " + runIndex + ": " + scentMapString;
            }
            else
                lines[stringIndex] = scentMapString;
            stringIndex++;
            runIndex++;
        }

        void TestComplete(long[] averagesArray) //Calculate shortest, longest  and the range of run times.
        {
            long shortest = 10000000000;
            long longest = 0;
            long range = 0;
            string newString = "";

            //Calculate the shortest and longest times.
            for (int i = 0; i < timesToRunTest; i++)
            {
                if (averagesArray[i] < shortest)
                    shortest = averagesArray[i];

                if (averagesArray[i] > longest)
                    longest = averagesArray[i];
            }

            //Convert values to strings and add to the array used to print to the text files.
            lines[stringIndex] = "";
            stringIndex++;

            newString = longest.ToString();
            lines[stringIndex] = "Longest: " + newString;
            stringIndex++;

            newString = shortest.ToString();
            lines[stringIndex] = "Shortest: " + newString;
            stringIndex++;

            range = longest - shortest;
            newString = range.ToString();
            lines[stringIndex] = "Range: " + newString;
            stringIndex++;

            int textFileIndex = levelIndex;

            if (stringIndex == 72)
                System.IO.File.WriteAllLines(@"content\Text Files\Grid" + textFileIndex + ".txt", lines);
        }
    }
}
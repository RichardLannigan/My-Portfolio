using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame8
{
    class SceneManager : DrawableGameComponent
    {
        Game game;
        Dictionary<string, Scene> scenes;
        Stack<Scene> screenStack;

        Scene activeScene;
        InputManager inputManager;//initilize values
        public static int mother = 2;
        public static int father = 1;
        public static int breedTarget = 1;
        public static int choosingM = 0;
        int parse;

        public SceneManager(Game game) : base(game)
        {
            this.game = game;
            scenes = new Dictionary<string, Scene>();
            screenStack = new Stack<Scene>();

            inputManager = game.Services.GetService(typeof(InputManager)) as InputManager;
        }

        public void AddScene(Scene scene)
        {            
            scene.LoadContent();
            scenes.Add(scene.Name, scene);
        }
        
        public override void Initialize()//load scenes initially
        {
            AddScene(new StartScreen(game, "Start"));
            AddScene(new HelpScreen(game, "Help"));
            AddScene(new ActionScreen(game, "Action"));
            AddScene(new Dayscreen(game, "end day"));
            AddScene(new ShopScreen(game, "Shop"));
            AddScene(new DebugMenu(game, "Debug"));
            AddScene(new CreatureStatus(game, "Cstatus"));
            AddScene(new BreedScene(game, "Breed"));
            AddScene(new ChooseScene(game, "Choose"));
            AddScene(new EggScene(game, "egg"));
            AddScene(new BattleScene(game, "battle"));

            foreach (var scene in scenes)
            {
                scene.Value.Hide();
            }

            scenes.TryGetValue("Start", out activeScene);
            screenStack.Push(activeScene);

            activeScene.Show();

            base.Initialize();            
        }

        

        public override void Update(GameTime gameTime)
        {
            activeScene = screenStack.Peek();
            activeScene.Update(gameTime);

            switch (activeScene.Name)
            {
                case "Start":
                    HandleStartScreen();
                    break;
                case "Help":
                    HandleHelpScreen();
                    break;
                case "Action":
                    HandleActionScreen();
                    break;
                case "end day":
                     HandleDayScreen();
                    break;
                case "Debug":
                    HandleDebugScreen();
                    break;
                case "Cstatus":
                    HandleCreatureStatus();
                    break;
                case "Breed":
                    HandleBreedScreen();
                    break;
                case "Choose":
                    HandleChooseScreen();
                    break;
                case "egg":
                    HandleEggScreen();
                    break;
                case "battle":
                    HandleBattleScreen();
                        break;
            }           

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            activeScene.Draw(gameTime);

            
        }



        private void HandleStartScreen()
        {
            if (inputManager.IsLeftButtonUp())
            {
                StartScreen sceen = activeScene as StartScreen;

                activeScene.Hide();
                switch (sceen.SelectedName)
                {
                    case "Game":
                        scenes.TryGetValue("Action", out activeScene);                                                
                        break;
                    case "Help":
                        scenes.TryGetValue("Help", out activeScene);                                               
                        break;
                    case "Shop":
                        scenes.TryGetValue("Shop", out activeScene);
                        break;
                    case "Debug":
                        scenes.TryGetValue("Debug", out activeScene);
                        break;
                    case "Exit":
                        Game.Exit();
                        break;
                }
                screenStack.Push(activeScene);
                activeScene.Show();
               
            }
        }

        private void HandleHelpScreen()
        {
            if (inputManager.IsLeftButtonUp())
            {
                HelpScreen sceen = activeScene as HelpScreen;

                if (sceen.SelectedName == "Back")
                {
                    activeScene.Hide();
                    screenStack.Pop();
                    activeScene = screenStack.Peek();
                    activeScene.Show();
                }
            }                         
        }

        private void HandleActionScreen()
        {
            if(inputManager.IsKeyUp(Keys.B))
            {
                activeScene.Hide();
                screenStack.Pop();
                activeScene = screenStack.Peek();
                activeScene.Show();
            }
        }

        private void HandleDebugScreen()
        {
            if (inputManager.IsLeftButtonUp())
            {
                DebugMenu sceen = activeScene as DebugMenu;
                activeScene.Hide();

                switch (sceen.SelectedName)

                {
                    case "Back":
                        screenStack.Pop();
                        activeScene = screenStack.Peek();
                        break;
                    case "Cstatus":
                        scenes.TryGetValue("Cstatus", out activeScene);
                        screenStack.Push(activeScene);
                        break;
                    case "Breed":
                        scenes.TryGetValue("Breed", out activeScene);
                        screenStack.Push(activeScene);
                        break;
               case "battle":
              scenes.TryGetValue("battle", out activeScene);
                        screenStack.Push(activeScene);
                    Game1.battleStatus = 0;
                    BattleScene.left = new battleWrapper(ref Game1.ranch[0]);
                    BattleScene.right = new battleWrapper(ref Game1.ranch[1]);
                    break;
                    case "end day":
                         scenes.TryGetValue("end day", out activeScene);
                        screenStack.Push(activeScene);
                        Dayscreen.EndDay();
                        break;
                }
                activeScene.Show();
            }
        }


        private void HandleCreatureStatus()
        {
            if (inputManager.IsLeftButtonUp())
            {
                CreatureStatus sceen = activeScene as CreatureStatus;
                activeScene.Hide();

                switch (sceen.SelectedName)
                {
                    case "Previous":
                        Game1.targetCreature--;
                        if (Game1.targetCreature < 1)
                            Game1.targetCreature = Game1.NoCreatures;
                        break;
                    case "Next":
                        Game1.targetCreature++;
                        if (Game1.targetCreature > Game1.NoCreatures)
                            Game1.targetCreature = 1;
                        break;
                    case "Back":
                        screenStack.Pop();
                        activeScene = screenStack.Peek();
                        break;

                }                
                activeScene.Show();               
            }
        }

        private void HandleDayScreen()
        {
            if (inputManager.IsLeftButtonUp())
            {
                Dayscreen sceen = activeScene as Dayscreen;
                activeScene.Hide();

                switch (sceen.SelectedName)
                {
                    case "Back":
                        screenStack.Pop();
                        activeScene = screenStack.Peek();
                        break;

                }
                activeScene.Show();
            }
        }

        private void HandleBreedScreen()
        {
            if (inputManager.IsLeftButtonUp())
            {
                BreedScene sceen = activeScene as BreedScene;
                activeScene.Hide();

                switch (sceen.SelectedName)
                {
                    case "Choose Mother":
                        choosingM = 0;
                        scenes.TryGetValue("Choose", out activeScene);
                        activeScene.LoadContent();
                        screenStack.Push(activeScene);
                        break;
                    case "Choose Father":
                        choosingM = 1;
                        scenes.TryGetValue("Choose", out activeScene);
                        activeScene.LoadContent();
                        screenStack.Push(activeScene);
                        break;
                    case "Begin Breeding":
                        Game1.ranch[Game1.NoCreatures].breed(Game1.ranch[mother - 1], Game1.ranch[father - 1]);
                        Game1.NoCreatures++;
                        screenStack.Pop();
                        activeScene = screenStack.Peek();
                        break;
                    case "Back":
                        screenStack.Pop();
                        activeScene = screenStack.Peek();
                        break;
                }
                
                activeScene.Show(); 
            }
        }

        private void HandleChooseScreen()
        {
            if (inputManager.IsLeftButtonUp())
            {
                ChooseScene sceen = activeScene as ChooseScene;
                activeScene.Hide();
                string selectedButton = sceen.SelectedName;

                switch (selectedButton)
                {
                    case "Back":
                        screenStack.Pop();
                        activeScene = screenStack.Peek();
                        break;
                    default:

                        if (int.TryParse(selectedButton, out parse))//if int,make appropriate target the selected int
                        {
                            if (choosingM == 0)
                            {
                                mother = parse;
                                Game1.targetCreature = parse;
                            }
                            else if (choosingM == 1)
                            {
                                father = parse;
                                Game1.targetCreature = parse;
                            }
                            else
                            {
                                breedTarget = parse;
                                Game1.targetCreature = parse;
                            }                            
                        }
                        break;
                }
                activeScene.Show(); 
            }
        }




        private void HandleEggScreen()//WIP
        {
            if (inputManager.IsLeftButtonUp())
            {
                EggScene sceen = activeScene as EggScene;
                activeScene.Hide();

                string selectedButton = sceen.SelectedName;
                switch (selectedButton)
                {
                    case "Back":
                        screenStack.Pop();
                        activeScene = screenStack.Peek();
                        break;
                    default:

                        if (int.TryParse(selectedButton, out parse))
                        {
                            if (choosingM == 0)
                            {
                                mother = parse;
                                Game1.targetCreature = parse;
                            }
                            else if (choosingM == 1)
                            {
                                father = parse;
                                Game1.targetCreature = parse;
                            }
                            else
                            {
                                breedTarget = parse;
                                Game1.targetCreature = parse;
                            }
                        }
                        break;

                }
                activeScene.Show();
            }

        }


        private void HandleBattleScreen()
        {
            if (inputManager.IsLeftButtonUp())
            {//TODO clean battletime here
                BattleScene sceen = activeScene as BattleScene;
                activeScene.Hide();
                string selection = sceen.SelectedName;
                if (selection == "back")//back
                {
                    screenStack.Pop();
                    activeScene = screenStack.Peek();
                }
                activeScene.Show();
            }
        }

    }
}

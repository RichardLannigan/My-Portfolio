#define LOG //Uncomment to enable battle log.
using System;
using System.Collections;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame8
{


    class BattleScene : Scene
    {
        public static battleWrapper left;
        public static battleWrapper right;
        double turntimer;
        TimeSpan playerTimer;
        TimeSpan enemyTimer;
        TimeSpan fireBallRightTimer;
        TimeSpan fireBallLeftTimer;
        TimeSpan playerJumpTimer;
        TimeSpan enemyJumpTimer;
        public static int leftUsed;
        public static int rightUsed;
        SpriteBatch spriteBatch;
        int enemyYPos;
        bool firstRun = true;
        bool actualFirstRun = true;
        bool battleEnd = false;
        bool playerFireball;
        bool enemyFireball;
        bool enemyJumpUp;
        double maxHealthLeft = 0;
        double maxHealthRight = 0;
        int numBattles = 0;
        bool leftWin = false;
        bool rightWin = false;
        int cumLeft = 0;
        int cumRight = 0;
        int zero = 0;
        public static string fileName;

        //Battle Screen
        Texture2D arenaScreen;
        Texture2D worldScreen;
        Texture2D playerCreature;
        Rectangle playerCreatureRectangle;
        Texture2D enemyCreature;
        Rectangle enemyCreatureRectangle;
        Texture2D fireBall;
        Rectangle fireBallRectangle;

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
        public BattleScene(Game game, string name)
        {
            spriteBatch = game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            this.game = game;
            this.name = name;
        }

        public override void Update(GameTime gameTime)
        {
            playerTimer += gameTime.ElapsedGameTime;
            enemyTimer += gameTime.ElapsedGameTime;
            fireBallLeftTimer += gameTime.ElapsedGameTime;
            fireBallRightTimer += gameTime.ElapsedGameTime;
            enemyJumpTimer += gameTime.ElapsedGameTime;
            //if (playerTimer > TimeSpan.FromSeconds(6))
            //    playerTimer = TimeSpan.Zero;

            if (enemyCreatureRectangle.Y > 500)
            {
                if (fireBallRectangle.X > enemyCreatureRectangle.X - fireBallRectangle.Width / 2 - 10 && fireBallRectangle.X < enemyCreatureRectangle.X + fireBallRectangle.Width)
                {
                    fireBallRectangle.X = playerCreatureRectangle.X + playerCreatureRectangle.Width;
                    playerTimer = TimeSpan.Zero;
                    playerFireball = false;
                    //fireBallRightTimer = 0;
                }
            }
            else if (fireBallRectangle.X > 1280)
            {
                    fireBallRectangle.X = playerCreatureRectangle.X + playerCreatureRectangle.Width;
                    playerTimer = TimeSpan.Zero;
                    playerFireball = false;
            }
            if (playerFireball == true)
                    fireBallRectangle.X += 3;

            if (!leftWin && !rightWin)
            {
                if (actualFirstRun)
                {
#if LOG
                    fileName = DateTime.Now.ToString("dd-MM-yy HH mm ss -- ") + left.fighter.BST + " vs " + right.fighter.BST + ".txt";
                    using (StreamWriter testLog = new StreamWriter(fileName, true))
                    {
                        testLog.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        testLog.WriteLine("~~~~ {0} Creature Fight ~~~~\n", DateTime.Now.ToString());
                        testLog.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        testLog.WriteLine(Environment.NewLine + "~~ Left Creature ~~" + Environment.NewLine + " Age: {0}  Amphibious: {1}  Aggressive: {2}  atkPerf: {3}  Bird: {4}  BLO: {5}  BST: {6}  Bulk: {7}  CON: {8}  "
                            + Environment.NewLine + "Courage: {9}  curStam: {10}  Cuteness: {11}  DEF: {12}  defPerf: {13}  Effort: {14}  eggHappy: {15}  Feet: {16}  Fickle: {17}  Happiness: {18}  "
                            + Environment.NewLine + "happyRate: {19}  HP: {20}  Insect: {21}  Intelligence: {22}  Lifespan: {23}  Limb: {24}  Litter: {25}  Mammal: {26}  maxAge: {27}  maxStam: {28}  "
                            + Environment.NewLine + "metabolism: {29}  Obediance: {30}  Reactions: {31}  Reptile: {32}  restRate: {33}  SPD: {34}  Spoilage: {35}  spoilRate: {36}  Stamina: {37}  "
                            + Environment.NewLine + "stamRate: {38}  Stance: {39}  STK: {40}  STR: {41}  Tallness: {42}  Tech: {43}  Thickness: {44}  Vanity: {45}  Wings: {46}  " + Environment.NewLine
                            + Environment.NewLine + "BLO: {47}   \n HP: {48}    DEF: {49}   CON: {50}   SPD: {51}   STR: {52}   STK: {53}",
                            left.fighter.age, left.fighter.aggressive.total, left.fighter.amphibious.total, left.fighter.atkPerf.total, left.fighter.bird.total,
                            left.fighter.BLO.total, left.fighter.BST, left.fighter.bulk.total, left.fighter.CON.total, left.fighter.courage.total, left.fighter.curStam,
                            left.fighter.cuteness.total, left.fighter.DEF.total, left.fighter.defPerf.total, zero, left.fighter.eggHappy.total, left.fighter.feet.total,
                            left.fighter.fickle.total, left.fighter.happiness, left.fighter.happyRate, left.fighter.HP.total, left.fighter.insect.total, zero,
                            left.fighter.lifespan.total, left.fighter.limb.total, left.fighter.litter.total, left.fighter.mammal.total, left.fighter.maxAge, left.fighter.maxStam,
                            left.fighter.metabolism.total, zero, zero, left.fighter.reptile.total, left.fighter.restRate, left.fighter.SPD.total,
                            left.fighter.spoilage, left.fighter.spoilRate, left.fighter.stamina.total, left.fighter.stamRate, left.fighter.stance.total, left.fighter.STK.total,
                            left.fighter.STR.total, left.fighter.tallness.total, left.fighter.tech.total, left.fighter.thickness.total, left.fighter.vanity.total, left.fighter.wings.total, left.fighter.BLO.total, left.fighter.HP.total,
                            left.fighter.DEF.total, left.fighter.CON.total, left.fighter.SPD.total, left.fighter.STR.total, left.fighter.STK.total);
                        testLog.WriteLine(Environment.NewLine + "~~ Right Creature ~~" + Environment.NewLine + " Age: {0}  Amphibious: {1}  Aggressive: {2}  atkPerf: {3}  Bird: {4}  BLO: {5}  BST: {6}  Bulk: {7}  CON: {8}  "
                            + Environment.NewLine + "Courage: {9}  curStam: {10}  Cuteness: {11}  DEF: {12}  defPerf: {13}  Effort: {14}  eggHappy: {15}  Feet: {16}  Fickle: {17}  Happiness: {18}  "
                            + Environment.NewLine + "happyRate: {19}  HP: {20}  Insect: {21}  Intelligence: {22}  Lifespan: {23}  Limb: {24}  Litter: {25}  Mammal: {26}  maxAge: {27}  maxStam: {28}  "
                            + Environment.NewLine + "metabolism: {29}  Obediance: {30}  Reactions: {31}  Reptile: {32}  restRate: {33}  SPD: {34}  Spoilage: {35}  spoilRate: {36}  Stamina: {37}  "
                            + Environment.NewLine + "stamRate: {38}  Stance: {39}  STK: {40}  STR: {41}  Tallness: {42}  Tech: {43}  Thickness: {44}  Vanity: {45}  Wings: {46}" + Environment.NewLine
                            + Environment.NewLine + "BLO: {47}    HP: {48}    DEF: {49}   CON: {50}   SPD: {51}   STR: {52}   STK: {53}" + Environment.NewLine + Environment.NewLine,
                            right.fighter.age, right.fighter.aggressive.total, right.fighter.amphibious.total, right.fighter.atkPerf.total, right.fighter.bird.total,
                            right.fighter.BLO.total, right.fighter.BST, right.fighter.bulk.total, right.fighter.CON.total, right.fighter.courage.total, right.fighter.curStam,
                            right.fighter.cuteness.total, right.fighter.DEF.total, right.fighter.defPerf.total, zero, right.fighter.eggHappy.total, right.fighter.feet.total,
                            right.fighter.fickle.total, right.fighter.happiness, right.fighter.happyRate, right.fighter.HP.total, right.fighter.insect.total, zero,
                            right.fighter.lifespan.total, right.fighter.limb.total, right.fighter.litter.total, right.fighter.mammal.total, right.fighter.maxAge, right.fighter.maxStam,
                            right.fighter.metabolism.total, zero, zero, right.fighter.reptile.total, right.fighter.restRate, right.fighter.SPD.total,
                            right.fighter.spoilage, right.fighter.spoilRate, right.fighter.stamina.total, right.fighter.stamRate, right.fighter.stance.total, right.fighter.STK.total,
                            right.fighter.STR.total, right.fighter.tallness.total, right.fighter.tech.total, right.fighter.thickness.total, right.fighter.vanity.total, right.fighter.wings.total, right.fighter.BLO.total, right.fighter.HP.total,
                            right.fighter.DEF.total, right.fighter.CON.total, right.fighter.SPD.total, right.fighter.STR.total, right.fighter.STK.total);
                    }

#endif
                    actualFirstRun = false;

                }

                if (firstRun)
                {
                    maxHealthLeft = left.fighter.HP.total;
                    maxHealthRight = right.fighter.HP.total;
                    firstRun = false;
                }


                if (Game1.battleStatus == 0)
                {
                    turntimer += gameTime.ElapsedGameTime.TotalMilliseconds;
                    while (turntimer >= 12)
                    {
                        battle.turnIncrement(left, right);
                        turntimer -= 12;
                        if (left.HP <= 0)
                        {
                            left.HP = maxHealthLeft;
                            right.HP = maxHealthRight;
                            firstRun = true;
                            rightWin = true;
                            battleEnd = true;
#if LOG
                            numBattles++;
                           // rightWin++;
                            if (numBattles < 101)
                            {
                                using (StreamWriter testLog = new StreamWriter(fileName, true))
                                    testLog.WriteLine("{0}: Right Creature Wins " + DateTime.Now.ToString("(HH:mm:s.fff)"), numBattles);
                            }
                            if (numBattles == 100)
                            {
                                //numBattles = 0;
                                //cumLeft += leftWin;
                                //cumRight += rightWin;
                                using (StreamWriter testLog = new StreamWriter(fileName, true))
                                {
                                    testLog.WriteLine(Environment.NewLine + "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~" + Environment.NewLine + "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~" +
                                        Environment.NewLine + "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~" + Environment.NewLine + "~~~~~~~~ Left wins: {0}% ~~~~~~~~~" + Environment.NewLine +
                                        "~~~~~~~ Right wins: {1}% ~~~~~~~~~" + Environment.NewLine + "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~" + Environment.NewLine + "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~" +
                                        Environment.NewLine + "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~" + Environment.NewLine, leftWin, rightWin);
                                    testLog.WriteLine(Environment.NewLine + "Cumulative left: {0}" + Environment.NewLine + "Cumulative right: {1}" + Environment.NewLine + Environment.NewLine +
                                        Environment.NewLine, cumLeft, cumRight);
                                }
                                //leftWin = 0;
                                //rightWin = 0;
                            }
#endif

                        }
                        //Game1.battleStatus = 2;
                        else if (right.HP <= 0)
                        {
                            left.HP = maxHealthLeft;
                            right.HP = maxHealthRight;
                            firstRun = true;
                            leftWin = true;
                            battleEnd = true;
#if LOG
                            numBattles++;
                            //leftWin++;
                            if (numBattles < 101)
                            {
                                using (StreamWriter testLog = new StreamWriter(fileName, true))
                                    testLog.WriteLine("{0}: Left Creature Wins " + DateTime.Now.ToString("(HH:mm:s.fff)"), numBattles);
                            }
                            if (numBattles == 100)
                            {
                                numBattles = 0;
                                //cumLeft += leftWin;
                                //cumRight += rightWin;
                                using (StreamWriter testLog = new StreamWriter(fileName, true))
                                {
                                    testLog.WriteLine(Environment.NewLine + "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~" + Environment.NewLine + "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~" +
                                                                            Environment.NewLine + "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~" + Environment.NewLine + "~~~~~~~~ Left wins: {0}% ~~~~~~~~~" + Environment.NewLine +
                                                                            "~~~~~~~ Right wins: {1}% ~~~~~~~~~" + Environment.NewLine + "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~" + Environment.NewLine + "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~" +
                                                                            Environment.NewLine + "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~", leftWin, rightWin);
                                    testLog.WriteLine(Environment.NewLine + "Cumulative left: {0}" + Environment.NewLine + "Cumulative right: {1}" + Environment.NewLine + Environment.NewLine +
                                        Environment.NewLine, cumLeft, cumRight);
                                }
                                //leftWin = 0;
                                //rightWin = 0;
                            }
#endif

                        }
                        //Game1.battleStatus = 1; 
                    }
                }
            }
            base.Update(gameTime);
        }
        public override void LoadContent()
        {
            Button tempButton;

            SceneComponents.Add(new BackgroundComponent(game, game.Content.Load<Texture2D>(@"Backgrounds\status")));
            playerCreatureRectangle = new Rectangle(100, 400, 270, 270);
            enemyCreatureRectangle = new Rectangle(850, 525, 275, 150);

            fireBallRectangle = new Rectangle(playerCreatureRectangle.X + playerCreatureRectangle.Width, playerCreatureRectangle.Y + 150, 100, 80);
            arenaScreen = game.Content.Load<Texture2D>("BattleScreens/Battle_Screen_Arena");
            worldScreen = game.Content.Load<Texture2D>("BattleScreens/Battle_Screen_World");
            playerCreature = game.Content.Load<Texture2D>("Creatures/playerCreature");
            enemyCreature = game.Content.Load<Texture2D>("Creatures/enemy");
            fireBall = game.Content.Load<Texture2D>("BattleScreens/FireBall");

            enemyYPos = enemyCreatureRectangle.Y;
            string[] menuItems = { "back" };
            for (int count = 0; count < menuItems.Length; count++)
            {
                tempButton = new Button(game,
                                        menuItems[count],
                                        new Vector2(game.Window.ClientBounds.Width / 2, game.Window.ClientBounds.Height - (count * 80 + 80)),
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
            bool flag = false;
            if (!flag)
            {
                base.Draw(gameTime);
                //string[] input = { "left HP", Convert.ToString((int)left.HP), "Right HP", Convert.ToString((int)right.HP) };
                //battle.list(game.Content.Load<SpriteFont>(@"Fonts\menufont"), new Vector2(100, 100), new Vector2(150, 0), input, Color.White);
                //input = new string[4] { "left Timer", Convert.ToString((int)left.charge), "Right timer", Convert.ToString((int)right.charge) };
                //battle.list(game.Content.Load<SpriteFont>(@"Fonts\menufont"), new Vector2(100, 200), new Vector2(150, 0), input, Color.White);
                //input = new string[4] { "left useing", "theres a bug", "Right useing", "this is buggy" };
                //if (left.Using == 0)
                //    input[1] = "strike";
                //else if (left.Using == 1)
                //    input[1] = "blow";
                //else if (left.Using == 2)
                //    input[1] = "block";
                //else if (left.Using == 3)
                //    input[1] = "brace";
                //else
                //    input[1] = "waiting";
                //if (right.Using == 0)
                //    input[3] = "strike";
                //else if (right.Using == 1)
                //    input[3] = "blow";
                //else if (right.Using == 2)
                //    input[3] = "block";
                //else if (right.Using == 3)
                //    input[3] = "brace";
                //else
                //    input[3] = "waiting";
                //battle.list(game.Content.Load<SpriteFont>(@"Fonts\menufont"), new Vector2(100, 300), new Vector2(150, 0), input, Color.White);
                spriteBatch.Draw(arenaScreen, new Vector2(0, 0), Color.White);
                spriteBatch.Draw(playerCreature, playerCreatureRectangle, Color.White);
                spriteBatch.Draw(enemyCreature, enemyCreatureRectangle, Color.White);
                //if (left.Using == 0)
                if (playerTimer > TimeSpan.FromSeconds(3.5))
                {
                    playerFireball = true;
                    spriteBatch.Draw(fireBall, fireBallRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                }
                if (enemyJumpTimer > TimeSpan.FromSeconds(3))
                {
                    enemyJumpUp = true;
                    if (enemyJumpUp)
                        enemyCreatureRectangle.Y -= 3;
                    else if (!enemyJumpUp)
                    {
                        enemyCreatureRectangle.Y += 3;
                        if (enemyJumpTimer > TimeSpan.FromSeconds(3) && enemyCreatureRectangle.Y == enemyYPos)
                            enemyJumpUp = true;
                    }
                }
                    
                        
            }
            spriteBatch.End();
        }
    }

}

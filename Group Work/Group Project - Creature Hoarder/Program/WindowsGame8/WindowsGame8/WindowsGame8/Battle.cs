#define LOG //Uncomment to add moves to battle log 
              //(will crash if BattleScene.cs LOG is commented)
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
using System.IO;

namespace WindowsGame8
{
    public class battleWrapper
    {
        public double HP;
        public double charge;
        public int Using;
        public creature fighter;
        public battleWrapper(ref creature player)
        {
            HP = player.HP.total;
            charge = 250;
            Using = -1;
            fighter = player;
        }
    } 
    static class battle
    {
        public static double UnNeg(double input)
        {
            if (input < 0)
                input = 0;
            return input;
        }
        public static void list(SpriteFont font, Vector2 position, Vector2 offset, string[] entries, Color textC)
        {
            for (int count = 0; count < entries.Length; count++)
            {
                Game1.spriteBatch.DrawString(font, entries[count], position, textC);
                position += offset;
            }
        }
        public static void turnIncrement(battleWrapper player, battleWrapper opp)//run 20 times a seconed, so turns take between 0.5 and 5 seconds
        {
            player.charge -= 12 + (player.fighter.SPD.total / 2);
            opp.charge -= 12 + (opp.fighter.SPD.total/2);
            if (player.charge <= 0 && opp.charge <= 0)
            {
                if (player.charge < opp.charge)
                {
                    Action(ref player,ref opp);
                    Action(ref opp, ref player);
                }
                else
                {
                    Action(ref opp,ref player);
                    Action(ref player, ref opp);
                }
            }
            else if (player.charge <= 0)
            {
                Action(ref player, ref opp);
            }
            else if (opp.charge <= 0)
            {
                Action(ref opp, ref player);
            }
        }
        public static int AI(ref battleWrapper actor)
        {
            int idea = Game1.rand.Next(1, 100);//roll a d100
            bool attacking;
            //if (actor.HP > actor.HP / 2)//if high health, low courage increses atk chance
            //{
            //    if ((((actor.fighter.aggressive.total/2) + -(actor.fighter.courage.total - 50)))/5 * 4 + 20 >= idea )
            //        attacking = true;
            //    else
            //        attacking = false;
            //}
            //else
            //{
                if ((((actor.fighter.aggressive.total / 2) 
                    + 25//aggresion range 25 to 75
                    + ((actor.fighter.courage.total - 50)* ((actor.HP / actor.fighter.HP.total)-0.5))))//+ courage range -25 to 25
                    / 10 * 9 + 10 >= idea)//place into 0 - 80 range , if less then rand(1,100) attack otherwise defend
                    attacking = true;
                else
                    attacking = false;
            //}
            idea = Game1.rand.Next(1, 100);
            if (attacking == true)
            {
                if (actor.fighter.atkPerf.total > idea)
                {
#if LOG
                    using (StreamWriter testLog = new StreamWriter(BattleScene.fileName, true))
                    {
                        testLog.WriteLine("{0}: uses Strike", actor.fighter.BST);
                    }
#endif
                    return 0;//STK
                }
                else
                {
#if LOG
                    using (StreamWriter testLog = new StreamWriter(BattleScene.fileName, true))
                    {
                        testLog.WriteLine("{0}: uses Blow", actor.fighter.BST);
                    }
#endif
                    return 1; //BLO
                }
            }
            else
            {
                if (actor.fighter.defPerf.total > idea)
                {
#if LOG
                    using (StreamWriter testLog = new StreamWriter(BattleScene.fileName, true))
                    {
                        testLog.WriteLine("{0}: uses Block", actor.fighter.BST);
                    }
#endif
                    return 2;//DEF
                }
                else
                {
#if LOG
                    using (StreamWriter testLog = new StreamWriter(BattleScene.fileName, true))
                    {
                        testLog.WriteLine("{0}: uses Brace", actor.fighter.BST);
                    }
#endif
                    return 3; //brace
                }
            }
        }
        public static void Strike(ref battleWrapper attacker, ref battleWrapper defender)
        {
            if (defender.Using == 2)
            {
                double damage =(attacker.fighter.STR.total + (attacker.fighter.STK.total * 1.5) + (attacker.fighter.BLO.total * 0.5)) - ((defender.fighter.DEF.total * 2) + (defender.fighter.CON.total));
                if (damage < 0)
                {
                    double counter = damage / (attacker.fighter.STR.total + (attacker.fighter.STK.total * 1.5) + (attacker.fighter.BLO.total * 0.5));
                    attacker.HP -= UnNeg((((defender.fighter.STR.total + defender.fighter.BLO.total + defender.fighter.STK.total)) - ((attacker.fighter.DEF.total) + (attacker.fighter.CON.total))) * (counter));
                }
                else
                    defender.HP -= UnNeg(damage);
                attacker.HP -= UnNeg(((defender.fighter.STR.total + defender.fighter.BLO.total + defender.fighter.STK.total)) - ((attacker.fighter.DEF.total) + (attacker.fighter.CON.total)));

            }
            else
            {
                defender.HP -= UnNeg((attacker.fighter.STR.total + (attacker.fighter.STK.total * 1.5) + (attacker.fighter.BLO.total * 0.5)) - ((defender.fighter.DEF.total) + (defender.fighter.CON.total)));
            }
            attacker.Using = -1;
            attacker.charge = 1500;

        }
        public static void Blow(ref battleWrapper attacker, ref battleWrapper defender)
        {
            if (defender.Using == 3)
            {
                defender.HP -= UnNeg(((attacker.fighter.STR.total * 1.33) + (attacker.fighter.BLO.total * 1.66) + attacker.fighter.STK.total) - ((defender.fighter.DEF.total * 1) + (defender.fighter.CON.total * 2)));
                attacker.HP -= UnNeg(((defender.fighter.STR.total + defender.fighter.BLO.total + defender.fighter.STK.total)) - ((attacker.fighter.DEF.total) + (attacker.fighter.CON.total)));
            }
            else if (defender.Using == 2)
            {
                defender.HP -= UnNeg(((attacker.fighter.STR.total * 1.33) + (attacker.fighter.BLO.total * 1.66) + attacker.fighter.STK.total) - ((defender.fighter.DEF.total * 2) + (defender.fighter.CON.total)));
                defender.Using = -1;
                defender.charge += 3000;
            }
            else
                defender.HP -= UnNeg(((attacker.fighter.STR.total * 1.33) + (attacker.fighter.BLO.total * 1.66) + attacker.fighter.STK.total) - ((defender.fighter.DEF.total) + (defender.fighter.CON.total)));
            attacker.Using = -1;
            attacker.charge = 4500;
        }
        public static void Block(ref battleWrapper blocker)
        {

            blocker.Using = -1;
            Action(ref blocker, ref blocker);
        }
        public static void Brace(ref battleWrapper blocker)
        {
            blocker.Using = -1;
            Action(ref blocker, ref blocker);
        }
        public static void Action(ref battleWrapper A, ref battleWrapper B)
        {
            if (A.Using == 0)
            {

                Strike(ref A, ref B);
            }
            else if (A.Using == 1)
            {

                Blow(ref A, ref B);
            }
            else if (A.Using == 2)
            {

                Block(ref A);
            }
            else if (A.Using == 3)
            {

                    Brace(ref A);
            }
            else
            {
                int decision = AI(ref A);
                A.Using = decision;
                if (decision == 0 || decision == 1)
                    A.charge = 1500;
                else
                    A.charge = 3000;
            }

        }
    }
}


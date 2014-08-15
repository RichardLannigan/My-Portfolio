﻿using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using System.Collections;

namespace WindowsGame8//stores the creature and stat definitions
{
    public abstract class stat//a generic stat class for inheritance
    {
        public double trained;//how much that stat has been trained
        public double baseStat;//The creatures starting stat in that area
        public double total;//the creatures total stat, for ease of acsess
        public double bonus;//any temporary bonuses,such as from equiptment
        public abstract void breed(stat mother,stat father);//blank classes shared amome all stats, deffined in the stat-type classes, generates stat through breeding.
        public abstract void generate(int amount);//generates stat randomly
        public abstract double sum();//adds up stat
        public abstract void clear();//clears creature
        //public creatureEnu GetEnumeratior()
        //{
        //    return new creatureEnu(this);
        //}
        //public stat()
        //{
        //    trained = 0.1;
        //    baseStat = 0.1;
        //    total = 0.1;
        //    bonus = 0.1;
        //}
    }
    public class growing : stat//for battle stats, 1's dominent, 300 long
    {
       public bool[] A = new bool[300];//arrays that hold genes
       public bool[] B = new bool[300];

       public override void breed(stat Mother, stat Father)
        {
            growing mother = (growing)Mother;
            growing father = (growing)Father;
            clear();
            for (int count = 0; count < 300; count++)
            {
                if (Game1.rand.Next(0, 1) == 1 ^ Game1.rand.Next(1, 200) == 1)
                    A[count] = mother.A[count];
                else
                    A[count] = mother.B[count];

                if (Game1.rand.Next(0, 1) == 1 ^ Game1.rand.Next(1, 200) == 1)
                    B[count] = father.A[count];
                else
                    B[count] = father.B[count];
            }
        }
        public override void generate(int amount)
        {
            clear();
             if (amount < 300)
            {
            for (int count = 0; count < amount; count++)
            {
                int result = Game1.rand.Next(0,600);
                if (result < 300)
                {
                    if (A[result] == false)
                        A[result] = true;
                    else
                        count--;
                }
                else
                {
                    result -= 300;
                    if (B[result] == false)
                        B[result] = true;
                    else
                        count--;
                }

            }
            }
             else
             {
                 for (int count = 0; count < 300; count++)
                 {
                     A[count] = true;
                     B[count] = true;
                 }
                 amount = 600 - amount;
                 for (int count = 0; count < amount; count++)
                 {
                     int result = Game1.rand.Next(0, 600);
                     if (result < 300)
                     {
                         if (A[result] == true)
                             A[result] = false;
                         else
                             count--;
                     }
                     else
                     {
                         result -= 300;
                         if (B[result] == true)
                             B[result] = false;
                         else
                             count--;
                     }
                 }
             }
        }
        public override double sum()
        {
            double result = 0;
            for (int count = 0; count < 300; count++)
            {
                if (A[count] == true || B[count] == true)
                    result++;
            }
            return result + 20;
        }
        public override void clear()
        {
            for (int count = 0; count < 300; count++)
            {
                A[count] = false;
                B[count] = false;
            }
        }

    }
    public class HP : growing//for HP, same as growing but multiplys sums result by 5 depcrited in trade for multiplying the total
    {
        public override double sum()
        {
            double result = 0;
            for (int count = 0; count < 300; count++)
            {
                if (A[count] == true || B[count] == true)
                    result++;
            }
            return (result + 20)*Game1.HPmultiplyer;
        }

    }
    public class spect100 : stat//100 long, spectrum co-dominent. use for stats that need alot on increments. max 200
    {
        bool[] A = new bool[100];
        bool[] B = new bool[100];

        public override void breed(stat Mother, stat Father)
        {
            spect100 mother = (spect100)Mother;
            spect100 father = (spect100)Father;
            clear();
            for (int count = 0; count < 100; count++)
            {
                if (Game1.rand.Next(0, 1) == 1 ^ Game1.rand.Next(1,200) == 1)
                    A[count] = mother.A[count];
                else
                    A[count] = mother.B[count];

                if (Game1.rand.Next(0, 1) == 1 ^ Game1.rand.Next(1, 200) == 1)
                    B[count] = father.A[count];
                else
                    B[count] = father.B[count];
            }
        }
        public override void generate(int amount)
        {
            clear();
             if (amount < 100)
            {
            for (int count = 0; count < amount; count++)
            {
                int result = Game1.rand.Next(0, 200);
                if (result < 100)
                {
                    if (A[result] == false)
                        A[result] = true;
                    else
                        count--;
                }
                else
                {
                    result -= 100;
                    if (B[result] == false)
                        B[result] = true;
                    else
                        count--;
                }

            }
                 }
            else
            {
                for (int count = 0; count < 100;count++){
                    A[count] = true;
                    B[count] = true;
                }
                amount = 200 - amount;
                for (int count = 0; count < amount; count++)
                {
                    int result = Game1.rand.Next(0, 200);
                    if (result < 100)
                    {
                        if (A[result] == true)
                            A[result] = false;
                        else
                            count--;
                    }
                    else
                    {
                        result -= 100;
                        if (B[result] == true)
                            B[result] = false;
                        else
                            count--;
                    }
                }
            }
        }
        public override double sum()
        {
            double result = 0;
            for (int count = 0; count < 100; count++)
            {
                if (A[count] == true)
                    result++;
                if (B[count] == true)
                    result++;
            }
            return result+1;
        }
        public override void clear()
        {
            for (int count = 0; count < 100; count++)
            {
                A[count] = false;
                B[count] = false;
            }
        }
    }
    public class spect50 : stat//50 long, spectrum co-dominent. use for stats that need to set by 50 or 100. max 100
    {
        bool[] A = new bool[50];
        bool[] B = new bool[50];

        public override void breed(stat Mother, stat Father)
        {
            clear();
            spect50 mother = (spect50)Mother;
            spect50 father = (spect50)Father;
            for (int count = 0; count < 50; count++)
            {
                if (Game1.rand.Next(0, 1) == 1 ^ Game1.rand.Next(1, 200) == 1)
                    A[count] = mother.A[count];
                else
                    A[count] = mother.B[count];

                if (Game1.rand.Next(0, 1) == 1 ^ Game1.rand.Next(1, 200) == 1)
                    B[count] = father.A[count];
                else
                    B[count] = father.B[count];
            }
        }
        public override void generate(int amount)
        {
            clear();
             if (amount < 50)
            {
            for (int count = 0; count < amount; count++)
            {
                int result = Game1.rand.Next(0,100);
                if (result < 50)
                {
                    if (A[result] == false)
                        A[result] = true;
                    else
                        count--;
                }
                else
                {
                    result -= 50;
                    if (B[result] == false)
                        B[result] = true;
                    else
                        count--;
                }

            }
                 }
            else
            {
                for (int count = 0; count < 50;count++){
                    A[count] = true;
                    B[count] = true;
                }
                amount = 100 - amount;
                for (int count = 0; count < amount; count++)
                {
                    int result = Game1.rand.Next(0, 100);
                    if (result < 50)
                    {
                        if (A[result] == true)
                            A[result] = false;
                        else
                            count--;
                    }
                    else
                    {
                        result -= 50;
                        if (B[result] == true)
                            B[result] = false;
                        else
                            count--;
                    }
                }
            }
        }
        public override double sum()
        {
            double result = 0;
            for (int count = 0; count < 50; count++)
            {
                if (A[count] == true)
                    result++;
                if (B[count] == true)
                    result++;
            }
            return result+1;
        }
        public override void clear()
        {
            for (int count = 0; count < 50; count++)
            {
                A[count] = false;
                B[count] = false;
            }
        }
    }

    public class spect30 : stat//30 long, spectrum co-dominent. use for most stats. max 60
    {
        bool[] A = new bool[30];
        bool[] B = new bool[30];

        public override void breed(stat Mother, stat Father)
        {
            clear();
            spect30 mother = (spect30)Mother;
            spect30 father = (spect30)Father;
            for (int count = 0; count < 30; count++)
            {
                if (Game1.rand.Next(0, 1) == 1 ^ Game1.rand.Next(1, 200) == 1)
                    A[count] = mother.A[count];
                else
                    A[count] = mother.B[count];

                if (Game1.rand.Next(0, 1) == 1 ^ Game1.rand.Next(1, 200) == 1)
                    B[count] = father.A[count];
                else
                    B[count] = father.B[count];
            }
        }
        public override void generate(int amount)
        {
            clear();
            if (amount < 30)
            {
                for (int count = 0; count < amount; count++)
                {
                    int result = Game1.rand.Next(0, 60);
                    if (result < 30)
                    {
                        if (A[result] == false)
                            A[result] = true;
                        else
                            count--;
                    }
                    else
                    {
                        result -= 30;
                        if (B[result] == false)
                            B[result] = true;
                        else
                            count--;
                    }
                }

            }
            else
            {
                for (int count = 0; count < 30;count++){
                    A[count] = true;
                    B[count] = true;
                }
                amount = 60 - amount;
                for (int count = 0; count < amount; count++)
                {
                    int result = Game1.rand.Next(0, 60);
                    if (result < 30)
                    {
                        if (A[result] == true)
                            A[result] = false;
                        else
                            count--;
                    }
                    else
                    {
                        result -= 30;
                        if (B[result] == true)
                            B[result] = false;
                        else
                            count--;
                    }
                }
            }
        }
        public override void clear()
        {
            for (int count = 0; count < 30; count++)
            {
                A[count] = false;
                B[count] = false;
            }
        }
        public override double sum()
        {
            double result = 0;
            for (int count = 0; count < 30; count++)
            {
                if (A[count] == true)
                    result++;
                if (B[count] == true)
                    result++;
            }
            return result+1;
        }
    }
    //public class creatureEnu
    //{
    //    private int position = -1;
    //    private stat t;

    //    public creatureEnu(stat t)
    //    {
    //        this.t = t;
    //    }

    //    // The IEnumerator interface requires a MoveNext method. 
    //    public bool MoveNext()
    //    {
    //        if (position < t.elements.Length - 1)
    //        {
    //            position++;
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    // The IEnumerator interface requires a Reset method. 
    //    public void Reset()
    //    {
    //        position = -1;
    //    }

    //    // The IEnumerator interface requires a Current method. 
    //    public object Current
    //    {
    //        get
    //        {
    //            return t.elements[position];
    //        }
    //    }
    //}
    public class creature //holds the creature data
    {
       public growing STR = new growing();//creatures attack strength, ups all attacks equally
       public growing HP = new HP();//creatures max HP
       public growing SPD = new growing();//how quickly the creatures turns arrive
       public growing STK = new growing();//how much damage the creature does with strikes
       public growing BLO = new growing();//how strong the creatures blows are
       public growing DEF = new growing();//the creatures resistence to strikes and ability to block
       public growing CON = new growing();//the creatures resitence to blows and ability to brace
       public spect50 aggressive = new spect50();//how likly the creature is to attack
       public spect50 courage = new spect50();//how being low on health affects the creature behavior
       public spect30 mammal = new spect30();//how mammilien the creature looks, affects STR 
       public spect30 reptile = new spect30();//how reptilien the creature is, affects HP
       public spect30 bird = new spect30();//hoe birdlike the creature is, effects STK
       public spect30 wings = new spect30();//how prominant the cratures wings are, affects SPD
       public spect30 insect = new spect30();//how insectoid the creature is, affects DEF
       public spect30 amphibious = new spect30();//how well the creatuture is adapted to water, affects BLO
       public spect30 stance = new spect30();//how high the creature is relative to the floor, affects CON
       //public spect30 intelegence = new spect30();//how many commands are avalible to the creature, have alot at birth lowers stats
       //public spect30 obediance = new spect30();//how much the creature is affectd by commands, have alot at birth lowers stats
       //public spect30 reactions = new spect30();//how quickly the crature is to follow commands, have alot at birth lowers stats
       public spect30 lifespan = new spect30();//how long the creature lives, and how much training and food affects it
       public spect50 atkPerf = new spect50();//whether or not the creature prefers strikes or blows
       public spect50 defPerf = new spect50();//whether or not the creature prefers blocks or braces
       //public spect50 effort = new spect50();//stats vs stamina usage
       public spect50 stamina = new spect50();//max stamina
       public spect50 tech = new spect50();//how likley the creature is to use abilities (once implemented)
       public spect30 litter = new spect30();//how many eggs the creature lays
       public spect50 fickle = new spect50();//how fast happiness raises and falls
       public spect50 vanity = new spect50();//how fast spoilage raises and falls
       public spect50 eggHappy = new spect50();//how happy and spoilt the creature is when it hatches
       public spect30 tallness = new spect30();//how tall the creature is, Higher STK and DEF(10%) at the cost of BLO and CON.(10%)
       public spect30 limb = new spect30();//how long the limbs are,More STK,BLO(10%) but less STR(20%)
       public spect30 bulk = new spect30();//how bulky the creature is More HP(20%) but less DEF,CON(10%) 
       public spect30 feet = new spect30();// what type of feet the creature has  SPD(15%) less STK,BLO,STR(5%)
       public spect30 thickness = new spect30();//how thick th creatures skin is DEF,CON,HP(5%) less SPD(15%)
       public spect30 cuteness = new spect30();//how cute the creature is STK,BLO,STR(5%) more DEF,CON,HP(5%)
       public spect50 metabolism = new spect50();//how muc the stanima the creature uses and gains

       public double BST;//total of baset stats, used as a general guage of stregnth
       public int age;//how old the creature is in days
       public double happiness;//how happy the creature is out of 100
       public double spoilage;//how spoiled the creature is out 
       public double curStam;//the creatures current stamina
       public double maxStam;//the creatures max stamina
       public double stamRate;//the rate which the creature gain and loses stamina
       public double happyRate;//how easilly the creature gets happy or sad
       public double spoilRate;//how easillt the creature gets spoiled
       public double restRate;// how quickly the creature rests
       public int maxAge;// how long the creature will live
       public int[] equips = new int[3];//current equipment on the creature

     

       public double ageMultiplier(int age,double lifespan)//checks the current age group of the creature and the correct multiplier
       {
           double comparison = lifespan/60;
           if (age < comparison * 4)
               return 0.3;
           else if (age < comparison * 12)
               return 0.5;
           else if (age < comparison * 20)
               return 0.85;
           else if (age < comparison * 32)
               return 1;
           else if (age < comparison * 44)
               return 0.9;
           else if (age < comparison * 52)
               return 0.75;
           else
               return 0.5;

       }

       
       //{
       //    foreach(stat in this)
       //    {
       //    }
       //}
       public void retrain()//updates the creatures stats RUN WHENEVER THE CREATURES STTS WOULD CHANGE
       {

           STR.total = Math.Round(((STR.baseStat + STR.trained + STR.bonus)));// * ageMultiplier(age, lifespan.total)));// * (happiness / 100 + 0.5)) * ((((-intelegence.baseStat - obediance.baseStat - reactions.baseStat) * 2 + 360) / 600) + ((effort.baseStat * 5) / 1000) + 0.45), 2);
           HP.total = Math.Round(((HP.baseStat + HP.trained + HP.bonus)*Game1.HPmultiplyer));// * ageMultiplier(age, lifespan.total)));// * (happiness / 100 + 0.5)) * ((((-intelegence.baseStat - obediance.baseStat - reactions.baseStat) * 2 + 360) / 600) + ((effort.baseStat * 5) / 1000) + 0.45), 2);
           STK.total = Math.Round(((STK.baseStat + STK.trained + STK.bonus)));// * ageMultiplier(age, lifespan.total)));// * (happiness / 100 + 0.5)) * ((((-intelegence.baseStat - obediance.baseStat - reactions.baseStat) * 2 + 360) / 600) + ((effort.baseStat * 5) / 1000) + 0.45), 2);
           SPD.total = Math.Round(((SPD.baseStat + SPD.trained + SPD.bonus)));// * ageMultiplier(age, lifespan.total)));// * (happiness / 100 + 0.5)) * ((((-intelegence.baseStat - obediance.baseStat - reactions.baseStat) * 2 + 360) / 600) + ((effort.baseStat * 5) / 1000) + 0.45), 2);
           BLO.total = Math.Round(((BLO.baseStat + BLO.trained + BLO.bonus)));// * ageMultiplier(age, lifespan.total)));// * (happiness / 100 + 0.5)) * ((((-intelegence.baseStat - obediance.baseStat - reactions.baseStat) * 2 + 360) / 600) + ((effort.baseStat * 5) / 1000) + 0.45), 2);
           DEF.total = Math.Round(((DEF.baseStat + DEF.trained + DEF.bonus)));// * ageMultiplier(age, lifespan.total)));// * (happiness / 100 + 0.5)) * ((((-intelegence.baseStat - obediance.baseStat - reactions.baseStat) * 2 + 360) / 600) + ((effort.baseStat * 5) / 1000) + 0.45), 2);
           CON.total = Math.Round(((CON.baseStat + CON.trained + CON.bonus)));// * ageMultiplier(age, lifespan.total)));// * (happiness / 100 + 0.5)) * ((((-intelegence.baseStat - obediance.baseStat - reactions.baseStat) * 2 + 360) / 600) + ((effort.baseStat * 5) / 1000) + 0.45), 2); 
           stamina.total = stamina.baseStat + stamina.trained; maxStam = stamina.total * 2 + 200 + stamina.bonus;
           //intelegence.total = intelegence.baseStat + intelegence.trained + intelegence.bonus;
           aggressive.total = aggressive.baseStat + aggressive.trained + aggressive.bonus;
           courage.total = courage.baseStat + courage.trained + courage.bonus;
           atkPerf.total = atkPerf.baseStat + atkPerf.trained + atkPerf.bonus;
           defPerf.total = defPerf.baseStat + defPerf.trained + defPerf.bonus;
          // effort.total = effort.baseStat + effort.trained + effort.bonus;
           //obediance.total = obediance.baseStat + obediance.trained + obediance.bonus;
           //reactions.baseStat + reactions.trained + reactions.bonus;

       }
       public void initilize()//initilizes the creatures stats and sets the bases
   {
       lifespan.total = lifespan.sum();
       mammal.total = mammal.sum();
        reptile.total = reptile.sum();
        bird.total = bird.sum();
        wings.total = wings.sum();
        insect.total = insect.sum();
        amphibious.total = amphibious.sum();
        stance.total = stance.sum();
        tallness.total = stance.sum();
        limb.total = limb.sum();
        bulk.total = bulk.sum();
        feet.total = feet.sum();
        thickness.total = thickness.sum();
        cuteness.total = cuteness.sum();
        //intelegence.baseStat = intelegence.sum();
        //obediance.baseStat = obediance.sum();
        //reactions.baseStat = reactions.sum();
        STR.baseStat = STR.sum() * (((mammal.total*6) - (limb.total * 4) - feet.total + cuteness.total - reptile.total - bird.total - wings.total - insect.total - amphibious.total - stance.total + 660) / 600 + 0.1);
        HP.baseStat = HP.sum() * (((reptile.total*6) + (bulk.total * 4) +  thickness.total - cuteness.total - mammal.total - bird.total - wings.total - insect.total - amphibious.total - stance.total + 420) / 600 + 0.1);
        SPD.baseStat = SPD.sum() * (((wings.total * 6) - (thickness.total * 3) + (feet.total * 3) - mammal.total - bird.total - reptile.total - insect.total - amphibious.total - stance.total + 540) / 600 + 0.1);
        STK.baseStat = STK.sum()  * (((bird.total * 6) + (tallness.total * 2) + (limb.total * 2) - cuteness.total - feet.total - mammal.total - reptile.total - wings.total - insect.total - amphibious.total - stance.total + 480) / 600 + 0.1);
        BLO.baseStat = BLO.sum()  * (((amphibious.total * 6) - (tallness.total * 2) + (limb.total * 2) - cuteness.total - feet.total - mammal.total - bird.total - wings.total - insect.total - reptile.total - stance.total + 600) / 600 + 0.1);
        DEF.baseStat = DEF.sum()  * (((insect.total * 6) + (tallness.total * 2) - (bulk.total * 2) + cuteness.total + thickness.total - mammal.total - bird.total - wings.total - reptile.total - amphibious.total - stance.total + 480) / 600 + 0.1);
        CON.baseStat = CON.sum() * (((stance.total * 6) - (tallness.total * 2) - (bulk.total * 2) + cuteness.total + thickness.total - mammal.total - bird.total - wings.total - reptile.total - amphibious.total - insect.total + 600) / 600 + 0.1);
        BST = Math.Round(STR.baseStat + HP.baseStat + SPD.baseStat + STK.baseStat + BLO.baseStat + DEF.baseStat + CON.baseStat,2);
        aggressive.baseStat = aggressive.sum();
        courage.baseStat = courage.sum();
        atkPerf.baseStat = atkPerf.sum();
        defPerf.baseStat = defPerf.sum();
        //effort.baseStat = effort.sum();
        stamina.baseStat = stamina.sum();
        tech.baseStat = tech.sum();
        litter.total = litter.sum();
        fickle.total = fickle.sum();
        vanity.total = vanity.sum();
        metabolism.total = metabolism.sum();
        eggHappy.total = eggHappy.sum();
        stamRate = metabolism.total /100 + 0.5;
        restRate = (metabolism.total/100) +0.5;
        happyRate = fickle.total /100 +0.5;
        spoilRate = vanity.total/100 + 0.5;
        maxAge = (int)lifespan.total + 30;
        maxStam = stamina.total/50 +200;
        happiness = eggHappy.total / 2 + 10;
        spoilage = eggHappy.total / 2 + 10;
        retrain();
   }
       public void generate(int BST)//generates a creature with a specified BST
       {
           int alSTR = 0, alHP = 0, alSPD = 0, alSTK = 0, alBLO = 0, alDEF = 0, alCON = 0;
           for (int count = 0; count < BST - 140; count++)
           {
               int allocate = Game1.rand.Next(0,7);
               if (allocate == 0)
                   alSTR++;
               else if (allocate == 1)
                   alHP++;
               else if (allocate == 2)
                   alSPD++;
               else if (allocate == 3)
                   alSTK++;
               else if (allocate == 4)
                   alBLO++;
               else if (allocate == 5)
                   alDEF++;
               else if (allocate == 6)
                   alCON++;
           }
               STR.generate(alSTR);
               HP.generate(alHP);
               SPD.generate(alSPD);
               STK.generate(alSTK);
               BLO.generate(alBLO);
               DEF.generate(alDEF);
               CON.generate(alCON);
             aggressive.generate(Game1.rand.Next(0,100));
             courage.generate(Game1.rand.Next(0,100));
             mammal.generate(Game1.rand.Next(0,60));
             reptile.generate(Game1.rand.Next(0,60));
             bird.generate(Game1.rand.Next(0,60));
             wings.generate(Game1.rand.Next(0,60));
             insect.generate(Game1.rand.Next(0,60));
             amphibious.generate(Game1.rand.Next(0,60));
             stance.generate(Game1.rand.Next(0,60));
             //intelegence.generate(Game1.rand.Next(0,60));
             //obediance.generate(Game1.rand.Next(0,60));
             //reactions.generate(Game1.rand.Next(0,60));
             lifespan.generate(Game1.rand.Next(0,100));
             atkPerf.generate(Game1.rand.Next(0,100));
             defPerf.generate(Game1.rand.Next(0,100));
             //effort.generate(Game1.rand.Next(0,100));
             stamina.generate(Game1.rand.Next(0,100));
             tech.generate(Game1.rand.Next(0,100));
             litter.generate(Game1.rand.Next(0,60));
             fickle.generate(Game1.rand.Next(0,60));
             vanity.generate(Game1.rand.Next(0,60));
             eggHappy.generate(Game1.rand.Next(0,60));
             tallness.generate(Game1.rand.Next(0,60));
             limb.generate(Game1.rand.Next(0,60));
             bulk.generate(Game1.rand.Next(0,60));
             feet.generate(Game1.rand.Next(0,60));
             thickness.generate(Game1.rand.Next(0,60));
             cuteness.generate(Game1.rand.Next(0,60));
             metabolism.generate(Game1.rand.Next(0, 100));
           happiness = (Game1.rand.Next(0,50) + 10);
           spoilage = (Game1.rand.Next(0,50) + 10);
           age = (Game1.rand.Next(0,(int)(lifespan.sum()/2 + 30)));
             initilize();
           curStam = Game1.rand.Next(0,(int)maxStam);


       }
       public void breed(creature mother, creature father)//breeds the 2 input creatures
       {
           STR.breed(mother.STR,father.STR);
           HP.breed(mother.HP, father.HP);
           SPD.breed(mother.SPD, father.SPD);
           STK.breed(mother.STK, father.STK);
           BLO.breed(mother.BLO, father.BLO);
           DEF.breed(mother.DEF, father.DEF);
           CON.breed(mother.CON, father.CON);
           aggressive.breed(mother.aggressive, father.aggressive);
           courage.breed(mother.courage, father.courage);
           mammal.breed(mother.mammal, father.mammal);
           reptile.breed(mother.reptile, father.reptile);
           bird.breed(mother.bird, father.bird);
           wings.breed(mother.wings, father.wings);
           insect.breed(mother.insect, father.insect);
           amphibious.breed(mother.amphibious, father.amphibious);
           stance.breed(mother.stance, father.stance);
           //intelegence.breed(mother.intelegence, father.intelegence);
           //obediance.breed(mother.obediance, father.obediance);
           //reactions.breed(mother.reactions, father.reactions);
           lifespan.breed(mother.lifespan, father.lifespan);
           atkPerf.breed(mother.atkPerf, father.atkPerf);
           defPerf.breed(mother.defPerf, father.defPerf);
           //effort.breed(mother.effort, father.effort);
           stamina.breed(mother.stamina, father.stamina);
           tech.breed(mother.tech, father.tech);
           litter.breed(mother.litter, father.litter);
           fickle.breed(mother.fickle, father.fickle);
           vanity.breed(mother.vanity, father.vanity);
           eggHappy.breed(mother.eggHappy, father.eggHappy);
           tallness.breed(mother.tallness, father.tallness);
           limb.breed(mother.limb, father.limb);
           bulk.breed(mother.bulk, father.bulk);
           feet.breed(mother.feet, father.feet);
           thickness.breed(mother.thickness, father.thickness);
           cuteness.breed(mother.cuteness, father.cuteness);
           metabolism.breed(mother.metabolism, father.metabolism);
           age = 0;
           initilize();
       }


    }
}

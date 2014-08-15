using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame8
{
    static class Jobs
    {
        static string FindName(int code)//codes 0-99 misc commands, 100-199 jobs, 200-299 training, 300-399 breed
        {
            switch (code)
            {
                case 0:
                    return "rest";//restores 150 stamina
                    break;
                case 1: 
                    return "swap";
                    break;
                case 100:
             return "Job:hunting";
                    break;
                case 101:
                    return "Job:childcare";
                    break;
                case 102:
                    return "Job:construction";
                    break;
                case 103:
                    return "Job:battle tutor";
                    break;
                case 104:
                    return "Job:police";
                    break;
                case 105:
                    return "Job:rescue";
                    break;
                case 106:
                    return "Job:security";
                    break;
                case 107:
                    return "Job:teacher";
                    break;
                case 108:
                    return "Job:performance";
                    break;
                case 109:
                    return "Job:sparring partner";
                    break;
                case 110:
                    return "Job:arena jobber";
                    break;
                case 200:
                    return "Training:HP";
                    break;
                case 201:
                    return "Training:STR";
                    break;
                case 202:
                    return "Training:STK";
                    break;
                case 203:
                    return "Training:BLO";
                    break;
                case 204:
                    return "Training:DEF";
                    break;
                case 205:
                    return "Training:CON";
                    break;
                case 206:
                    return "Training:SPD";
                    break;
            }
            if (code > 299 && code < 399)
                return "breed with " + (code - 300);
            return "error";
        }
        public static double DoJob(int code,ref creature C)
        {
            switch (code)
            {
                case 0:
                    if (C.curStam + 150 > C.maxStam)
                        C.curStam = C.maxStam;//rest
                    else
                        C.curStam += 150;
                    return -1;
                    break;
                case 1:
                    //return "swap";
                    break;
                case 100:
                    return work(C.SPD.total,C.STR.total,C.STK.total,C.aggressive.total,ref C);//hunting
                    break;
                case 101:
                    return work(C.SPD.total,C.HP.total/Game1.HPmultiplyer,C.DEF.total,-C.aggressive.total+100,ref C);//childcare
                    break;
                case 102:
                    return work(C.BLO.total,C.STR.total,C.CON.total,-C.courage.total+100,ref C);//construction
                    break;
                case 103:
                    return work(C.HP.total/Game1.HPmultiplyer,C.STK.total,C.DEF.total,C.aggressive.total,ref C);//battle tutor
                    break;
                case 104:
                    return work(C.SPD.total,C.BLO.total,C.STR.total,C.aggressive.total,ref C);//police
                    break;
                case 105:
                    return  work(C.SPD.total,C.CON.total,C.STK.total,C.courage.total,ref C);//rescue
                    break;
                case 106:
                    return  work(C.BLO.total,C.CON.total,C.STR.total,C.aggressive.total,ref C);//security
                    break;
                case 107:
                    return work(C.HP.total/Game1.HPmultiplyer,C.CON.total,C.DEF.total,-C.aggressive.total+100,ref C);//teacher
                    break;
                case 108:
                    return work(C.HP.total/Game1.HPmultiplyer,C.CON.total,C.SPD.total,C.courage.total,ref C);//performance";
                    break;
                case 109:
                    return work(C.BLO.total,C.STK.total,C.DEF.total,-C.courage.total+100,ref C);//sparring partner";
                    break;
                case 110:
                    return work(C.HP.total/Game1.HPmultiplyer,C.CON.total,C.BLO.total,C.courage.total,ref C);//arena jobber";
                    break;
                case 200:
                    return train(C.HP,ref C);
                    break;
                case 201:
                    return train(C.STR, ref C);
                    break;
                case 202:
                    return train(C.STK, ref C);
                    break;
                case 203:
                    return train(C.BLO, ref C);
                    break;
                case 204:
                    return train(C.DEF, ref C);
                    break;
                case 205:
                    return train(C.CON, ref C);
                    break;
                case 206:
                    return train(C.SPD, ref C);
                    break;
            }
            return -1;
        }
        public static double work(double stat1, double stat2, double stat3, double behave,/*double intel,*/ ref creature C)
        {
            double ret;
            if (C.curStam >= 100)
            {
                C.curStam -= 100;
                ret = ((stat1 + stat2 + stat3)/11 *(((behave + /*intel*/45)/400)+(C.happiness/100) + 0.5) + 1);
            Game1.player.Money += (int)ret;
                return ret;
            }
            else
            {
                ret = (((stat1 + stat2 + stat3) / 11 * (((behave + /*intel*/45) / 400) + (C.happiness / 100) + 0.5)) * (C.curStam / 100) + 1);
                C.happiness -= C.curStam / 10;
                C.curStam = 0;
                Game1.player.Money += (int)ret;
                return ret;
            }

        }
        public static double train(stat S, ref creature C)
        {
               double ret = (C.BST / 50) * (C.happiness / 250 + 0.8);
               S.trained += ret;
               return ret;
        }
    }
}

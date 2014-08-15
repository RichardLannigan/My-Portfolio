using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Assignment2
{
    class SphereList
    {
        public static List<Sphere> RedSphereList = new List<Sphere>();
        public static List<Sphere> BlueSphereList = new List<Sphere>();
        public static List<Sphere> GreenSphereList = new List<Sphere>();
        public static List<Sphere> YellowSphereList = new List<Sphere>();
        public static List<Sphere> EnemySphereList = new List<Sphere>();

        public static List<ScoreSphere> ScoreBoardRedList = new List<ScoreSphere>();
        public static List<ScoreSphere> ScoreBoardBlueList = new List<ScoreSphere>();
        public static List<ScoreSphere> ScoreBoardGreenList = new List<ScoreSphere>();
        public static List<ScoreSphere> ScoreBoardYellowList = new List<ScoreSphere>();

        public static void LoadContent(ContentManager contentManager, Random randX, Random randY)
        {
            int positionX = randX.Next(0, 38);
            int positionY = randY.Next(0, 34);

            for (int i = 0; i < Game1.redSphereCount; i++)
            {
                RedSphereList.Add(new Sphere(contentManager.Load<Texture2D>("Red Spheres/RedSphere_SpriteSheet_Small_02"), new Vector2(0, 0), 100, 100));
            }
            for (int i = 0; i < Game1.blueSphereCount; i++)
            {
                BlueSphereList.Add(new Sphere(contentManager.Load<Texture2D>("Blue Spheres/BlueSphere_SpriteSheet_Small_02"), new Vector2(0, 0), 100, 100));
            }
            for (int i = 0; i < Game1.greenSphereCount; i++)
            {
                GreenSphereList.Add(new Sphere(contentManager.Load<Texture2D>("Green Spheres/GreenSphere_SpriteSheet_Small_02"), new Vector2(0, 0), 100, 100));
            }
            for (int i = 0; i < Game1.yellowSphereCount; i++)
            {
                YellowSphereList.Add(new Sphere(contentManager.Load<Texture2D>("Yellow Spheres/YellowSphere_SpriteSheet_Small_02"), new Vector2(0, 0), 100, 100));
            }
            for (int i = 0; i < Game1.enemySphereCount; i++)
            {
                EnemySphereList.Add(new Sphere(contentManager.Load<Texture2D>("Black_Sphere_SpriteSheet"), new Vector2(0, 0), 100, 100));
            }
        }
        public static void CreateScoreBoard(ContentManager contentManager)
        {
            for (int i = 0; i < Game1.redScore; i++)
            {
                ScoreBoardRedList.Add(new ScoreSphere(contentManager.Load<Texture2D>("Red Spheres/Red_Sphere_Score"), new Vector2(5, (Game1.cameraPosition.Y + Game1.ScreenHeight) - (i * 30))));
            }
            for (int i = 0; i < Game1.blueScore; i++)
            {
                ScoreBoardBlueList.Add(new ScoreSphere(contentManager.Load<Texture2D>("Blue Spheres/Blue_Sphere_Score"), new Vector2(35, (Game1.cameraPosition.Y + Game1.ScreenHeight) - (i * 30))));
            }
            for (int i = 0; i < Game1.greenScore; i++)
            {
                ScoreBoardGreenList.Add(new ScoreSphere(contentManager.Load<Texture2D>("Green Spheres/Green_Sphere_Score"), new Vector2(Game1.ScreenWidth - 65, (Game1.cameraPosition.Y + Game1.ScreenHeight) - (i * 30))));
            }
            for (int i = 0; i < Game1.yellowScore; i++)
            {
                ScoreBoardYellowList.Add(new ScoreSphere(contentManager.Load<Texture2D>("Yellow Spheres/Yellow_Sphere_Score"), new Vector2(Game1.ScreenWidth - 30, (Game1.cameraPosition.Y + Game1.ScreenHeight) - (i * 30))));
            }
        }

        //ScoreBoardRedList.Add(new ScoreSphere(contentManager.Load<Texture2D>("Blue_Sphere_Score"), new Vector2(5, Game1.cameraPosition.Y - (i * 60))));

        public static void Initialize()
        {
            //SphereList.Add(new RedSphere(Content.Load<Texture2D>("Red_Sphere"), new Vector2(10, 10));
        }

        public static void Reset()
        {
            foreach (Sphere s in RedSphereList)
            {

            }
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Text;

namespace WindowsGame8
{
    class ActionScreen : Scene
    {
        TileEngine tileEngine;

        List<Map> myMaps = new List<Map>();
        Texture myTexture;

        


        SpriteBatch spriteBatch;

        public ActionScreen(Game game, string name)
        {
            spriteBatch = game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            this.game = game;
            this.name = name;
        }

        

        public override void LoadContent()
        {





            LoadTMX(@"Content\MapLayer\Island_04.tmx");

            tileEngine = new TileEngine(game, myTexture, myMaps);
            SceneComponents.Add(tileEngine);

            tileEngine.Show();

            base.LoadContent();
        }

        

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }




        private void LoadTMX(string fileName)
        {
            XElement tmxDoc = XElement.Load(fileName);

            CreateTexture(tmxDoc);

            AddMapLayer(tmxDoc, 0, "Ground", 1.0f);
            AddMapLayer(tmxDoc, 1, "Layer 2", 0.5f);

            CreateCollisionMap(tmxDoc);
        }

        private void CreateTexture(XElement tmxDoc)
        {
            XElement tileset = null;
            int tilesetTileWidth = 0;
            int tilesetTileHeight = 0;
            int tilesetTileSpacing = 0;
            string source = null;

            tileset = tmxDoc.Descendants("tileset").ElementAt(0);

            tilesetTileWidth = int.Parse(tileset.Attribute("tilewidth").Value);  // tile size on tilemap
            tilesetTileHeight = int.Parse(tileset.Attribute("tileheight").Value); // tile height on tilemap

            if (tileset.Attribute("spacing") != null)
            {
                tilesetTileSpacing = int.Parse(tileset.Attribute("spacing").Value);
            }
            else
            {
                tilesetTileSpacing = 0;
            }

            source = tileset.Element("image").Attribute("source").Value;
            source = Path.GetFileNameWithoutExtension(source);
            Texture2D tileMapTexture = game.Content.Load<Texture2D>(@"Tiles\" + source);
            myTexture = new Texture(tileMapTexture, tilesetTileWidth, tilesetTileHeight, tilesetTileSpacing);
        }

        private void AddMapLayer(XElement tmxDoc, int layerNum, string layerName, float depth)
        {
            int tempgid;
            var layer = tmxDoc.Descendants("layer").ElementAt(layerNum);
            int width = int.Parse(layer.Attribute("width").Value);
            int height = int.Parse(layer.Attribute("height").Value);
            Map myMap = new Map(width, height, depth);


            IEnumerable<XElement> tile = from el in tmxDoc.Descendants("layer")
                   where (string)el.Attribute("name") == layerName
                   select el.Element("data");

            List<int> temp = new List<int>();
            foreach (var gid in tile.Descendants())
            {
                tempgid = int.Parse(gid.Attribute("gid").Value);
                tempgid -= 1;
                temp.Add(tempgid);
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    myMap.SetTile(x, y, temp[y * width + x]);
                }
            }
            myMaps.Add(myMap);            
        }




        private void CreateCollisionMap(XElement tmxDoc)
        {
            int tempgid;
            var layer = tmxDoc.Descendants("layer").ElementAt(2);
            int width = int.Parse(layer.Attribute("width").Value);
            int height = int.Parse(layer.Attribute("height").Value);
            Collisions.collisionMap = new Map(width, height, 0);


            IEnumerable<XElement> tile = from el in tmxDoc.Descendants("layer")
                                         where (string)el.Attribute("name") == "Collision"
                                         select el.Element("data");

            List<int> temp = new List<int>();
            foreach (var gid in tile.Descendants())
            {
                tempgid = int.Parse(gid.Attribute("gid").Value);
                tempgid -= 1;
                temp.Add(tempgid);
            }
            
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Collisions.collisionMap.SetTile(x, y, temp[y * width + x]);
                }
            }

            
        }

    }
}



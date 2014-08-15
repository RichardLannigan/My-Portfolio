using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame8
{
    class Scene
    {
        List<GameComponent> sceneComponents;
        protected string name;
        protected Game game;
        public string Name { get { return name; } }

        public List<GameComponent> SceneComponents
        {
            get { return sceneComponents; }
            set { sceneComponents = value; }
        }


        public Scene()
        {            
            sceneComponents = new List<GameComponent>();           
        }

        public virtual void LoadContent() { }






        public virtual void Update(GameTime gameTime)
        {
            foreach (var component in sceneComponents)
            {
                if(component.Enabled)
                    component.Update(gameTime);
            }

            
        }


        public virtual void Draw(GameTime gameTime)
        {
            DrawableGameComponent drawComponent;

            foreach (var component in sceneComponents)
            {
                drawComponent = component as DrawableGameComponent;
                if (drawComponent != null && drawComponent.Visible)
                    drawComponent.Draw(gameTime);
            }

            
        }

        public virtual void Show()
        {
            DrawableGameComponent drawComponent;
            //Enabled = true;
            //Visible = true;
            foreach (var component in sceneComponents)
            {
                component.Enabled = true;
                drawComponent = component as DrawableGameComponent;
                if (drawComponent != null)
                    drawComponent.Visible = true;
            }
        }

        public virtual void Hide()
        {
            DrawableGameComponent drawComponent;
            //Enabled = false;
            //Visible = false;
            foreach (var component in sceneComponents)
            {
                component.Enabled = false;
                drawComponent = component as DrawableGameComponent;
                if (drawComponent != null)
                    drawComponent.Visible = false;
            }
        }

        public virtual void Pause()
        {
            DrawableGameComponent drawComponent;
            //Enabled = false;
            //Visible = true;
            foreach (var component in sceneComponents)
            {
                component.Enabled = false;
                drawComponent = component as DrawableGameComponent;
                if (drawComponent != null)
                    drawComponent.Visible = true;
            }
        }

       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame8
{
    class InputManager : GameComponent
    {
        MouseState mouseState;
        MouseState oldMouseState;

        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;

        public InputManager(Game game) : base(game) { }

        public override void Initialize()
        {
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            oldKeyboardState = keyboardState;
            oldMouseState = mouseState;

            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            
            base.Update(gameTime);
        }



        public Point MousePosition
        {
            get { return new Point(mouseState.X, mouseState.Y); }
        }

        public bool IsLeftButtonDown()
        {
            return oldMouseState.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed;
        }

        public bool IsLeftButtonUp()
        {
            return oldMouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released;
        }

        public bool IsLeftButtonPressed()
        {
            return mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Pressed;
        }



        public bool IsKeyDown(Keys key)
        {
            return keyboardState.IsKeyDown(key) && oldKeyboardState.IsKeyUp(key);
        }

        public bool IsKeyUp(Keys key)
        {
            return keyboardState.IsKeyUp(key) && oldKeyboardState.IsKeyDown(key);            
        }

        public bool IsKeyPressed(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }

        

        

    }
}

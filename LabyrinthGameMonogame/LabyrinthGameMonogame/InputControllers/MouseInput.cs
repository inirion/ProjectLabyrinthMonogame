using System;
using System.Collections.Generic;
using LabyrinthGameMonogame.Enums;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using LabyrinthGameMonogame.GUI.Screens;

namespace LabyrinthGameMonogame.InputControllers
{
    class MouseInput : IMouseInput
    {
        #region Variables
        private Dictionary<MouseKeys, Delegate> KeyBindings;
        MouseState currentState;
        MouseState previousState;
        MouseState originalState;
        Vector2 currentMousePos;
        Vector2 previousMousePos;

        public Vector2 CurrentMousePos { get => currentMousePos; set => currentMousePos = value; }
        public Vector2 PreviousMousePos { get => previousMousePos; set => previousMousePos = value; }
        public MouseState OriginalState { get => originalState; set => originalState = value; }
        public MouseState CurrentState { get => currentState; set => currentState = value; }
        #endregion
        public MouseInput()
        {
            CurrentState = previousState;

            CurrentMousePos = PreviousMousePos;
            CurrentMousePos = new Vector2(CurrentState.X, CurrentState.Y);
            OriginalState = CurrentState;

            KeyBindings = new Dictionary<MouseKeys, Delegate>();
            KeyBindings.Add(MouseKeys.LeftButton, new Func<MouseKeys, MouseState, ButtonState>(GetButtonState));
            KeyBindings.Add(MouseKeys.RightButton, new Func<MouseKeys, MouseState, ButtonState>(GetButtonState));
            KeyBindings.Add(MouseKeys.MiddleButton, new Func<MouseKeys, MouseState, ButtonState>(GetButtonState));
            KeyBindings.Add(MouseKeys.Button1, new Func<MouseKeys, MouseState, ButtonState>(GetButtonState));
            KeyBindings.Add(MouseKeys.Button2, new Func<MouseKeys, MouseState, ButtonState>(GetButtonState));

            Update();
        }

        public void CentrePosition()
        {
            currentMousePos = new Vector2(ScreenManager.Instance.Graphics.GraphicsDevice.Viewport.Width / 2, ScreenManager.Instance.Graphics.GraphicsDevice.Viewport.Height / 2);
            Mouse.SetPosition((int)currentMousePos.X,(int)currentMousePos.Y);
            previousMousePos = currentMousePos;
            originalState = Mouse.GetState();
            previousState = Mouse.GetState();
            CurrentState = Mouse.GetState();
        }

        public void Update()
        {
            previousState = CurrentState;
            CurrentState = Mouse.GetState();

            PreviousMousePos = CurrentMousePos;
            CurrentMousePos = new Vector2(CurrentState.X, CurrentState.Y);
        }

        public bool Clicked(MouseKeys key)
        {
            return KeyBindings[key].DynamicInvoke(key, CurrentState).Equals(ButtonState.Released) 
                && KeyBindings[key].DynamicInvoke(key, previousState).Equals(ButtonState.Pressed);
        }

        public bool Clicked(MouseKeys key, Rectangle target)
        {
            if ((KeyBindings[key].DynamicInvoke(key, CurrentState).Equals(ButtonState.Released)
                && KeyBindings[key].DynamicInvoke(key, previousState).Equals(ButtonState.Pressed))
                && (target.Contains(currentMousePos)))
                return true;
            else return false;
        }

        public bool Pressed(MouseKeys key)
        {
            return KeyBindings[key].DynamicInvoke(key,CurrentState).Equals(ButtonState.Pressed);
        }

        public bool Hovered(Rectangle target)
        {
            return target.Contains(currentMousePos);
        }

        public void resetMousePosition()
        {
            currentMousePos.X = 0;
            currentMousePos.Y = 0;
            previousMousePos.X = 0;
            previousMousePos.Y = 0;
   
        }

        private ButtonState GetButtonState(MouseKeys key, MouseState state)
        {
            switch (key)
            {
                case MouseKeys.LeftButton:
                    return state.LeftButton;
                case MouseKeys.RightButton:
                    return state.RightButton;
                case MouseKeys.MiddleButton:
                    return state.MiddleButton;
                case MouseKeys.Button1:
                    return state.XButton1;
                case MouseKeys.Button2:
                    return state.XButton2;
            }
            return ButtonState.Released;
        }
    }
}

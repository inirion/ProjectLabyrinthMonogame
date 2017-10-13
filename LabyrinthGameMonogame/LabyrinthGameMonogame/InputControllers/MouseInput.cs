using System;
using System.Collections.Generic;
using LabyrinthGameMonogame.Enums;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace LabyrinthGameMonogame.InputControllers
{
    class MouseInput : IMouseInput
    {
        #region Variables
        private Dictionary<MouseKeys, Delegate> KeyBindings;
        MouseState currentState;
        MouseState previousState;
        Vector2 currentMousePos;
        Vector2 previousMousePos;

        public Vector2 CurrentMousePos { get => currentMousePos; set => currentMousePos = value; }
        #endregion
        public MouseInput()
        {
            currentState = previousState;

            CurrentMousePos = previousMousePos;
            CurrentMousePos = new Vector2(currentState.X, currentState.Y);

            KeyBindings = new Dictionary<MouseKeys, Delegate>();
            KeyBindings.Add(MouseKeys.LeftButton, new Func<MouseKeys, MouseState, ButtonState>(GetButtonState));
            KeyBindings.Add(MouseKeys.RightButton, new Func<MouseKeys, MouseState, ButtonState>(GetButtonState));
            KeyBindings.Add(MouseKeys.MiddleButton, new Func<MouseKeys, MouseState, ButtonState>(GetButtonState));
            KeyBindings.Add(MouseKeys.Button1, new Func<MouseKeys, MouseState, ButtonState>(GetButtonState));
            KeyBindings.Add(MouseKeys.Button2, new Func<MouseKeys, MouseState, ButtonState>(GetButtonState));

            Update();
        }
        public void Update()
        {
            previousState = currentState;
            currentState = Mouse.GetState();

            previousMousePos = CurrentMousePos;
            CurrentMousePos = new Vector2(currentState.X, currentState.Y);
        }

        public bool Clicked(MouseKeys key)
        {
            return KeyBindings[key].DynamicInvoke(key, currentState).Equals(ButtonState.Released) 
                && KeyBindings[key].DynamicInvoke(key, previousState).Equals(ButtonState.Pressed);
        }

        public bool Clicked(MouseKeys key, Rectangle target)
        {
            if ((KeyBindings[key].DynamicInvoke(key, currentState).Equals(ButtonState.Released)
                && KeyBindings[key].DynamicInvoke(key, previousState).Equals(ButtonState.Pressed))
                && (target.Contains(currentMousePos)))
                return true;
            else return false;
        }

        public bool Pressed(MouseKeys key)
        {
            return KeyBindings[key].DynamicInvoke(key,currentState).Equals(ButtonState.Pressed);
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

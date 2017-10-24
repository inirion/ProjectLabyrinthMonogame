using System;
using System.Collections.Generic;
using LabyrinthGameMonogame.Enums;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using LabyrinthGameMonogame.GUI.Screens;

namespace LabyrinthGameMonogame.InputControllers
{
    class MouseInput
    {
        #region Variables
        private Dictionary<MouseKeys, Delegate> KeyBindings;
        MouseState currentState;
        MouseState previousState;
        MouseState originalState;
        Vector2 currentMousePos;
        Vector2 previousMousePos;
        float sensitivity;
        private IScreenManager screenManager;

        public Vector2 CurrentMousePos { get => currentMousePos; set => currentMousePos = value; }
        public Vector2 PreviousMousePos { get => previousMousePos; set => previousMousePos = value; }
        public MouseState OriginalState { get => originalState; set => originalState = value; }
        public MouseState CurrentState { get => currentState; set => currentState = value; }
        public float Sensitivity { get => sensitivity; set => sensitivity = value; }
        #endregion
        public MouseInput(Game game)
        {
            screenManager = (IScreenManager)game.Services.GetService(typeof(IScreenManager));
            sensitivity = 0.04f;
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

        public void CentrePosition(Vector2 pos)
        {
            currentMousePos = pos;
            Mouse.SetPosition((int)currentMousePos.X, (int)currentMousePos.Y);
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
            return KeyBindings[key].DynamicInvoke(key, CurrentState).Equals(ButtonState.Pressed);
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

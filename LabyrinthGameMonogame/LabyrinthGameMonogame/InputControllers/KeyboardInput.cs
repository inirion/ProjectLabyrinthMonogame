using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using LabyrinthGameMonogame.Enums;

namespace LabyrinthGameMonogame.InputControllers
{
    class KeyboardInput: IKeyboardInput
    {
        #region Variables
        private Dictionary<KeyboardKeys, Keys> KeyBindings;
        KeyboardState currentState;
        KeyboardState previousState;
        #endregion

        public KeyboardInput() {
            currentState = previousState;
            KeyBindings = new Dictionary<KeyboardKeys, Keys>
            {
                {KeyboardKeys.Up,Keys.W },
                {KeyboardKeys.Left,Keys.A },
                {KeyboardKeys.Down,Keys.S },
                {KeyboardKeys.Right,Keys.D },
                {KeyboardKeys.Back,Keys.Escape },
                {KeyboardKeys.Confirm,Keys.Enter }
            };
            Update();
        }

        public void Update()
        {
            previousState = currentState;
            currentState = Keyboard.GetState();
        }

        public bool Pressed(params KeyboardKeys[] input)
        {
            List<bool> flags = new List<bool>();
            for (int i = 0; i < input.Length; i++) 
            {
                flags.Add(false);
            }
            int index = 0;
            foreach(KeyboardKeys key in input)
            {
                if (currentState.IsKeyDown(KeyBindings[key]) && previousState.IsKeyDown(KeyBindings[key]))
                {
                    flags[index] = true;
                }
                index++;
            }
            if (flags.TrueForAll(i => i == true)) return true;
            return false;
        }

        public bool Clicked(KeyboardKeys input)
        {
            bool flag = false;
            if (currentState.IsKeyUp(KeyBindings[input]) && !previousState.IsKeyUp(KeyBindings[input]))
            {
                flag =  true;
            }
            if (previousState.GetPressedKeys().Length > 1) flag = false;
            return flag;
        }
    }
}

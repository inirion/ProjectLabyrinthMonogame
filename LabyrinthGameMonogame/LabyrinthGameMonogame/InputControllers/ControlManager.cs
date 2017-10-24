using LabyrinthGameMonogame.GUI.Screens;
using Microsoft.Xna.Framework;

namespace LabyrinthGameMonogame.InputControllers
{
    class ControlManager: IControlManager
    {
        private KeyboardInput keyboard;
        private MouseInput mouse;
        

        public ControlManager(Game game)
        {
            this.keyboard = new KeyboardInput();
            this.mouse = new MouseInput(game);
            
        }

        KeyboardInput IControlManager.Keyboard => keyboard;
        MouseInput IControlManager.Mouse => mouse;
    }
}

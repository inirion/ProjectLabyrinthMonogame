namespace LabyrinthGameMonogame.Controllers
{
    class ControlManager
    {
        private KeyboardInput keyboard;
        private MouseInput mouse;

        public KeyboardInput Keyboard
        {
            get {
                return keyboard;
            }
        }

        public MouseInput Mouse
        {
            get
            {
                return mouse;
            }
        }

        private static ControlManager instance;
        private ControlManager()
        {
            keyboard = new KeyboardInput();
            mouse = new MouseInput();
        }

        public static ControlManager Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new ControlManager();
                }
                return instance;
            }
        }
    }
}

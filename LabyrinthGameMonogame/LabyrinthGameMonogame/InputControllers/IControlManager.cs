using LabyrinthGameMonogame.GUI.Screens;

namespace LabyrinthGameMonogame.InputControllers
{
    interface IControlManager
    {
        KeyboardInput Keyboard { get; }
        MouseInput Mouse { get; }
    }
}

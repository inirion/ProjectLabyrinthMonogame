using LabyrinthGameMonogame.Enums;

namespace LabyrinthGameMonogame.InputControllers
{
    public interface IMouseInput
    {
        bool Pressed(MouseKeys key);
        bool Clicked(MouseKeys key);
    }
}

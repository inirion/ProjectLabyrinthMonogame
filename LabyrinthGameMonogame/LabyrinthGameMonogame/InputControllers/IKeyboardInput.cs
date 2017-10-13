using LabyrinthGameMonogame.Enums;

namespace LabyrinthGameMonogame.InputControllers
{
    public interface IKeyboardInput
    {
        bool Pressed(params KeyboardKeys[] input);
        bool Clicked(KeyboardKeys input);
    }
}

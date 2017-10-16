using LabyrinthGameMonogame.Enums;

namespace LabyrinthGameMonogame.InputControllers
{
    public interface IKeyboardInput
    {
        bool Pressed(bool supportSingleKey, params KeyboardKeys[] input);
        bool Clicked(KeyboardKeys input);
    }
}

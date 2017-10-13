using LabyrinthGameMonogame.Enums;

namespace LabyrinthGameMonogame.Controllers
{
    public interface IKeyboardInput
    {
        bool Pressed(params KeyboardKeys[] input);
        bool Clicked(KeyboardKeys input);
    }
}

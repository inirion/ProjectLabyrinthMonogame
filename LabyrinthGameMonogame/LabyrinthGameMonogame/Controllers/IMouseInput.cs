using LabyrinthGameMonogame.Enums;

namespace LabyrinthGameMonogame.Controllers
{
    public interface IMouseInput
    {
        bool Pressed(MouseKeys key);
        bool Clicked(MouseKeys key);
    }
}

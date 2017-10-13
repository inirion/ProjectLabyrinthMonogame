using Microsoft.Xna.Framework.Graphics;

namespace LabyrinthGameMonogame.GUI.Screens
{
    interface IScreen
    {
        void Update();
        void Draw(SpriteBatch spriteBatch);
    }
}

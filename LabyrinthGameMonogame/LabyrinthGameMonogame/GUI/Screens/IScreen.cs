using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LabyrinthGameMonogame.GUI.Screens
{
    interface IScreen
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}

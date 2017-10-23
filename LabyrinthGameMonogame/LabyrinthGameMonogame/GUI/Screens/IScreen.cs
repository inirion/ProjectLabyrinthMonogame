using LabyrinthGameMonogame.GUI.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LabyrinthGameMonogame.GUI.Screens
{
    interface IScreen
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void CentreButtons(); 
    }
}

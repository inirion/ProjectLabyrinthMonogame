using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LabyrinthGameMonogame.InputControllers;
using LabyrinthGameMonogame.Enums;
using Microsoft.Xna.Framework.Content;

namespace LabyrinthGameMonogame.GUI.Screens
{
    class GameScreen : IScreen
    {
        public GameScreen(ContentManager content)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public void Update(GameTime gameTime)
        {
            if (ControlManager.Instance.Keyboard.Clicked(KeyboardKeys.Back))
            {
                ScreenManager.Instance.ActiveScreenType = ScreenTypes.Pause;
            }
        }
    }
}

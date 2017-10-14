using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LabyrinthGameMonogame.InputControllers;
using LabyrinthGameMonogame.Enums;
using Microsoft.Xna.Framework.Content;
using LabyrinthGameMonogame.GameFolder;

namespace LabyrinthGameMonogame.GUI.Screens
{
    class GameScreen : IScreen
    {
        LabirynthGame game;

        public GameScreen(ContentManager content)
        {
            game = new LabirynthGame();
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            game.Draw();
        }

        public void Update(GameTime gameTime)
        {
            game.ResetGame();
            if (ControlManager.Instance.Keyboard.Clicked(KeyboardKeys.Back))
            {
                ScreenManager.Instance.ActiveScreenType = ScreenTypes.Pause;
                GameManager.Instance.IsGameRunning = false;
            }

            game.Update();
        }
    }
}

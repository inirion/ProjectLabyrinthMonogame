using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LabyrinthGameMonogame.InputControllers;
using LabyrinthGameMonogame.Enums;
using Microsoft.Xna.Framework.Content;
using LabyrinthGameMonogame.GameFolder;
using System.Diagnostics;
using System.Collections.Generic;
using LabyrinthGameMonogame.GUI.Buttons;

namespace LabyrinthGameMonogame.GUI.Screens
{
    class GameScreen : ScreenDrawable
    {
        LabirynthGame labirynthGame;
        IGameManager gameManager;

        public GameScreen(Game game) : base(game)
        {
            gameManager = (IGameManager)game.Services.GetService(typeof(IGameManager));
            labirynthGame = new LabirynthGame(game);
        }

        public override void Draw(GameTime gameTime)
        {
            labirynthGame.ResetGame();
            labirynthGame.Draw();
        }

        public override void Update(GameTime gameTime)
        {
            if (controlManager.Keyboard.Clicked(KeyboardKeys.Back))
            {
                screenManager.ActiveScreenType = ScreenTypes.Pause;

                gameManager.IsGameRunning = false;
            }
            labirynthGame.Update(gameTime);
            base.Update(gameTime);
        }
    }
}

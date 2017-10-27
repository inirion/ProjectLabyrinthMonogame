using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.Factories;
using LabyrinthGameMonogame.GameFolder;
using LabyrinthGameMonogame.GUI.Buttons;
using LabyrinthGameMonogame.InputControllers;
using LabyrinthGameMonogame.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LabyrinthGameMonogame.GUI.Screens
{
    class PauseScreen : ScreenDrawable
    {
        private IGameManager gameManager;
        public PauseScreen(Game game) : base(game)
        {
            gameManager = (IGameManager)game.Services.GetService(typeof(IGameManager));
            buttons = ButtonFactory.CreatePauseButtons();
        }

        protected override void LoadContent()
        {
            SetupButtons();
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            AssetHolder.Instance.GandalfMusicInstance.Pause();
            foreach (Button btn in buttons)
            {
                btn.Color = Color.White;
                if (controlManager.Mouse.Hovered(btn.ButtonRect) && btn.Enabled)
                {
                    btn.Color = Color.Red;
                }

                if (controlManager.Mouse.Clicked(MouseKeys.LeftButton, btn.ButtonRect) && btn.Enabled)
                {
                    screenManager.ActiveScreenType = btn.GoesTo;
                    if (btn.GoesTo == ScreenTypes.Game)
                    {
                        gameManager.IsGameRunning = true;
                        controlManager.Mouse.CentrePosition(new Vector2(screenManager.Dimensions.X / 2, screenManager.Dimensions.Y / 2));
                        if(gameManager.Type == LabiryntType.Prim)
                            AssetHolder.Instance.GandalfMusicInstance.Resume();
                    }
                    if (btn.GoesTo == ScreenTypes.ModelLabirynthLevel || btn.GoesTo == ScreenTypes.VertexLabirynthLevel)
                    {
                        gameManager.IsGameRunning = false;
                        gameManager.ResetGame = true;
                        if (gameManager.Type == LabiryntType.Prim)
                            AssetHolder.Instance.GandalfMusicInstance.Stop();
                    }
                    btn.Color = Color.White;
                }
            }
            if (controlManager.Keyboard.Clicked(KeyboardKeys.Back))
            {
                gameManager.IsGameRunning = true;
                controlManager.Mouse.CentrePosition(new Vector2(screenManager.Dimensions.X / 2, screenManager.Dimensions.Y / 2));
                screenManager.ActiveScreenType = ScreenTypes.Game;
                AssetHolder.Instance.GandalfMusicInstance.Play();
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            foreach (Button btn in buttons)
            {
                spriteBatch.DrawString(btn.Font, btn.Text, new Vector2(btn.ButtonRect.X, btn.ButtonRect.Y), btn.Color);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

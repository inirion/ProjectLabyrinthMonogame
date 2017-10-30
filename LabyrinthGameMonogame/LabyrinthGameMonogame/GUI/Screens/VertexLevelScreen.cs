using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.Factories;
using LabyrinthGameMonogame.GameFolder;
using LabyrinthGameMonogame.GUI.Buttons;
using Microsoft.Xna.Framework;

namespace LabyrinthGameMonogame.GUI.Screens
{
    class VertexLevelScreen : ScreenDrawable
    {
        private IGameManager gameManager;
        public VertexLevelScreen(Game game) : base(game)
        {
            gameManager = (IGameManager)game.Services.GetService(typeof(IGameManager));
            buttons = ButtonFactory.CreateLevelButtonsVertex();
        }

        protected override void LoadContent()
        {
            SetupButtons();
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
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
                    gameManager.DifficultyLevel = btn.DifficultyLevel;

                    btn.Color = Color.White;
                }
                
            }
            if (controlManager.Keyboard.Clicked(KeyboardKeys.Back))
            {
                screenManager.ActiveScreenType = ScreenTypes.LevelType;
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

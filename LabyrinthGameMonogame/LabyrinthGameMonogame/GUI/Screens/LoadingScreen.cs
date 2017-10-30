using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.Factories;
using LabyrinthGameMonogame.GameFolder;
using LabyrinthGameMonogame.GUI.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LabyrinthGameMonogame.GUI.Screens
{
    class LoadingScreen : ScreenDrawable
    {
        private double timeToDisplay;
        private IGameManager gameManager;
        public LoadingScreen(Game game): base(game)
        {
            gameManager = (IGameManager)game.Services.GetService(typeof(IGameManager));
            buttons = ButtonFactory.CreateLoadingButtons();
            screenManager.IsTransitioning = true;
            timeToDisplay = 2.0f;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            foreach (Button btn in buttons)
            {
                spriteBatch.DrawString(
                    btn.Font,
                    btn.Text,
                    new Vector2(btn.ButtonRect.X + btn.ButtonRect.Width / 2, btn.ButtonRect.Y + btn.ButtonRect.Height / 2),
                    btn.Color,
                    0,
                    new Vector2(btn.ButtonRect.Width / 2, btn.ButtonRect.Height / 2),
                    1.0f,
                    SpriteEffects.None,
                    0);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }


        public override void Update(GameTime gameTime)
        {
            timeToDisplay -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeToDisplay < 0)
            {
                screenManager.IsTransitioning = false;
                gameManager.ResetGame = true;
                gameManager.IsGameRunning = true;
                timeToDisplay = 2.0f;
                screenManager.ActiveScreenType = ScreenTypes.Game;
               
            }
            base.Update(gameTime);
        }

        public override void SetupButtons()
        {
            float gap = buttons[0].Font.MeasureString(buttons[0].Text).Y + buttons[0].Font.MeasureString(buttons[0].Text).Y / 2;
            float offset = 0;
            foreach (Button btn in buttons)
            {
                btn.ButtonRect = new Rectangle(
                    (int)((screenManager.Dimensions.X / 2) - (btn.Font.MeasureString(btn.Text).X) / 2),
                    (int)(gap*2 - (btn.Font.MeasureString(btn.Text).Y) + offset),
                    (int)(btn.Font.MeasureString(btn.Text).X),
                    (int)(btn.Font.MeasureString(btn.Text).Y)
                    );
                offset += gap;
            }
        }

        protected override void LoadContent()
        {
            SetupButtons();
            base.LoadContent();
        }
    }
}

using LabyrinthGameMonogame.GUI.Buttons;
using LabyrinthGameMonogame.Factories;
using Microsoft.Xna.Framework;
using LabyrinthGameMonogame.InputControllers;
using LabyrinthGameMonogame.Enums;

namespace LabyrinthGameMonogame.GUI.Screens
{
    class InfoScreen : ScreenDrawable
    {
        public InfoScreen(Game game) : base(game)
        {
            buttons = ButtonFactory.CreateInfoButtons();
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
                    if (btn.GoesTo == ScreenTypes.Exit) screenManager.IsTransitioning = true;
                    btn.Color = Color.White;
                }
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

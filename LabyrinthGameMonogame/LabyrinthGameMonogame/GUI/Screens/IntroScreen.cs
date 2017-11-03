using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.Factories;
using LabyrinthGameMonogame.GUI.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LabyrinthGameMonogame.GUI.Screens
{
    class IntroScreen : ScreenDrawable
    {

        private double timeToDisplay;
        private float procentage;
        private float zoom;
        public IntroScreen(Game game): base(game)
        {
            procentage = 1;
            zoom = 1;
            buttons = ButtonFactory.CreateIntroButtons();
            screenManager.IsTransitioning = true;
            timeToDisplay = 2;
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
                    btn.Color * procentage,
                    0,
                    new Vector2(btn.ButtonRect.Width / 2, btn.ButtonRect.Height / 2),
                    zoom,
                    SpriteEffects.None,
                    0);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }


        public override void Update(GameTime gameTime)
        {
            timeToDisplay -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeToDisplay > 0)
            {
                procentage -= 0.01f;
                zoom += 0.03f;
            }
            else
            {
                screenManager.IsTransitioning = false;
                screenManager.ActiveScreenType = ScreenTypes.MainMenu;
            }
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            SetupButtons();
            base.LoadContent();
        }
    }
}

using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.Factories;
using LabyrinthGameMonogame.GUI.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthGameMonogame.GUI.Screens
{
    class ExitScreen : IScreen
    {
        private double timeToDisplay;
        private float procentage;
        private float zoom;
        private List<Button> buttons;
        public ExitScreen(ContentManager content)
        {
            procentage = 1;
            zoom = 1;
            buttons = ButtonFactory.CreateExitButtons();
            ScreenManager.Instance.IsTransitioning = true;
            timeToDisplay = 2;
        }
        public void CentreButtons()
        {
            float gap = buttons[0].Font.MeasureString(buttons[0].Text).Y + buttons[0].Font.MeasureString(buttons[0].Text).Y / 2;
            float offset = 0;
            foreach (Button btn in buttons)
            {
                btn.ButtonRect = new Rectangle(
                    (int)((ScreenManager.Instance.Dimensions.X / 2) - (btn.Font.MeasureString(btn.Text).X) / 2),
                    (int)(ScreenManager.Instance.Dimensions.Y / 2 - (btn.Font.MeasureString(btn.Text).Y) + offset),
                    (int)(btn.Font.MeasureString(btn.Text).X),
                    (int)(btn.Font.MeasureString(btn.Text).Y)
                    );
                offset += gap;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
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
        }

        public void Update(GameTime gameTime)
        {
            timeToDisplay -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeToDisplay > 0)
            {
                procentage -= 0.01f;
                zoom += 0.03f;
            }
            else
            {
                ScreenManager.Instance.IsTransitioning = false;
                ScreenManager.Instance.ActiveScreenType = ScreenTypes.Exit;
            }

        }
    }
}

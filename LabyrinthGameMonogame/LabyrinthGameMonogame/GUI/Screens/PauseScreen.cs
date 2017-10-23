using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.Factories;
using LabyrinthGameMonogame.GameFolder;
using LabyrinthGameMonogame.GUI.Buttons;
using LabyrinthGameMonogame.InputControllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LabyrinthGameMonogame.GUI.Screens
{
    class PauseScreen : IScreen
    {
        private List<Button> buttons;
        public PauseScreen(ContentManager content)
        {
            buttons = ButtonFactory.CreatePauseButtons();
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
        public void Update(GameTime gameTime)
        {
            foreach (Button btn in buttons)
            {
                btn.Color = Color.White;
                if (ControlManager.Instance.Mouse.Hovered(btn.ButtonRect) && btn.Enabled)
                {
                    btn.Color = Color.Red;
                }

                if (ControlManager.Instance.Mouse.Clicked(MouseKeys.LeftButton, btn.ButtonRect) && btn.Enabled)
                {
                    ScreenManager.Instance.ActiveScreenType = btn.GoesTo;
                    if (btn.GoesTo == ScreenTypes.Game)
                    {
                        GameManager.Instance.IsGameRunning = true;
                        ControlManager.Instance.Mouse.CentrePosition();
                    }
                    if (btn.GoesTo == ScreenTypes.MainMenu)
                    {
                        GameManager.Instance.IsGameRunning = false;
                        GameManager.Instance.ResetGame = true;
                    }
                    btn.Color = Color.White;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (Button btn in buttons)
            {
                spriteBatch.DrawString(btn.Font, btn.Text, new Vector2(btn.ButtonRect.X, btn.ButtonRect.Y), btn.Color);
            }

            spriteBatch.End();
        }


    }
}

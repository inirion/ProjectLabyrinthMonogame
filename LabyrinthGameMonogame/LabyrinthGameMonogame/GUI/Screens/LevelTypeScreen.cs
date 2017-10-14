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
    class LevelTypeScreen : IScreen
    {
        private List<Button> buttons;
        public LevelTypeScreen(ContentManager content)
        {
            buttons = ButtonFactory.CreateLevelButtons();
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
                    if (btn.GoesTo == ScreenTypes.Game) GameManager.Instance.IsGameRunning = true;
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

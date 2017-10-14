using System.Collections.Generic;
using LabyrinthGameMonogame.GUI.Buttons;
using LabyrinthGameMonogame.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using LabyrinthGameMonogame.InputControllers;
using LabyrinthGameMonogame.Enums;

namespace LabyrinthGameMonogame.GUI.Screens
{
    class MainMenuScreen : IScreen
    {
        private List<Button> buttons;
        public MainMenuScreen(ContentManager content)
        {
            buttons = ButtonFactory.CreateMainMenuButtons();
        }

        public void Update(GameTime gameTime)
        {
            foreach(Button btn in buttons)
            {
                btn.Color = Color.White;
                if (ControlManager.Instance.Mouse.Hovered(btn.ButtonRect) && btn.Enabled)
                {
                    btn.Color = Color.Red;
                }

                if (ControlManager.Instance.Mouse.Clicked(MouseKeys.LeftButton, btn.ButtonRect) && btn.Enabled)
                {
                    ScreenManager.Instance.ActiveScreenType = btn.GoesTo;
                    if (btn.GoesTo == ScreenTypes.Exit) ScreenManager.Instance.IsTransitioning = true;
                    btn.Color = Color.White;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach(Button btn in buttons)
            {
                spriteBatch.DrawString(btn.Font, btn.Text, new Vector2(btn.ButtonRect.X, btn.ButtonRect.Y), btn.Color);
            }

            spriteBatch.End();
        }
    }
}

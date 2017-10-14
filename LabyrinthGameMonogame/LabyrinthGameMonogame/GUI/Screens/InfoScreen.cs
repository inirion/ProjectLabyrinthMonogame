using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using LabyrinthGameMonogame.GUI.Buttons;
using LabyrinthGameMonogame.Factories;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using LabyrinthGameMonogame.InputControllers;
using LabyrinthGameMonogame.Enums;

namespace LabyrinthGameMonogame.GUI.Screens
{
    class InfoScreen : IScreen
    {
        private List<Button> buttons;

        public InfoScreen(ContentManager content)
        {
            buttons = ButtonFactory.CreateInfoButtons();
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
                    btn.Color = Color.White;
                }
            }
        }
    }
}

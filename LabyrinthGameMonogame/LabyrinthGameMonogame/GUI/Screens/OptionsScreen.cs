using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LabyrinthGameMonogame.GUI.Buttons;
using Microsoft.Xna.Framework.Content;
using LabyrinthGameMonogame.Factories;
using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.InputControllers;
using System.Diagnostics;

namespace LabyrinthGameMonogame.GUI.Screens
{

    class OptionsScreen : IScreen
    {
        private List<Button> buttons;
        private float tempSencitivity;
        private Vector2 tempDimencions;
        int index;
        public OptionsScreen(ContentManager content)
        {
            buttons = ButtonFactory.CreateOptionsButtons();
            index = 0;
            tempSencitivity = ControlManager.Instance.Mouse.Sensitivity*1000;
            tempDimencions = ScreenManager.Instance.Dimensions;
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
                    if(btn.GoesTo != ScreenManager.Instance.ActiveScreenType && btn.GoesTo != ScreenTypes.OptionsResolution&& btn.GoesTo != ScreenTypes.OptionsSensitivity)
                        ScreenManager.Instance.ActiveScreenType = btn.GoesTo;
                    if (btn.Text.Contains("+") && btn.GoesTo == ScreenTypes.OptionsSensitivity)
                    {
                        if (tempSencitivity < 100)
                        {
                            buttons[4].Text = ChangeSensitivity(10);
                        }
                    }
                    if (btn.Text.Contains("+") && btn.GoesTo == ScreenTypes.OptionsResolution)
                    {
                        if (index + 1 < ScreenManager.Instance.Resolutions.Count)
                        {
                            buttons[1].Text = ChangeResolution(index+1,1);
                        }
                    }
                    if (btn.Text.Contains("-") && btn.GoesTo == ScreenTypes.OptionsSensitivity)
                    {
                        if (tempSencitivity > 10)
                        {
                            buttons[4].Text = ChangeSensitivity(-10);
                        }
                    }
                    if (btn.Text.Contains("-") && btn.GoesTo == ScreenTypes.OptionsResolution)
                    {
                        if (index - 1 >= 0)
                        {
                            buttons[1].Text = ChangeResolution(index - 1, -1);
                        }
                    }
                    if (btn.Text.Contains("Fullscreen"))
                    {
                        btn.Text = ChangeScreenMode();
                    }
                    if (btn.Text.Contains("Apply"))
                    {
                        ScreenManager.Instance.ChangeScreenMode();
                        ControlManager.Instance.Mouse.Sensitivity = tempSencitivity/1000;
                        ScreenManager.Instance.Dimensions = tempDimencions;
                        ScreenManager.Instance.ChangeResolution();
                    }
                    btn.Color = Color.White;
                }
            }
        }
        private string ChangeScreenMode()
        {
            ScreenManager.Instance.Fullscreen = !ScreenManager.Instance.Fullscreen;
            return "Fullscreen: " + ScreenManager.Instance.Fullscreen.ToString();
        }

        private string ChangeSensitivity(float amount)
        {
            tempSencitivity += amount;
            return "Sensitivity: " + tempSencitivity + "%";
        }

        private string ChangeResolution(int i,int value)
        {
            if(ScreenManager.Instance.Resolutions[i].Width != tempDimencions.X
                || ScreenManager.Instance.Resolutions[i].Height != tempDimencions.Y)
            {
                tempDimencions = new Vector2(ScreenManager.Instance.Resolutions[i].Width, ScreenManager.Instance.Resolutions[i].Height);
                index+= value;
                
            }
            return "Resolution: " + (int)tempDimencions.X + "x" + (int)tempDimencions.Y;
        }
        /// <summary>
        /// shit that must be done prob can be easy avoided just dont have time =(
        /// </summary>
        public void CentreButtons()
        {
            float gap = buttons[1].Font.MeasureString(buttons[1].Text).Y + buttons[1].Font.MeasureString(buttons[1].Text).Y / 2;
            float offset = 0;

            PlaceButton(buttons[1], offset);
            offset += gap;
            PlaceButton(buttons[4], offset);
            offset += gap;
            PlaceButton(buttons[6], offset);
            offset += gap;
            PlaceButton(buttons[7], offset);
            offset += gap;
            PlaceButton(buttons[8], offset);

            PlaceButtonMinus(buttons[0], buttons[1], gap);
            PlaceButtonPlus(buttons[2], buttons[1], gap);
            PlaceButtonMinus(buttons[3], buttons[4], gap);
            PlaceButtonPlus(buttons[5], buttons[4], gap);


        }
        private void PlaceButton(Button btn, float offset)
        {
            btn.ButtonRect = new Rectangle(
                    (int)((ScreenManager.Instance.Dimensions.X / 2) - (btn.Font.MeasureString(btn.Text).X) / 2),
                    (int)(ScreenManager.Instance.Dimensions.Y / 2 - (btn.Font.MeasureString(btn.Text).Y) + offset),
                    (int)(btn.Font.MeasureString(btn.Text).X),
                    (int)(btn.Font.MeasureString(btn.Text).Y)
                    );
        }
        private void PlaceButtonPlus(Button btnPlus, Button resButton, float offset)
        {
            btnPlus.ButtonRect = new Rectangle(
                    (int)(resButton.ButtonRect.X + resButton.ButtonRect.Width + offset),
                    (int)(resButton.ButtonRect.Y),
                    (int)(btnPlus.Font.MeasureString(btnPlus.Text).X),
                    (int)(btnPlus.Font.MeasureString(btnPlus.Text).Y)
                    );
        }
        private void PlaceButtonMinus(Button btnMinus, Button resButton, float offset)
        {
            btnMinus.ButtonRect = new Rectangle(
                    (int)(resButton.ButtonRect.X - offset),
                    (int)(resButton.ButtonRect.Y),
                    (int)(btnMinus.Font.MeasureString(btnMinus.Text).X),
                    (int)(btnMinus.Font.MeasureString(btnMinus.Text).Y)
                    );
        }
    }
}

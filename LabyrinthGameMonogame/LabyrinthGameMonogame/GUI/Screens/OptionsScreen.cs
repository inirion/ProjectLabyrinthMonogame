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

    class OptionsScreen : ScreenDrawable
    {
        private float tempSencitivity;
        private Vector2 tempDimencions;
        bool tempFullScreen;
        int index;
        public OptionsScreen(Game game) : base(game)
        {
            buttons = ButtonFactory.CreateOptionsButtons();
            index = 0;
            tempSencitivity = controlManager.Mouse.Sensitivity * 1000;
            tempDimencions = screenManager.Dimensions;
            tempFullScreen = screenManager.Fullscreen;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            foreach (Button btn in buttons)
            {
                spriteBatch.DrawString(btn.Font, btn.Text, new Vector2(btn.ButtonRect.X, btn.ButtonRect.Y), btn.Color);
            }

            spriteBatch.End();
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
                    if (btn.GoesTo != screenManager.ActiveScreenType && btn.GoesTo != ScreenTypes.OptionsResolution && btn.GoesTo != ScreenTypes.OptionsSensitivity)
                    {
                        reset();
                        screenManager.ActiveScreenType = btn.GoesTo;
                    }
                        
                    if (btn.Text.Contains("+") && btn.GoesTo == ScreenTypes.OptionsSensitivity)
                    {
                        if (tempSencitivity < 100)
                        {
                            buttons[4].Text = ChangeSensitivity(10);
                        }
                    }
                    if (btn.Text.Contains("+") && btn.GoesTo == ScreenTypes.OptionsResolution)
                    {
                        if (index + 1 < screenManager.Resolutions.Count)
                        {
                            buttons[1].Text = ChangeResolution(index + 1, 1);
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
                        screenManager.Fullscreen = tempFullScreen;
                        screenManager.ChangeScreenMode();
                        controlManager.Mouse.Sensitivity = tempSencitivity / 1000;
                        screenManager.Dimensions = tempDimencions;
                        screenManager.ChangeResolution();
                    }
                    btn.Color = Color.White;
                }
            }
            if (controlManager.Keyboard.Clicked(KeyboardKeys.Back))
            {
                reset();
                screenManager.ActiveScreenType = ScreenTypes.MainMenu;
            }
        }

        private void reset()
        {
            tempSencitivity = controlManager.Mouse.Sensitivity * 1000;
            tempDimencions = screenManager.Dimensions;
            tempFullScreen = screenManager.Fullscreen;
            foreach (Button btn in buttons)
            {
                if (btn.Text.Contains("Resolution"))
                {
                    btn.Text = "Resolution: " + (int)tempDimencions.X + "x" + (int)tempDimencions.Y;
                }
                if (btn.Text.Contains("Sensitivity"))
                {
                    btn.Text = "Sensitivity: " + tempSencitivity + "%";
                }
                if (btn.Text.Contains("Fullscreen"))
                {
                    btn.Text = "Fullscreen: " + tempFullScreen.ToString();
                }
            }
        }
        private string ChangeScreenMode()
        {
            tempFullScreen = !tempFullScreen;
            return "Fullscreen: " + tempFullScreen.ToString();
        }

        private string ChangeSensitivity(float amount)
        {
            tempSencitivity += amount;
            return "Sensitivity: " + tempSencitivity + "%";
        }

        private string ChangeResolution(int i, int value)
        {
            if (screenManager.Resolutions[i].Width != tempDimencions.X
                || screenManager.Resolutions[i].Height != tempDimencions.Y)
            {
                tempDimencions = new Vector2(screenManager.Resolutions[i].Width, screenManager.Resolutions[i].Height);
                index += value;
            }
            return "Resolution: " + (int)tempDimencions.X + "x" + (int)tempDimencions.Y;
        }
        
        
                
        public override void SetupButtons()
        {
            buttons[1].Text = "Resolution: " + (int)screenManager.Dimensions.X + "x" + (int)screenManager.Dimensions.Y;
            buttons[4].Text = "Sensitivity: " + (int)(controlManager.Mouse.Sensitivity * 1000) + "%";
            buttons[6].Text = "Fullscreen: " + screenManager.Fullscreen.ToString();
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
                    (int)((screenManager.Dimensions.X / 2) - (btn.Font.MeasureString(btn.Text).X) / 2),
                    (int)(screenManager.Dimensions.Y / 2 - (btn.Font.MeasureString(btn.Text).Y) + offset),
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

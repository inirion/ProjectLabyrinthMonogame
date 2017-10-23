using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.GUI.Buttons;
using LabyrinthGameMonogame.GUI.Screens;
using LabyrinthGameMonogame.InputControllers;
using LabyrinthGameMonogame.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LabyrinthGameMonogame.Factories
{
    static class ButtonFactory
    {

        public static void Initialize(ContentManager content)
        {
        }

        public static List<Button> CreateMainMenuButtons()
        {
            List<Button> buttons = new List<Button>();
            buttons.Add(new Button("New Game",ScreenTypes.LevelType, AssetHolder.Instance.Font, true));
            buttons.Add(new Button("Options", ScreenTypes.Options, AssetHolder.Instance.Font, true));
            buttons.Add(new Button("Info", ScreenTypes.Info, AssetHolder.Instance.Font,true));
            buttons.Add(new Button("Exit", ScreenTypes.Exit, AssetHolder.Instance.Font,true));

            CentreButtons(buttons);

            return buttons;
        }

        public static List<Button> CreateOptionsButtons()
        {
            List<Button> buttons = new List<Button>();
            buttons.Add(new Button("-", ScreenTypes.OptionsResolution, AssetHolder.Instance.Font, true));
            buttons.Add(new Button("Resolution: " + (int)ScreenManager.Instance.Dimensions.X + "x" + (int)ScreenManager.Instance.Dimensions.Y, ScreenTypes.Options, AssetHolder.Instance.Font, false));
            buttons.Add(new Button("+", ScreenTypes.OptionsResolution, AssetHolder.Instance.Font, true));
            buttons.Add(new Button("-", ScreenTypes.OptionsSensitivity, AssetHolder.Instance.Font, true));
            buttons.Add(new Button("Sensitivity: " + (int)(ControlManager.Instance.Mouse.Sensitivity*1000) + "%", ScreenTypes.Options, AssetHolder.Instance.Font, false));
            buttons.Add(new Button("+", ScreenTypes.OptionsSensitivity, AssetHolder.Instance.Font, true));
            buttons.Add(new Button("Fullscreen: "+ ScreenManager.Instance.Fullscreen.ToString(), ScreenTypes.Options, AssetHolder.Instance.Font, true));
            buttons.Add(new Button("Apply", ScreenTypes.Options, AssetHolder.Instance.Font, true));
            buttons.Add(new Button("Back", ScreenTypes.MainMenu, AssetHolder.Instance.Font, true));

            CentreOptionButtons(buttons);

            return buttons;
        }

        public static List<Button> CreateLevelButtons()
        {
            List<Button> buttons = new List<Button>();
            buttons.Add(new Button("Easy", ScreenTypes.Game, AssetHolder.Instance.Font, true, DifficultyLevel.Easy));
            buttons.Add(new Button("Medium", ScreenTypes.Game, AssetHolder.Instance.Font, true, DifficultyLevel.Medium));
            buttons.Add(new Button("Hard", ScreenTypes.Game, AssetHolder.Instance.Font, true, DifficultyLevel.Hard));
            buttons.Add(new Button("Back", ScreenTypes.MainMenu, AssetHolder.Instance.Font, true));

            CentreButtons(buttons);

            return buttons;
        }

        public static List<Button> CreateInfoButtons()
        {
            List<Button> buttons = new List<Button>();
            buttons.Add(new Button("Authors:", ScreenTypes.Info, AssetHolder.Instance.Font, false));
            buttons.Add(new Button("Grzegorz Kokoszka", ScreenTypes.Info, AssetHolder.Instance.Font, false));
            buttons.Add(new Button("Daniel Splawski", ScreenTypes.Info, AssetHolder.Instance.Font, false));
            buttons.Add(new Button("Kuba Wozniak", ScreenTypes.Info, AssetHolder.Instance.Font, false));
            buttons.Add(new Button("Back", ScreenTypes.MainMenu, AssetHolder.Instance.Font, true));

            CentreButtons(buttons);

            return buttons;
        }

        public static List<Button> CreateIntroButtons()
        {
            List<Button> buttons = new List<Button>();
            buttons.Add(new Button("The Labirynth Game =)", ScreenTypes.Info, AssetHolder.Instance.Font, false));

            CentreButtons(buttons);

            return buttons;
        }

        public static List<Button> CreateExitButtons()
        {
            List<Button> buttons = new List<Button>();
            buttons.Add(new Button("Thanks For Playing !! xD", ScreenTypes.Info, AssetHolder.Instance.Font, false));

            CentreButtons(buttons);

            return buttons;
        }

        public static List<Button> CreatePauseButtons()
        {
            List<Button> buttons = new List<Button>();
            buttons.Add(new Button("Continue", ScreenTypes.Game, AssetHolder.Instance.Font, true));
            buttons.Add(new Button("Exit", ScreenTypes.MainMenu, AssetHolder.Instance.Font, true));

            CentreButtons(buttons);

            return buttons;
        }

        private static void CentreButtons(List<Button> btns)
        {
            float gap = btns[0].Font.MeasureString(btns[0].Text).Y + btns[0].Font.MeasureString(btns[0].Text).Y / 2;
            float offset = 0;
            foreach (Button btn in btns)
            {
                btn.ButtonRect = new Rectangle(
                    (int)((ScreenManager.Instance.Dimensions.X / 2) - (btn.Font.MeasureString(btn.Text).X)/2),
                    (int)(ScreenManager.Instance.Dimensions.Y / 2 - (btn.Font.MeasureString(btn.Text).Y) + offset),
                    (int)(btn.Font.MeasureString(btn.Text).X),
                    (int)(btn.Font.MeasureString(btn.Text).Y)
                    );
                offset += gap;
            }
        }
        private static void CentreOptionButtons(List<Button> btns)
        {
            float gap = btns[1].Font.MeasureString(btns[1].Text).Y + btns[1].Font.MeasureString(btns[1].Text).Y / 2;
            float offset = 0;

            PlaceButton(btns[1], offset);
            offset += gap;
            PlaceButton(btns[4], offset);
            offset += gap;
            PlaceButton(btns[6], offset);
            offset += gap;
            PlaceButton(btns[7], offset);
            offset += gap;
            PlaceButton(btns[8], offset);

            PlaceButtonMinus(btns[0], btns[1], gap);
            PlaceButtonPlus(btns[2], btns[1], gap);
            PlaceButtonMinus(btns[3], btns[4], gap);
            PlaceButtonPlus(btns[5], btns[4], gap);
            

        }
        private static void PlaceButton(Button btn,float offset)
        {
            btn.ButtonRect = new Rectangle(
                    (int)((ScreenManager.Instance.Dimensions.X / 2) - (btn.Font.MeasureString(btn.Text).X) / 2),
                    (int)(ScreenManager.Instance.Dimensions.Y / 2 - (btn.Font.MeasureString(btn.Text).Y) + offset),
                    (int)(btn.Font.MeasureString(btn.Text).X),
                    (int)(btn.Font.MeasureString(btn.Text).Y)
                    );
        }
        private static void PlaceButtonPlus(Button btnPlus, Button resButton, float offset)
        {
            btnPlus.ButtonRect = new Rectangle(
                    (int)(resButton.ButtonRect.X + resButton.ButtonRect.Width +offset),
                    (int)(resButton.ButtonRect.Y ),
                    (int)(btnPlus.Font.MeasureString(btnPlus.Text).X),
                    (int)(btnPlus.Font.MeasureString(btnPlus.Text).Y)
                    );
        }
        private static void PlaceButtonMinus(Button btnMinus, Button resButton, float offset)
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

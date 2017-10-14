using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.GUI.Buttons;
using LabyrinthGameMonogame.GUI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LabyrinthGameMonogame.Factories
{
    class ButtonFactory
    {
        public static List<Button> CreateMainMenuButtons(ContentManager content)
        {
            List<Button> buttons = new List<Button>();
            SpriteFont font = content.Load<SpriteFont>("Font");
            buttons.Add(new Button("New Game",ScreenTypes.LevelType, font,true));
            buttons.Add(new Button("Info", ScreenTypes.Info, font,true));
            buttons.Add(new Button("Exit", ScreenTypes.Exit, font,true));

            CentreButtons(buttons);

            return buttons;
        }

        public static List<Button> CreateLevelButtons(ContentManager content)
        {
            List<Button> buttons = new List<Button>();
            SpriteFont font = content.Load<SpriteFont>("Font");
            buttons.Add(new Button("Easy", ScreenTypes.Game, font, true));
            buttons.Add(new Button("Medium", ScreenTypes.Game, font, true));
            buttons.Add(new Button("Hard", ScreenTypes.Game, font, true));
            buttons.Add(new Button("Back", ScreenTypes.MainMenu, font, true));

            CentreButtons(buttons);

            return buttons;
        }

        public static List<Button> CreateInfoButtons(ContentManager content)
        {
            List<Button> buttons = new List<Button>();
            SpriteFont font = content.Load<SpriteFont>("Font");
            buttons.Add(new Button("Authors:", ScreenTypes.Info, font, false));
            buttons.Add(new Button("Grzegorz Kokoszka", ScreenTypes.Info, font, false));
            buttons.Add(new Button("Daniel Splawski", ScreenTypes.Info, font, false));
            buttons.Add(new Button("Kuba Wozniak", ScreenTypes.Info, font, false));
            buttons.Add(new Button("Back", ScreenTypes.MainMenu, font, true));

            CentreButtons(buttons);

            return buttons;
        }

        public static List<Button> CreateIntroButtons(ContentManager content)
        {
            List<Button> buttons = new List<Button>();
            SpriteFont font = content.Load<SpriteFont>("Font");
            buttons.Add(new Button("The Labirynth Game =)", ScreenTypes.Info, font, false));

            CentreButtons(buttons);

            return buttons;
        }

        public static List<Button> CreateExitButtons(ContentManager content)
        {
            List<Button> buttons = new List<Button>();
            SpriteFont font = content.Load<SpriteFont>("Font");
            buttons.Add(new Button("Thanks For Playing !! xD", ScreenTypes.Info, font, false));

            CentreButtons(buttons);

            return buttons;
        }

        public static List<Button> CreatePauseButtons(ContentManager content)
        {
            List<Button> buttons = new List<Button>();
            SpriteFont font = content.Load<SpriteFont>("Font");
            buttons.Add(new Button("Continue", ScreenTypes.Game, font, true));
            buttons.Add(new Button("Exit", ScreenTypes.MainMenu, font, true));

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
    }
}

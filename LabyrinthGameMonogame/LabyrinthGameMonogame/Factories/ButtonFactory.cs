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

        public static List<Button> CreateMainMenuButtons()
        {
            List<Button> buttons = new List<Button>();
            buttons.Add(new Button("New Game",ScreenTypes.LevelType, AssetHolder.Instance.Font, true));
            buttons.Add(new Button("Options", ScreenTypes.Options, AssetHolder.Instance.Font, true));
            buttons.Add(new Button("Info", ScreenTypes.Info, AssetHolder.Instance.Font,true));
            buttons.Add(new Button("Exit", ScreenTypes.Exit, AssetHolder.Instance.Font,true));

            return buttons;
        }

        public static List<Button> CreateOptionsButtons()
        {

            List<Button> buttons = new List<Button>();
            buttons.Add(new Button("-", ScreenTypes.OptionsResolution, AssetHolder.Instance.Font, true));
            buttons.Add(new Button("Resolution: ", ScreenTypes.Options, AssetHolder.Instance.Font, false));
            buttons.Add(new Button("+", ScreenTypes.OptionsResolution, AssetHolder.Instance.Font, true));
            buttons.Add(new Button("-", ScreenTypes.OptionsSensitivity, AssetHolder.Instance.Font, true));
            buttons.Add(new Button("Sensitivity: ", ScreenTypes.Options, AssetHolder.Instance.Font, false));
            buttons.Add(new Button("+", ScreenTypes.OptionsSensitivity, AssetHolder.Instance.Font, true));
            buttons.Add(new Button("Fullscreen: ", ScreenTypes.Options, AssetHolder.Instance.Font, true));
            buttons.Add(new Button("Apply", ScreenTypes.Options, AssetHolder.Instance.Font, true));
            buttons.Add(new Button("Back", ScreenTypes.MainMenu, AssetHolder.Instance.Font, true));

            return buttons;
        }

        public static List<Button> CreateLevelButtons()
        {
            List<Button> buttons = new List<Button>();
            buttons.Add(new Button("Easy", ScreenTypes.Game, AssetHolder.Instance.Font, true, DifficultyLevel.Easy));
            buttons.Add(new Button("Medium", ScreenTypes.Game, AssetHolder.Instance.Font, true, DifficultyLevel.Medium));
            buttons.Add(new Button("Hard", ScreenTypes.Game, AssetHolder.Instance.Font, true, DifficultyLevel.Hard));
            buttons.Add(new Button("Back", ScreenTypes.MainMenu, AssetHolder.Instance.Font, true));

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

            return buttons;
        }

        public static List<Button> CreateIntroButtons()
        {
            List<Button> buttons = new List<Button>();
            buttons.Add(new Button("The Labirynth Game =)", ScreenTypes.Info, AssetHolder.Instance.Font, false));

            return buttons;
        }

        public static List<Button> CreateExitButtons()
        {
            List<Button> buttons = new List<Button>();
            buttons.Add(new Button("Thanks For Playing !! xD", ScreenTypes.Info, AssetHolder.Instance.Font, false));

            return buttons;
        }

        public static List<Button> CreatePauseButtons()
        {
            List<Button> buttons = new List<Button>();
            buttons.Add(new Button("Continue", ScreenTypes.Game, AssetHolder.Instance.Font, true));
            buttons.Add(new Button("Exit", ScreenTypes.MainMenu, AssetHolder.Instance.Font, true));

            return buttons;
        }
    }
}

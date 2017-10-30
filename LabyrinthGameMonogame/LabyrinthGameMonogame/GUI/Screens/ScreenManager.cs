using LabyrinthGameMonogame.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LabyrinthGameMonogame.GUI.Screens
{
    class ScreenManager:IScreenManager
    {
        #region Variables
        private Dictionary<ScreenTypes, ScreenDrawable> activeScreen;
        private ScreenTypes activeScreenType;
        private ScreenTypes previousScreenType;
        Vector2 dimensions;
        List<DisplayMode> resolutions;
        private bool isTransitioning;
        GraphicsDeviceManager graphics;
        bool fullscreen;
        Game game;

        public ScreenManager(Game game, GraphicsDeviceManager graphics)
        {
            this.game = game;
            this.graphics = graphics;
        }
        #endregion

        public void Initialize()
        {
            
            isTransitioning = true;
            fullscreen = false;
            previousScreenType = ScreenTypes.Intro;
            activeScreenType = ScreenTypes.Intro;
            resolutions = new List<DisplayMode>();
            foreach (DisplayMode mode in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
            {
                if(mode.Width >=800)
                    resolutions.Add(mode);
            }
            resolutions.Sort(delegate (DisplayMode a, DisplayMode b)
            {
                int xdiff = a.Width.CompareTo(b.Width);
                if (xdiff != 0) return xdiff;
                else return a.Height.CompareTo(b.Height);
            });
            dimensions = new Vector2(resolutions[0].Width, resolutions[0].Height);
            
        }
        public void LoadContent()
        {
            activeScreen = new Dictionary<ScreenTypes, ScreenDrawable>()
            {
                {ScreenTypes.MainMenu,new MainMenuScreen(game) },
                {ScreenTypes.Info, new InfoScreen(game) },
                {ScreenTypes.Intro, new IntroScreen(game) },
                {ScreenTypes.LevelType, new LevelTypeScreen(game) },
                {ScreenTypes.Exit, new ExitScreen(game) },
                {ScreenTypes.Game, new GameScreen(game) },
                {ScreenTypes.Pause, new PauseScreen(game) },
                {ScreenTypes.Options, new OptionsScreen(game) },
                {ScreenTypes.GameOptions, new GameOptionScreen(game) },
                {ScreenTypes.ModelLabirynthLevel, new ModelLevelScreen(game) },
                {ScreenTypes.VertexLabirynthLevel, new VertexLevelScreen(game) }
            };
            foreach (ScreenDrawable screen in activeScreen.Values)
            {
                screen.Initialize();
            }
            ReCentreButtons();
        }
        public void ReCentreButtons()
        {
            foreach(ScreenTypes screen in activeScreen.Keys)
            {
                if (screen != ScreenTypes.Game)
                {
                    activeScreen[screen].SetupButtons();
                }
            }
        }

        public void ChangeScreenMode()
        {

            if (fullscreen)
            {
                graphics.IsFullScreen = true;
            }
            else
            {
                graphics.IsFullScreen = false;
            }
            graphics.ApplyChanges();

        }

        List<DisplayMode> IScreenManager.Resolutions { get => resolutions; }
        bool IScreenManager.Fullscreen { get => fullscreen; set => fullscreen = value; }
        ScreenTypes IScreenManager.ActiveScreenType { get => activeScreenType; set { previousScreenType = activeScreenType; activeScreenType = value; } }
        ScreenTypes IScreenManager.PreviousScreenType { get => previousScreenType; set => previousScreenType = value; }
        bool IScreenManager.IsTransitioning { get => isTransitioning; set => isTransitioning = value; }
        Vector2 IScreenManager.Dimensions { get => dimensions; set => dimensions = value; }
        GraphicsDeviceManager IScreenManager.Graphics { get => graphics; }

        public void ChangeResolution()
        {
            graphics.PreferredBackBufferWidth = (int)dimensions.X;
            graphics.PreferredBackBufferHeight = (int)dimensions.Y;
            graphics.ApplyChanges();
            ReCentreButtons();
        }

        public void Update(GameTime gameTime)
        {
            if (activeScreenType == ScreenTypes.Exit && !isTransitioning)
                game.Exit();
            activeScreen[activeScreenType].Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            activeScreen[activeScreenType].Draw(gameTime);
        }
    }
}

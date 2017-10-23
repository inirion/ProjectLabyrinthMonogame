using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace LabyrinthGameMonogame.GUI.Screens
{
    class ScreenManager
    {
        #region Variables
        private static ScreenManager instance;
        private Dictionary<ScreenTypes, IScreen> activeScreen;
        private ScreenTypes activeScreenType;
        private ScreenTypes previousScreenType;
        Vector2 dimensions;
        List<DisplayMode> resolutions;
        private ContentManager content;
        private bool isTransitioning;
        GraphicsDeviceManager graphics;
        bool fullscreen;
        #endregion

        private ScreenManager() {
        }
        
        public static ScreenManager Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new ScreenManager();
                }
                return instance;
            }
        }
        public void Initialize(ContentManager content, GraphicsDeviceManager graphics)
        {
            IsTransitioning = true;
            fullscreen = false;
            PreviousScreenType = ScreenTypes.Intro;
            activeScreenType = ScreenTypes.Intro;
            Resolutions = new List<DisplayMode>();
            foreach (DisplayMode mode in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
            {
                if(mode.Width >=800)
                    Resolutions.Add(mode);
            }
            Resolutions.Sort(delegate (DisplayMode a, DisplayMode b)
            {
                int xdiff = a.Width.CompareTo(b.Width);
                if (xdiff != 0) return xdiff;
                else return a.Height.CompareTo(b.Height);
            });
            Dimensions = new Vector2(Resolutions[0].Width, Resolutions[0].Height);
            Content = content;
            Graphics = graphics;
            ButtonFactory.Initialize(Content);
        }
        public void LoadContent()
        {
            activeScreen = new Dictionary<ScreenTypes, IScreen>()
            {
                {ScreenTypes.MainMenu,new MainMenuScreen(content) },
                {ScreenTypes.Info, new InfoScreen(content) },
                {ScreenTypes.Intro, new IntroScreen(content) },
                {ScreenTypes.LevelType, new LevelTypeScreen(content) },
                {ScreenTypes.Exit, new ExitScreen(content) },
                {ScreenTypes.Game, new GameScreen(content) },
                {ScreenTypes.Pause, new PauseScreen(content) },
                {ScreenTypes.Options, new OptionsScreen(content) }
            };
        }
        public void ReCentreButtons()
        {
            foreach(ScreenTypes screen in activeScreen.Keys)
            {
                if (screen != ScreenTypes.Game)
                {
                    activeScreen[screen].CentreButtons();
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

        public void Update(GameTime gameTime, Game game)
        {
            if (activeScreenType == ScreenTypes.Exit && !isTransitioning) game.Exit();
            activeScreen[activeScreenType].Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            activeScreen[activeScreenType].Draw(spriteBatch);
        }

        public Vector2 Dimensions { get => dimensions;  set => dimensions = value; }
        public ContentManager Content { get => content; set => content = value; }
        public ScreenTypes ActiveScreenType { get => activeScreenType; set { PreviousScreenType = activeScreenType; activeScreenType = value; } }
        public bool IsTransitioning { get => isTransitioning; set => isTransitioning = value; }
        public GraphicsDeviceManager Graphics { get => graphics; set => graphics = value; }
        internal ScreenTypes PreviousScreenType { get => previousScreenType; set => previousScreenType = value; }
        public List<DisplayMode> Resolutions { get => resolutions; set => resolutions = value; }
        public bool Fullscreen { get => fullscreen; set => fullscreen = value; }

        public void ChangeResolution()
        {
            graphics.PreferredBackBufferWidth = (int)Dimensions.X;
            graphics.PreferredBackBufferHeight = (int)Dimensions.Y;
            graphics.ApplyChanges();
            ReCentreButtons();
        }
    }
}

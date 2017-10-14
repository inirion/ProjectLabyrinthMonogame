using LabyrinthGameMonogame.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LabyrinthGameMonogame.GUI.Screens
{
    class ScreenManager
    {
        #region Variables
        private static ScreenManager instance;
        private Dictionary<ScreenTypes, IScreen> activeScreen;
        private ScreenTypes activeScreenType;
        Vector2 dimensions;
        private ContentManager content;
        private bool isTransitioning;
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
        public void Initialize(ContentManager content)
        {
            IsTransitioning = true;
            activeScreenType = ScreenTypes.Intro;
            Dimensions = new Vector2(800, 600);
            this.content = content;
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
                {ScreenTypes.Pause, new PauseScreen(content) }
            };
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

        public Vector2 Dimensions { get => dimensions; private set => dimensions = value; }
        public ContentManager Content { get => content; set => content = value; }
        public ScreenTypes ActiveScreenType { get => activeScreenType; set => activeScreenType = value; }
        public bool IsTransitioning { get => isTransitioning; set => isTransitioning = value; }
    }
}

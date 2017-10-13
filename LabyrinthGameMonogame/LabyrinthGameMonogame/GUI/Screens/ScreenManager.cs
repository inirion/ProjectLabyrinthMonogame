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
            activeScreenType = ScreenTypes.MainMenu;
            Dimensions = new Vector2(800, 600);
            this.content = content;
        }
        public void LoadContent()
        {
            activeScreen = new Dictionary<ScreenTypes, IScreen>()
            {
                {ScreenTypes.MainMenu,new MainMenuScreen(content) },
                {ScreenTypes.Info, new InfoScreen(content) }
            };
        }

        public void Update()
        {
            activeScreen[activeScreenType].Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            activeScreen[activeScreenType].Draw(spriteBatch);
        }

        public Vector2 Dimensions { get => dimensions; private set => dimensions = value; }
        public ContentManager Content { get => content; set => content = value; }
        public ScreenTypes ActiveScreenType { get => activeScreenType; set => activeScreenType = value; }
    }
}

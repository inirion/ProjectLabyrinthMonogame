using LabyrinthGameMonogame.InputControllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LabyrinthGameMonogame.GUI.Screens;
using LabyrinthGameMonogame.GameFolder;
using LabyrinthGameMonogame.Utils;

namespace LabyrinthGameMonogame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        IScreenManager screenManager;
        IControlManager controlManager;
        IGameManager gameManager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            screenManager = new ScreenManager(this,graphics);
            controlManager = new ControlManager(this);
            gameManager = new GameManager();
            Services.AddService(typeof(IScreenManager), screenManager);
            Services.AddService(typeof(IControlManager), controlManager);
            Services.AddService(typeof(IGameManager), gameManager);
        }

        protected override void Initialize()
        {
            screenManager.Initialize();
            AssetHolder.Instance.Initialize(Content);
            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            screenManager.LoadContent();
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (gameManager.IsGameRunning)
                IsMouseVisible = false;
            else
                IsMouseVisible = true;

            controlManager.Mouse.Update();
            controlManager.Keyboard.Update();
            screenManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            screenManager.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}

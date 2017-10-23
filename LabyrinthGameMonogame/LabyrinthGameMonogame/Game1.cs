using LabyrinthGameMonogame.InputControllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LabyrinthGameMonogame.GUI.Screens;
using LabyrinthGameMonogame.Factories;
using Microsoft.Xna.Framework.Input;
using LabyrinthGameMonogame.GameFolder;
using LabyrinthGameMonogame.Utils;

namespace LabyrinthGameMonogame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
        }

        protected override void Initialize()
        {
            ScreenManager.Instance.Initialize(Content,graphics);
            AssetHolder.Instance.Initialize(Content);
            base.Initialize();

            ScreenManager.Instance.ChangeResolution();
            
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ScreenManager.Instance.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GameManager.Instance.IsGameRunning)
                IsMouseVisible = false;
            else
                IsMouseVisible = true;
            ControlManager.Instance.Mouse.Update();
            ControlManager.Instance.Keyboard.Update();

            ScreenManager.Instance.Update(gameTime,this);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            ScreenManager.Instance.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}

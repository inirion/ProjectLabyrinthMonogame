using LabyrinthGameMonogame.InputControllers;
using LabyrinthGameMonogame.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LabyrinthGameMonogame.GUI.Screens;
using Microsoft.Xna.Framework.Content;
using LabyrinthGameMonogame.Factories;

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
            ScreenManager.Instance.Initialize(Content);
            ButtonFactory.Initialize(Content);
            base.Initialize();

            graphics.PreferredBackBufferWidth = (int)ScreenManager.Instance.Dimensions.X;
            graphics.PreferredBackBufferHeight = (int)ScreenManager.Instance.Dimensions.Y;
            graphics.ApplyChanges();

            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ScreenManager.Instance.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            ControlManager.Instance.Mouse.Update();
            ControlManager.Instance.Keyboard.Update();
            //if (ControlManager.Instance.Keyboard.Clicked(KeyboardKeys.Back)) 
            //    Exit();

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

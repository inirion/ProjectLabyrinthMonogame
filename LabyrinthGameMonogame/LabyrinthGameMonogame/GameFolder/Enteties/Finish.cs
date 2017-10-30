using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.GUI.Screens;
using LabyrinthGameMonogame.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LabyrinthGameMonogame.GameFolder.Enteties
{
    class Finish
    {
        private Cube keyObject;
        private double millisecondsPerFrame;
        private float angle;
        private double timeSinceLastUpdate;
        private IGameManager gameManager;

        public Finish( Vector3 position,GraphicsDevice graphicsDevice, Game game)
        {
            gameManager = (IGameManager)game.Services.GetService(typeof(IGameManager));
            angle = 0;
            millisecondsPerFrame = 50;
            timeSinceLastUpdate = 0;
            keyObject = new Cube(graphicsDevice, new Vector3(0.2f), position,5.0f);
            keyObject.texture = AssetHolder.Instance.FinishTexture;
        }
        public void SetFinishPoint(Vector3 posision)
        {
            keyObject.Posision = posision;
            keyObject.SetBoundingBox();
        }

        public void Update(GameTime gameTime,Player player,IScreenManager screenManager)
        {
            timeSinceLastUpdate += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeSinceLastUpdate >= millisecondsPerFrame)
            {
                timeSinceLastUpdate = 0;
                float gt = (float)gameTime.ElapsedGameTime.TotalSeconds;
                float rp, rm;
                if (this.angle > 360.0f) this.angle = 0.0f;
                this.angle += 50.0f * gt;
                rp = MathHelper.ToRadians(angle);
                rm = MathHelper.ToRadians(-angle);

                keyObject.World = Matrix.Identity;
                keyObject.World *= Matrix.CreateScale(1.0f);
                keyObject.World *= Matrix.CreateRotationY(rm);
                keyObject.World *= Matrix.CreateTranslation(keyObject.Posision);
            }
            if (keyObject.BoundingBox.Intersects(new BoundingSphere(player.position, 0.3f)) && player.allKeysCollected)
            {
                screenManager.ActiveScreenType = ScreenTypes.LoadingScreen;
            }
        }

        public void Draw(Matrix View, Matrix Projection, BasicEffect basicEffect)
        {
            keyObject.Draw(View,Projection,basicEffect);
        }
    }
}

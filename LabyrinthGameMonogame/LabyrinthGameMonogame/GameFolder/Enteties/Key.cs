using LabyrinthGameMonogame.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LabyrinthGameMonogame.GameFolder.Enteties
{
    class Key
    {
        private Cube finishCube;
        public BoundingBox boundingBox { get
            {
                return finishCube.BoundingBox;
            } }
        private double millisecondsPerFrame;
        private float angle;
        private double timeSinceLastUpdate;
        private IGameManager gameManager;


        public Key(Vector3 position, GraphicsDevice graphicsDevice, Game game)
        {
            gameManager = (IGameManager)game.Services.GetService(typeof(IGameManager));
            angle = 0;
            millisecondsPerFrame = 50;
            timeSinceLastUpdate = 0;
            finishCube = new Cube(graphicsDevice, new Vector3(0.05f), new Vector3(position.X, 0.3f, position.Z), 20.0f);
            finishCube.texture = AssetHolder.Instance.FinishTexture;
        }
        public void SetFinishPoint(Vector3 posision)
        {
            finishCube.Posision = posision;
            finishCube.SetBoundingBox();
        }

        public void Update(GameTime gameTime)
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

                finishCube.World = Matrix.Identity;
                finishCube.World *= Matrix.CreateScale(1.0f);
                finishCube.World *= Matrix.CreateRotationY(rm);
                finishCube.World *= Matrix.CreateTranslation(finishCube.Posision);
            }
        }

        public void Draw(Matrix View, Matrix Projection, BasicEffect basicEffect)
        {
            finishCube.Draw(View, Projection, basicEffect);
        }
    }
}

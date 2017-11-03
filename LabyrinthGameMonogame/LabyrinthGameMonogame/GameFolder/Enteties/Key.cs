using LabyrinthGameMonogame.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LabyrinthGameMonogame.GameFolder.Enteties
{
    
    class Key
    {
        private Cube keyObject;
        public BoundingBox boundingBox { get
            {
                return keyObject.BoundingBox;
            } }
        private double millisecondsPerFrame;
        private float angle;
        private double timeSinceLastUpdate;
        private IGameManager gameManager;
        public Vector2 Position { get
            {
                return keyObject.Position2D;
            } }
        GraphicsDevice graphicsDevice;


        public Key(Vector3 position, GraphicsDevice graphicsDevice, Game game)
        {
            this.graphicsDevice = graphicsDevice;
            gameManager = (IGameManager)game.Services.GetService(typeof(IGameManager));
            angle = 0;
            millisecondsPerFrame = 5;
            timeSinceLastUpdate = 0;
            keyObject = new Cube(graphicsDevice, new Vector3(0.1f), new Vector3(position.X, 0.3f, position.Z), 10.0f);
            keyObject.texture = AssetHolder.Instance.KeyTexture;
            keyObject.World = Matrix.Identity;
            keyObject.World *= Matrix.CreateRotationX(45.0f);
            keyObject.World *= Matrix.CreateTranslation(keyObject.Posision);

        }
        public void SetFinishPoint(Vector3 posision)
        {
            keyObject.Posision = posision;
            keyObject.SetBoundingBox();
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

                keyObject.World = Matrix.Identity;
                keyObject.World *= Matrix.CreateRotationX(45);
                keyObject.World *= Matrix.CreateRotationY(rm);
                keyObject.World *= Matrix.CreateTranslation(keyObject.Posision);
            }
        }

        public void Draw(Matrix View, Matrix Projection, BasicEffect basicEffect)
        {
            keyObject.Draw(View, Projection, basicEffect);
        }
    }
}

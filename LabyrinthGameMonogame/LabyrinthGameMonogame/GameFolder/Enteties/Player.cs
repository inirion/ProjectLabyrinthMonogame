using Microsoft.Xna.Framework;

namespace LabyrinthGameMonogame.GameFolder.Enteties
{
    class Player
    {
        Vector3 position;
        Camera camera;
        float movementSpeed;

        public Player(Vector3 position,float movementSpeed)
        {
            Camera = new Camera(position, new Vector3(0, 0, 0), movementSpeed, 0.05f);
            this.Position = position;
        }

        public void Update(GameTime gameTime)
        {
            Camera.Update(gameTime);
            this.position = camera.Position;
        }

        public Vector3 Position { get => position; set { position = value; camera.Position = value; } }
        public Camera Camera { get => camera; set => camera = value; }
    }
}

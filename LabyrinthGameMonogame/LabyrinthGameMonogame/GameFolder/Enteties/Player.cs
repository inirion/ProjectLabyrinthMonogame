using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.GUI.Screens;
using LabyrinthGameMonogame.InputControllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LabyrinthGameMonogame.GameFolder.Enteties
{
    class Player
    {
        Vector3 position;
        Camera camera;
        float movementSpeed;
        float playerHeight;
        bool isJumping;
        IControlManager controlManager;

        public Player(Vector3 position, float movementSpeed,Game game)
        {
            controlManager = (IControlManager)game.Services.GetService(typeof(IControlManager));
            Camera = new Camera(position, new Vector3(0, 0, 0), movementSpeed, movementSpeed * 1.5f,game);
            PlayerHeight = 0.2f;
            this.Position = position;
            this.MovementSpeed = movementSpeed;
        }

        public void Reset(Vector3 position, Game game)
        {
            this.Position = position;
            this.camera.MouseSpeed = controlManager.Mouse.Sensitivity;
            this.camera = new Camera(position, new Vector3(0, 0, 0), movementSpeed, movementSpeed * 1.5f, game);
        }
        public void Update(GameTime gameTime)
        {
            float speed = movementSpeed;
            if (controlManager.Keyboard.Clicked(KeyboardKeys.Jump) && Position.Y <= PlayerHeight)
            {
                IsJumping = true;
            }
            if (controlManager.Keyboard.Pressed(false, KeyboardKeys.LeftShift))
            {
                speed *= 2;
            }
            camera.CameraSpeed = speed;



            Camera.Update(gameTime);
            this.position = camera.Position;
        }

        public Vector3 Position { get => position; set { position = value; camera.Position = value; } }
        public Camera Camera { get => camera; set => camera = value; }
        public bool IsJumping { get => isJumping; set => isJumping = value; }
        public float PlayerHeight { get => playerHeight; set => playerHeight = value; }
        public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    }
}

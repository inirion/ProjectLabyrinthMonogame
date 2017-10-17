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

        public Player(Vector3 position,float movementSpeed)
        {
            Camera = new Camera(position, new Vector3(0, 0, 0), movementSpeed, movementSpeed*1.5f, ControlManager.Instance.Mouse.Sensitivity);
            PlayerHeight = 0.2f;
            this.Position = position;
            this.MovementSpeed = movementSpeed;
        }

        public void Update(GameTime gameTime)
        {
            float speed = movementSpeed;
            if (ControlManager.Instance.Keyboard.Clicked(KeyboardKeys.Jump) && Position.Y <= PlayerHeight)
            { 
                IsJumping = true;
            }
            if (ControlManager.Instance.Keyboard.Pressed(false, KeyboardKeys.LeftShift))
            {
                speed *= 2;
            }
            camera.CameraSpeed = speed;



            Camera.Update(gameTime, ref isJumping, PlayerHeight);
            this.position = camera.Position;
        }

        public Vector3 Position { get => position; set { position = value; camera.Position = value; } }
        public Camera Camera { get => camera; set => camera = value; }
        public bool IsJumping { get => isJumping; set => isJumping = value; }
        public float PlayerHeight { get => playerHeight; set => playerHeight = value; }
        public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    }
}

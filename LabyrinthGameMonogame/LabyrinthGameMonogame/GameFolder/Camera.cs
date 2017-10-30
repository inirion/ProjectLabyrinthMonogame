using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.GUI.Screens;
using LabyrinthGameMonogame.InputControllers;
using LabyrinthGameMonogame.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace LabyrinthGameMonogame.GameFolder
{
    class Camera
    {
        private Vector3 cameraPosition;
        private Vector3 cameraRotation;
        private float cameraSpeed;
        private float jumpingCameraSpeed;
        private float mouseSpeed;
        private Vector3 cameraLookAt;
        private Vector3 mouseRotationBuffer;
        private float time;
        private IScreenManager screenManager;
        private IControlManager controlManager;
        private IGameManager gameManager;
        private Game game;


        public Vector3 Position
        {
            get { return cameraPosition; }
            set
            {
                cameraPosition = value;
                UpdateLookAt();
            }
        }

        public Vector3 Rotation
        {
            get { return cameraRotation; }
            set
            {
                cameraRotation = value;
                UpdateLookAt();
            }
        }

        public Matrix Projection
        {
            get;
            private set;
        }

        public Matrix View
        {
            get
            {
                return Matrix.CreateLookAt(cameraPosition, cameraLookAt, Vector3.Up);
            }
        }

        public float CameraSpeed { get => cameraSpeed; set { cameraSpeed = value; } }

        public float JumpingCameraSpeed { get => jumpingCameraSpeed; set => jumpingCameraSpeed = value; }
        public float MouseSpeed { get => mouseSpeed; set => mouseSpeed = value; }

        public Camera(Vector3 position, Vector3 rotation, float cameraSpeed, float jumpingCameraSpeed,Game game)
        {
            this.game = game;
            this.controlManager = (IControlManager)game.Services.GetService(typeof(IControlManager));
            this.screenManager = (IScreenManager)game.Services.GetService(typeof(IScreenManager));
            this.gameManager = (IGameManager)game.Services.GetService(typeof(IGameManager));
            this.CameraSpeed = cameraSpeed;
            this.JumpingCameraSpeed = jumpingCameraSpeed;
            this.MouseSpeed = controlManager.Mouse.Sensitivity;

            Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                game.GraphicsDevice.Viewport.AspectRatio,
                0.05f,
                1000.0f);

            MoveTo(position, rotation);
        }

        private void MoveTo(Vector3 pos, Vector3 rot)
        {
            Position = pos;
            Rotation = rot;
        }

        private void UpdateLookAt()
        {
            Matrix rotationMatrix = Matrix.CreateRotationX(cameraRotation.X) * Matrix.CreateRotationY(cameraRotation.Y);
            Vector3 lookAtOffset = Vector3.Transform(Vector3.UnitZ, rotationMatrix);
            cameraLookAt = cameraPosition + lookAtOffset;
        }

        private Vector3 PreviewMove(Vector3 amount)
        {
            Matrix rotate = Matrix.CreateRotationY(cameraRotation.Y);
            Vector3 movement = new Vector3(amount.X, amount.Y, amount.Z);
            movement = Vector3.Transform(movement, rotate);
            return cameraPosition + new Vector3(
                CollisionChecker.Instance.CheckCollision(cameraPosition + new Vector3(movement.X, 0, 0), gameManager.Type) ? 0 : movement.X,
                CollisionChecker.Instance.CheckCollision(cameraPosition + new Vector3(0, movement.Y, 0), gameManager.Type) ? 0 : movement.Y,
                CollisionChecker.Instance.CheckCollision(cameraPosition + new Vector3(0, 0, movement.Z), gameManager.Type) ? 0 : movement.Z);
        }

        private void Move(Vector3 scale)
        {
            MoveTo(PreviewMove(scale), Rotation);
        }

        public void Update(GameTime gameTime)
        {
            this.MouseSpeed = controlManager.Mouse.Sensitivity;
            float speed = CameraSpeed;
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector3 moveVector = Vector3.Zero;

            if (controlManager.Keyboard.Pressed(false, KeyboardKeys.Up))
                moveVector.Z = 1;
            if (controlManager.Keyboard.Pressed(false, KeyboardKeys.Down))
                moveVector.Z = -1;
            if (controlManager.Keyboard.Pressed(false, KeyboardKeys.Left))
                moveVector.X = 1;
            if (controlManager.Keyboard.Pressed(false, KeyboardKeys.Right))
                moveVector.X = -1;
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (cameraPosition.Y <= 2f)
                {
                    moveVector.Y = 1f;

                }
            }else if (Keyboard.GetState().IsKeyUp(Keys.Space))
            {
                moveVector.Y = -1f;

            }

            if (moveVector != Vector3.Zero)
            {
                moveVector.Normalize();
                moveVector *= dt * speed;

                Move(moveVector);
            }

            float deltaX;
            float deltaY;

            if (controlManager.Mouse.CurrentState != controlManager.Mouse.OriginalState)
            {
                deltaX = controlManager.Mouse.CurrentState.X - (game.GraphicsDevice.Viewport.Width / 2);
                deltaY = controlManager.Mouse.CurrentState.Y - (game.GraphicsDevice.Viewport.Height / 2);
                mouseRotationBuffer.X -= MouseSpeed * deltaX * dt;
                mouseRotationBuffer.Y -= MouseSpeed * deltaY * dt;

                if (mouseRotationBuffer.Y < MathHelper.ToRadians(-75.0f))
                    mouseRotationBuffer.Y = mouseRotationBuffer.Y - (mouseRotationBuffer.Y - MathHelper.ToRadians(-75.0f));
                if (mouseRotationBuffer.Y > MathHelper.ToRadians(75.0f))
                    mouseRotationBuffer.Y = mouseRotationBuffer.Y - (mouseRotationBuffer.Y - MathHelper.ToRadians(75.0f));

                Rotation = new Vector3(-MathHelper.Clamp(mouseRotationBuffer.Y,
                                    MathHelper.ToRadians(-75.0f), MathHelper.ToRadians(75.0f)),
                                    MathHelper.WrapAngle(mouseRotationBuffer.X), 0);

                deltaX = 0;
                deltaY = 0;

            }

            Mouse.SetPosition(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2);

            controlManager.Mouse.OriginalState = controlManager.Mouse.CurrentState;

        }
    }
}

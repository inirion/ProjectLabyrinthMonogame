using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.GUI.Screens;
using LabyrinthGameMonogame.InputControllers;
using LabyrinthGameMonogame.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

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

        public Camera(Vector3 position, Vector3 rotation, float cameraSpeed,float jumpingCameraSpeed, float mouseSpeed)
        {
            this.CameraSpeed = cameraSpeed;
            this.JumpingCameraSpeed = jumpingCameraSpeed;
            this.mouseSpeed = mouseSpeed;

            Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                ScreenManager.Instance.Graphics.GraphicsDevice.Viewport.AspectRatio,
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
                CollisionChecker.Instance.CheckCollision(cameraPosition + new Vector3(movement.X, 0, 0)) ? 0 : movement.X,
                CollisionChecker.Instance.CheckCollision(cameraPosition + new Vector3(0, movement.Y, 0)) ? 0 : movement.Y,
                CollisionChecker.Instance.CheckCollision(cameraPosition + new Vector3(0, 0, movement.Z)) ? 0 : movement.Z);
        }

        private void Move(Vector3 scale)
        {
            MoveTo(PreviewMove(scale), Rotation);
        }

        public void Update(GameTime gameTime, ref bool isJumping, float playerHeight)
        {
            float speed = CameraSpeed;
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector3 moveVector = Vector3.Zero;

            if (ControlManager.Instance.Keyboard.Pressed(false,KeyboardKeys.Up))
                moveVector.Z = 1;
            if (ControlManager.Instance.Keyboard.Pressed(false,KeyboardKeys.Down))
                moveVector.Z = -1;
            if (ControlManager.Instance.Keyboard.Pressed(false,KeyboardKeys.Left))
                moveVector.X = 1;
            if (ControlManager.Instance.Keyboard.Pressed(false,KeyboardKeys.Right))
                moveVector.X = -1;

            if (isJumping && cameraPosition.Y <= playerHeight +0.5f)
            {
                moveVector.Y = 1f;
                speed = JumpingCameraSpeed;
                
            }
            if (!isJumping && cameraPosition.Y >= playerHeight)
            {
                moveVector.Y = -1f;
                speed = JumpingCameraSpeed;
            }
            if(cameraPosition.Y >= playerHeight+0.5f)
            {
                isJumping = false;
                speed = cameraSpeed;
            }

            if (moveVector != Vector3.Zero)
            {
                moveVector.Normalize();
                moveVector *= dt * speed;

                Move(moveVector);
            }

            float deltaX;
            float deltaY;

            if (ControlManager.Instance.Mouse.CurrentState != ControlManager.Instance.Mouse.OriginalState)
            {
                deltaX = ControlManager.Instance.Mouse.CurrentState.X - (ScreenManager.Instance.Graphics.GraphicsDevice.Viewport.Width / 2);
                deltaY = ControlManager.Instance.Mouse.CurrentState.Y - (ScreenManager.Instance.Graphics.GraphicsDevice.Viewport.Height / 2);

                mouseRotationBuffer.X -= mouseSpeed * deltaX * dt;
                mouseRotationBuffer.Y -= mouseSpeed * deltaY * dt;

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

            Mouse.SetPosition(ScreenManager.Instance.Graphics.GraphicsDevice.Viewport.Width / 2, ScreenManager.Instance.Graphics.GraphicsDevice.Viewport.Height / 2);

            ControlManager.Instance.Mouse.OriginalState = ControlManager.Instance.Mouse.CurrentState;

        }
    }
}

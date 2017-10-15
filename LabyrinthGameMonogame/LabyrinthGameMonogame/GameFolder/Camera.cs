using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.GUI.Screens;
using LabyrinthGameMonogame.InputControllers;
using Microsoft.Xna.Framework;

namespace LabyrinthGameMonogame.GameFolder
{
    class Camera
    {
        Vector3 cameraPosition;
        Matrix viewMatrix;
        Matrix projectionMatrix;
        float leftrightRot;
        float updownRot;
        readonly float rotationSpeed;
        readonly float moveSpeed;

        public Vector3 CameraPosition { get => cameraPosition; set => cameraPosition = value; }
        public Matrix ViewMatrix { get => viewMatrix; set => viewMatrix = value; }
        public Matrix ProjectionMatrix { get => projectionMatrix; set => projectionMatrix = value; }

        public Camera()
        {
            CameraPosition = new Vector3(0, 0, 0);
            leftrightRot = MathHelper.PiOver2;
            updownRot = -MathHelper.Pi / 10.0f;
            rotationSpeed = 0.2f;
            moveSpeed = 10.0f;
            ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, ScreenManager.Instance.Graphics.GraphicsDevice.Viewport.AspectRatio, 0.3f, 1000.0f);
            ControlManager.Instance.Mouse.CentrePosition();
        }

        private void ProcessInput(float amount)
        {
            if (ControlManager.Instance.Mouse.CurrentState != ControlManager.Instance.Mouse.OriginalState)
            {
                float xDifference = ControlManager.Instance.Mouse.CurrentMousePos.X - ControlManager.Instance.Mouse.OriginalState.X;
                float yDifference = ControlManager.Instance.Mouse.CurrentMousePos.Y - ControlManager.Instance.Mouse.OriginalState.Y;
                leftrightRot -= rotationSpeed * xDifference * amount;
                updownRot -= rotationSpeed * yDifference * amount;
                ControlManager.Instance.Mouse.CentrePosition();
                UpdateViewMatrix();
            }

            Vector3 moveVector = new Vector3(0, 0, 0);
            if (ControlManager.Instance.Keyboard.Pressed(KeyboardKeys.Up))
                moveVector += new Vector3(0, 0, -1);
            if (ControlManager.Instance.Keyboard.Pressed(KeyboardKeys.Down))
                moveVector += new Vector3(0, 0, 1);
            if (ControlManager.Instance.Keyboard.Pressed(KeyboardKeys.Right))
                moveVector += new Vector3(1, 0, 0);
            if (ControlManager.Instance.Keyboard.Pressed(KeyboardKeys.Left))
                moveVector += new Vector3(-1, 0, 0);
            if (ControlManager.Instance.Keyboard.Pressed(KeyboardKeys.UpZAexis))
                moveVector += new Vector3(0, 1, 0);
            if (ControlManager.Instance.Keyboard.Pressed(KeyboardKeys.DownZAexis))
                moveVector += new Vector3(0, -1, 0);
            AddToCameraPosition(moveVector * amount);
        }

        private void UpdateViewMatrix()
        {
            Matrix cameraRotation = Matrix.CreateRotationX(updownRot) * Matrix.CreateRotationY(leftrightRot);

            Vector3 cameraOriginalTarget = new Vector3(0, 0, -1);
            Vector3 cameraOriginalUpVector = new Vector3(0, 1, 0);

            Vector3 cameraRotatedTarget = Vector3.Transform(cameraOriginalTarget, cameraRotation);
            Vector3 cameraFinalTarget = CameraPosition + cameraRotatedTarget;

            Vector3 cameraRotatedUpVector = Vector3.Transform(cameraOriginalUpVector, cameraRotation);

            ViewMatrix = Matrix.CreateLookAt(CameraPosition, cameraFinalTarget, cameraRotatedUpVector);
        }

        private void AddToCameraPosition(Vector3 vectorToAdd)
        {
            Matrix cameraRotation = Matrix.CreateRotationX(updownRot) * Matrix.CreateRotationY(leftrightRot);
            Vector3 rotatedVector = Vector3.Transform(vectorToAdd, cameraRotation);
            CameraPosition += moveSpeed * rotatedVector;
            UpdateViewMatrix();
        }

        public void Update(GameTime gameTime)
        {
            float timeDifference = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;
            ProcessInput(timeDifference);
        }
    }
}

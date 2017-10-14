using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.GUI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace LabyrinthGameMonogame.GameFolder
{
    class LabirynthGame
    {
        Model model;
        DifficultyLevel difficultyLevel;
        float angle;
        Random random;
        Vector3 aexis;

        DifficultyLevel DifficultyLevel { get => difficultyLevel; set => difficultyLevel = value; }

        public LabirynthGame()
        {
            model = ScreenManager.Instance.Content.Load<Model>("Wooden_House");
            GameManager.Instance.IsGameRunning = true;
            random = new Random();
            aexis = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
        }

        public void GenerateMap()
        {
            
        }

        public void ResetGame()
        {
            if(GameManager.Instance.ResetGame)
            {
                angle = 0;
                aexis = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
                GameManager.Instance.ResetGame = false;
            }
        }

        public void Update()
        {

        }

        void DrawModel(Vector3 modelPosition)
        {
            foreach (var mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;
                    effect.World = Matrix.CreateTranslation(modelPosition);
                    var cameraPosition = new Vector3(5, 5, 5);
                    cameraPosition = Vector3.Transform(cameraPosition - modelPosition, Matrix.CreateFromAxisAngle(aexis, angle += 0.01f)) + modelPosition;
                    var cameraLookAtVector = Vector3.Zero;
                    var cameraUpVector = Vector3.UnitZ;
                    effect.View = Matrix.CreateLookAt(
                        cameraPosition, cameraLookAtVector, cameraUpVector);
                    float aspectRatio =
                        ScreenManager.Instance.Graphics.PreferredBackBufferWidth / (float)ScreenManager.Instance.Graphics.PreferredBackBufferHeight;
                    float fieldOfView = Microsoft.Xna.Framework.MathHelper.PiOver4;
                    float nearClipPlane = 1;
                    float farClipPlane = 200;
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                        fieldOfView, aspectRatio, nearClipPlane, farClipPlane);
                }

                mesh.Draw();
            }
        }

        public void Draw()
        {
            if (GameManager.Instance.IsGameRunning) {
                ScreenManager.Instance.Graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

                DrawModel(new Vector3(-4, 0, 0));
                DrawModel(new Vector3(0, 0, 0));
                DrawModel(new Vector3(4, 0, 0));
                DrawModel(new Vector3(-4, 0, 3));
                DrawModel(new Vector3(0, 0, 3));
                DrawModel(new Vector3(4, 0, 3));

            }
        }
    }
}

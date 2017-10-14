using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.GUI.Screens;
using LabyrinthGameMonogame.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace LabyrinthGameMonogame.GameFolder
{
    class LabirynthGame
    {
        Model model;
        float angle;
        float gap;
        Random random;
        Vector3 aexis;
        LabirynthGenerator lab;

        public LabirynthGame()
        {
            model = ScreenManager.Instance.Content.Load<Model>("Wooden_House");
            GameManager.Instance.IsGameRunning = true;
            random = new Random();
            aexis = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            lab = new LabirynthGenerator();
        }

        public void GenerateMap()
        {
            
            lab.CreateMaze();
        }

        public void ResetGame()
        {
            if(GameManager.Instance.ResetGame)
            {
                gap = 0.7f;
                angle = 0;
                aexis = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
                GameManager.Instance.ResetGame = false;
                GenerateMap();
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
                    var cameraPosition = new Vector3(0, 2, 50);
                    //cameraPosition = Vector3.Transform(cameraPosition - modelPosition, Matrix.CreateFromAxisAngle(aexis, angle)) + modelPosition;
                    var cameraLookAtVector = Vector3.Zero;
                    var cameraUpVector = Vector3.UnitZ;
                    effect.View = Matrix.CreateLookAt(
                       cameraPosition, cameraLookAtVector+new Vector3(2,2,0), cameraUpVector);
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
                
                for(int i = 0; i< lab.Maze.GetLength(0); i++)
                {
                    for (int j = 0; j < lab.Maze.GetLength(1); j++)
                    {
                        if (lab.Maze[i,j] == 1)
                        DrawModel(new Vector3(gap * i, gap * j, 0));

                    }
                }

            }
        }
    }
}

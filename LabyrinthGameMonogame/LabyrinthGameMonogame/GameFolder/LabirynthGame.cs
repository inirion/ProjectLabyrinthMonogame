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
        Camera camera;
        float angle;
        float gap;
        Random random;
        Vector2 dim;
        Vector3 aexis;
        LabirynthGenerator lab;


        public LabirynthGame()
        {
            model = ScreenManager.Instance.Content.Load<Model>("Wooden_House");
            random = new Random();
            aexis = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            lab = new LabirynthGenerator();
            camera = new Camera(new Vector3(0,0,0),new Vector3(0,0,0),5.0f,0.05f);
        }

        public void GenerateMap()
        {
            
            lab.CreateMaze();
        }

        public void ResetGame()
        {
            if(GameManager.Instance.ResetGame)
            {
                gap = 1.0f;
                angle = 0;
                aexis = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
                GameManager.Instance.ResetGame = false;
                GenerateMap();
                //coords x, z, y
                camera.Position = new Vector3(gap, 2, gap);
            }
        }

        public void Update(GameTime gameTime)
        {
            camera.Update(gameTime);
            angle += 0.1f;
            //Debug.WriteLine(angle);
        }

        void DrawModel(Vector3 modelPosition,float rotation)
        {
            foreach (var mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {   
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;
                    Matrix worldMatrix = Matrix.CreateRotationX(MathHelper.ToRadians(270.0f))* Matrix.CreateRotationY(MathHelper.ToRadians(rotation)) * Matrix.CreateTranslation(modelPosition);

                    effect.World = worldMatrix;
                    effect.View = camera.View;
                    effect.Projection = camera.Projection;
                }

                mesh.Draw();
            }
        }
        public void Draw()
        {
            if (GameManager.Instance.IsGameRunning) {
                ScreenManager.Instance.Graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
                ScreenManager.Instance.Graphics.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
                for (int i = 0; i< lab.Maze.GetLength(0); i++)
                {
                    for (int j = 0; j < lab.Maze.GetLength(1); j++)
                    {
                        switch ((LabiryntElement)lab.Maze[i, j])
                        {
                            case LabiryntElement.Wall:
                                DrawModel(new Vector3(gap * i, 0, gap * j),0);
                                break;
                            case LabiryntElement.WallEN:
                                DrawModel(new Vector3(gap * i, 0, gap * j),0);
                                break;
                            case LabiryntElement.WallES:
                                DrawModel(new Vector3(gap * i, 0, gap * j),0);
                                break;
                            case LabiryntElement.WallEW:
                                DrawModel(new Vector3(gap * i, 0, gap * j),0);
                                break;
                            case LabiryntElement.WallNS:
                                DrawModel(new Vector3(gap * i, 0, gap * j),90);
                                break;
                            case LabiryntElement.WallWN:
                                DrawModel(new Vector3(gap * i, 0, gap * j),0);
                                break;
                            case LabiryntElement.WallWS:
                                DrawModel(new Vector3(gap * i, 0, gap * j),0);
                                break;
                        }
                    }
                }

            }
        }
    }
}

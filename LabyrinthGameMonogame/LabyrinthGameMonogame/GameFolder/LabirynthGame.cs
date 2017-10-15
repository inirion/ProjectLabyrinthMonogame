using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.GUI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LabyrinthGameMonogame.GameFolder
{
    class LabirynthGame
    {
        Model model;
        Camera camera;
        float angle;
        float gap;
        Random random;
        Vector3 aexis;
        LabirynthGenerator lab;


        public LabirynthGame()
        {
            model = ScreenManager.Instance.Content.Load<Model>("Wooden_House");
            random = new Random();
            aexis = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            lab = new LabirynthGenerator();
            camera = new Camera();
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

        public void Update(GameTime gameTime)
        {
            camera.Update(gameTime);
        }

        void DrawModel(Vector3 modelPosition)
        {
            foreach (var mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {   
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;
                    Matrix worldMatrix = Matrix.CreateTranslation(modelPosition);
                    effect.World = worldMatrix;
                    effect.View = camera.ViewMatrix;
                    effect.Projection = camera.ProjectionMatrix;
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
                        switch ((LabiryntElement)lab.Maze[i, j])
                        {
                            case LabiryntElement.Wall:
                                DrawModel(new Vector3(gap * i, 0, gap * j));
                                break;
                            case LabiryntElement.WallEN:
                                DrawModel(new Vector3(gap * i, 0, gap * j));
                                break;
                            case LabiryntElement.WallES:
                                DrawModel(new Vector3(gap * i, 0, gap * j));
                                break;
                            case LabiryntElement.WallEW:
                                DrawModel(new Vector3(gap * i, 0, gap * j));
                                break;
                            case LabiryntElement.WallNS:
                                DrawModel(new Vector3(gap * i, 0, gap * j));
                                break;
                            case LabiryntElement.WallWN:
                                DrawModel(new Vector3(gap * i, 0, gap * j));
                                break;
                            case LabiryntElement.WallWS:
                                DrawModel(new Vector3(gap * i, 0, gap * j));
                                break;
                        }
                    }
                }

            }
        }
    }
}

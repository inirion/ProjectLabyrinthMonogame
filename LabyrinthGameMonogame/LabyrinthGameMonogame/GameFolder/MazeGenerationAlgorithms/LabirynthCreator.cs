﻿using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.GameFolder;
using LabyrinthGameMonogame.GameFolder.Enteties;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace LabyrinthGameMonogame.GameFolder.MazeGenerationAlgorithms
{
    class LabirynthCreator
    {
        GrowingTreePrimGenerator primGenerator;
        RecursiveBacktrackGenerator recursiveGenerator;
        List<ModelWall> modelMap;
        List<VertexWall> vertexMap;

        Vector2 size;


        public LabirynthCreator(Game game) {
            primGenerator = new GrowingTreePrimGenerator(game);
            recursiveGenerator = new RecursiveBacktrackGenerator(game);
            modelMap = new List<ModelWall>();
            VertexMap = new List<VertexWall>();
            Size = new Vector2();
        }

        public List<ModelWall> ModelMap { get => modelMap; set => modelMap = value; }
        public Vector2 Size { get => size; set => size = value; }
        public List<VertexWall> VertexMap { get => vertexMap; set => vertexMap = value; }

        public Vector3 GetStartingPositionModelMap()
        {
            Vector3 tmp = modelMap.FirstOrDefault(i => i.LabiryntElement == LabiryntElement.Start).Position;
            tmp.Y = 0.2f;
            return tmp;
        }

        public Vector3 GetFinishPositionModelMap()
        {
            Vector3 tmp = modelMap.FirstOrDefault(i => i.LabiryntElement == LabiryntElement.Finish).Position;
            return tmp;
        }

        public Vector3 GetStartingPositionVertexMap()
        {
            Vector3 tmp = primGenerator.spawnpoint;
            tmp.Y = 0.2f;
            return tmp;
        }

        public Vector3 GetFinishPositionVertexMap()
        {
            Vector3 tmp = primGenerator.exitpoint;
            return tmp;
        }

        public void CreateVertexMap(GraphicsDevice graphics)
        {
            vertexMap.Clear();
            vertexMap = new List<VertexWall>();
            primGenerator.CreateMaze();

            for (int i = 0; i < primGenerator.Maze.GetLength(0); i++)
            {
                for (int j = 0; j < primGenerator.Maze.GetLength(1); j++)
                {
                    if(primGenerator.Maze[i,j] == (int)LabiryntElement.Wall)
                        VertexMap.Add(new VertexWall(graphics, new Vector3(0.5f), new Vector3(i,0.5f,j)));
                }
            }
                    
            
        }

        public void CreateModelMap()
        {
            modelMap.Clear();
            modelMap = new List<ModelWall>();

            recursiveGenerator.CreateMaze();

            for (int i = 0; i < recursiveGenerator.Maze.GetLength(0); i++)
            {
                for (int j = 0; j < recursiveGenerator.Maze.GetLength(1); j++)
                {
                    if(i== recursiveGenerator.Maze.GetLength(1)-1 && j == 0)
                    {
                        size.X = i;
                    }
                    if( i== 0 && j == recursiveGenerator.Maze.GetLength(1) - 1)
                    {
                        size.Y = j;
                    }
                    switch ((LabiryntElement)recursiveGenerator.Maze[i, j])
                    {
                        case LabiryntElement.Wall:
                            modelMap.Add(new ModelWall(LabiryntElement.Wall, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            modelMap.Add(new ModelWall(LabiryntElement.Wall, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 90, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.WallEN:
                            modelMap.Add(new ModelWall(LabiryntElement.WallEN, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            modelMap.Add(new ModelWall(LabiryntElement.Pillar, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.2f, 0.2f, 0.17f)));
                            modelMap.Add(new ModelWall(LabiryntElement.WallEN, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 270, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.WallES:
                            modelMap.Add(new ModelWall(LabiryntElement.WallES, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 180, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            modelMap.Add(new ModelWall(LabiryntElement.Pillar, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.2f, 0.2f, 0.17f)));
                            modelMap.Add(new ModelWall(LabiryntElement.WallES, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 270, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.WallEW:
                            modelMap.Add(new ModelWall(LabiryntElement.WallEW, "BakedBrick", new Vector3(i, 0, j), new Vector3(270, 90, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.WallNS:
                            modelMap.Add(new ModelWall(LabiryntElement.WallNS, "BakedBrick", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.WallWN:
                            modelMap.Add(new ModelWall(LabiryntElement.WallWN, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            modelMap.Add(new ModelWall(LabiryntElement.Pillar, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.2f, 0.2f, 0.17f)));
                            modelMap.Add(new ModelWall(LabiryntElement.WallWN, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 90, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.WallWS:
                            modelMap.Add(new ModelWall(LabiryntElement.WallWS, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 180, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            modelMap.Add(new ModelWall(LabiryntElement.Pillar, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.2f, 0.2f, 0.17f)));
                            modelMap.Add(new ModelWall(LabiryntElement.WallWS, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 90, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.Wall3WayEast:
                            modelMap.Add(new ModelWall(LabiryntElement.WallWS, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 180, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            modelMap.Add(new ModelWall(LabiryntElement.Pillar, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.2f, 0.2f, 0.17f)));
                            modelMap.Add(new ModelWall(LabiryntElement.WallWS, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 90, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            modelMap.Add(new ModelWall(LabiryntElement.WallWS, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.Wall3WayWest:
                            modelMap.Add(new ModelWall(LabiryntElement.WallWS, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 180, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            modelMap.Add(new ModelWall(LabiryntElement.Pillar, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.2f, 0.2f, 0.17f)));
                            modelMap.Add(new ModelWall(LabiryntElement.WallWS, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 270, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            modelMap.Add(new ModelWall(LabiryntElement.WallWS, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.Wall3WayNorth:
                            modelMap.Add(new ModelWall(LabiryntElement.WallWS, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 270, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            modelMap.Add(new ModelWall(LabiryntElement.Pillar, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.2f, 0.2f, 0.17f)));
                            modelMap.Add(new ModelWall(LabiryntElement.WallWS, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            modelMap.Add(new ModelWall(LabiryntElement.WallWS, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 90, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.Wall3WaySouth:
                            modelMap.Add(new ModelWall(LabiryntElement.WallWS, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 90, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            modelMap.Add(new ModelWall(LabiryntElement.Pillar, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.2f, 0.2f, 0.17f)));
                            modelMap.Add(new ModelWall(LabiryntElement.WallWS, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 180, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            modelMap.Add(new ModelWall(LabiryntElement.WallWS, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 270, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.Start:
                            modelMap.Add(new ModelWall(LabiryntElement.Start, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.Finish:
                            modelMap.Add(new ModelWall(LabiryntElement.Finish, "Wooden_House", new Vector3(i, 0, j), new Vector3(0, 0, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                    }
                }
            }
            modelMap.ForEach(i => i.setupModel());
        }
    }
}
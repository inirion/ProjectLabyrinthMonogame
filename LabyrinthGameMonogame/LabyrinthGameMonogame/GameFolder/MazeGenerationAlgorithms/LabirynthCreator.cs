using LabyrinthGameMonogame.Enums;
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
        List<Cube> vertexMap;
        List<Cube> groundMap;

        Vector2 size;


        public LabirynthCreator(Game game) {
            primGenerator = new GrowingTreePrimGenerator(game);
            recursiveGenerator = new RecursiveBacktrackGenerator(game);
            modelMap = new List<ModelWall>();
            VertexMap = new List<Cube>();
            Size = new Vector2();
            GroundMap = new List<Cube>();
        }

        public List<ModelWall> ModelMap { get => modelMap; set => modelMap = value; }
        public Vector2 Size { get => size; set => size = value; }
        public List<Cube> VertexMap { get => vertexMap; set => vertexMap = value; }
        internal List<Cube> GroundMap { get => groundMap; set => groundMap = value; }

        public List<Key> GetKeys(LabiryntType type,GraphicsDevice gd, Game game)
        {
            List<Key> keys = new List<Key>();
            if (type == LabiryntType.Prim)
            {
                for(int i = 0;i < primGenerator.keys.Count; i++)
                {
                    keys.Add(new Key(primGenerator.keys[i],gd,game));
                }
            }
            if(type == LabiryntType.Recursive)
            {
                for (int i = 0; i < recursiveGenerator.keys.Count; i++)
                {
                    keys.Add(new Key(recursiveGenerator.keys[i], gd, game));
                }
            }
            return keys;
        }
        public Vector3 GetStartingPositionModelMap()
        {
            Vector3 tmp = recursiveGenerator.spawnpoint;
            tmp.Y = 0.2f;
            return tmp;
        }

        public Vector3 GetFinishPositionModelMap()
        {
            Vector3 tmp = recursiveGenerator.exitpoint;
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
            groundMap.Clear();
            primGenerator.CreateMaze();

            for (int i = 0; i < primGenerator.Maze.GetLength(0); i++)
            {
                for (int j = 0; j < primGenerator.Maze.GetLength(1); j++)
                {
                    if(primGenerator.Maze[i,j] == (int)LabiryntElement.Wall)
                        VertexMap.Add(new Cube(graphics, new Vector3(0.5f), new Vector3(i,0.5f,j),2.0f));
                    if (primGenerator.Maze[i, j] == (int)LabiryntElement.Road 
                        || primGenerator.Maze[i, j] == (int)LabiryntElement.Start 
                        || primGenerator.Maze[i, j] == (int)LabiryntElement.Finish
                        || primGenerator.Maze[i, j] == (int)LabiryntElement.Key)
                        GroundMap.Add(new Cube(graphics, new Vector3(0.5f), new Vector3(i, -0.5f, j), 2.0f));
                }
            }
                    
            
        }

        public void CreateModelMap(Game game)
        {
            modelMap.Clear();
            groundMap.Clear();

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
                            modelMap.Add(new ModelWall(LabiryntElement.Wall, new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            modelMap.Add(new ModelWall(LabiryntElement.Wall, new Vector3(i, 0, j), new Vector3(270, 90, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.WallEN:
                            modelMap.Add(new ModelWall(LabiryntElement.WallEN, new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            modelMap.Add(new ModelWall(LabiryntElement.Pillar, new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.2f, 0.2f, 0.17f)));
                            modelMap.Add(new ModelWall(LabiryntElement.WallEN, new Vector3(i, 0, j), new Vector3(270, 270, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.WallES:
                            modelMap.Add(new ModelWall(LabiryntElement.WallES, new Vector3(i, 0, j), new Vector3(270, 180, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            modelMap.Add(new ModelWall(LabiryntElement.Pillar, new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.2f, 0.2f, 0.17f)));
                            modelMap.Add(new ModelWall(LabiryntElement.WallES, new Vector3(i, 0, j), new Vector3(270, 270, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.WallEW:
                            modelMap.Add(new ModelWall(LabiryntElement.WallEW, new Vector3(i, 0, j), new Vector3(270, 90, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.WallNS:
                            modelMap.Add(new ModelWall(LabiryntElement.WallNS, new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.WallWN:
                            modelMap.Add(new ModelWall(LabiryntElement.WallWN, new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            modelMap.Add(new ModelWall(LabiryntElement.Pillar, new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.2f, 0.2f, 0.17f)));
                            modelMap.Add(new ModelWall(LabiryntElement.WallWN, new Vector3(i, 0, j), new Vector3(270, 90, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.WallWS:
                            modelMap.Add(new ModelWall(LabiryntElement.WallWS, new Vector3(i, 0, j), new Vector3(270, 180, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            modelMap.Add(new ModelWall(LabiryntElement.Pillar, new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.2f, 0.2f, 0.17f)));
                            modelMap.Add(new ModelWall(LabiryntElement.WallWS, new Vector3(i, 0, j), new Vector3(270, 90, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.Wall3WayEast:
                            modelMap.Add(new ModelWall(LabiryntElement.WallWS, new Vector3(i, 0, j), new Vector3(270, 180, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            modelMap.Add(new ModelWall(LabiryntElement.Pillar, new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.2f, 0.2f, 0.17f)));
                            modelMap.Add(new ModelWall(LabiryntElement.WallWS, new Vector3(i, 0, j), new Vector3(270, 90, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            modelMap.Add(new ModelWall(LabiryntElement.WallWS, new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.Wall3WayWest:
                            modelMap.Add(new ModelWall(LabiryntElement.WallWS, new Vector3(i, 0, j), new Vector3(270, 180, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            modelMap.Add(new ModelWall(LabiryntElement.Pillar, new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.2f, 0.2f, 0.17f)));
                            modelMap.Add(new ModelWall(LabiryntElement.WallWS, new Vector3(i, 0, j), new Vector3(270, 270, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            modelMap.Add(new ModelWall(LabiryntElement.WallWS, new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.Wall3WayNorth:
                            modelMap.Add(new ModelWall(LabiryntElement.WallWS, new Vector3(i, 0, j), new Vector3(270, 270, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            modelMap.Add(new ModelWall(LabiryntElement.Pillar, new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.2f, 0.2f, 0.17f)));
                            modelMap.Add(new ModelWall(LabiryntElement.WallWS, new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            modelMap.Add(new ModelWall(LabiryntElement.WallWS, new Vector3(i, 0, j), new Vector3(270, 90, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.Wall3WaySouth:
                            modelMap.Add(new ModelWall(LabiryntElement.WallWS, new Vector3(i, 0, j), new Vector3(270, 90, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            modelMap.Add(new ModelWall(LabiryntElement.Pillar, new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.2f, 0.2f, 0.17f)));
                            modelMap.Add(new ModelWall(LabiryntElement.WallWS, new Vector3(i, 0, j), new Vector3(270, 180, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            modelMap.Add(new ModelWall(LabiryntElement.WallWS, new Vector3(i, 0, j), new Vector3(270, 270, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break; 
                    }
                    groundMap.Add(new Cube(game.GraphicsDevice, new Vector3(0.5f), new Vector3(i, -0.5f, j), 2f));
                }
            }
            modelMap.ForEach(i => i.setupModel());
        }
    }
}

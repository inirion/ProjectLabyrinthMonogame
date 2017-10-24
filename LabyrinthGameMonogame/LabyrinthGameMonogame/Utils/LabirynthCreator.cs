using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.GameFolder;
using LabyrinthGameMonogame.GameFolder.Enteties;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace LabyrinthGameMonogame.Utils
{
    class LabirynthCreator
    {
        LabirynthGenerator generator;
        List<Wall> map;
        Vector2 size;


        public LabirynthCreator(Game game) {
            generator = new LabirynthGenerator(game);
            Map = new List<Wall>();
            Size = new Vector2();
        }

        public List<Wall> Map { get => map; set => map = value; }
        public Vector2 Size { get => size; set => size = value; }

        public Vector3 GetStartingPosition()
        {
            Vector3 tmp = map.FirstOrDefault(i => i.LabiryntElement == LabiryntElement.Start).Position;
            tmp.Y = 0.2f;
            return tmp;
        }

        public Vector3 GetFinishPosition()
        {
            Vector3 tmp = map.FirstOrDefault(i => i.LabiryntElement == LabiryntElement.Finish).Position;
            return tmp;
        }

        public void CreateMap()
        {
            map.Clear();
            map = new List<Wall>();

            generator.CreateMaze();

            for (int i = 0; i < generator.Maze.GetLength(0); i++)
            {
                for (int j = 0; j < generator.Maze.GetLength(1); j++)
                {
                    if(i== generator.Maze.GetLength(1)-1 && j == 0)
                    {
                        size.X = i;
                    }
                    if( i== 0 && j == generator.Maze.GetLength(1) - 1)
                    {
                        size.Y = j;
                    }
                    switch ((LabiryntElement)generator.Maze[i, j])
                    {
                        case LabiryntElement.Wall:
                            map.Add(new Wall(LabiryntElement.Wall, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            map.Add(new Wall(LabiryntElement.Wall, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 90, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.WallEN:
                            map.Add(new Wall(LabiryntElement.WallEN, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            map.Add(new Wall(LabiryntElement.Pillar, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.2f, 0.2f, 0.17f)));
                            map.Add(new Wall(LabiryntElement.WallEN, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 270, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.WallES:
                            map.Add(new Wall(LabiryntElement.WallES, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 180, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            map.Add(new Wall(LabiryntElement.Pillar, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.2f, 0.2f, 0.17f)));
                            map.Add(new Wall(LabiryntElement.WallES, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 270, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.WallEW:
                            map.Add(new Wall(LabiryntElement.WallEW, "BakedBrick", new Vector3(i, 0, j), new Vector3(270, 90, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.WallNS:
                            map.Add(new Wall(LabiryntElement.WallNS, "BakedBrick", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.WallWN:
                            map.Add(new Wall(LabiryntElement.WallWN, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            map.Add(new Wall(LabiryntElement.Pillar, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.2f, 0.2f, 0.17f)));
                            map.Add(new Wall(LabiryntElement.WallWN, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 90, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.WallWS:
                            map.Add(new Wall(LabiryntElement.WallWS, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 180, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            map.Add(new Wall(LabiryntElement.Pillar, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.2f, 0.2f, 0.17f)));
                            map.Add(new Wall(LabiryntElement.WallWS, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 90, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.Wall3WayEast:
                            map.Add(new Wall(LabiryntElement.WallWS, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 180, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            map.Add(new Wall(LabiryntElement.Pillar, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.2f, 0.2f, 0.17f)));
                            map.Add(new Wall(LabiryntElement.WallWS, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 90, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            map.Add(new Wall(LabiryntElement.WallWS, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.Wall3WayWest:
                            map.Add(new Wall(LabiryntElement.WallWS, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 180, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            map.Add(new Wall(LabiryntElement.Pillar, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.2f, 0.2f, 0.17f)));
                            map.Add(new Wall(LabiryntElement.WallWS, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 270, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            map.Add(new Wall(LabiryntElement.WallWS, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.Wall3WayNorth:
                            map.Add(new Wall(LabiryntElement.WallWS, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 270, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            map.Add(new Wall(LabiryntElement.Pillar, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.2f, 0.2f, 0.17f)));
                            map.Add(new Wall(LabiryntElement.WallWS, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            map.Add(new Wall(LabiryntElement.WallWS, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 90, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.Wall3WaySouth:
                            map.Add(new Wall(LabiryntElement.WallWS, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 90, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            map.Add(new Wall(LabiryntElement.Pillar, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.2f, 0.2f, 0.17f)));
                            map.Add(new Wall(LabiryntElement.WallWS, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 180, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            map.Add(new Wall(LabiryntElement.WallWS, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 270, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.Start:
                            map.Add(new Wall(LabiryntElement.Start, "Wooden_House", new Vector3(i, 0, j), new Vector3(270, 0, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                        case LabiryntElement.Finish:
                            map.Add(new Wall(LabiryntElement.Finish, "Wooden_House", new Vector3(i, 0, j), new Vector3(0, 0, 0), new Vector3(0.063f, 0.05f, 0.07f)));
                            break;
                    }
                }
            }
            map.ForEach(i => i.setupModel());
        }
    }
}

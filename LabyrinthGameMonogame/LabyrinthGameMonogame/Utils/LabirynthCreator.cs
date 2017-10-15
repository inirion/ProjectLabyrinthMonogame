using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.GameFolder;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace LabyrinthGameMonogame.Utils
{
    class LabirynthCreator
    {
        LabirynthGenerator generator;
        List<GameObject> map;

        public LabirynthCreator() {
            generator = new LabirynthGenerator();
            Map = new List<GameObject>();
        }

        public List<GameObject> Map { get => map; set => map = value; }

        public Vector3 GetStartingPosition()
        {
            Vector3 tmp = map.FirstOrDefault(i => i.LabiryntElement == LabiryntElement.Start).Position;
            tmp.Y += 1;
            return tmp;
        }

        public void CreateMap()
        {
            float gap = 1.0f;
            map.Clear();
            map = new List<GameObject>();

            generator.CreateMaze();

            for (int i = 0; i < generator.Maze.GetLength(0); i++)
            {
                for (int j = 0; j < generator.Maze.GetLength(1); j++)
                {
                    switch ((LabiryntElement)generator.Maze[i, j])
                    {
                        case LabiryntElement.Wall:
                            map.Add(new GameObject(LabiryntElement.Wall,"Wooden_House", new Vector3(gap * i, 0, gap * j), new Vector3(270, 0, 0)));
                            break;
                        case LabiryntElement.WallEN:
                            map.Add(new GameObject(LabiryntElement.WallEN,"Wooden_House", new Vector3(gap * i, 0, gap * j), new Vector3(270, 0, 0)));
                            break;
                        case LabiryntElement.WallES:
                            map.Add(new GameObject(LabiryntElement.WallEN, "Wooden_House", new Vector3(gap * i, 0, gap * j), new Vector3(270, 0, 0)));
                            break;
                        case LabiryntElement.WallEW:
                            map.Add(new GameObject(LabiryntElement.WallEN, "Wooden_House", new Vector3(gap * i, 0, gap * j), new Vector3(270, 0, 0)));
                            break;
                        case LabiryntElement.WallNS:
                            map.Add(new GameObject(LabiryntElement.WallEN, "Wooden_House", new Vector3(gap * i, 0, gap * j), new Vector3(270, 90, 0)));
                            break;
                        case LabiryntElement.WallWN:
                            map.Add(new GameObject(LabiryntElement.WallEN, "Wooden_House", new Vector3(gap * i, 0, gap * j), new Vector3(270, 0, 0)));
                            break;
                        case LabiryntElement.WallWS:
                            map.Add(new GameObject(LabiryntElement.WallEN, "Wooden_House", new Vector3(gap * i, 0, gap * j), new Vector3(270, 0, 0)));
                            break;
                        case LabiryntElement.Start:
                            map.Add(new GameObject(LabiryntElement.Start, "Wooden_House", new Vector3(gap * i, 0, gap * j), new Vector3(0, 0, 0)));
                            break;
                        case LabiryntElement.Finish:
                            map.Add(new GameObject(LabiryntElement.Finish, "Wooden_House", new Vector3(gap * i, 0, gap * j), new Vector3(0, 0, 0)));
                            break;
                    }
                }
            }
            map.ForEach(i => i.setupModel());
        }
    }
}

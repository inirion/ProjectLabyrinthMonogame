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


        public LabirynthCreator() {
            generator = new LabirynthGenerator();
            Map = new List<Wall>();
            Size = new Vector2();
        }

        public List<Wall> Map { get => map; set => map = value; }
        public Vector2 Size { get => size; set => size = value; }

        public Vector3 GetStartingPosition()
        {
            Vector3 tmp = map.FirstOrDefault(i => i.LabiryntElement == LabiryntElement.Start).Position;
            tmp.Y = .2f;
            return tmp;
        }

        public Vector3 GetFinishPosition()
        {
            Vector3 tmp = map.FirstOrDefault(i => i.LabiryntElement == LabiryntElement.Finish).Position;
            return tmp;
        }

        public void CreateMap()
        {
            float gap = 1.0f;
            map.Clear();
            map = new List<Wall>();

            generator.CreateMaze();

            for (int i = 0; i < generator.Maze.GetLength(0); i++)
            {
                for (int j = 0; j < generator.Maze.GetLength(1); j++)
                {
                    if(i== generator.Maze.GetLength(1)-1 && j == 0)
                    {
                        size.X = i * gap;
                    }
                    if( i== 0 && j == generator.Maze.GetLength(1) - 1)
                    {
                        size.Y = j*gap;
                    }
                    switch ((LabiryntElement)generator.Maze[i, j])
                    {
                        case LabiryntElement.Wall:
                            map.Add(new Wall(LabiryntElement.Wall, "Wooden_House", new Vector3(gap * i, 0, gap * j), new Vector3(270, 0, 0)));
                            break;
                        case LabiryntElement.WallEN:
                            map.Add(new Wall(LabiryntElement.WallEN,"Wooden_House", new Vector3(gap * i, 0, gap * j), new Vector3(270, 0, 0)));
                            break;
                        case LabiryntElement.WallES:
                            map.Add(new Wall(LabiryntElement.WallEN, "Wooden_House", new Vector3(gap * i, 0, gap * j), new Vector3(270, 0, 0)));
                            break;
                        case LabiryntElement.WallEW:
                            map.Add(new Wall(LabiryntElement.WallEN, "Wooden_House", new Vector3(gap * i, 0, gap * j), new Vector3(270, 0, 0)));
                            break;
                        case LabiryntElement.WallNS:
                            map.Add(new Wall(LabiryntElement.WallEN, "Wooden_House", new Vector3(gap * i, 0, gap * j), new Vector3(270, 90, 0)));
                            break;
                        case LabiryntElement.WallWN:
                            map.Add(new Wall(LabiryntElement.WallEN, "Wooden_House", new Vector3(gap * i, 0, gap * j), new Vector3(270, 0, 0)));
                            break;
                        case LabiryntElement.WallWS:
                            map.Add(new Wall(LabiryntElement.WallEN, "Wooden_House", new Vector3(gap * i, 0, gap * j), new Vector3(270, 0, 0)));
                            break;
                        case LabiryntElement.Start:
                            map.Add(new Wall(LabiryntElement.Start, "Wooden_House", new Vector3(gap * i, 0, gap * j), new Vector3(0, 0, 0)));
                            break;
                        case LabiryntElement.Finish:
                            map.Add(new Wall(LabiryntElement.Finish, "Wooden_House", new Vector3(gap * i, 0, gap * j), new Vector3(50, 0, 0)));
                            break;
                    }
                }
            }
            map.ForEach(i => i.setupModel());
        }
    }
}

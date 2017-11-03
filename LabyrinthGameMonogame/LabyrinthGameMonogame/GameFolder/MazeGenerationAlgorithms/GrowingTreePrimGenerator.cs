using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.Utils.Randomizers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LabyrinthGameMonogame.GameFolder.MazeGenerationAlgorithms
{
    public enum PickMethod : byte { Newest, Oldest, Random, Cyclic };

    [Flags]
    public enum Direction : byte { North = 0x1, West = 0x2, South = 0x4, East = 0x8 };
    class GrowingTreePrimGenerator
    {
        public int[,] Maze { get; private set; }
        public Vector3 spawnpoint { get; private set; }
        public Vector3 exitpoint { get; private set; }
        public UInt16 width { get; private set; }
        public UInt16 height { get; private set; }
        public List<Vector3> keys { get; private set; }
        IRandomizer rnd;
        IGameManager gameManager;

        public Random random = new Random((int)DateTime.Now.Ticks & (0x0000FFFF));

        private UInt16 CyclePick = 0;

        public GrowingTreePrimGenerator( Game game, UInt16 startx = 0, UInt16 starty = 0)
        {
            keys = new List<Vector3>();
            gameManager = (IGameManager)game.Services.GetService(typeof(IGameManager));
            rnd = new LabirynthRandomizer();
            this.width = (UInt16)gameManager.DifficultyLevel;
            this.height = (UInt16)gameManager.DifficultyLevel;

            Maze = BuildBaseMaze(width, height);
        }


        public void cell(UInt16 x, UInt16 y, byte value = 0)
        {
            if (x <= this.width && y <= this.height)
            {
                Maze[x, y] = value;
            }
        }


        public int[,] BuildBaseMaze(UInt16 width, UInt16 length)
        {
            int[,] maze = new int[width, length];

            return maze;
        }

        public void Display()
        {
            for (int i = 0; i < Maze.GetLength(0); i++)
            {
                for (int j = 0; j < Maze.GetLength(1); j++)
                {
                        Debug.Write(Maze[i, j]);

                }
                Debug.WriteLine("");
            }
        }


        public void CreateMaze()
        {
            keys.Clear();
            this.width = (UInt16)gameManager.DifficultyLevel;
            this.height = (UInt16)gameManager.DifficultyLevel;
            Maze = BuildBaseMaze(width, height);
            List<UInt16[]> CarvedMaze = new List<UInt16[]>();
            List<UInt16[]> cells = new List<UInt16[]>();

            Array DirectionArray = Enum.GetValues(typeof(Direction));

            UInt16 x = (Byte)(random.Next(Maze.GetLength(0) - 1) + 1);
            UInt16 y = (Byte)(random.Next(Maze.GetLength(1) - 1) + 1);
            Int16 nx;
            Int16 ny;

            CarvedMaze.Add(new UInt16[3] { x, y, 1 });

            cells.Add(new UInt16[2] { x, y });

            while (cells.Count > 0)
            {
                Int16 index = (Int16)chooseIndex((UInt16)cells.Count, PickMethod.Random);
                UInt16[] cell_picked = cells[index];

                x = cell_picked[0];
                y = cell_picked[1];
                CarvedMaze.Add(new UInt16[3] { x, y, 1 });

                Direction[] tmpdir = RandomizeDirection();

                foreach (Direction way in tmpdir)
                {
                    SByte[] move = DoAStep(way);

                    nx = (Int16)(x + move[0]);
                    ny = (Int16)(y + move[1]);

                    if (nx >= 0 && ny >= 0 && nx < Maze.GetLength(0) && ny < Maze.GetLength(1) && Maze[nx, ny] == 0)
                    {
                        CarvedMaze.Add(new UInt16[3] { (UInt16)nx, (UInt16)ny, 2 });

                        Maze[x, y] |= (byte)way;
                        Maze[nx, ny] |= (byte)OppositeDirection(way);

                        cells.Add(new UInt16[2] { (UInt16)nx, (UInt16)ny });

                        index = -1;
                        CarvedMaze.Add(new UInt16[3] { (UInt16)nx, (UInt16)ny, 3 });
                        break;
                    }
                    else
                    {
                    }
                }
                if (index != -1)
                {
                    UInt16[] cell_removed = cells[index];

                    cells.RemoveAt(index);
                    CarvedMaze.Add(new UInt16[3] { (UInt16)x, (UInt16)y, 4 });
                }
            }
            Maze = LineToBlock();


            List<Point> points = GetSuitableStartFinishPoint();
            
            int start = rnd.Roll(0, points.Count);
            Maze[points[start].X, points[start].Y] = (int)LabiryntElement.Start;
            spawnpoint = new Vector3(points[start].X, 0, points[start].Y);
            points.RemoveAt(start);

            int finish = rnd.Roll(0, points.Count);
            Maze[points[finish].X, points[finish].Y] = (int)LabiryntElement.Finish;
            exitpoint = new Vector3(points[finish].X, 0, points[finish].Y);
            points.RemoveAt(finish);

            int key = 0;
            if (points.Count > 0)
            {
                key = rnd.Roll(0, points.Count);
                Maze[points[key].X, points[key].Y] = (int)LabiryntElement.Key;
                keys.Add(new Vector3(points[key].X, 0, points[key].Y));
                points.RemoveAt(key);
            }

            if (points.Count > 0)
            {
                key = rnd.Roll(0, points.Count);
                Maze[points[key].X, points[key].Y] = (int)LabiryntElement.Key;
                keys.Add(new Vector3(points[key].X, 0, points[key].Y));
                points.RemoveAt(key);
            }

            points.Clear();
            //Display();
        }

        private List<Point> GetSuitableStartFinishPoint()
        {
            List<Point> points = new List<Point>();
            for (int x = 1; x < Maze.GetLength(0) - 1; x++)
            {
                for (int y = 1; y < Maze.GetLength(1) - 1; y++)
                {
                    if (Maze[x, y] == (int)LabiryntElement.Road)
                    {
                        if (Maze[x, y - 1] == (int)LabiryntElement.Road && Maze[x, y + 1] != (int)LabiryntElement.Road && Maze[x - 1, y] != (int)LabiryntElement.Road && Maze[x + 1, y] != (int)LabiryntElement.Road)
                        {
                            points.Add(new Point(x, y));
                        }
                        if (Maze[x, y - 1] != (int)LabiryntElement.Road && Maze[x, y + 1] == (int)LabiryntElement.Road && Maze[x - 1, y] != (int)LabiryntElement.Road && Maze[x + 1, y] != (int)LabiryntElement.Road)
                        {
                            points.Add(new Point(x, y));
                        }
                        if (Maze[x, y - 1] != (int)LabiryntElement.Road && Maze[x, y + 1] != (int)LabiryntElement.Road && Maze[x - 1, y] == (int)LabiryntElement.Road && Maze[x + 1, y] != (int)LabiryntElement.Road)
                        {
                            points.Add(new Point(x, y));
                        }
                        if (Maze[x, y - 1] != (int)LabiryntElement.Road && Maze[x, y + 1] != (int)LabiryntElement.Road && Maze[x - 1, y] != (int)LabiryntElement.Road && Maze[x + 1, y] == (int)LabiryntElement.Road)
                        {
                            points.Add(new Point(x, y));
                        }
                    }
                }
            }
            

            return points;
        }

        public UInt16 chooseIndex(UInt16 max, PickMethod pickmet)
        {
            UInt16 index = 0;

            switch (pickmet)
            {
                case PickMethod.Cyclic:
                    CyclePick = (UInt16)((CyclePick + 1) % max);
                    index = CyclePick;
                    break;

                case PickMethod.Random:
                    Random random = new Random((int)DateTime.Now.Ticks & (0x0000FFFF));
                    index = (UInt16)(random.Next(max - 1));
                    break;

                case PickMethod.Oldest:
                    index = 0;
                    break;

                case PickMethod.Newest:
                default:
                    if (max >= 1)
                    {
                        index = (UInt16)(max - 1);
                    }
                    else
                    {
                        index = 0;
                    }

                    break;
            }
            return index;
        }


        public Direction chooseARandomDirection()
        {
            Direction randir;

            var EnumToArray = Enum.GetValues(typeof(Direction));
            Byte tmp1 = (Byte)(random.Next(EnumToArray.Length - 1));
            randir = (Direction)EnumToArray.GetValue(tmp1);

            Debug.Print(string.Format("Dir=[{0}]", randir));

            return randir;
        }


        public Direction[] RandomizeDirection()
        {
            Direction[] randir;

            Array tmparray = Enum.GetValues(typeof(Direction));

            randir = (Direction[])tmparray;

            Shuffle<Direction>(randir);

            return randir;
        }



        private void Shuffle<T>(T[] array)
        {
            int n = array.Length;

            for (int i = 0; i < n; i++)
            {
                int r = i + (int)(random.NextDouble() * (n - i));
                T t = array[r];
                array[r] = array[i];
                array[i] = t;
            }
        }


        public Direction OppositeDirection(Direction forward)
        {
            Direction opposite = Direction.North;

            switch (forward)
            {
                case Direction.North:
                    opposite = Direction.South;
                    break;
                case Direction.South:
                    opposite = Direction.North;
                    break;
                case Direction.East:
                    opposite = Direction.West;
                    break;
                case Direction.West:
                    opposite = Direction.East;
                    break;
            }

            return opposite;
        }


        public SByte[] DoAStep(Direction facingDirection)
        {
            SByte[] step = { 0, 0 };

            switch (facingDirection)
            {
                case Direction.North:
                    step[0] = 0;
                    step[1] = -1;
                    break;
                case Direction.South:
                    step[0] = 0;
                    step[1] = 1;
                    break;
                case Direction.East:
                    step[0] = 1;
                    step[1] = 0;
                    break;
                case Direction.West:
                    step[0] = -1;
                    step[1] = 0;
                    break;
            }
            return step;
        }


        public int[,] LineToBlock()
        {
            int[,] blockmaze;

            if (Maze == null || Maze.GetLength(0) <= 1 && Maze.GetLength(1) <= 1)
            {
                return null;
            }

            blockmaze = new int[2 * Maze.GetLength(0) + 1, 2 * Maze.GetLength(1) + 1];

            for (UInt16 wall = 0; wall < 2 * Maze.GetLength(1) + 1; wall++)
            {
                blockmaze[0, wall] = (int)LabiryntElement.Wall;
            }

            for (UInt16 wall = 0; wall < 2 * Maze.GetLength(0) + 1; wall++)
            {
                blockmaze[wall, 0] = (int)LabiryntElement.Wall;
            }

            for (UInt16 y = 0; y < Maze.GetLength(1); y++)
            {
                for (UInt16 x = 0; x < Maze.GetLength(0); x++)
                {
                    blockmaze[2 * x + 1, 2 * y + 1] = (int)LabiryntElement.Road;
                    if ((Maze[x, y] & (Byte)Direction.East) != 0)
                    {
                        blockmaze[2 * x + 2, 2 * y + 1] = 0; // B
                    }
                    else
                    {
                        blockmaze[2 * x + 2, 2 * y + 1] = (int)LabiryntElement.Wall;
                    }

                    if ((Maze[x, y] & (Byte)Direction.South) != 0)
                    {
                        blockmaze[2 * x + 1, 2 * y + 2] = (int)LabiryntElement.Road; // C
                    }
                    else
                    {
                        blockmaze[2 * x + 1, 2 * y + 2] = (int)LabiryntElement.Wall;
                    }
                    blockmaze[2 * x + 2, 2 * y + 2] = (int)LabiryntElement.Wall;
                }
            }

            return blockmaze;
        }
    }
}

﻿using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.Utils.Randomizers;
using System.Diagnostics;

namespace LabyrinthGameMonogame.GameFolder
{
    class LabirynthGenerator
    {
        int[,] maze;
        int size;
        int calculatedSize;
        int[] backtrack_x;
        int[] backtrack_y;
        IRandomizer rnd;

        public int[,] Maze { get => maze; set => maze = value; }

        public LabirynthGenerator()
        {
            rnd = new LabirynthRandomizer();
        }

        private void InitMaze()
        {
            for (int a = 0; a < calculatedSize; a++)
            {
                for (int b = 0; b < calculatedSize; b++)
                {
                    if (a % 2 == 0 || b % 2 == 0)
                        Maze[a,b] = (int)LabiryntElement.Wall;
                    else
                        Maze[a,b] = (int)LabiryntElement.Road;
                }
            }
        }

        private bool IsClosed(int[,] maze, int x, int y)
        {
            if ((maze[x - 1,y] == (int)LabiryntElement.Wall || maze[x - 1, y] == (int)LabiryntElement.WallNS || maze[x - 1, y] == (int)LabiryntElement.WallEW)
                && (maze[x,y - 1] == (int)LabiryntElement.Wall || maze[x, y - 1] == (int)LabiryntElement.WallNS || maze[x, y - 1] == (int)LabiryntElement.WallEW)
                && (maze[x,y + 1] == (int)LabiryntElement.Wall || maze[x, y + 1] == (int)LabiryntElement.WallNS || maze[x, y + 1] == (int)LabiryntElement.WallEW)
                && (maze[x + 1,y] == (int)LabiryntElement.Wall || maze[x + 1, y] == (int)LabiryntElement.WallNS || maze[x + 1, y] == (int)LabiryntElement.WallEW)) return true;

            return false;
        }

        private void setWallsTurns(int x, int y)
        {
            if (maze[x, y - 1] == (int)LabiryntElement.WallEW && maze[x, y + 1] == (int)LabiryntElement.Road && maze[x + 1, y] == (int)LabiryntElement.Road && maze[x - 1, y] == (int)LabiryntElement.WallNS)//prawo-gora
            {
                maze[x, y] = (int)LabiryntElement.WallEN;
            }
            if (maze[x, y - 1] == (int)LabiryntElement.WallEW && maze[x, y + 1] == (int)LabiryntElement.Road && maze[x+1, y] == (int)LabiryntElement.WallNS && maze[x-1, y] == (int)LabiryntElement.Road)//prawo-dol
            {
                maze[x, y] = (int)LabiryntElement.WallES;
            }
            if (maze[x, y - 1] == (int)LabiryntElement.Road && maze[x, y + 1] == (int)LabiryntElement.WallEW && maze[x + 1, y] == (int)LabiryntElement.Road && maze[x - 1, y] == (int)LabiryntElement.WallNS)//lewo-gora
            {
                maze[x, y] = (int)LabiryntElement.WallWN;
            }
            if (maze[x, y - 1] == (int)LabiryntElement.Road && maze[x, y + 1] == (int)LabiryntElement.WallEW && maze[x + 1, y] == (int)LabiryntElement.WallNS && maze[x - 1, y] == (int)LabiryntElement.Road)//lewo-dol
            {
                maze[x, y] = (int)LabiryntElement.WallWS;
            }
        }

        private void setWalls(int x, int y)
        {
            if (maze[x, y - 1] == (int)LabiryntElement.Road && maze[x, y + 1] == (int)LabiryntElement.Road)//pionowa
            {
                maze[x, y] = (int)LabiryntElement.WallNS;
            }
            if (maze[x - 1, y] == (int)LabiryntElement.Road && maze[x + 1, y] == (int)LabiryntElement.Road)//pozioma
            {
                maze[x, y] = (int)LabiryntElement.WallEW;
            }
        }
        private void setWallsConnections(int x, int y)
        {
            
            if (maze[x, y-1] == (int)LabiryntElement.Road && maze[x + 1, y] == (int)LabiryntElement.WallNS && maze[x -1, y] == (int)LabiryntElement.WallNS && maze[x, y+1] == (int)LabiryntElement.WallEW)//pionowa 3 elementy(wolne lewo)
            {
                maze[x, y] = (int)LabiryntElement.WallNS;
            }
            if (maze[x, y + 1] == (int)LabiryntElement.Road && maze[x + 1, y] == (int)LabiryntElement.WallNS && maze[x - 1, y] == (int)LabiryntElement.WallNS && maze[x, y - 1] == (int)LabiryntElement.WallEW)//pionowa 3 elementy(wolne prawo)
            {
                maze[x, y] = (int)LabiryntElement.WallNS;
            }
            if (maze[x + 1, y] == (int)LabiryntElement.Road && maze[x - 1, y] == (int)LabiryntElement.WallNS && maze[x, y + 1] == (int)LabiryntElement.WallEW && maze[x, y - 1] == (int)LabiryntElement.WallEW)//pozioma 3 elementy(wolne dol)
            {
                maze[x, y] = (int)LabiryntElement.WallEW;
            }
            if (maze[x - 1, y] == (int)LabiryntElement.Road && maze[x + 1, y] == (int)LabiryntElement.WallNS && maze[x, y + 1] == (int)LabiryntElement.WallEW && maze[x, y - 1] == (int)LabiryntElement.WallEW)//pozioma 3 elementy(wolne gora)
            {
                maze[x, y] = (int)LabiryntElement.WallEW;
            }
        }

        public void CreateMaze()
        {
            switch (GameManager.Instance.DifficultyLevel)
            {
                case DifficultyLevel.Easy:
                    size = 5;
                    break;
                case DifficultyLevel.Medium:
                    size = 10;
                    break;
                case DifficultyLevel.Hard:
                    size = 15;
                    break;
            }
            calculatedSize = size * 2 + 1;
            Maze = new int[calculatedSize, calculatedSize];
            backtrack_x = new int[size * size];
            backtrack_y = new int[size * size];

            InitMaze();
            GenerateMaze(0, 1, 1, 1);
            for (int i = 1; i < calculatedSize-1; i++)
            {
                for (int j = 1; j < calculatedSize-1; j++)
                {
                    if(maze[i,j] == (int)LabiryntElement.Wall)
                    {
                        setWalls(i, j);
                    }
                }
            }
            for (int i = 1; i < calculatedSize - 1; i++)
            {
                for (int j = 1; j < calculatedSize - 1; j++)
                {
                    if (maze[i, j] == (int)LabiryntElement.Wall)
                    {
                        setWallsConnections(i, j);
                    }
                }
            }
            for (int i = 1; i < calculatedSize - 1; i++)
            {
                for (int j = 1; j < calculatedSize - 1; j++)
                {
                    if (maze[i, j] == (int)LabiryntElement.Wall)
                    {
                        setWallsTurns(i, j);
                    }
                }
            }

            for (int i = 0; i < calculatedSize; i++)
            {
                for (int j = 0; j < calculatedSize; j++)
                {
                    if(Maze[i, j] == (int)LabiryntElement.Wall)
                        Debug.Write("?");
                    else if(Maze[i, j] == (int)LabiryntElement.WallNS)
                        Debug.Write("|");
                    else if (Maze[i, j] == (int)LabiryntElement.WallEW)
                        Debug.Write("-");
                    else if (maze[i,j] == (int)LabiryntElement.WallES)
                        Debug.Write("╗");
                    else if (maze[i, j] == (int)LabiryntElement.WallEN)
                        Debug.Write("╝");
                    else if (maze[i, j] == (int)LabiryntElement.WallWS)
                        Debug.Write("╔");
                    else if (maze[i, j] == (int)LabiryntElement.WallWN)
                        Debug.Write("╚");
                    else
                        Debug.Write(" ");
                }
                Debug.WriteLine("");
            }
            Debug.WriteLine("");
            Debug.WriteLine("");
        }

        private void GenerateMaze(int index, int x, int y, int visited)
        {


            if (visited < size*size)
            {
                int neighbour_valid = -1;
                int[] neighbour_x = new int[4];
                int[] neighbour_y = new int[4];
                int[] step = new int[4];
                int x_next = 0;
                int y_next = 0;

                if (x - 2 > 0 && IsClosed(Maze, x - 2, y))
                {
                    neighbour_valid++;
                    neighbour_x[neighbour_valid] = x - 2;
                    neighbour_y[neighbour_valid] = y;
                    step[neighbour_valid] = 1;
                }
                if (y - 2 > 0 && IsClosed(Maze, x, y - 2))
                {
                    neighbour_valid++;
                    neighbour_x[neighbour_valid] = x;
                    neighbour_y[neighbour_valid] = y - 2;
                    step[neighbour_valid] = 2;
                }

                if (y + 2 < calculatedSize && IsClosed(Maze, x, y + 2))
                {
                    neighbour_valid++;
                    neighbour_x[neighbour_valid] = x;
                    neighbour_y[neighbour_valid] = y + 2;
                    step[neighbour_valid] = 3;
                }

                if (x + 2 < calculatedSize && IsClosed(Maze, x + 2, y))
                {
                    neighbour_valid++;
                    neighbour_x[neighbour_valid] = x + 2;
                    neighbour_y[neighbour_valid] = y;
                    step[neighbour_valid] = 4;
                }

                if (neighbour_valid == -1)
                {
                    // cofanie
                    x_next = backtrack_x[index];
                    y_next = backtrack_y[index];
                    index--;
                }

                if (neighbour_valid != -1)
                {
                    int randomization = neighbour_valid + 1;
                    int random = rnd.Roll(0, randomization);
                    x_next = neighbour_x[random];
                    y_next = neighbour_y[random];
                    index++;
                    backtrack_x[index] = x_next;
                    backtrack_y[index] = y_next;
                    
                    int rstep = step[random];
                    if (rstep == 1)
                    {
                        Maze[x_next + 1, y_next] = (int)LabiryntElement.Road;
                    }
                        
                    else if (rstep == 2)
                    {
                        Maze[x_next, y_next + 1] = (int)LabiryntElement.Road;
                    }
                        
                    else if (rstep == 3)
                    {
                        Maze[x_next, y_next - 1] = (int)LabiryntElement.Road;
                    }
                        
                    else if (rstep == 4)
                    {
                        Maze[x_next - 1, y_next] = (int)LabiryntElement.Road;
                    }
                       
                    visited++;
                }
                GenerateMaze(index, x_next, y_next, visited);
            }
        }
    }
}
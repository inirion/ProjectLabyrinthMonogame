using LabyrinthGameMonogame.Enums;
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
            if (maze[x - 1,y] == (int)LabiryntElement.Wall && maze[x,y - 1] == (int)LabiryntElement.Wall && maze[x,y + 1] == (int)LabiryntElement.Wall && maze[x + 1,y] == (int)LabiryntElement.Wall) return true;

            return false;
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
            for (int i = 0; i < calculatedSize; i++)
            {
                for (int j = 0; j < calculatedSize; j++)
                {
                    if(Maze[i, j] == 1)
                        Debug.Write("#");
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
                        Maze[x_next + 1,y_next] = (int)LabiryntElement.Road;
                    else if (rstep == 2)
                        Maze[x_next,y_next + 1] = (int)LabiryntElement.Road;
                    else if (rstep == 3)
                        Maze[x_next,y_next - 1] = (int)LabiryntElement.Road;
                    else if (rstep == 4)
                        Maze[x_next - 1,y_next] = (int)LabiryntElement.Road;
                    visited++;
                }
                GenerateMaze(index, x_next, y_next, visited);
            }
        }
    }
}

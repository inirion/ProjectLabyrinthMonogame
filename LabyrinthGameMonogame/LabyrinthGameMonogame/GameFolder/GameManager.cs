using LabyrinthGameMonogame.Enums;

namespace LabyrinthGameMonogame.GameFolder
{
    class GameManager
    {
        private static GameManager instance;

        private GameManager()
        {
            IsGameRunning = false;
            ResetGame = false;
            DifficultyLevel = DifficultyLevel.Easy;
        }

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }
                return instance;
            }
        }

        public bool IsGameRunning { get => isGameRunning; set => isGameRunning = value; }
        public bool ResetGame { get => resetGame; set => resetGame = value; }
        internal DifficultyLevel DifficultyLevel { get => difficultyLevel; set => difficultyLevel = value; }

        private bool isGameRunning;
        private bool resetGame;
        private DifficultyLevel difficultyLevel;
    }
}

using LabyrinthGameMonogame.Enums;

namespace LabyrinthGameMonogame.GameFolder
{
    class GameManager:IGameManager
    {
        public GameManager()
        {
            isGameRunning = false;
            resetGame = false;
            difficultyLevel = DifficultyLevel.Easy;
        }

        private bool isGameRunning;
        private bool resetGame;
        private DifficultyLevel difficultyLevel;

        bool IGameManager.IsGameRunning { get => isGameRunning; set => isGameRunning = value; }
        bool IGameManager.ResetGame { get => resetGame; set => resetGame = value; }
        DifficultyLevel IGameManager.DifficultyLevel { get => difficultyLevel; set => difficultyLevel = value; }
    }
}

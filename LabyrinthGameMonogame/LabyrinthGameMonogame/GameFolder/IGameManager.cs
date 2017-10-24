using LabyrinthGameMonogame.Enums;

namespace LabyrinthGameMonogame.GameFolder
{
    interface IGameManager
    {
        bool IsGameRunning { get; set; }
        bool ResetGame { get; set; }
        DifficultyLevel DifficultyLevel { get; set; }
    }
}

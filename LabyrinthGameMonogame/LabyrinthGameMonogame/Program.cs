using LabyrinthGameMonogame.GUI.Screens;
using System;

namespace LabyrinthGameMonogame
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ScreenManager.Instance.ToString();
            using (var game = new Game1())
                game.Run();
        }
    }
}

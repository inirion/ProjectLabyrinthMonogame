using System;

namespace LabyrinthGameMonogame.Utils.Randomizers
{
    class LabirynthRandomizer : IRandomizer
    {
        private static Random rand = new Random();
        public int Roll(int minimumValue,int maximumValue)
        {
            return rand.Next(minimumValue, maximumValue);
        }
    }
}

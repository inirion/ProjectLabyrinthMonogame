﻿using LabyrinthGameMonogame.GameFolder.Enteties;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace LabyrinthGameMonogame.Utils
{
    class CollisionChecker
    {
        private static CollisionChecker instance;
        private CollisionChecker()
        {
            Walls = new List<Wall>();
        }
        public static CollisionChecker Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new CollisionChecker();
                }
                return instance;
            }
        }

        internal List<Wall> Walls { get => walls; set => walls = value; }

        List<Wall> walls;


        public bool CheckCollision(Vector3 cameraPosition)
        {
            bool flag = false;
            if(walls.Exists(i => i.BoundingBox.Contains(cameraPosition) == ContainmentType.Contains) )
            {
                flag = true;
            }
            return flag;
        }
    }
}
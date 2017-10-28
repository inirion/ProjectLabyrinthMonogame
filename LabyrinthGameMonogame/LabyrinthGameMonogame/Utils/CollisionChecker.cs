using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.GameFolder.Enteties;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace LabyrinthGameMonogame.Utils
{
    class CollisionChecker
    {
        private static CollisionChecker instance;

        private CollisionChecker()
        {
            Walls = new List<ModelWall>();
            VertexWalls = new List<Cube>();
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

        internal List<ModelWall> Walls { get => walls; set => walls = value; }
        internal List<Cube> VertexWalls { get => vertexWalls; set => vertexWalls = value; }

        List<ModelWall> walls;
        List<Cube> vertexWalls;


        public bool CheckCollision(Vector3 cameraPosition, LabiryntType type)
        {
            bool flag = false;
            if (type == LabiryntType.Recursive)
            {
                if (walls.Exists(i => i.BoundingBox.Contains(new BoundingSphere(cameraPosition,0.1f)) == ContainmentType.Intersects))
                {
                    flag = true;
                }
            }else if(type == LabiryntType.Prim)
            {
                if (vertexWalls.Exists(i =>i.BoundingBox.Contains(new BoundingSphere(cameraPosition,0.1f)) == ContainmentType.Intersects))
                    flag =  true;
            }
            return flag;
        }
    }
}

using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.GameFolder.Enteties;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

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
        internal List<Cube> Floor { get => floor; set => floor = value; }

        List<ModelWall> walls;
        List<Cube> vertexWalls;
        List<Cube> floor;


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
                List<Cube> visibleForCollision = vertexWalls.Where(m => new BoundingSphere(cameraPosition, 5f).Intersects(m.BoundingBox)).ToList();
                if (visibleForCollision.Exists(i =>i.BoundingBox.Contains(new BoundingBox(new Vector3(cameraPosition.X-0.1f,cameraPosition.Y-0.5f,cameraPosition.Z-0.1f), new Vector3(cameraPosition.X+0.1f, cameraPosition.Y+0.5f, cameraPosition.Z+0.1f))) == ContainmentType.Intersects))
                    flag =  true;
            }
            List<Cube> visible = Floor.Where(m => new BoundingSphere(cameraPosition, 5f).Intersects(m.BoundingBox)).ToList();
            if (visible.Exists(i => i.BoundingBox.Contains(new BoundingBox(new Vector3(cameraPosition.X - 0.1f, cameraPosition.Y - 0.4f, cameraPosition.Z - 0.1f), new Vector3(cameraPosition.X + 0.1f, cameraPosition.Y + 0.4f, cameraPosition.Z + 0.1f))) == ContainmentType.Intersects))
            {
                flag = true;
            }
            return flag;
        }
    }
}

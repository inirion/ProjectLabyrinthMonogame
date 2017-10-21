using Microsoft.Xna.Framework;

namespace LabyrinthGameMonogame.GameFolder.Enteties
{
    abstract class GameObject
    {
        private BoundingBox box;
        private bool haveCollider;
        public virtual BoundingBox Box { get => box; set => box = value; }
        public virtual bool HaveCollider { get => haveCollider; set => haveCollider = value; }
    }
}

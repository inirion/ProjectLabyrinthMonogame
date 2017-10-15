using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.GUI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LabyrinthGameMonogame.GameFolder
{
    class GameObject
    {
        Model model;
        LabiryntElement labiryntElement;
        Vector3 angle;
        Vector3 position;
        Matrix worldMatrix;


        public GameObject(LabiryntElement labiryntElement,string modelName,Vector3 position, Vector3 angle)
        {
            Model = ScreenManager.Instance.Content.Load<Model>(modelName);
            this.angle = angle;
            this.Position = position;
            this.LabiryntElement = labiryntElement;
        }

        public Matrix WorldMatrix { get => worldMatrix; set => worldMatrix = value; }
        public Model Model { get => model; set => model = value; }
        public Vector3 Position { get => position; set => position = value; }
        internal LabiryntElement LabiryntElement { get => labiryntElement; set => labiryntElement = value; }

        public void setupModel()
        {
            WorldMatrix = Matrix.CreateRotationX(MathHelper.ToRadians(angle.X)) 
                * Matrix.CreateRotationY(MathHelper.ToRadians(angle.Y)) 
                * Matrix.CreateRotationZ(MathHelper.ToRadians(angle.Z)) 
                * Matrix.CreateTranslation(Position);
        }
    }
}

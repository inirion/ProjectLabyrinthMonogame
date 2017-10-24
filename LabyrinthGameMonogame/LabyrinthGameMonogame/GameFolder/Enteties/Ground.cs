using LabyrinthGameMonogame.GUI.Screens;
using LabyrinthGameMonogame.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LabyrinthGameMonogame.GameFolder.Enteties
{
    class Ground
    {
        Model model;
        Vector3 angle;
        Vector3 position;
        Vector3 scale;
        Matrix worldMatrix;

        public Matrix WorldMatrix { get => worldMatrix; set => worldMatrix = value; }
        public Vector3 Position { get => position; set => position = value; }
        public Vector3 Angle { get => angle; set => angle = value; }
        public Model Model { get => model; set => model = value; }
        public Vector3 Scale { get => scale; set => scale = value; }

        public void setScale(float x, float y, float z)
        {
            scale.X = x;
            scale.Y = y;
            scale.Z = z;
        }

        public Ground(Vector3 position, Vector3 angle, Vector3 scale,Game game)
        {
            Model = AssetHolder.Instance.Floor;
            Position = position;
            Angle = angle;
            Scale = scale;
        }

        public void setupModel()
        {
            WorldMatrix = Matrix.CreateScale(Scale)
                * Matrix.CreateRotationX(MathHelper.ToRadians(Angle.X))
                * Matrix.CreateRotationY(MathHelper.ToRadians(Angle.Y))
                * Matrix.CreateRotationZ(MathHelper.ToRadians(Angle.Z))
                * Matrix.CreateTranslation(Position);
        }

    }
}

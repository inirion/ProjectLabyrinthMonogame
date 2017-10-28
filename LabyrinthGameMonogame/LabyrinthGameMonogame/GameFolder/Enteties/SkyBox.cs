using LabyrinthGameMonogame.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace LabyrinthGameMonogame.GameFolder.Enteties
{
    class SkyBox
    {
        Vector3 ModelPosition;
        float ModelRotation = 0.0f;
        Model SkySphere;
        Vector3 angle;
        Vector3 position;
        Vector3 scale;
        Matrix worldMatrix;
        float rotateX, rotateY, rotateZ;

        public SkyBox(Vector3 position, Vector3 angle, Vector3 scale)
        {
            this.angle = angle;
            this.scale = scale;
            this.position = position;
            this.SkySphere = AssetHolder.Instance.Skybox;
            setupModel();
            rotateX = 0.1f;
            rotateY = 0.07f;
            rotateZ = 0.05f;
        }

        public void Draw(Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in SkySphere.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {

                    effect.World = worldMatrix;
                    effect.View = view;
                    effect.Projection = projection;
                }
                mesh.Draw();
            }
        }
        public void setupModel()
        {
            worldMatrix = Matrix.CreateScale(scale)
                * Matrix.CreateRotationX(MathHelper.ToRadians(angle.X))
                * Matrix.CreateRotationY(MathHelper.ToRadians(angle.Y))
                * Matrix.CreateRotationZ(MathHelper.ToRadians(angle.Z))
                * Matrix.CreateTranslation(position);
        }

        public void Update(GameTime gameTime)
        {
            if(MathHelper.ToRadians(angle.X) < 360.0f)
                angle.X += rotateX;
            if(MathHelper.ToRadians(angle.X) > 360.0f)
            {
                angle.X = 0;
            }
            if (MathHelper.ToRadians(angle.Y) < 360.0f)
                angle.Y += rotateY;
            if (MathHelper.ToRadians(angle.Y) > 360.0f)
            {
                angle.Y = 0;
            }
            if (MathHelper.ToRadians(angle.Z) < 360.0f)
                angle.Z += rotateZ;
            if (MathHelper.ToRadians(angle.Z) > 360.0f)
            {
                angle.Z = 0;
            }
            setupModel();
        }
    }
}

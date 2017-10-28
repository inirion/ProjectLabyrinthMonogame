using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.GUI.Screens;
using LabyrinthGameMonogame.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LabyrinthGameMonogame.GameFolder.Enteties
{
    class ModelWall
    {
        LabiryntElement labiryntElement;
        Vector3 angle;
        Vector3 position;
        Vector3 scale;
        Matrix worldMatrix;
        BoundingBox boundingBox;

        protected void UpdateBoundingBox()
        {
            // Initialize minimum and maximum corners of the bounding box to max and min values
            Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);

            // For each mesh of the model
            foreach (ModelMesh mesh in AssetHolder.Instance.Assets[LabiryntElement].Meshes)
            {
                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                {
                    // Vertex buffer parameters
                    int vertexStride = meshPart.VertexBuffer.VertexDeclaration.VertexStride;
                    int vertexBufferSize = meshPart.NumVertices * vertexStride;

                    // Get vertex data as float
                    float[] vertexData = new float[vertexBufferSize / sizeof(float)];
                    meshPart.VertexBuffer.GetData<float>(vertexData);

                    // Iterate through vertices (possibly) growing bounding box, all calculations are done in world space
                    for (int i = 0; i < vertexBufferSize / sizeof(float); i += vertexStride / sizeof(float))
                    {
                        Vector3 transformedPosition = Vector3.Transform(new Vector3(vertexData[i], vertexData[i + 1], vertexData[i + 2]), worldMatrix);

                        min = Vector3.Min(min, transformedPosition);
                        max = Vector3.Max(max, transformedPosition);
                    }
                }
            }

            // Create and return bounding box
            BoundingBox =  new BoundingBox(min, max);
        }

        public void Draw(Matrix View, Matrix Projection)
        {
            foreach (ModelMesh mesh in AssetHolder.Instance.Assets[LabiryntElement].Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;

                    effect.World = WorldMatrix;
                    effect.View = View;
                    effect.Projection = Projection;
                }
                mesh.Draw();
            }
        }

        public ModelWall(LabiryntElement labiryntElement,Vector3 position, Vector3 angle, Vector3 scale)
        {
            this.angle = angle;
            this.scale = scale;
            this.Position = position;
            this.LabiryntElement = labiryntElement;
        }

        public Matrix WorldMatrix { get => worldMatrix; set => worldMatrix = value; }
        public Vector3 Position { get => position; set => position = value; }
        public LabiryntElement LabiryntElement { get => labiryntElement; set => labiryntElement = value; }
        public BoundingBox BoundingBox { get => boundingBox; set => boundingBox = value; }

        public void setupModel()
        {
            WorldMatrix = Matrix.CreateScale(scale)
                * Matrix.CreateRotationX(MathHelper.ToRadians(angle.X)) 
                * Matrix.CreateRotationY(MathHelper.ToRadians(angle.Y)) 
                * Matrix.CreateRotationZ(MathHelper.ToRadians(angle.Z)) 
                * Matrix.CreateTranslation(Position);
            UpdateBoundingBox();
        }
    }
}

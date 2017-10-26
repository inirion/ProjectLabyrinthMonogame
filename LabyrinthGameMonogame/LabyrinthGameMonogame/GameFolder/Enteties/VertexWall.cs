using LabyrinthGameMonogame.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LabyrinthGameMonogame.GameFolder.Enteties
{
    class VertexWall
    {
        private Color color;
        private GraphicsDevice graphic;
        private BasicEffect basicEffect;
        private Matrix world;
        private float angle;
        private VertexPositionNormalTexture[] vertices;
        
        public BoundingBox BoundingBox { get; set; }
        public Vector3 Size { get; set; }
        public Vector3 Posision { get; set; }
        public Vector2 Position2D { get; set; }
        public Quaternion Rotation { get; set; }
        public Texture2D texture { get; set; }

        public VertexWall(GraphicsDevice graphic,Vector3 size, Vector3 position)
        {
            Size = size;
            Posision = position;
            Position2D = Position2D;
            angle = 1.0f;
            color = Color.Red;
            this.graphic = graphic;
            basicEffect = new BasicEffect(this.graphic);
            world = Matrix.Identity * Matrix.CreateTranslation(Posision);
            texture = AssetHolder.Instance.WallTexture;
            BuildVerticles(Size, Vector3.Zero);

            basicEffect.AmbientLightColor = Color.White.ToVector3();
            basicEffect.EnableDefaultLighting();
            basicEffect.PreferPerPixelLighting = true;
            basicEffect.TextureEnabled = true;
        }

        public void Draw(Matrix View, Matrix Projection)
        {
            basicEffect.World = world;
            basicEffect.View = View;
            basicEffect.Projection = Projection;
            basicEffect.Texture = texture;

            foreach(EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
            }
            graphic.DrawUserPrimitives<VertexPositionNormalTexture>(
                PrimitiveType.TriangleList, vertices, 0,12);

        }

        private void BuildVerticles(Vector3 size, Vector3 position)
        {
            vertices = new VertexPositionNormalTexture[36];
            Vector3 topLeftFront = position + new Vector3(-1.0f, 1.0f, -1.0f) * size;
            Vector3 topRightFront = position + new Vector3(1.0f, 1.0f, -1.0f) * size;
            Vector3 bottomLeftFront = position + new Vector3(-1.0f, -1.0f, -1.0f) * size;
            Vector3 bottomRightFront = position + new Vector3(1.0f, -1.0f, -1.0f) * size;

            Vector3 topLeftBack = position + new Vector3(-1.0f, 1.0f, 1.0f) * size;
            Vector3 topRightBack = position + new Vector3(1.0f, 1.0f, 1.0f) * size;
            Vector3 bottomLeftBack = position + new Vector3(-1.0f, -1.0f, 1.0f) * size;
            Vector3 bottomRightBack = position + new Vector3(1.0f, -1.0f, 1.0f) * size;

            Vector3 frontNormal = new Vector3(0.0f, 0.0f, 1.0f) * size;
            Vector3 backNormal = new Vector3(0.0f, 0.0f, -1.0f) * size;
            Vector3 topNormal = new Vector3(0.0f, 1.0f, .0f) * size;
            Vector3 bottomNormal = new Vector3(0.0f, -1.0f, 0.0f) * size;
            Vector3 leftNormal = new Vector3(-1.0f, 0.0f, 0.0f) * size;
            Vector3 rightNormal = new Vector3(1.0f, 0.0f, 0.0f) * size;

            Vector2 textureTopLeft = new Vector2(0.5f * size.X, 0.0f * size.Y);
            Vector2 textureTopRight = new Vector2(0.0f * size.X, 0.0f * size.Y);
            Vector2 textureBottomLeft = new Vector2(0.5f * size.X, 0.5f * size.Y);
            Vector2 textureBottomRight = new Vector2(0.0f * size.X, 0.5f * size.Y);
            
            //front
            vertices[0] = new VertexPositionNormalTexture(topLeftFront, frontNormal, textureTopLeft);
            vertices[1] = new VertexPositionNormalTexture(bottomLeftFront, frontNormal, textureBottomLeft);
            vertices[2] = new VertexPositionNormalTexture(topRightFront, frontNormal, textureTopRight);
            vertices[3] = new VertexPositionNormalTexture(bottomLeftFront, frontNormal, textureBottomLeft);
            vertices[4] = new VertexPositionNormalTexture(bottomRightFront, frontNormal, textureBottomRight);
            vertices[5] = new VertexPositionNormalTexture(topRightFront, frontNormal, textureTopRight);

            //back
            vertices[6] = new VertexPositionNormalTexture(topLeftBack, backNormal, textureTopRight);
            vertices[7] = new VertexPositionNormalTexture(topRightBack, backNormal, textureTopLeft);
            vertices[8] = new VertexPositionNormalTexture(bottomLeftBack, backNormal, textureBottomRight);
            vertices[9] = new VertexPositionNormalTexture(bottomLeftBack, backNormal, textureBottomRight);
            vertices[10] = new VertexPositionNormalTexture(topRightBack, backNormal, textureTopLeft);
            vertices[11] = new VertexPositionNormalTexture(bottomRightBack, backNormal, textureBottomLeft);

            //top
            vertices[12] = new VertexPositionNormalTexture(topLeftFront, topNormal, textureBottomLeft);
            vertices[13] = new VertexPositionNormalTexture(topRightBack, topNormal, textureTopRight);
            vertices[14] = new VertexPositionNormalTexture(topLeftBack, topNormal, textureTopLeft);
            vertices[15] = new VertexPositionNormalTexture(topLeftFront, topNormal, textureBottomLeft);
            vertices[16] = new VertexPositionNormalTexture(topRightFront, topNormal, textureBottomRight);
            vertices[17] = new VertexPositionNormalTexture(topRightBack, topNormal, textureTopRight);

            //bottom
            vertices[18] = new VertexPositionNormalTexture(bottomLeftFront, bottomNormal, textureTopLeft);
            vertices[19] = new VertexPositionNormalTexture(bottomLeftBack, bottomNormal, textureBottomLeft);
            vertices[20] = new VertexPositionNormalTexture(bottomRightBack, bottomNormal, textureBottomRight);
            vertices[21] = new VertexPositionNormalTexture(bottomLeftFront, bottomNormal, textureTopLeft);
            vertices[22] = new VertexPositionNormalTexture(bottomRightBack, bottomNormal, textureBottomRight);
            vertices[23] = new VertexPositionNormalTexture(bottomRightFront, bottomNormal, textureTopRight);

            //left
            vertices[24] = new VertexPositionNormalTexture(topLeftFront, leftNormal, textureTopRight);
            vertices[25] = new VertexPositionNormalTexture(bottomLeftBack, leftNormal, textureBottomLeft);
            vertices[26] = new VertexPositionNormalTexture(bottomLeftFront, leftNormal, textureBottomRight);
            vertices[27] = new VertexPositionNormalTexture(topLeftBack, leftNormal, textureTopLeft);
            vertices[28] = new VertexPositionNormalTexture(bottomLeftBack, leftNormal, textureBottomLeft);
            vertices[29] = new VertexPositionNormalTexture(topLeftFront, leftNormal, textureTopRight);

            //right
            vertices[30] = new VertexPositionNormalTexture(topRightFront, rightNormal, textureTopLeft);
            vertices[31] = new VertexPositionNormalTexture(bottomRightFront, rightNormal, textureBottomLeft);
            vertices[32] = new VertexPositionNormalTexture(bottomRightBack, rightNormal, textureBottomRight);
            vertices[33] = new VertexPositionNormalTexture(topRightBack, rightNormal, textureTopRight);
            vertices[34] = new VertexPositionNormalTexture(topRightFront, rightNormal, textureTopLeft);
            vertices[35] = new VertexPositionNormalTexture(bottomRightBack, rightNormal, textureBottomRight);

            Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 transformedPosition = Vector3.Transform(vertices[i].Position, world);

                min = Vector3.Min(min, transformedPosition);
                max = Vector3.Max(max, transformedPosition);
            }
            BoundingBox = new BoundingBox(min, max);
            
        }
    }
}

using LabyrinthGameMonogame.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LabyrinthGameMonogame.GameFolder.Enteties
{
    class VertexWall
    {
        private GraphicsDevice graphic;
        private Matrix world;
        private float angle;
        private VertexPositionNormalTexture[] vertices;
        public VertexPositionNormalTexture[] ffront;
        public VertexPositionNormalTexture[] bback;
        public VertexPositionNormalTexture[] lleft;
        public VertexPositionNormalTexture[] rright;
        public VertexPositionNormalTexture[] ttop;
        public VertexPositionNormalTexture[] bbot;
        private int index;
        double millisecondsPerFrame;
        double timeSinceLastUpdate;
        List<Texture2D> textures;


        public BoundingBox BoundingBox { get; set; }
        public Vector3 Size { get; set; }
        public Vector3 Posision { get; set; }
        public Vector2 Position2D { get; set; }
        public Quaternion Rotation { get; set; }
        public Texture2D texture { get; set; }
        public List<Texture2D> Textures { get => textures; set => textures = value; }

        public VertexWall(GraphicsDevice graphic,Vector3 size, Vector3 position)
        {
            
            millisecondsPerFrame = 50;
            timeSinceLastUpdate = 0;
            index = 0;
            Size = size;
            Posision = position;
            Position2D = Position2D;
            angle = 1.0f;
            this.graphic = graphic;
            world = Matrix.Identity * Matrix.CreateTranslation(Posision);
            Textures = AssetHolder.Instance.SelectedTexture;
            texture = Textures[index];
            BuildVerticles(Size, Vector3.Zero);
        }

        public void Draw(Matrix View, Matrix Projection, BasicEffect basicEffect)
        {
            basicEffect.TextureEnabled = true;
            basicEffect.World = world;
            basicEffect.View = View;
            basicEffect.Projection = Projection;
            basicEffect.Texture = texture;
          
            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                //graphic.DrawUserPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleList, ffront, 0, 2);
                //graphic.DrawUserPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleList, bback, 0, 2);
                //graphic.DrawUserPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleList, rright, 0, 2);
                //graphic.DrawUserPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleList, lleft, 0, 2);
                //graphic.DrawUserPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleList, ttop, 0, 2);
                //graphic.DrawUserPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleList, bbot, 0, 2);
                graphic.DrawUserPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleList, vertices, 0, 12);
            }
            
        }

        public void Update(GameTime gameTime)
        {
            //rotation
            /*float gt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float rp, rm;
            if (this.angle > 360.0f) this.angle = 0.0f;
            this.angle += 50.0f * gt;
            rp = MathHelper.ToRadians(angle);
            rm = MathHelper.ToRadians(-angle);

            this.world = Matrix.Identity;
            this.world *= Matrix.CreateScale(1.0f);
            this.world *= Matrix.CreateRotationY(rm);
            this.world *= Matrix.CreateTranslation(Posision);
            */
            
            timeSinceLastUpdate += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeSinceLastUpdate >= millisecondsPerFrame)
            {
                timeSinceLastUpdate = 0;
                index++;
                if (index == Textures.Count)
                {
                    index = 0;
                }
                texture = Textures[index];
            }
        }
        public void changeTexture()
        {
            textures = AssetHolder.Instance.SelectedTexture;
            index = 0;
            if (AssetHolder.Instance.SelectedTexture == AssetHolder.Instance.GandalfTextures)
                AssetHolder.Instance.MusicInstance.Play();
            else if(AssetHolder.Instance.SelectedTexture[0] == AssetHolder.Instance.WallTexture)
                AssetHolder.Instance.MusicInstance.Stop();
        }

        private void BuildVerticles(Vector3 size, Vector3 position)
        {
            vertices = new VertexPositionNormalTexture[36];
            //ffront = new VertexPositionNormalTexture[6];
            //bback = new VertexPositionNormalTexture[6];
            //lleft = new VertexPositionNormalTexture[6];
            //rright = new VertexPositionNormalTexture[6];
            //ttop = new VertexPositionNormalTexture[6];
            //bbot = new VertexPositionNormalTexture[6];
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

            Vector2 textureTopLeft = new Vector2(2f * size.X, 0.0f * size.Y);
            Vector2 textureTopRight = new Vector2(0.0f * size.X, 0.0f * size.Y);
            Vector2 textureBottomLeft = new Vector2(2f * size.X, 2f * size.Y);
            Vector2 textureBottomRight = new Vector2(0.0f * size.X, 2f * size.Y);
            
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

            /* //front
             ffront[0] = new VertexPositionNormalTexture(topLeftFront, frontNormal, textureTopLeft);
             ffront[1] = new VertexPositionNormalTexture(bottomLeftFront, frontNormal, textureBottomLeft);
             ffront[2] = new VertexPositionNormalTexture(topRightFront, frontNormal, textureTopRight);
             ffront[3] = new VertexPositionNormalTexture(bottomLeftFront, frontNormal, textureBottomLeft);
             ffront[4] = new VertexPositionNormalTexture(bottomRightFront, frontNormal, textureBottomRight);
             ffront[5] = new VertexPositionNormalTexture(topRightFront, frontNormal, textureTopRight);

             //back
             bback[0] = new VertexPositionNormalTexture(topLeftBack, backNormal, textureTopRight);
             bback[1] = new VertexPositionNormalTexture(topRightBack, backNormal, textureTopLeft);
             bback[2] = new VertexPositionNormalTexture(bottomLeftBack, backNormal, textureBottomRight);
             bback[3] = new VertexPositionNormalTexture(bottomLeftBack, backNormal, textureBottomRight);
             bback[4] = new VertexPositionNormalTexture(topRightBack, backNormal, textureTopLeft);
             bback[5] = new VertexPositionNormalTexture(bottomRightBack, backNormal, textureBottomLeft);

             //top
             ttop[0] = new VertexPositionNormalTexture(topLeftFront, topNormal, textureBottomLeft);
             ttop[1] = new VertexPositionNormalTexture(topRightBack, topNormal, textureTopRight);
             ttop[2] = new VertexPositionNormalTexture(topLeftBack, topNormal, textureTopLeft);
             ttop[3] = new VertexPositionNormalTexture(topLeftFront, topNormal, textureBottomLeft);
             ttop[4] = new VertexPositionNormalTexture(topRightFront, topNormal, textureBottomRight);
             ttop[5] = new VertexPositionNormalTexture(topRightBack, topNormal, textureTopRight);

             //bottom
             bbot[0] = new VertexPositionNormalTexture(bottomLeftFront, bottomNormal, textureTopLeft);
             bbot[1] = new VertexPositionNormalTexture(bottomLeftBack, bottomNormal, textureBottomLeft);
             bbot[2] = new VertexPositionNormalTexture(bottomRightBack, bottomNormal, textureBottomRight);
             bbot[3] = new VertexPositionNormalTexture(bottomLeftFront, bottomNormal, textureTopLeft);
             bbot[4] = new VertexPositionNormalTexture(bottomRightBack, bottomNormal, textureBottomRight);
             bbot[5] = new VertexPositionNormalTexture(bottomRightFront, bottomNormal, textureTopRight);

             //left
             lleft[0] = new VertexPositionNormalTexture(topLeftFront, leftNormal, textureTopRight);
             lleft[1] = new VertexPositionNormalTexture(bottomLeftBack, leftNormal, textureBottomLeft);
             lleft[2] = new VertexPositionNormalTexture(bottomLeftFront, leftNormal, textureBottomRight);
             lleft[3] = new VertexPositionNormalTexture(topLeftBack, leftNormal, textureTopLeft);
             lleft[4] = new VertexPositionNormalTexture(bottomLeftBack, leftNormal, textureBottomLeft);
             lleft[5] = new VertexPositionNormalTexture(topLeftFront, leftNormal, textureTopRight);

             //right
             rright[0] = new VertexPositionNormalTexture(topRightFront, rightNormal, textureTopLeft);
             rright[1] = new VertexPositionNormalTexture(bottomRightFront, rightNormal, textureBottomLeft);
             rright[2] = new VertexPositionNormalTexture(bottomRightBack, rightNormal, textureBottomRight);
             rright[3] = new VertexPositionNormalTexture(topRightBack, rightNormal, textureTopRight);
             rright[4] = new VertexPositionNormalTexture(topRightFront, rightNormal, textureTopLeft);
             rright[5] = new VertexPositionNormalTexture(bottomRightBack, rightNormal, textureBottomRight);
             */
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

using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.GUI.Screens;
using LabyrinthGameMonogame.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LabyrinthGameMonogame.GameFolder
{
    class LabirynthGame
    {
        Camera camera;
        float angle;
        LabirynthCreator labirynth;


        public LabirynthGame()
        {
            labirynth = new LabirynthCreator();
            camera = new Camera(new Vector3(0,0,0),new Vector3(0,0,0),5.0f,0.05f);
        }

        public void ResetGame()
        {
            if(GameManager.Instance.ResetGame)
            {
                labirynth.CreateMap();
                angle = 0;
                GameManager.Instance.ResetGame = false;
                //coords x, z, y
                camera.Position = labirynth.GetStartingPosition();
            }
        }

        public void Update(GameTime gameTime)
        {
            camera.Update(gameTime);
            angle += 0.1f;
            //Debug.WriteLine(angle);
        }

        void DrawModel(GameObject gameObject)
        {
            foreach (var mesh in gameObject.Model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {   
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;

                    effect.World = gameObject.WorldMatrix;
                    effect.View = camera.View;
                    effect.Projection = camera.Projection;
                }

                mesh.Draw();
            }
        }
        public void Draw()
        {
            if (GameManager.Instance.IsGameRunning) {
                ScreenManager.Instance.Graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
                ScreenManager.Instance.Graphics.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
                labirynth.Map.ForEach(i => DrawModel(i));

            }
        }
    }
}

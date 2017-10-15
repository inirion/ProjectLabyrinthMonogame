using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.GameFolder.Enteties;
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
        Player player;
        float angle;
        LabirynthCreator labirynth;
        Vector3 finish;


        public LabirynthGame()
        {
            labirynth = new LabirynthCreator();
            player = new Player(new Vector3(), 3.0f);
            finish = new Vector3();
        }

        public void ResetGame()
        {
            if(GameManager.Instance.ResetGame)
            {
                labirynth.CreateMap();
                angle = 0;
                GameManager.Instance.ResetGame = false;
                //coords x, z, y
                player.Position = labirynth.GetStartingPosition();
                finish = labirynth.GetFinishPosition();
            }
        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            if ((player.Position.X > finish.X - 0.5f && player.Position.X < finish.X + 0.5f)
                && (player.Position.Z > finish.Z - 0.5f && player.Position.Z < finish.Z + 0.5f))
            {
                GameManager.Instance.ResetGame = true;
            }
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
                    effect.View = player.Camera.View;
                    effect.Projection = player.Camera.Projection;
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

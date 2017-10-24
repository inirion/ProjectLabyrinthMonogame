using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.GameFolder.Enteties;
using LabyrinthGameMonogame.GUI.Screens;
using LabyrinthGameMonogame.InputControllers;
using LabyrinthGameMonogame.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace LabyrinthGameMonogame.GameFolder
{
    class LabirynthGame
    {
        Player player;
        double frameRate = 0.0;
        float anglex = 0, angley = 0, anglez = 0;
        LabirynthCreator labirynth;
        Vector3 finish;
        Ground ground;
        Game game;
        IGameManager gameManager;
        IScreenManager screenManager;

        public LabirynthGame(Game game)
        {
            this.game = game;
            gameManager = (IGameManager)game.Services.GetService(typeof(IGameManager));
            screenManager = (IScreenManager)game.Services.GetService(typeof(IScreenManager));
            labirynth = new LabirynthCreator(game);
            player = new Player(new Vector3(), 1.0f,game);
            finish = new Vector3();
            ground = new Ground(new Vector3(0, -0.1f, 0), new Vector3(0, 0, 0), new Vector3(1, 0.01f, 0.2f),game);
            ground.setupModel();
            CollisionChecker.Instance.Walls = labirynth.Map;
            anglex = ground.Scale.X;
            angley = ground.Scale.Y;
            anglez = ground.Scale.Z;

        }

        public void ResetGame()
        {
            if (gameManager.ResetGame)
            {
                labirynth.CreateMap();
                CollisionChecker.Instance.Walls = labirynth.Map;
                gameManager.ResetGame = false;
                //coords x, z, y
                player.Reset(labirynth.GetStartingPosition(),game);
                finish = labirynth.GetFinishPosition();
                switch (gameManager.DifficultyLevel)
                {
                    case DifficultyLevel.Easy:
                        ground.Scale = new Vector3(1.25f, 0.01f, 0.55f);
                        break;
                    case DifficultyLevel.Medium:
                        ground.Scale = new Vector3(1.25f * 2, 0.01f, 0.55f * 2);
                        break;
                    case DifficultyLevel.Hard:
                        ground.Scale = new Vector3(1.25f * 3, 0.01f, 0.55f * 3);
                        break;
                }


                ground.Position = new Vector3((int)gameManager.DifficultyLevel, -0.04f, (int)gameManager.DifficultyLevel);
                ground.setupModel();
            }
        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            if ((player.Position.X > finish.X - 0.5f && player.Position.X < finish.X + 0.5f)
                && (player.Position.Z > finish.Z - 0.5f && player.Position.Z < finish.Z + 0.5f))
            {
                gameManager.ResetGame = true;
            }
            ground.setupModel();



        }


        void DrawGroud(Ground ground)
        {
            foreach (var mesh in ground.Model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;

                    effect.World = ground.WorldMatrix;
                    effect.View = player.Camera.View;
                    effect.Projection = player.Camera.Projection;
                }
                mesh.Draw();
            }
        }

        public void Draw()
        {
            if (gameManager.IsGameRunning)
            {
                screenManager.Graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
                screenManager.Graphics.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
                labirynth.Map.ForEach(i => i.Draw(player.Camera.View, player.Camera.Projection));
                DrawGroud(ground);
            }
        }
    }
}

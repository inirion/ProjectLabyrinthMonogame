﻿using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.GameFolder.Enteties;
using LabyrinthGameMonogame.GameFolder.MazeGenerationAlgorithms;
using LabyrinthGameMonogame.GUI.Screens;
using LabyrinthGameMonogame.InputControllers;
using LabyrinthGameMonogame.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Linq;

namespace LabyrinthGameMonogame.GameFolder
{
    class LabirynthGame
    {
        Player player;
        double frameRate = 0.0;
        float anglex = 0, angley = 0, anglez = 0;
        LabirynthCreator labirynth;
        Vector3 finish;
        SkyBox skyBox;
        Ground ground;
        Game game;
        IGameManager gameManager;
        IScreenManager screenManager;
        IControlManager controlManager;
        BoundingFrustum frustum;
        BasicEffect basicEffect;

        public LabirynthGame(Game game)
        {
            basicEffect = new BasicEffect(game.GraphicsDevice)
            {
                TextureEnabled = true,
                World = Matrix.Identity,
                PreferPerPixelLighting = false
            };
            
            basicEffect.EnableDefaultLighting();
            AssetHolder.Instance.MusicInstance.IsLooped = true;
            this.game = game;
            gameManager = (IGameManager)game.Services.GetService(typeof(IGameManager));
            screenManager = (IScreenManager)game.Services.GetService(typeof(IScreenManager));
            controlManager = (IControlManager)game.Services.GetService(typeof(IControlManager));
            labirynth = new LabirynthCreator(game);
            player = new Player(new Vector3(), 1.0f,game);
            finish = new Vector3();
            ground = new Ground(new Vector3(0, -0.1f, 0), new Vector3(0, 0, 0), new Vector3(1, 0.01f, 0.2f),game);
            ground.setupModel();
            if(gameManager.Type == LabiryntType.Recursive)
                CollisionChecker.Instance.Walls = labirynth.ModelMap;
            else if (gameManager.Type == LabiryntType.Prim)
                CollisionChecker.Instance.VertexWalls = labirynth.VertexMap;

            anglex = ground.Scale.X;
            angley = ground.Scale.Y;
            anglez = ground.Scale.Z;
            skyBox = new SkyBox(new Vector3((float)DifficultyLevel.Hard / 2, (float)DifficultyLevel.Hard / 2, 0), new Vector3(90,0,0), new Vector3(1f));
        }

        public void ResetGame()
        {
            if (gameManager.ResetGame)
            {
                if (gameManager.Type == LabiryntType.Recursive)
                {
                    labirynth.CreateModelMap();
                    CollisionChecker.Instance.Walls = labirynth.ModelMap;
                    player.Reset(labirynth.GetStartingPositionModelMap(), game);
                    finish = labirynth.GetFinishPositionModelMap();
                }
                    
                else if(gameManager.Type == LabiryntType.Prim)
                {
                    labirynth.CreateVertexMap(game.GraphicsDevice);
                    CollisionChecker.Instance.VertexWalls = labirynth.VertexMap;
                    player.Reset(labirynth.GetStartingPositionVertexMap(), game);
                    finish = labirynth.GetFinishPositionVertexMap();
                    if (AssetHolder.Instance.MusicInstance.State == Microsoft.Xna.Framework.Audio.SoundState.Paused)
                        AssetHolder.Instance.MusicInstance.Resume();
                    AssetHolder.Instance.MusicInstance.Stop();
                    AssetHolder.Instance.SelectedTexture = new List<Texture2D>() { AssetHolder.Instance.WallTexture };
                    labirynth.VertexMap.ForEach(i => i.changeTexture());
                   
                }
                frustum = new BoundingFrustum(player.Camera.View * player.Camera.Projection);
                gameManager.ResetGame = false;
                //coords x, z, y
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
            if (gameManager.Type == LabiryntType.Prim)
            {
                if (controlManager.Keyboard.Clicked(KeyboardKeys.Z))
                {
                    AssetHolder.Instance.SelectedTexture = new List<Texture2D>() { AssetHolder.Instance.WallTexture };
                    labirynth.VertexMap.ForEach(i => i.changeTexture());
                }
                if (controlManager.Keyboard.Clicked(KeyboardKeys.X))
                {
                    AssetHolder.Instance.SelectedTexture = AssetHolder.Instance.GandalfTextures;
                    AssetHolder.Instance.MusicInstance.Stop();
                    AssetHolder.Instance.MusicInstance.Play();
                    labirynth.VertexMap.ForEach(i => i.changeTexture());
                }
            }
            player.Update(gameTime);
            if ((player.Position.X > finish.X - 0.5f && player.Position.X < finish.X + 0.5f)
                && (player.Position.Z > finish.Z - 0.5f && player.Position.Z < finish.Z + 0.5f))
            {
                gameManager.ResetGame = true;
            }
            ground.setupModel();
            if (gameManager.Type == LabiryntType.Prim)
            {
                frustum = new BoundingFrustum(player.Camera.View * player.Camera.Projection);
                List<VertexWall> visible = labirynth.VertexMap.Where(m => frustum.Contains(m.BoundingBox) != ContainmentType.Disjoint).ToList();
                visible.ForEach(i => i.Update(gameTime));
                //labirynth.VertexMap.ForEach(i => i.Update(gameTime));
            }
            skyBox.Update(gameTime);
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
                screenManager.Graphics.GraphicsDevice.RasterizerState = new RasterizerState() { MultiSampleAntiAlias = true };
                screenManager.Graphics.GraphicsDevice.SamplerStates[0] = new SamplerState() { Filter = TextureFilter.Anisotropic };
                if (gameManager.Type == LabiryntType.Prim)
                {
                    labirynth.VertexMap.Where(m => frustum.Contains(m.BoundingBox) != ContainmentType.Disjoint).ToList().ForEach(i => i.Draw(player.Camera.View, player.Camera.Projection, basicEffect));
                    //labirynth.VertexMap.ForEach(i => i.Draw(player.Camera.View, player.Camera.Projection));
                }else if (gameManager.Type == LabiryntType.Recursive)
                {
                    labirynth.ModelMap.ForEach(i => i.Draw(player.Camera.View, player.Camera.Projection));
                }
                /*screenManager.Graphics.GraphicsDevice.RasterizerState = new RasterizerState() { CullMode = CullMode.CullClockwiseFace };
                skyBox.Draw(player.Camera.View, player.Camera.Projection,basicEffect);
                screenManager.Graphics.GraphicsDevice.RasterizerState = new RasterizerState() { CullMode = CullMode.CullCounterClockwiseFace };
                */
                skyBox.Draw(player.Camera.View, player.Camera.Projection);
                DrawGroud(ground);
            }
        }
    }
}

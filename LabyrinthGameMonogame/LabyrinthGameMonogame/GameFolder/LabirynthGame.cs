using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.GameFolder.Enteties;
using LabyrinthGameMonogame.GameFolder.MazeGenerationAlgorithms;
using LabyrinthGameMonogame.GUI.Screens;
using LabyrinthGameMonogame.InputControllers;
using LabyrinthGameMonogame.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace LabyrinthGameMonogame.GameFolder
{
    class LabirynthGame
    {
        Player player;
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
        Finish finishPoint;
        List<Key> keys;
        Minimap minimap;

        public LabirynthGame(Game game)
        {
            keys = new List<Key>();
            basicEffect = new BasicEffect(game.GraphicsDevice)
            {
                TextureEnabled = true,
                World = Matrix.Identity,
                PreferPerPixelLighting = true
            };

            basicEffect.EnableDefaultLighting();
            basicEffect.DirectionalLight0.Enabled = true;
            basicEffect.DirectionalLight0.SpecularColor = new Vector3(0, 0, 0);
            basicEffect.DirectionalLight1.Enabled = true;
            basicEffect.DirectionalLight2.Enabled = true;
            //basicEffect.AmbientLightColor = new Vector3(0.2f, 0.2f, 0.2f);
            //basicEffect.EmissiveColor = new Vector3(1, 0, 0);

            AssetHolder.Instance.GandalfMusicInstance.IsLooped = true;
            AssetHolder.Instance.KeyPickupfMusicInstance.IsLooped = false;
            this.game = game;
            gameManager = (IGameManager)game.Services.GetService(typeof(IGameManager));
            screenManager = (IScreenManager)game.Services.GetService(typeof(IScreenManager));
            controlManager = (IControlManager)game.Services.GetService(typeof(IControlManager));
            labirynth = new LabirynthCreator(game);
            player = new Player(new Vector3(), 2.0f,game);
            finish = new Vector3();
            if(gameManager.Type == LabiryntType.Recursive)
                CollisionChecker.Instance.Walls = labirynth.ModelMap;
            else if (gameManager.Type == LabiryntType.Prim)
                CollisionChecker.Instance.VertexWalls = labirynth.VertexMap;

            skyBox = new SkyBox(new Vector3((float)DifficultyLevel.Hard / 2, (float)DifficultyLevel.Hard / 2, 0), new Vector3(90,0,0), new Vector3(5f));
            finishPoint = new Finish(finish, game.GraphicsDevice,game);
            keys = labirynth.GetKeys(gameManager.Type,game.GraphicsDevice,game);
            ground = new Ground(game,labirynth.GroundMap);
            minimap = new Minimap(labirynth.getMap(gameManager.Type), game,screenManager);
        }

        public void ResetGame()
        {
            if (gameManager.ResetGame && !screenManager.IsTransitioning)
            {
                if (gameManager.Type == LabiryntType.Recursive)
                {
                    labirynth.CreateModelMap(game);
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
                    if (AssetHolder.Instance.GandalfMusicInstance.State == Microsoft.Xna.Framework.Audio.SoundState.Paused)
                        AssetHolder.Instance.GandalfMusicInstance.Resume();
                    AssetHolder.Instance.GandalfMusicInstance.Stop();
                    AssetHolder.Instance.SelectedTexture = new List<Texture2D>() { AssetHolder.Instance.WallTexture };
                    labirynth.VertexMap.ForEach(i => i.changeTexture());
                   
                }
                finishPoint.SetFinishPoint(new Vector3(finish.X,0.3f,finish.Z));
                keys = labirynth.GetKeys(gameManager.Type, game.GraphicsDevice, game);
                frustum = new BoundingFrustum(player.Camera.View * player.Camera.Projection);
                ground = new Ground(game,labirynth.GroundMap);
                CollisionChecker.Instance.Floor = ground.GroundObjects;
                minimap.Reset(labirynth.getMap(gameManager.Type), game, screenManager);
                gameManager.ResetGame = false;
               
                //coords x, z, y
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
                    AssetHolder.Instance.GandalfMusicInstance.Stop();
                    AssetHolder.Instance.GandalfMusicInstance.Play();
                    labirynth.VertexMap.ForEach(i => i.changeTexture());
                }
            }
            player.Update(gameTime, ref keys, minimap);
            keys.ForEach(i => i.Update(gameTime));
            finishPoint.Update(gameTime,player,screenManager);

            ground.Update(gameTime,player);
            if (gameManager.Type == LabiryntType.Prim)
            {
                frustum = new BoundingFrustum(player.Camera.View * player.Camera.Projection);
                List<Cube> visible = labirynth.VertexMap.Where(m => frustum.Contains(m.BoundingBox) != ContainmentType.Disjoint).ToList();
                visible.ForEach(i => i.Update(gameTime));
            }
            skyBox.Update(gameTime);
            minimap.Update(controlManager);
        }


        //void DrawGroud(Ground ground)
        //{
        //    foreach (var mesh in ground.Model.Meshes)
        //    {
        //        foreach (BasicEffect effect in mesh.Effects)
        //        {
        //            effect.EnableDefaultLighting();
        //            effect.PreferPerPixelLighting = true;

        //            effect.World = ground.WorldMatrix;
        //            effect.View = player.Camera.View;
        //            effect.Projection = player.Camera.Projection;
        //        }
        //        mesh.Draw();
        //    }
        //}

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
                }else if (gameManager.Type == LabiryntType.Recursive)
                {
                    labirynth.ModelMap.ForEach(i => i.Draw(player.Camera.View, player.Camera.Projection));
                }
                
                skyBox.Draw(player.Camera.View, player.Camera.Projection);
                finishPoint.Draw(player.Camera.View, player.Camera.Projection, basicEffect);
                ground.Draw(player.Camera.View, player.Camera.Projection, basicEffect);
                keys.ForEach(i => i.Draw(player.Camera.View, player.Camera.Projection, basicEffect));
                minimap.Draw(new Vector2(player.position.X, player.position.Z));
                //DrawGroud(ground);
            }
        }
    }
}

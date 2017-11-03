using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.GUI.Screens;
using LabyrinthGameMonogame.InputControllers;
using LabyrinthGameMonogame.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LabyrinthGameMonogame.GameFolder.Enteties
{
    class Player
    {
        public Vector3 position;
        Camera camera;
        float movementSpeed;
        float playerHeight;
        bool isJumping;
        BoundingSphere boundingSphere;
        List<Key> keys;
        public bool allKeysCollected;
        float height;
        IControlManager controlManager;

        public Player(Vector3 position, float movementSpeed,Game game)
        {
            keys = new List<Key>();
            controlManager = (IControlManager)game.Services.GetService(typeof(IControlManager));
            Camera = new Camera(Position, new Vector3(0, 0, 0), movementSpeed, movementSpeed * 1.5f, game);
            Height = 0.5f;
            this.Position = position;
            this.position.Y = Height;
            this.MovementSpeed = movementSpeed;
            
            BoundingSphere = new BoundingSphere(position, 0.1f);
        }

        public void Reset(Vector3 position, Game game)
        {
            this.Position = position;
            this.position.Y = Height;
            this.camera.MouseSpeed = controlManager.Mouse.Sensitivity;
            this.camera = new Camera(this.position, new Vector3(0, 0, 0), movementSpeed, movementSpeed * 1.5f, game);
            BoundingSphere = new BoundingSphere(position, 0.1f);
        }
        public void Update(GameTime gameTime,ref List<Key> keys, Minimap minimap)
        {
            if (keys.Count == 0) allKeysCollected = true;
            else allKeysCollected = false;
            float speed = movementSpeed;
            if (controlManager.Keyboard.Clicked(KeyboardKeys.Jump) && Position.Y <= PlayerHeight)
            {
                IsJumping = true;
            }
            if (controlManager.Keyboard.Pressed(false, KeyboardKeys.LeftShift))
            {
                speed *= 4;
            }
            camera.CameraSpeed = speed;

            for(int i = 0; i < keys.Count; i++)
            {
                if(keys[i].boundingBox.Intersects(new BoundingSphere(position, 0.3f)))
                {
                    AssetHolder.Instance.KeyPickupfMusicInstance.Play();
                    this.keys.Add(keys[i]);
                    minimap.Reset(keys[i].Position);
                    keys.RemoveAt(i);
                }
            }
            Camera.Update(gameTime);
            this.position = camera.Position;

            BoundingSphere = new BoundingSphere(position, 0.1f);
        }

        public Vector3 Position { get => position; set { position = value; camera.Position = value; } }
        public Camera Camera { get => camera; set => camera = value; }
        public bool IsJumping { get => isJumping; set => isJumping = value; }
        public float PlayerHeight { get => playerHeight; set => playerHeight = value; }
        public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
        public BoundingSphere BoundingSphere { get => boundingSphere; set => boundingSphere = value; }
        public float Height { get => height; set => height = value; }
    }
}

using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.GUI.Screens;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LabyrinthGameMonogame.Utils
{
    class AssetHolder
    {
        private Dictionary<LabiryntElement, Model> assets;
        private SpriteFont font;
        private Model floor;
        private Texture2D wallTexture;
        private static AssetHolder instance;
        public static AssetHolder Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AssetHolder();
                }
                return instance;
            }
        }
        public Dictionary<LabiryntElement, Model> Assets { get => assets; set => assets = value; }
        public SpriteFont Font { get => font; set => font = value; }
        public Model Floor { get => floor; set => floor = value; }
        public Texture2D WallTexture { get => wallTexture; set => wallTexture = value; }

        public void Initialize(ContentManager content)
        {
            assets = new Dictionary<LabiryntElement, Model>();
            assets.Add(LabiryntElement.WallEW, content.Load<Model>("Wall"));
            assets.Add(LabiryntElement.WallNS, content.Load<Model>("Wall"));
            assets.Add(LabiryntElement.Wall, content.Load<Model>("Wall"));
            assets.Add(LabiryntElement.WallEN, content.Load<Model>("Connector"));
            assets.Add(LabiryntElement.WallES, content.Load<Model>("Connector"));
            assets.Add(LabiryntElement.WallWN, content.Load<Model>("Connector"));
            assets.Add(LabiryntElement.WallWS, content.Load<Model>("Connector"));
            assets.Add(LabiryntElement.Start, content.Load<Model>("Wooden_House"));
            assets.Add(LabiryntElement.Finish, content.Load<Model>("Wooden_House"));
            assets.Add(LabiryntElement.Pillar, content.Load<Model>("Pillar"));

            Font = content.Load<SpriteFont>("Font");
            Floor = content.Load<Model>("Floor");
            WallTexture = content.Load<Texture2D>("wallTexture");
        }
    }
}

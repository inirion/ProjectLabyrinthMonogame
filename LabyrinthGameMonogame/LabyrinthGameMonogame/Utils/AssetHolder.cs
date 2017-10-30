using LabyrinthGameMonogame.Enums;
using Microsoft.Xna.Framework.Audio;
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
        private Model skybox;
        private SoundEffect keyPickup;
        private Texture2D groundTexture;
        private Texture2D wallTexture;
        private List<Texture2D> gandalfTextures;
        private List<Texture2D> selectedTexture;
        private SoundEffect gandalfMusic;
        private Texture2D finishTexture;
        private Texture2D keyTexture;
        private SoundEffectInstance gandalfMusicInstance;
        private SoundEffectInstance keyPickupInstance;
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
        public List<Texture2D> GandalfTextures { get => gandalfTextures; set => gandalfTextures = value; }
        public SoundEffectInstance GandalfMusicInstance { get => gandalfMusicInstance; set => gandalfMusicInstance = value; }
        public SoundEffectInstance KeyPickupfMusicInstance { get => keyPickupInstance; set => keyPickupInstance = value; }
        public List<Texture2D> SelectedTexture { get => selectedTexture; set => selectedTexture = value; }
        public Model Skybox { get => skybox; set => skybox = value; }
        public Texture2D FinishTexture { get => finishTexture; set => finishTexture = value; }
        public Texture2D GroundTexture { get => groundTexture; set => groundTexture = value; }
        public Texture2D KeyTexture { get => keyTexture; set => keyTexture = value; }
        public SoundEffect KeyPickup { get => keyPickup; set => keyPickup = value; }

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
            assets.Add(LabiryntElement.Start, content.Load<Model>("Pillar"));
            assets.Add(LabiryntElement.Finish, content.Load<Model>("Pillar"));
            assets.Add(LabiryntElement.Pillar, content.Load<Model>("Pillar"));

            Font = content.Load<SpriteFont>("Font");
            Floor = content.Load<Model>("Floor");
            WallTexture = content.Load<Texture2D>("wallTexture");
            GandalfTextures = new List<Texture2D>();
            GandalfTextures.Add(content.Load<Texture2D>("gandalf1"));
            GandalfTextures.Add(content.Load<Texture2D>("gandalf2"));
            GandalfTextures.Add(content.Load<Texture2D>("gandalf3"));
            GandalfTextures.Add(content.Load<Texture2D>("gandalf4"));
            GandalfTextures.Add(content.Load<Texture2D>("gandalf5"));
            GandalfTextures.Add(content.Load<Texture2D>("gandalf6"));
            GandalfTextures.Add(content.Load<Texture2D>("gandalf7"));
            GandalfTextures.Add(content.Load<Texture2D>("gandalf8"));
            GandalfTextures.Add(content.Load<Texture2D>("gandalf9"));
            GandalfTextures.Add(content.Load<Texture2D>("gandalf10"));
            GandalfTextures.Add(content.Load<Texture2D>("gandalf11"));
            GandalfTextures.Add(content.Load<Texture2D>("gandalf12"));
            gandalfMusic = content.Load<SoundEffect>("gandalfMusic");
            GandalfMusicInstance = gandalfMusic.CreateInstance();
            SelectedTexture = new List<Texture2D>() { AssetHolder.Instance.WallTexture };
            Skybox = content.Load<Model>("Skybox");
            FinishTexture = content.Load<Texture2D>("finishTexture");
            GroundTexture = content.Load<Texture2D>("groundTexture");
            KeyTexture = content.Load<Texture2D>("keyTexture");
            keyPickup = content.Load<SoundEffect>("key_pickup");
            KeyPickupfMusicInstance = keyPickup.CreateInstance();
        }
    }
}

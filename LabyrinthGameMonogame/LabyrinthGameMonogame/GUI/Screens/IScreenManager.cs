using LabyrinthGameMonogame.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LabyrinthGameMonogame.GUI.Screens
{
    interface IScreenManager
    {
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
        void Initialize();
        void LoadContent();
        ScreenTypes ActiveScreenType { get; set; }
        ScreenTypes PreviousScreenType { get; set; }
        bool IsTransitioning { get; set; }
        Vector2 Dimensions { get; set; }
        List<DisplayMode> Resolutions { get; }
        GraphicsDeviceManager Graphics { get; } 
        bool Fullscreen { get; set; }
        void ChangeScreenMode();
        void ChangeResolution();
    }
}

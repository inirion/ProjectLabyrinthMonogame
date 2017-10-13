using LabyrinthGameMonogame.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LabyrinthGameMonogame.GUI.Buttons
{
    class Button
    {
        #region Variables
        private Rectangle buttonRect;
        private string text;
        private SpriteFont font;
        private ScreenTypes goesTo;
        private Color color;
        private bool enabled;
        #endregion

        public Button(string Text,ScreenTypes goesTo, SpriteFont font, bool enabled)
        {
            color = Color.White;
            buttonRect = new Rectangle();
            this.Text = Text;
            Font = font;
            Enabled = enabled;
            GoesTo = goesTo;
        }

        public Rectangle ButtonRect { get => buttonRect; set => buttonRect = value; }
        public string Text { get => text; set => text = value; }
        public SpriteFont Font { get => font; set => font = value; }
        public Color Color { get => color; set => color = value; }
        public bool Enabled { get => enabled; set => enabled = value; }
        internal ScreenTypes GoesTo { get => goesTo; set => goesTo = value; }
    }
}

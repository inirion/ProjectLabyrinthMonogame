using LabyrinthGameMonogame.GUI.Buttons;
using LabyrinthGameMonogame.InputControllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LabyrinthGameMonogame.GUI.Screens
{
    class ScreenDrawable : DrawableGameComponent
    {
        protected List<Button> buttons;
        protected SpriteBatch spriteBatch;
        protected Game game;
        protected IScreenManager screenManager => (IScreenManager)game.Services.GetService(typeof(IScreenManager));
        protected IControlManager controlManager => (IControlManager)game.Services.GetService(typeof(IControlManager));
        public ScreenDrawable(Game game) : base(game)
        {
            this.game = game;
        }

        public override void Initialize()
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            
            base.Initialize();
        }

        public virtual void SetupButtons() {
            float gap = buttons[0].Font.MeasureString(buttons[0].Text).Y + buttons[0].Font.MeasureString(buttons[0].Text).Y / 2;
            float offset = 0;
            foreach (Button btn in buttons)
            {
                btn.ButtonRect = new Rectangle(
                    (int)((screenManager.Dimensions.X / 2) - (btn.Font.MeasureString(btn.Text).X) / 2),
                    (int)(screenManager.Dimensions.Y / 2 - (btn.Font.MeasureString(btn.Text).Y) + offset),
                    (int)(btn.Font.MeasureString(btn.Text).X),
                    (int)(btn.Font.MeasureString(btn.Text).Y)
                    );
                offset += gap;
            }
        }
    }
}

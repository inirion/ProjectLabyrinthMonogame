using LabyrinthGameMonogame.Enums;
using LabyrinthGameMonogame.GUI.Screens;
using LabyrinthGameMonogame.InputControllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LabyrinthGameMonogame.GameFolder
{
    class Minimap
    {
        private int[,] map;
        private Texture2D wall;
        private Texture2D key;
        private Texture2D player;
        private Texture2D finish;
        SpriteBatch spriteBatch;
        private bool toggle;
        private double timeToDisplay;
        int timesUsed;

        public Minimap(int[,] map, Game game, IScreenManager screenManager)
        {
            timesUsed = 0;
            timeToDisplay = 4;
            toggle = false;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            int sizeX = (int)screenManager.Dimensions.X / map.GetLength(0);
            int sizeY = (int)screenManager.Dimensions.Y / map.GetLength(1);

            wall = new Texture2D(game.GraphicsDevice, sizeX, sizeY);
            key = new Texture2D(game.GraphicsDevice, sizeX, sizeY);
            player = new Texture2D(game.GraphicsDevice, sizeX, sizeY);
            finish = new Texture2D(game.GraphicsDevice, sizeX, sizeY);

            Color[] data = new Color[sizeX * sizeY];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Gray;
            wall.SetData(data);

            for (int i = 0; i < data.Length; ++i) data[i] = Color.Gold;
            key.SetData(data);

            for (int i = 0; i < data.Length; ++i) data[i] = Color.Blue;
            player.SetData(data);

            for (int i = 0; i < data.Length; ++i) data[i] = Color.Red;
            finish.SetData(data);
            this.map = map;
        }

        public void Reset(int [,] map, Game game, IScreenManager screenManager)
        {
            toggle = false;
            int sizeX = (int)screenManager.Dimensions.X / map.GetLength(0);
            int sizeY = (int)screenManager.Dimensions.Y / map.GetLength(1);

            wall = new Texture2D(game.GraphicsDevice, sizeX, sizeY);
            key = new Texture2D(game.GraphicsDevice, sizeX, sizeY);
            player = new Texture2D(game.GraphicsDevice, 10, 10);
            finish = new Texture2D(game.GraphicsDevice, sizeX, sizeY);

            Color[] data = new Color[sizeX * sizeY];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Gray;
            wall.SetData(data);

            for (int i = 0; i < data.Length; ++i) data[i] = Color.Gold;
            key.SetData(data);
            data = new Color[10 * 10];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Blue;
            player.SetData(data);
            data = new Color[sizeX * sizeY];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Red;
            finish.SetData(data);
            this.map = map;
            timeToDisplay = 2;
            timesUsed = 0;
        }
        public void Reset(Vector2 key)
        {
            this.map[(int)key.X,(int)key.Y] = (int)LabiryntElement.Road;
        }

        public void Update(IControlManager control, GameTime gameTime)
        {
            if (control.Keyboard.Clicked(KeyboardKeys.Map))
            {
                toggle = true;
            }
            if (toggle)
            {
                timeToDisplay -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timeToDisplay <= 0)
                {
                    timesUsed++;
                    toggle = !toggle;
                    timeToDisplay = 4;
                }
            }
        }

        public void Draw(Vector2 playerPosition)
        {
            
            spriteBatch.Begin();
            if (toggle && timesUsed <=2)
            {
                Vector2 pos = new Vector2(playerPosition.X * wall.Width + wall.Width / 2 + player.Width, playerPosition.Y * wall.Height + wall.Height / 2 + player.Height);
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        if (isWall(i,j))
                            spriteBatch.Draw(wall, new Vector2(i * wall.Width, j * wall.Height ), Color.White);
                        if (map[i, j] == (int)LabiryntElement.Key)
                            spriteBatch.Draw(key, new Vector2(i * wall.Width, j * wall.Height), Color.White);
                        if (map[i, j] == (int)LabiryntElement.Finish)
                            spriteBatch.Draw(finish, new Vector2(i * wall.Width, j * wall.Height), Color.White);
                    }
                }
                spriteBatch.Draw(player, pos, color:Color.White, origin:new Vector2(player.Width+player.Width/2,player.Height+player.Height/2));
            }
            spriteBatch.End();
        }
        private bool isWall(int x, int y)
        {
            if (map[x, y] == (int)LabiryntElement.Wall || map[x, y] == (int)LabiryntElement.Wall3WayEast|| map[x, y] == (int)LabiryntElement.Wall3WayNorth|| map[x, y] == (int)LabiryntElement.Wall3WaySouth
                || map[x, y] == (int)LabiryntElement.WallEN|| map[x, y] == (int)LabiryntElement.WallES|| map[x, y] == (int)LabiryntElement.WallEW|| map[x, y] == (int)LabiryntElement.WallNS 
                || map[x, y] == (int)LabiryntElement.WallWS || map[x, y] == (int)LabiryntElement.Wall3WayWest || map[x, y] == (int)LabiryntElement.Pillar || map[x, y] == (int)LabiryntElement.WallWN)
            {
                return true;
            }
            return false;
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using System.Collections.Generic;
using TiledSharp;

namespace SeasonsPlatformer
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;
        private TmxMap map;
        private TileMapManager mapManager;
        private List<Rectangle> collisionObjects;
        private Matrix matrix;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
           
            _graphics.PreferredBackBufferWidth = 128 * 2;//Making the window size twice our tilemap size
            _graphics.PreferredBackBufferHeight = 128 * 2;
            _graphics.ApplyChanges();
            var Width = _graphics.PreferredBackBufferWidth;
            var Height = _graphics.PreferredBackBufferHeight;
            var WindowSize = new Vector2(Width, Height);
            var mapSize = new Vector2(128, 128);//Our tile map size
            matrix = Matrix.CreateScale(new Vector3(WindowSize / mapSize, 1));
            player = new Player();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            map = new TmxMap("Content/map.tmx");
            Texture2D[] tileset = { Content.Load<Texture2D>("Summer - spring/Ground/" + map.Tilesets[0].Name.ToString()),
            Content.Load<Texture2D>("Summer - spring/Trees/" + map.Tilesets[1].Name.ToString())};
        
        int tileWidth = map.Tilesets[0].TileWidth;
            int tileHeight = map.Tilesets[0].TileHeight;
            int TileSetTilesWide = tileset[0].Width / tileWidth;
            mapManager = new TileMapManager(_spriteBatch, map, tileset[0], TileSetTilesWide, tileWidth, tileHeight);
            collisionObjects = new List<Rectangle>();
            foreach (var o in map.ObjectGroups["Collisions"].Objects)
            {
                collisionObjects.Add(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height));
            }

            SpriteSheet[] sheets = { Content.Load<SpriteSheet>("Player/Player.sf",new JsonContentLoader()),
                                  Content.Load<SpriteSheet>("Player/Player_Jump.sf",new JsonContentLoader())};
            player.Load(sheets);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var initpos = player.pos;
            var Ypos = player.pos.Y;
            player.Update(gameTime);
            foreach (var rect in collisionObjects)
            {
                if (rect.Intersects(new Rectangle(player.playerBounds.X+2,player.playerBounds.Y-1,4,4)))
                {
                    player.pos.X = initpos.X;
                    player.isIdle = true;
                    player.isFalling = false;
                }
                
                if(rect.Contains(new Point(player.playerBounds.X+4,player.playerBounds.Y+9)))
                {
                    
                    player.isFalling = false;
                    player.pos.Y = initpos.Y;
                }
                else
                {
                    player.isFalling = true;
                }
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            mapManager.Draw(matrix);
            player.Draw(_spriteBatch, matrix);
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel.Design;

namespace TucSpaceShooter
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;
        private Texture2D playerShip;
        private Texture2D playerShipAcc;
        private Texture2D stageOneBgr;
        private Vector2 playerPosition;
        private int bgrCounter;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.PreferredBackBufferWidth = 540;
            _graphics.ApplyChanges();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            playerShip = Content.Load<Texture2D>("TUCShip");
            playerShipAcc = Content.Load<Texture2D>("TUCShipFire");
            stageOneBgr = Content.Load<Texture2D>("Background_2");
            player = new Player(playerPosition, _graphics);
            playerPosition = player.Position;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.PlayerMovement(player, _graphics);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            
            player.DrawGame(_spriteBatch, playerShip, playerShipAcc, stageOneBgr, player, bgrCounter);
            
            _spriteBatch.End();
            base.Draw(gameTime);
            bgrCounter++;
            if (bgrCounter == 720)
            {
                bgrCounter = 0;
            }
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel.Design;

namespace TucSpaceShooter
{
    public enum GameStates
    {
        Menu,
        Play,
        Highscore
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private static GameStates currentState;

        private Player player;
        private Texture2D playerShip;
        private Texture2D playerShipAcc;
        private Texture2D stageOneBgr;
        private Vector2 playerPosition;
        private Texture2D healthBar;
        private Texture2D healthPoint;
        private Texture2D healthEmpty;
        private int bgrCounter;

        public static GameStates CurrentState { get => currentState; set => currentState = value; }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            currentState = GameStates.Play;//Ska göra så att man startar i menyn, byt ut Menu för att starta i annan state
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
            player = new Player(playerPosition, _graphics, 5);
            playerPosition = player.Position;
            healthBar = Content.Load<Texture2D>("HealthContainer");
            healthPoint = Content.Load<Texture2D>("FullHeart");
            healthEmpty = Content.Load<Texture2D>("EmptyHeart");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            switch (currentState)
            {
                case GameStates.Menu:
                    //Kod för meny
                    break;
                case GameStates.Play:
                    //kod för Play
                    player.PlayerMovement(player, _graphics);
                    
                    break;
                case GameStates.Highscore:
                    //kod för highscore
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here

            switch (currentState)
            {
                case GameStates.Menu:
                    //kod för meny
                    _spriteBatch.Begin();
                    
                    _spriteBatch.End();

                    break;
                case GameStates.Play:
                    //kod för Play
                    _spriteBatch.Begin();
                    player.DrawGame(_spriteBatch, playerShip, playerShipAcc, stageOneBgr, player, bgrCounter);
                    player.PlayerHealth(player, healthBar, healthPoint, healthEmpty, _spriteBatch);
                    _spriteBatch.End();
                    bgrCounter++;
                    if (bgrCounter == 720)
                    {
                        bgrCounter = 0;
                    }
                    break;
                case GameStates.Highscore:
                    //kod för highscore
                    
                    break;
            }
            base.Draw(gameTime);
        }
    }
}

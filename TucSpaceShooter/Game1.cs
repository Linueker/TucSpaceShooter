using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using static TucSpaceShooter.Powerup;


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
        private Random random;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Play
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

        //Bullet
        private Texture2D bulletTexture;
        private List<Bullet> bullets = new List<Bullet>();
        private TimeSpan lastBulletTime;
        private TimeSpan bulletCooldown;

        // Powerups
        private Powerup powerup;
        private Texture2D jetpack;
        private Texture2D shield;
        private Texture2D repair;
        private Texture2D doublePoints;
        private Texture2D triplePoints;

        private List<Powerup> powerups;

        private int powerupWidth;
        private int powerupHeight;

        public static GameStates CurrentState { get => currentState; set => currentState = value; }


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            powerups = new List<Powerup>();
            random = new Random();
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

            powerup = new Powerup(playerPosition);
            jetpack = Content.Load<Texture2D>("JetpackShip");
            shield = Content.Load<Texture2D>("ShieldShip");
            repair = Content.Load<Texture2D>("RepairShip");
            doublePoints = Content.Load<Texture2D>("2xPoints");
            triplePoints = Content.Load<Texture2D>("3xPoints");

            powerupWidth = 15;
            powerupHeight = 15;

            healthBar = Content.Load<Texture2D>("HealthContainer");
            healthPoint = Content.Load<Texture2D>("FullHeart");
            healthEmpty = Content.Load<Texture2D>("EmptyHeart");

            bulletTexture = Content.Load<Texture2D>("PlayerBullets");
        }

        protected override void Update(GameTime gameTime )
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

                    player.HandlePowerupCollision(powerups);
                    powerup.SpawnPowerup(random, _graphics, powerupWidth, jetpack, shield, repair, doublePoints, triplePoints, powerups);
                    powerup.UpdatePowerups(gameTime, powerups, _graphics);

                    break;
                case GameStates.Highscore:
                    //kod för highscore
                    break;
            }
            {
                if (currentState == GameStates.Play)
                {
                    // Existing player movement and game update code...
                    player.PlayerMovement(player, _graphics);

                    // Bullet firing logic
                    if (Keyboard.GetState().IsKeyDown(Keys.Space) && gameTime.TotalGameTime - lastBulletTime > bulletCooldown)
                    {
                        // Assuming the player's ship's guns are at these 
                        Vector2 leftGunPosition = new Vector2(player.Position.X + 26 - bulletTexture.Width / 2, player.Position.Y);
                        Vector2 rightGunPosition = new Vector2(player.Position.X + -4  - bulletTexture.Width / 2, player.Position.Y);

                        // Add new bullets to the list from both guns
                        bullets.Add(new Bullet(leftGunPosition));
                        bullets.Add(new Bullet(rightGunPosition));

                        // Record the time the bullets were fired
                        lastBulletTime = gameTime.TotalGameTime;
                    }

                    // Update existing bullets
                    foreach (var bullet in bullets.ToList())
                    {
                        bullet.Update();
                        if (!bullet.IsActive)
                        {
                            bullets.Remove(bullet);
                        }
                    }
                }
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

                    foreach (Powerup powerup in powerups)
                    {
                        _spriteBatch.Draw(powerup.Texture, powerup.Position, Color.White);
                    }

                    player.PlayerHealth(player, healthBar, healthPoint, healthEmpty, _spriteBatch);
                   
                    foreach (var bullet in bullets)
                    {
                        bullet.Draw(_spriteBatch, bulletTexture);
                    }

                    _spriteBatch.End();
                    bgrCounter++;
                    if (bgrCounter == 2160)
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

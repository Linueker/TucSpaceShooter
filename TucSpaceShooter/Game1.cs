using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        Highscore,
        Quit
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
        private Song gameMusic;
        private bool gameMusicIsPlaying;

        //Bullet
        private Texture2D bulletTexture;
        private List<Bullet> bullets = new List<Bullet>();
        private TimeSpan lastBulletTime;
        private TimeSpan bulletCooldown;
        private bool spaceWasPressed = false;
        private SoundEffect shoot;

        // Powerups
        private Powerup powerup;
        private Texture2D jetpack;
        private Texture2D shield;
        private Texture2D repair;
        private Texture2D doublePoints;
        private Texture2D triplePoints;

        private Texture2D playerShield;

        private SoundEffect pickUp;

        private List<Powerup> powerups;

        private int powerupWidth;
        private int powerupHeight;

        //Menu
        private MenuScreen menu;
        private Texture2D startButtonTexture;
        private Rectangle startButtonBounds;
        private Texture2D highscoreButtonTexture;
        private Rectangle highscoreButtonBounds;
        private Texture2D quitButtonTexture;
        private Rectangle quitButtonBounds;
        Song menuMusic;
        private bool menuMusicIsPlaying;



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
            currentState = GameStates.Menu;//Ska göra så att man startar i menyn, byt ut Menu för att starta i annan state
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

            playerShield = Content.Load<Texture2D>("PlayerShield");
            
            pickUp = Content.Load<SoundEffect>("power_up_grab-88510");

            powerupWidth = 15;
            powerupHeight = 15;

            healthBar = Content.Load<Texture2D>("LeftHealthContainer");
            healthPoint = Content.Load<Texture2D>("FullHeartRed");
            healthEmpty = Content.Load<Texture2D>("EmptyHeartNew");

            bulletTexture = Content.Load<Texture2D>("PlayerBullets");
            shoot = Content.Load<SoundEffect>("laser-gun-shot-sound-future-sci-fi-lazer-wobble-chakongaudio-174883");
            SoundEffect.MasterVolume = 0.5f;
            Bullet.LoadContent(bulletTexture);

            //Menu
            startButtonTexture = Content.Load<Texture2D>("StartButton");
            highscoreButtonTexture = Content.Load<Texture2D>("HiscoreButton");
            quitButtonTexture = Content.Load<Texture2D>("QuitButton");

            startButtonBounds = new Rectangle((_graphics.PreferredBackBufferWidth-startButtonTexture.Width)/2,270, startButtonTexture.Width, startButtonTexture.Height);
            highscoreButtonBounds = new Rectangle((_graphics.PreferredBackBufferWidth-highscoreButtonTexture.Width)/2,300, highscoreButtonTexture.Width, highscoreButtonTexture.Height);
            quitButtonBounds = new Rectangle((_graphics.PreferredBackBufferWidth-quitButtonTexture.Width)/2,330,quitButtonTexture.Width, quitButtonTexture.Height);
            menu = new MenuScreen(startButtonTexture, startButtonBounds, highscoreButtonTexture, highscoreButtonBounds, quitButtonTexture, quitButtonBounds);

            menuMusic = Content.Load<Song>("electric-dreams-167873");
            menuMusicIsPlaying = false;
            gameMusic = Content.Load<Song>("kim-lightyear-angel-eyes-vision-ii-189557");
            gameMusicIsPlaying = false;
            MediaPlayer.Volume = 0.5f;

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
                    if (!menuMusicIsPlaying)
                    {
                        MediaPlayer.Play(menuMusic);
                        menuMusicIsPlaying = true;  
                    }
                    menu.Update(gameTime);
                    break;
                case GameStates.Play:
                    //kod för Play
                    if (!gameMusicIsPlaying)
                    {
                        MediaPlayer.Play(gameMusic);
                        gameMusicIsPlaying = true;
                    }
                    player.PlayerMovement(player, _graphics);
                    player.HandlePowerupCollision(powerups, pickUp);
                    powerup.SpawnPowerup(random, _graphics, powerupWidth, jetpack, shield, repair, doublePoints, triplePoints, powerups);
                    powerup.UpdatePowerups(gameTime, powerups, _graphics);
                    Bullet.UpdateAll(gameTime, player, shoot);
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

                    menu.Draw(_spriteBatch);

                    _spriteBatch.End();
                    break;
                case GameStates.Play:
                    //kod för Play
                    _spriteBatch.Begin();

                    Background.DrawBackground(bgrCounter, _spriteBatch, stageOneBgr);
                    player.DrawPlayer(_spriteBatch, playerShip, playerShipAcc, player, bgrCounter, playerShield);
                    DrawPowerups(_spriteBatch, powerups);
                    player.DrawPlayerHealth(player, healthBar, healthPoint, healthEmpty, _spriteBatch);
                    
                    Bullet.DrawAll(_spriteBatch);
                    
                    _spriteBatch.End();
                    bgrCounter++;
                    break;
                case GameStates.Highscore:
                    //kod för highscore
                    _spriteBatch.Begin();

                    GraphicsDevice.Clear(Color.Orange);

                    _spriteBatch.End();
                    break;
                case GameStates.Quit:
                    Exit();
                    break;
            }
            if (bgrCounter == 2160)
            {
                bgrCounter = 0;
            }
            base.Draw(gameTime);
        }
    }
}

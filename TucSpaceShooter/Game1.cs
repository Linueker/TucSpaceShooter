using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using static TucSpaceShooter.Powerup;


namespace TucSpaceShooter
{
    public enum GameStates
    {
        Menu,
        Play,
        GameOver,
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

        //enemy
        private EnemyTypOne enemiesOne;
        private List<EnemyTypOne> enemyTypOnesList= new List<EnemyTypOne>();
        private List<EnemyTypeTwo> enemyTypTwoList= new List<EnemyTypeTwo>();
        private EnemyTypeTwo enemiesTwo;
        private List<EnemyTypeThree> enemyTypThreeList = new List<EnemyTypeThree>();
        private EnemyTypeThree enemiesThree;
        private EnmeyBoss bossEnemy;
        private Texture2D enemyShipOne;
        private Texture2D enemyShipTwo;
        private Texture2D enemyShipThree;
        private Texture2D BossShip;
        private Vector2 enemyPosition;
        private Vector2 enemyPositiontwo;
        private Vector2 enemyPositionthree;
        private Vector2 enemyPositionBoss;
        private Song bossMusic;
        private bool bossMusicIsPlaying;
        private SpriteFont font;

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
        private Texture2D powerUpBar;

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
        private Texture2D menuBackground;
        private Texture2D[] bossEye;
        private int currentBossEye;
        private int menuCounter;
        private Texture2D menuForeground;
        private Texture2D[] menuTitle;
        private int currentMenuTitle;

        //GameOver
        private Texture2D gameOverImg;
        private Song endSong;
        private bool endSongIsPlaying;

        //GameTimer
        float timer = 0f;
        //Sätter tiden för hur länge fiender ska spawnas innan bossen kommer (sekunder + f). Höj om banan ska va längre. 
        float enemyDuration = 10f;
        bool drawEnemy = true;

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
            powerUpBar = Content.Load<Texture2D>("RightHealthContainer");

            playerShield = Content.Load<Texture2D>("PlayerShield");
            
            pickUp = Content.Load<SoundEffect>("power_up_grab-88510");

            Fonts.LoadContent(Content);

            powerupWidth = 15;
            powerupHeight = 15;

            healthBar = Content.Load<Texture2D>("LeftHealthContainer");
            healthPoint = Content.Load<Texture2D>("FullHeartRed");
            healthEmpty = Content.Load<Texture2D>("EmptyHeartNew");

            bulletTexture = Content.Load<Texture2D>("PlayerBullets");
            shoot = Content.Load<SoundEffect>("laser-gun-shot-sound-future-sci-fi-lazer-wobble-chakongaudio-174883");
            SoundEffect.MasterVolume = 0.5f;
            Bullet.LoadContent(bulletTexture);

            enemiesOne = new EnemyTypOne(enemyPosition, _graphics,10);
            enemyTypOnesList.Add(enemiesOne);
            enemiesTwo = new EnemyTypeTwo(enemyPositiontwo, _graphics,10);
            enemyTypTwoList.Add(enemiesTwo);
            enemiesTwo.ResetPosition(_graphics);
            enemiesThree = new EnemyTypeThree(enemyPositionthree, _graphics, 10);
            enemyTypThreeList.Add(enemiesThree);
            bossEnemy = new EnmeyBoss(enemyPositionBoss, _graphics, 10);
            enemyPosition = enemiesOne.Position;
            enemyPositiontwo = enemiesTwo.Position;
            enemyPositionthree = enemiesThree.Position;
            enemyPositionBoss = bossEnemy.Position;
            enemyShipOne = Content.Load<Texture2D>("EnemyY");
            enemyShipTwo = Content.Load<Texture2D>("EnemyYX");
            enemyShipThree = Content.Load<Texture2D>("Enemy3X");
            BossShip = Content.Load<Texture2D>("BossMonsterRnd");

            //Menu
            startButtonTexture = Content.Load<Texture2D>("StartButton");
            highscoreButtonTexture = Content.Load<Texture2D>("HiscoreButton");
            quitButtonTexture = Content.Load<Texture2D>("QuitButton");

            startButtonBounds = new Rectangle((_graphics.PreferredBackBufferWidth-startButtonTexture.Width)/2,285, startButtonTexture.Width, startButtonTexture.Height);
            highscoreButtonBounds = new Rectangle((_graphics.PreferredBackBufferWidth-highscoreButtonTexture.Width)/2,335, highscoreButtonTexture.Width, highscoreButtonTexture.Height);
            quitButtonBounds = new Rectangle((_graphics.PreferredBackBufferWidth-quitButtonTexture.Width)/2,385,quitButtonTexture.Width, quitButtonTexture.Height);
            menu = new MenuScreen(startButtonTexture, startButtonBounds, highscoreButtonTexture, highscoreButtonBounds, quitButtonTexture, quitButtonBounds);

            menuMusic = Content.Load<Song>("electric-dreams-167873");
            menuMusicIsPlaying = false;
            gameMusic = Content.Load<Song>("kim-lightyear-angel-eyes-vision-ii-189557");
            gameMusicIsPlaying = false;
            bossMusic = Content.Load<Song>("a-hero-of-the-80s_v2_60sec-178277");
            bossMusicIsPlaying = false;
            MediaPlayer.Volume = 0.5f;

            menuBackground = Content.Load<Texture2D>("Background_2");
            bossEye = new Texture2D[24];
            for (int i = 1; i <= 24; i++)
            {
                bossEye[i - 1] = Content.Load<Texture2D>("BossEye" + i.ToString());
            }
            menuForeground = Content.Load<Texture2D>("Foreground");

            menuTitle = new Texture2D[10];
            for (int i = 1; i <= 10; i++)
            {
                menuTitle[i - 1] = Content.Load<Texture2D>("MenuTitle" + i.ToString());
            }

            //GameOver
            gameOverImg = Content.Load<Texture2D>("GameOverTitle");
            endSong = Content.Load<Song>("lady-of-the-80x27s-128379");
            endSongIsPlaying = false;
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
                    menuCounter++;
                    if (menuCounter >= 6)//ändra siffran för att anpassa meny animationernas hastighet
                    {
                        menuCounter = 0;
                        currentBossEye++;
                        currentMenuTitle++;

                        if (currentBossEye > bossEye.Length-1)
                        {
                            currentBossEye = 0;
                        }
                        if (currentMenuTitle > menuTitle.Length - 1)
                        {
                            currentMenuTitle = 0;
                        }
                    }
                    menu.MainMenu();
                    break;
                case GameStates.Play:
                    //kod för Play
                    if(player.Health == 0)
                    {
                        CurrentState = GameStates.GameOver;
                    }
                    if (!gameMusicIsPlaying)
                    {
                        MediaPlayer.Play(gameMusic);
                        gameMusicIsPlaying = true;
                    }
                    timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    
                    if (timer >= enemyDuration)
                    {
                        drawEnemy = false;
                    }

                    base.Update(gameTime);
                    player.PlayerMovement(player, _graphics);
                    player.HandlePowerupCollision(powerups, pickUp);
                    powerup.SpawnPowerup(random, _graphics, powerupWidth, jetpack, shield, repair, doublePoints, triplePoints, powerups);
                    powerup.UpdatePowerups(gameTime, powerups, _graphics);
                    //enemiesOne.MoveToRandomPosition(_graphics);
                    if (drawEnemy)
                    {
                        foreach (EnemyTypOne enemy in enemyTypOnesList.ToList())
                        {
                            enemy.MoveToRandomPosition(_graphics);
                            enemy.Damage(_graphics, player);
                        }
                        foreach (EnemyTypeTwo enemy in enemyTypTwoList.ToList())
                        {
                            enemy.MoveToRandomPosition(_graphics);
                            enemy.Damage(_graphics, player);
                        }
                        foreach (EnemyTypeThree enemy in enemyTypThreeList.ToList())
                        {
                            enemy.MoveToRandomPosition(_graphics);
                            enemy.Damage(_graphics, player);
                        }
                        enemiesTwo.MoveToRandomPosition(_graphics);
                        enemiesThree.MoveToRandomPosition(_graphics);
                    }
                    else
                    {
                        if (!bossMusicIsPlaying)
                        {
                            MediaPlayer.Play(bossMusic);
                            bossMusicIsPlaying = true;
                        }
                        bossEnemy.MoveToRandomPosition(_graphics);
                    }
                    
                    Bullet.UpdateAll(gameTime, player, shoot);
      
                    break;
                case GameStates.Highscore:
                    //kod för highscore
                    break;

                case GameStates.GameOver:
                    if (!endSongIsPlaying)
                    {
                        MediaPlayer.Play(endSong);
                        endSongIsPlaying = true;
                    }
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

                    _spriteBatch.Draw(menuBackground, Vector2.Zero, Color.White);
                    _spriteBatch.Draw(bossEye[currentBossEye], new Vector2(0,130), Color.White);
                    _spriteBatch.Draw(menuForeground, Vector2.Zero, Color.White);
                    _spriteBatch.Draw(menuTitle[currentMenuTitle], new Vector2((_graphics.PreferredBackBufferWidth - menuTitle[currentMenuTitle].Width)/2,70), Color.White);

                    menu.Draw(_spriteBatch);

                    _spriteBatch.End();
                    break;
                case GameStates.Play:
                    //kod för Play
                    _spriteBatch.Begin();

                    Background.DrawBackground(bgrCounter, _spriteBatch, stageOneBgr);
                    player.DrawPlayer(_spriteBatch, playerShip, playerShipAcc, player, bgrCounter, playerShield);
                    DrawPowerups(_spriteBatch, powerups);

                    player.DrawPlayerHealth(player, healthBar, healthPoint, healthEmpty, _spriteBatch, powerUpBar, jetpack, shield, doublePoints, triplePoints);

                    //_spriteBatch.Draw(enemyShipOne, enemiesOne.Position, Color.White);
                    if (drawEnemy)
                    {
                        foreach (EnemyTypOne enemy in enemyTypOnesList)
                        {
                            if (enemy.EnemyHealth != 0)
                            {
                                _spriteBatch.Draw(enemyShipOne, enemy.Position, Color.White);
                                break;
                            }
                        }
                        foreach (EnemyTypeTwo enemy in enemyTypTwoList)
                        {
                            if (enemy.EnemyHealth != 0)
                            {
                                _spriteBatch.Draw(enemyShipTwo, enemiesTwo.Position, Color.White);
                                break;
                            }
                        }
                        foreach (EnemyTypeThree enemy in enemyTypThreeList)
                        {
                            if (enemy.EnemyHealth != 0)
                            {
                                _spriteBatch.Draw(enemyShipThree, enemiesThree.Position, Color.White);
                                break;
                            }
                        }
                    }
                    else
                    {
                        _spriteBatch.Draw(BossShip, bossEnemy.Position, Color.White);
                    }
                    Bullet.DrawAll(_spriteBatch);

                    Fonts.DrawText(_spriteBatch, "SCORE: " + player.points.GetCurrentPoints(), new Vector2(10, 10), Color.White);

                    player.DrawPlayerHealth(player, healthBar, healthPoint, healthEmpty, _spriteBatch, powerUpBar, jetpack, shield, doublePoints, triplePoints);

                    _spriteBatch.End();
                    bgrCounter++;
                    break;
                case GameStates.Highscore:
                    //kod för highscore
                    _spriteBatch.Begin();

                    GraphicsDevice.Clear(Color.Orange);

                    _spriteBatch.End();
                    break;
                case GameStates.GameOver:
                    _spriteBatch.Begin();

                    Background.DrawBackground(bgrCounter, _spriteBatch, stageOneBgr);
                    _spriteBatch.Draw(gameOverImg, new Vector2(100, 100), Color.White);
                    Fonts.DrawText(_spriteBatch, "FINAL SCORE: \n" + player.points.GetCurrentPoints(), new Vector2(175, 300), Color.White);
                    Fonts.DrawText(_spriteBatch, "ENTER NAME: \n", new Vector2(175, 430), Color.White);
                    bgrCounter++;
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

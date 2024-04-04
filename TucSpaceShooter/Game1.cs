using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
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

        private KeyboardState keyboardState;
        private Keys[] keys;
        private string userInput;
        private HighscoreScreen playerHighscore;
        
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
        private List<EnemyTypeTwo> enemyTypTwoList = new List<EnemyTypeTwo>();
        private EnemyTypeTwo enemiesTwo;
        private List<EnemyTypeThree> enemyTypThreeList = new List<EnemyTypeThree>();
        private EnemyTypeThree enemiesThree;
        private EnemeyBoss bossEnemy;
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
        private Texture2D enemyBulletTexture;
        private Texture2D LeftBulletTexture;
        private Texture2D RightBulletTexture;
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
        private Song menuMusic;
        private bool menuMusicIsPlaying;
        private Texture2D menuBackground;
        private Texture2D[] bossEye;
        private int currentBossEye;
        private int menuCounter;
        private Texture2D menuForeground;
        private Texture2D[] menuTitle;
        private int currentMenuTitle;

        //Highscore
        private HighscoreScreen highscoreMenu;
        private Texture2D highscoreBackground;
        private Texture2D[] highscoreTitle;
        private int currentHighscoreTitle;
        private int highscoreTitleCounter;
        private Texture2D highscoreBoard;
        private Texture2D backButtonTexture;
        private Rectangle backButtonBounds;
        private Rectangle saveButtonBounds;
        private Button saveButton;
        List<string> highscores;
        private string topTen;
        private bool victory;
        private int placing;
        int rowCounter;

        //GameOver
        private Texture2D gameOverImg;
        private Song endSong;
        private bool endSongIsPlaying;

        //GameTimer
        float timer = 0f;
        //Sätter tiden för hur länge fiender ska spawnas innan bossen kommer (sekunder + f). Höj om banan ska va längre. 
        float enemyDuration = 30f;
        bool drawEnemy = true;

        //Explosion
        private Texture2D[] bossExplosion;
        private Texture2D[] explosion;
        private int currentExplosion;
        private int explosionCounter;
        private float explosionTimer;
        private bool drawExplosionOne = true;
        private bool drawExplosionTwo = true;
        private bool drawExplosionThree = true;
        private bool drawExplosionBoss = true;
        private SoundEffect bossExplosionSound;
        private SoundEffect EnemyExplosionSound;


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
            Fonts.LoadContent(Content);
            userInput = "";
            MediaPlayer.Volume = 0.5f;
            SoundEffect.MasterVolume = 0.5f;

            //Player
            playerShip = Content.Load<Texture2D>("TUCShip");
            playerShipAcc = Content.Load<Texture2D>("TUCShipFire");
            stageOneBgr = Content.Load<Texture2D>("Background_2");
            player = new Player(playerPosition, _graphics, 5);
            playerPosition = player.Position;
            healthBar = Content.Load<Texture2D>("LeftHealthContainer");
            healthPoint = Content.Load<Texture2D>("FullHeartRed");
            healthEmpty = Content.Load<Texture2D>("EmptyHeartNew");
            gameMusic = Content.Load<Song>("kim-lightyear-angel-eyes-vision-ii-189557");
            gameMusicIsPlaying = false;
            playerHighscore = new HighscoreScreen(backButtonTexture,saveButtonBounds);

            //PowerUps
            powerup = new Powerup(playerPosition);
            jetpack = Content.Load<Texture2D>("JetpackShip");
            shield = Content.Load<Texture2D>("ShieldShip");
            repair = Content.Load<Texture2D>("RepairShip");
            doublePoints = Content.Load<Texture2D>("2xPoints");
            triplePoints = Content.Load<Texture2D>("3xPoints");
            powerUpBar = Content.Load<Texture2D>("RightHealthContainer");
            playerShield = Content.Load<Texture2D>("PlayerShield");
            pickUp = Content.Load<SoundEffect>("power_up_grab-88510");
            powerupWidth = 15;
            powerupHeight = 15;
            
            //Bullets
            bulletTexture = Content.Load<Texture2D>("PlayerBullets");
            enemyBulletTexture = Content.Load<Texture2D>("EnemyBullets");
            LeftBulletTexture = Content.Load<Texture2D>("EnemyBulletLeft");
            RightBulletTexture = Content.Load<Texture2D>("EnemyBulletRight");
            shoot = Content.Load<SoundEffect>("laser-gun-shot-sound-future-sci-fi-lazer-wobble-chakongaudio-174883");
            Bullet.LoadContent(bulletTexture, enemyBulletTexture, LeftBulletTexture, RightBulletTexture);

            // Ladda explosionstexterna från Content
            explosion = new Texture2D[7];
            for (int i = 1; i <= 7; i++)
            {
                explosion[i - 1] = Content.Load<Texture2D>("Explosion" + i.ToString());
            }
            bossExplosion = new Texture2D[7];
            for (int i = 1; i <= 7; i++)
            {
                bossExplosion[i - 1] = Content.Load<Texture2D>("BossExplosion" + i.ToString());
            }

            //Enemies
            enemyShipOne = Content.Load<Texture2D>("EnemyY");
            enemiesOne = new EnemyTypOne(enemyPosition, _graphics, enemyShipOne, 5);
            enemyTypOnesList.Add(enemiesOne);
            enemyShipTwo = Content.Load<Texture2D>("EnemyYX");
            enemiesTwo = new EnemyTypeTwo(enemyPositiontwo, _graphics, enemyShipTwo, 10);
            enemyTypTwoList.Add(enemiesTwo);
            //enemiesTwo.ResetPosition(_graphics);
            enemyShipThree = Content.Load<Texture2D>("Enemy3X");
            enemiesThree = new EnemyTypeThree(enemyPositionthree, _graphics, enemyShipThree, 8);
            enemyTypThreeList.Add(enemiesThree);
            /*Boss*/
            BossShip = Content.Load<Texture2D>("BossMonsterRnd");
            bossEnemy = new EnemeyBoss(enemyPositionBoss, _graphics, BossShip, 100);
            enemyPosition = enemiesOne.Position;
            enemyPositiontwo = enemiesTwo.Position;
            enemyPositionthree = enemiesThree.Position;
            enemyPositionBoss = bossEnemy.Position;
            bossMusic = Content.Load<Song>("a-hero-of-the-80s_v2_60sec-178277");
            bossMusicIsPlaying = false;
            bossExplosionSound = Content.Load<SoundEffect>("retro-explosion-102364");
            EnemyExplosionSound = Content.Load<SoundEffect>("supernatural-explosion-104295");


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

            //Highscore
            highscoreBackground = Content.Load<Texture2D>("Background_2");
            highscoreBoard = Content.Load<Texture2D>("HiscoreBoard");
            highscoreTitle = new Texture2D[10];
            for(int i = 1;i <= 10; i++)
            {
                highscoreTitle[i - 1] = Content.Load<Texture2D>("HiScoreTitle" + i.ToString());
            }
            backButtonTexture = Content.Load<Texture2D>("BackButton");
            backButtonBounds = new Rectangle(0,0/*(_graphics.PreferredBackBufferHeight-backButtonTexture.Height)*/,backButtonTexture.Width,backButtonTexture.Height);
            saveButtonBounds = new Rectangle(240,500/*(_graphics.PreferredBackBufferHeight-backButtonTexture.Height)*/, backButtonTexture.Width, backButtonTexture.Height);
            highscoreMenu = new HighscoreScreen(backButtonTexture, backButtonBounds);
            highscores = new List<string>();
            topTen = "";
            victory = false;
            placing = 1;
            rowCounter = 180;

            //GameOver
            gameOverImg = Content.Load<Texture2D>("GameOverTitle");
            endSong = Content.Load<Song>("lady-of-the-80x27s-128379");
            endSongIsPlaying = false;
            saveButton = new Button(backButtonTexture, saveButtonBounds);
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

                    explosionTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (explosionTimer >= 0.02f) // Justera tiden för att styra hastigheten på explosionen
                    {
                        explosionTimer = 0f;
                        explosionCounter++;
                        if (explosionCounter >= 7)
                        {
                            explosionCounter = 0;
                            currentExplosion++;
                            if (currentExplosion >= explosion.Length)
                            {
                                currentExplosion = 0;
                                // Återställ fienden när explosionen har loopat en gång
                                if (enemiesOne.EnemyHealth <= 0)
                                {
                                    enemiesOne.ResetPosition(_graphics);
                                    currentExplosion = 0;
                                }
                                if (enemiesTwo.EnemyHealth <= 0)
                                {
                                    enemiesTwo.ResetPosition(_graphics);
                                    currentExplosion = 0; // Återställ explosionens räknare när fienden återställs
                                }
                                if (enemiesThree.EnemyHealth <= 0)
                                {
                                    enemiesThree.ResetPosition(_graphics);
                                    currentExplosion = 0; // Återställ explosionens räknare när fienden återställs
                                }

                                if (bossEnemy.EnemyHealth <= 0)
                                {
                                    currentExplosion = 0;
                                }
                            }
                        }
                    }

                    base.Update(gameTime);
                    Bullet.UpdatePlayerBullets(gameTime, player, shoot);
                    Bullet.UpdateEnemyBullets(gameTime, enemiesOne, enemiesThree, enemiesTwo, bossEnemy);
                    player.PlayerMovement(player, _graphics);
                    player.HandlePowerupCollision(powerups, pickUp);
                    powerup.SpawnPowerup(random, _graphics, powerupWidth, jetpack, shield, repair, doublePoints, triplePoints, powerups);
                    powerup.UpdatePowerups(gameTime, powerups, _graphics);
                    //enemiesOne.MoveToRandomPosition(_graphics);
                    if (drawEnemy)
                    {
                        foreach (EnemyTypOne enemy in enemyTypOnesList)
                        {
                            enemy.MoveToRandomPosition(_graphics);
                            enemy.DamageToTheEnemy(_graphics, player, _spriteBatch);
                            if (!player.IsShieldActive)
                            {
                                enemy.MakeDamageToPlayer(gameTime, player);
                                enemy.EnemyBulletCollision(player);
                            }
                        }
                        foreach (EnemyTypeTwo enemy in enemyTypTwoList)
                        {
                            enemy.MoveToRandomPosition(_graphics);
                            enemy.DamageToTheEnemy(_graphics, player, _spriteBatch);
                            if (!player.IsShieldActive)
                            {
                                enemy.MakeDamageToPlayer(gameTime, player);
                                enemy.EnemyBulletCollision(player);
                            }
                        }
                        foreach (EnemyTypeThree enemy in enemyTypThreeList)
                        {
                            enemy.MoveToRandomPosition(_graphics);
                            enemy.DamageToTheEnemy(_graphics, player, _spriteBatch);
                            if (!player.IsShieldActive)
                            {
                                enemy.MakeDamageToPlayer(gameTime, player);
                                enemy.EnemyBulletCollision(player);
                            }
                        }
                        enemiesTwo.MoveToRandomPosition(_graphics);
                        enemiesThree.MoveToRandomPosition(_graphics);
                        if (!drawEnemy)
                        {
                            bossEnemy.MoveToRandomPosition(_graphics);
                        }
                        
                    }
                    else
                    {
                        if (!bossMusicIsPlaying)
                        {
                            MediaPlayer.Play(bossMusic);
                            Bullet.EnemyBullets.Clear();
                            enemyTypOnesList.Clear();
                            enemyTypTwoList.Clear();    
                            enemyTypThreeList.Clear();
                            bossMusicIsPlaying = true;
                        }
                        bossEnemy.MoveToRandomPosition(_graphics);
                        bossEnemy.DamageToTheEnemy(_graphics, player, _spriteBatch, bossExplosionSound);
                        if (!player.IsShieldActive)
                        {
                            bossEnemy.BossBulletCollision(player);
                        }
                    }
                    if(enemiesOne.EnemyHealth == 0 ||  enemiesTwo.EnemyHealth == 0 || enemiesThree.EnemyHealth == 0)
                    {
                        EnemyExplosionSound.Play();
                    }

                    break;
                case GameStates.Highscore:
                    //kod för highscore
                    highscoreTitleCounter++;
                    if (highscoreTitleCounter == 6)
                    {
                        highscoreTitleCounter = 0;
                        currentHighscoreTitle++;
                        if (currentHighscoreTitle > highscoreTitle.Length-1)
                        {
                            currentHighscoreTitle = 0;
                        }
                    }
                    highscoreMenu.ClickButton(victory);

                    if (!victory)
                    {
                        highscores = HighscoreScreen.ReadJson();
                        highscores.Sort((x, y) => HighscoreScreen.ExtractScore(y).CompareTo(HighscoreScreen.ExtractScore(x)));

                        foreach (var score in highscores.Take(15))
                        {
                            topTen += $"{placing}: {score}";
                            placing++;
                        }
                        victory = true;
                    }
                    break;

                case GameStates.GameOver:
                    if (!endSongIsPlaying)
                    {
                        MediaPlayer.Play(endSong);
                        endSongIsPlaying = true;
                    }
                    keyboardState = Keyboard.GetState();
                    keys = keyboardState.GetPressedKeys();
                    userInput = HighscoreScreen.EnterPlayerName(userInput, keys, keyboardState);
                    playerHighscore.Save(player, userInput, highscores, saveButton);

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

                    Background.DrawBackground(bgrCounter,_spriteBatch, stageOneBgr);
                    _spriteBatch.Draw(bossEye[currentBossEye], new Vector2(0,130), Color.White);
                    _spriteBatch.Draw(menuForeground, Vector2.Zero, Color.White);
                    _spriteBatch.Draw(menuTitle[currentMenuTitle], new Vector2((_graphics.PreferredBackBufferWidth - menuTitle[currentMenuTitle].Width)/2,70), Color.White);

                    menu.Draw(_spriteBatch);
                    bgrCounter++;

                    _spriteBatch.End();
                    break;
                case GameStates.Play:
                    //kod för Play
                    _spriteBatch.Begin();

                    Background.DrawBackground(bgrCounter, _spriteBatch, stageOneBgr);
                    player.DrawPlayer(_spriteBatch, playerShip, playerShipAcc, player, bgrCounter, playerShield);
                    DrawPowerups(_spriteBatch, powerups);
                    
                    if (drawEnemy)
                    {
                        enemiesOne.DrawEnemy(_spriteBatch);
                        enemiesTwo.DrawEnemy(_spriteBatch);
                        enemiesThree.DrawEnemy(_spriteBatch);
                        Bullet.DrawEnemyBullets(_spriteBatch);
                        if (drawExplosionOne)
                        {
                            if (enemiesOne.EnemyHealth <= 0)
                            {
                                _spriteBatch.Draw(explosion[currentExplosion], new Vector2(enemiesOne.Position.X - 16, enemiesOne.Position.Y - 10), Color.White);
                            }
                        }
                        if (drawExplosionTwo)
                        {
                            if (enemiesTwo.EnemyHealth <= 0)
                            {
                                _spriteBatch.Draw(explosion[currentExplosion], new Vector2(enemiesTwo.Position.X - 8, enemiesTwo.Position.Y - 10), Color.White);
                            }
                        }
                        if (drawExplosionThree)
                        {
                            if (enemiesThree.EnemyHealth <= 0)
                            {
                                _spriteBatch.Draw(explosion[currentExplosion], new Vector2(enemiesThree.Position.X - 8, enemiesThree.Position.Y - 10), Color.White);

                            }
                        }
                    }
                    else
                    {
                        _spriteBatch.Draw(BossShip, new Vector2(bossEnemy.Position.X-60, bossEnemy.Position.Y), Color.White);
                        Bullet.DrawBossBullets(_spriteBatch);
                        if (drawExplosionBoss)
                        {
                            if (bossEnemy.EnemyHealth <= 0)
                            {
                                _spriteBatch.Draw(bossExplosion[currentExplosion], new Vector2(bossEnemy.Position.X-56, bossEnemy.Position.Y), Color.White);
                            }
                        }
                    }
                    
                    Bullet.DrawPlayerBullets(_spriteBatch);
                    
                    Fonts.DrawText(_spriteBatch, "SCORE: " + player.points.GetCurrentPoints(), new Vector2(10, 10), Color.White);
                    player.DrawPlayerHealth(player, healthBar, healthPoint, healthEmpty, _spriteBatch, powerUpBar, jetpack, shield, doublePoints, triplePoints);
                    _spriteBatch.End();
                    bgrCounter++;
                    break;
                case GameStates.Highscore:
                    //kod för highscore
                    _spriteBatch.Begin();
                    Background.DrawBackground(bgrCounter, _spriteBatch, stageOneBgr);
                    //_spriteBatch.Draw(highscoreBackground, Vector2.Zero, Color.White);
                    _spriteBatch.Draw(highscoreBoard, new Vector2((_graphics.PreferredBackBufferWidth - highscoreBoard.Width)/2,140), Color.White);
                    _spriteBatch.Draw(highscoreTitle[currentHighscoreTitle], new Vector2((_graphics.PreferredBackBufferWidth - highscoreTitle[currentHighscoreTitle].Width)/2,0), Color.White);
                    Fonts.DrawText(_spriteBatch, topTen, new Vector2(160, 180), Color.Purple);
                    highscoreMenu.DrawBackbutton(_spriteBatch);

                    _spriteBatch.End();
                    bgrCounter++;
                    break;
                case GameStates.GameOver:
                    _spriteBatch.Begin();

                    Background.DrawBackground(bgrCounter, _spriteBatch, stageOneBgr);
                    _spriteBatch.Draw(gameOverImg, new Vector2(100, 100), Color.White);
                    Fonts.DrawText(_spriteBatch, "FINAL SCORE: \n" + player.points.GetCurrentPoints(), new Vector2(175, 300), Color.White);
                    Fonts.DrawText(_spriteBatch, "ENTER NAME: \n", new Vector2(175, 430), Color.White);
                    Fonts.DrawText(_spriteBatch, userInput, new Vector2(175, 460), Color.White);
                    _spriteBatch.Draw(backButtonTexture, saveButtonBounds, Color.White);    
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

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
        private GameStates currentState;

        // Play
        private Player player;
        private Texture2D playerShip;
        private Texture2D playerShipAcc;
        private Texture2D stageOneBgr;
        private Vector2 playerPosition;
        private int bgrCounter;

        // Powerups
        private Texture2D jetpack;
        private Texture2D shield;
        private Texture2D repair;
        private Texture2D doublePoints;
        private Texture2D triplePoints;

        private List<Powerup> powerups;

        private int powerupWidth;
        private int powerupHeight;

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
            player = new Player(playerPosition, _graphics);
            playerPosition = player.Position;

            jetpack = Content.Load<Texture2D>("JetpackShip");
            shield = Content.Load<Texture2D>("ShieldShip");
            repair = Content.Load<Texture2D>("RepairShip");
            doublePoints = Content.Load<Texture2D>("2xPoints");
            triplePoints = Content.Load<Texture2D>("3xPoints");

            powerupWidth = 15;
            powerupHeight = 15;
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
                    player.HandlePowerupCollision(powerups);
                    SpawnPowerup();
                    UpdatePowerups(gameTime);
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
                    //Kod för meny

                    break;
                case GameStates.Play:
                    //kod för Play
                    _spriteBatch.Begin();

                    player.DrawGame(_spriteBatch, playerShip, playerShipAcc, stageOneBgr, player, bgrCounter);

                    foreach (Powerup powerup in powerups)
                    {
                        _spriteBatch.Draw(powerup.Texture, powerup.Position, Color.White);
                    }
                    _spriteBatch.End();

                    break;
                case GameStates.Highscore:
                    //kod för highscore

                    break;
            }

            base.Draw(gameTime);
            bgrCounter++;
            if (bgrCounter == 720)
            {
                bgrCounter = 0;
            }
        }
        private void SpawnPowerup()
        {
            // Slumpmässigt beslut om att skapa en ny powerup
            if (random.Next(1000) < 5) // Justera tröskelvärdet för att ändra frekvensen av powerup-generering
            {
                int maxX = _graphics.PreferredBackBufferWidth - powerupWidth; // Maximalt x-värde för slumpmässig position

                // Generera en X-koordinat inom spelplanen
                int randomX = random.Next(maxX);

                // Generera en Y-koordinat inom den synliga delen av spelplanen
                int randomY = random.Next(-_graphics.PreferredBackBufferHeight, 0);

                Vector2 powerupPosition = new Vector2(randomX, randomY);

                PowerupType powerupType = (PowerupType)random.Next(Enum.GetNames(typeof(PowerupType)).Length);

                Texture2D powerupTexture;
                switch (powerupType)
                {
                    case PowerupType.Jetpack:
                        powerupTexture = jetpack;
                        break;
                    case PowerupType.Shield:
                        powerupTexture = shield;
                        break;
                    case PowerupType.Repair:
                        powerupTexture = repair;
                        break;
                    case PowerupType.DoublePoints:
                        powerupTexture = doublePoints;
                        break;
                    case PowerupType.TriplePoints:
                        powerupTexture = triplePoints;
                        break;
                    default:
                        powerupTexture = null;
                        break;
                }

                // Skapa och lägg till powerupen i listan
                Powerup newPowerup = new Powerup(powerupPosition, powerupType, 4, powerupTexture); // Du behöver ange en varaktighet för powerupen (0 för nu)
                powerups.Add(newPowerup);
            }
        }




        private void UpdatePowerups(GameTime gameTime)
        {
            // Uppdatera varje powerup
            for (int i = powerups.Count - 1; i >= 0; i--)
            {
                powerups[i].Update(gameTime);

                // Flytta powerupen neråt
                powerups[i].Position += new Vector2(0, 3); // Fallhastighet för Powerup

                // Kontrollera om powerupen har nått botten av spelplanen
                if (powerups[i].Position.Y > _graphics.PreferredBackBufferHeight)
                {
                    powerups.RemoveAt(i); // Ta bort powerupen om den når botten av spelplanen
                }
            }
        }


    }
}

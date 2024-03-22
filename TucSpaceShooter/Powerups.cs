using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace TucSpaceShooter
{
    public class Powerup : Entity
    {
        public enum PowerupType
        {
            Jetpack,
            Shield,
            Repair,
            DoublePoints,
            TriplePoints
        }

        public PowerupType Type { get; private set; }
        public bool IsActive { get; private set; }
        public float Duration { get; private set; }
        public float ElapsedTime { get; private set; }
        public Texture2D Texture { get; private set; }

        public Powerup(Vector2 position, PowerupType type, float duration, Texture2D texture) : base(position)
        {
            Type = type;
            IsActive = false;
            Duration = duration;
            ElapsedTime = 0;
            Texture = texture;
        }

        public Powerup(Vector2 position) : base(position)
        {
        }

        public void Activate()
        {
            IsActive = true;
            ElapsedTime = 0;
        }

        public void Deactivate()
        {
            IsActive = false;
            ElapsedTime = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                ElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (ElapsedTime >= Duration)
                {
                    Deactivate();
                }
            }
        }

        public void ApplyPowerup(Player player)
        {
            switch (Type)
            {
                case PowerupType.Jetpack:
                    player.ActivateJetpack();
                    break;
                case PowerupType.Shield:
                    player.ActivateShield();
                    break;
                case PowerupType.Repair:
                    player.ActivateRepair();
                    break;
                case PowerupType.DoublePoints:
                    player.ActivateDoublePoints();
                    break;
                case PowerupType.TriplePoints:
                    player.ActivateTriplePoints();
                    break;
                default:
                    break;
            }
            Deactivate();
        }

        public static void DrawPowerups(SpriteBatch spriteBatch, List<Powerup> powerups)
        {
            foreach (Powerup powerup in powerups)
            {
                powerup.Draw(spriteBatch);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
        public void UpdatePowerups(GameTime gameTime, List<Powerup> powerups, GraphicsDeviceManager _graphics)
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
        public void SpawnPowerup(Random random, GraphicsDeviceManager _graphics, int powerupWidth, Texture2D jetpack, Texture2D shield, Texture2D repair, Texture2D doublePoints, Texture2D triplePoints, List<Powerup> powerups)
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
    }
}

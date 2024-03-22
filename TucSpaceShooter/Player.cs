using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Mime;
using System.Numerics;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace TucSpaceShooter
{
    public class Player : Creature
    {

        private int health;
        private int speed = 2;

        public int Health { get => health; set => health = value; }

        public Player(Vector2 position, GraphicsDeviceManager graphics, int health) : base(position)
        {
            this.position.X = graphics.PreferredBackBufferWidth / 2 - 30;
            this.position.Y = graphics.PreferredBackBufferHeight - 110;
            this.health = health;
        }

        public void ActivateJetpack()
        {
            // Logik för Jetpack Powerup
        }

        public void ActivateShield()
        {
            // Logik för Shield Powerup
        }

        public void ActivateRepair()
        {
            // Logik för Repair Powerup.
        }

        public void ActivateDoublePoints()
        {
            // Logik för Double Points Powerup.
        }

        public void ActivateTriplePoints()
        {
            // Logik för Triple Points Powerup. 
        }

        public void HandlePowerupCollision(List<Powerup> powerups, SoundEffect pickUp)
        {
            foreach (Powerup powerup in powerups)
            {
                if (Intersects(powerup))
                {
                    pickUp.Play();
                    powerup.ApplyPowerup(this);
                    powerups.Remove(powerup);
                    break;
                }
            }
        }

        public bool Intersects(Powerup powerup)
        {
            int playerHeight = 38;
            int playerWidth = 40;
            int powerupHeight = 16;
            int powerupWidth = 16;

            Rectangle playerBounds = new Rectangle((int)Position.X, (int)Position.Y, playerWidth, playerHeight); 
            Rectangle powerupBounds = new Rectangle((int)powerup.Position.X, (int)powerup.Position.Y, powerupWidth, powerupHeight); 

            return playerBounds.Intersects(powerupBounds);
        }

        // Ritar upp spelar-sprite
        public void DrawPlayer(SpriteBatch spriteBatch, Texture2D pShip, Texture2D pShipFire, Player player, int counter)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Down)
                || Keyboard.GetState().IsKeyDown(Keys.Up)
                || Keyboard.GetState().IsKeyDown(Keys.Left)
                || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                // bakgrunds-countern utnyttjas för att får "blinkande" eld när spelarskeppet rör på sig.
                if (counter % 3 == 0)
                {
                    spriteBatch.Draw(pShipFire, new Vector2(player.Position.X - 20, player.Position.Y - 19), Color.White);
                }
            }
            spriteBatch.Draw(pShip, new Vector2(player.Position.X - 20, player.Position.Y - 19), Color.White);
            if (counter == 40 || counter == 150)
            {
                player.health--;
            }
        }

        // Ritar ut spelarens hälsa i fönstret. 
        public void DrawPlayerHealth(Player player, Texture2D healthBar, Texture2D healthPoint, Texture2D healthEmpty, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(healthBar, new Vector2(-2, 670), Color.White);
            if (player.health == 5)
            {
                spriteBatch.Draw(healthPoint, new Vector2(5, 674), Color.White);
                spriteBatch.Draw(healthPoint, new Vector2(36, 674), Color.White);
                spriteBatch.Draw(healthPoint, new Vector2(67, 674), Color.White);
                spriteBatch.Draw(healthPoint, new Vector2(98, 674), Color.White);
                spriteBatch.Draw(healthPoint, new Vector2(129, 674), Color.White);
            }
            else if (player.health == 4)
            {
                spriteBatch.Draw(healthPoint, new Vector2(5, 674), Color.White);
                spriteBatch.Draw(healthPoint, new Vector2(36, 674), Color.White);
                spriteBatch.Draw(healthPoint, new Vector2(67, 674), Color.White);
                spriteBatch.Draw(healthPoint, new Vector2(98, 674), Color.White);
                spriteBatch.Draw(healthEmpty, new Vector2(129, 674), Color.White);
            }
            else if (player.health == 3)
            {
                spriteBatch.Draw(healthPoint, new Vector2(5, 674), Color.White);
                spriteBatch.Draw(healthPoint, new Vector2(36, 674), Color.White);
                spriteBatch.Draw(healthPoint, new Vector2(67, 674), Color.White);
                spriteBatch.Draw(healthEmpty, new Vector2(98, 674), Color.White);
                spriteBatch.Draw(healthEmpty, new Vector2(129, 674), Color.White);
            }
            else if (player.health == 2)
            {
                spriteBatch.Draw(healthPoint, new Vector2(5, 674), Color.White);
                spriteBatch.Draw(healthPoint, new Vector2(36, 674), Color.White);
                spriteBatch.Draw(healthEmpty, new Vector2(67, 674), Color.White);
                spriteBatch.Draw(healthEmpty, new Vector2(98, 674), Color.White);
                spriteBatch.Draw(healthEmpty, new Vector2(129, 674), Color.White);
            }
            else if (player.health == 1)
            {
                spriteBatch.Draw(healthPoint, new Vector2(5, 674), Color.White);
                spriteBatch.Draw(healthEmpty, new Vector2(36, 674), Color.White);
                spriteBatch.Draw(healthEmpty, new Vector2(67, 674), Color.White);
                spriteBatch.Draw(healthEmpty, new Vector2(98, 674), Color.White);
                spriteBatch.Draw(healthEmpty, new Vector2(129, 674), Color.White);
            }
            else if (player.health == 0)
            {
                spriteBatch.Draw(healthEmpty, new Vector2(5, 674), Color.White);
                spriteBatch.Draw(healthEmpty, new Vector2(36, 674), Color.White);
                spriteBatch.Draw(healthEmpty, new Vector2(67, 674), Color.White);
                spriteBatch.Draw(healthEmpty, new Vector2(98, 674), Color.White);
                spriteBatch.Draw(healthEmpty, new Vector2(129, 674), Color.White);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        public void MoveUp(Player player)
        {
            if (player.position.Y != 20)
            {
                player.position.Y -= speed;
            }
        }
        public void MoveDown(Player player, GraphicsDeviceManager graphics)
        {
            if(player.position.Y != graphics.PreferredBackBufferHeight - 100)
            player.position.Y += speed;
        }
        public void MoveLeft(Player player)
        {
            if (player.position.X != 10)
            {
                player.position.X -= speed;
            }
        }
        public void MoveRight(Player player, GraphicsDeviceManager graphics)
        {
            if (player.position.X != graphics.PreferredBackBufferWidth - 30)
            {
                player.position.X += speed;
            }
        }
        public void PlayerMovement(Player player, GraphicsDeviceManager graphics)
        {
            
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                player.MoveUp(player);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                player.MoveDown(player, graphics);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                player.MoveLeft(player);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                player.MoveRight(player, graphics);
            }
        }
    }
}

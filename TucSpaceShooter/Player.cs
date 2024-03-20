using Microsoft.Xna.Framework;
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
            this.position.Y = graphics.PreferredBackBufferHeight - 70;
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

        public void HandlePowerupCollision(List<Powerup> powerups)
        {
            foreach (Powerup powerup in powerups)
            {
                if (Intersects(powerup))
                {
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

        public void DrawPowerups(SpriteBatch spriteBatch, List<Powerup> powerups)
        {
            foreach (Powerup powerup in powerups)
            {
                powerup.Draw(spriteBatch);
            }
        }


        public void DrawGame(SpriteBatch spriteBatch, Texture2D pShip, Texture2D pShipFire, Texture2D bgr, Player player, int counter)
        {
            
            if (counter == 0)
            {
                spriteBatch.Draw(bgr, new Vector2(0, (-720 + counter/3)), Color.White);
            }
            spriteBatch.Draw(bgr, new Vector2(0, counter/3), Color.White);
            spriteBatch.Draw(bgr, new Vector2(0, (-720 + counter/3)), Color.White);
            if (Keyboard.GetState().IsKeyDown(Keys.Down)
                || Keyboard.GetState().IsKeyDown(Keys.Up)
                || Keyboard.GetState().IsKeyDown(Keys.Left)
                || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (counter % 3 == 0)
                {
                    spriteBatch.Draw(pShipFire, new Vector2(player.Position.X - 20, player.Position.Y - 19), Color.White);
                }
            }
            spriteBatch.Draw(pShip, new Vector2(player.Position.X - 20, player.Position.Y - 19), Color.White);
        }
        public void PlayerHealth(Player player, Texture2D healthBar, Texture2D healthPoint, Texture2D healthEmpty, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(healthBar, new Vector2(10, 10), Color.White);
            if (player.health == 5)
            {
                spriteBatch.Draw(healthPoint, new Vector2(14, 12), Color.White);
                spriteBatch.Draw(healthPoint, new Vector2(31, 12), Color.White);
                spriteBatch.Draw(healthPoint, new Vector2(48, 12), Color.White);
                spriteBatch.Draw(healthPoint, new Vector2(65, 12), Color.White);
                spriteBatch.Draw(healthPoint, new Vector2(82, 12), Color.White);
            }
            else if (player.health == 4)
            {
                spriteBatch.Draw(healthPoint, new Vector2(14, 12), Color.White);
                spriteBatch.Draw(healthPoint, new Vector2(31, 12), Color.White);
                spriteBatch.Draw(healthPoint, new Vector2(48, 12), Color.White);
                spriteBatch.Draw(healthPoint, new Vector2(65, 12), Color.White);
                spriteBatch.Draw(healthEmpty, new Vector2(82, 12), Color.White);
            }
            else if (player.health == 3)
            {
                spriteBatch.Draw(healthPoint, new Vector2(14, 12), Color.White);
                spriteBatch.Draw(healthPoint, new Vector2(31, 12), Color.White);
                spriteBatch.Draw(healthPoint, new Vector2(48, 12), Color.White);
                spriteBatch.Draw(healthEmpty, new Vector2(65, 12), Color.White);
                spriteBatch.Draw(healthEmpty, new Vector2(82, 12), Color.White);
            }
            else if (player.health == 2)
            {
                spriteBatch.Draw(healthPoint, new Vector2(14, 12), Color.White);
                spriteBatch.Draw(healthPoint, new Vector2(31, 12), Color.White);
                spriteBatch.Draw(healthEmpty, new Vector2(48, 12), Color.White);
                spriteBatch.Draw(healthEmpty, new Vector2(65, 12), Color.White);
                spriteBatch.Draw(healthEmpty, new Vector2(82, 12), Color.White);
            }
            else if (player.health == 1)
            {
                spriteBatch.Draw(healthPoint, new Vector2(14, 12), Color.White);
                spriteBatch.Draw(healthEmpty, new Vector2(31, 12), Color.White);
                spriteBatch.Draw(healthEmpty, new Vector2(48, 12), Color.White);
                spriteBatch.Draw(healthEmpty, new Vector2(65, 12), Color.White);
                spriteBatch.Draw(healthEmpty, new Vector2(82, 12), Color.White);
            }
            else if (player.health == 0)
            {
                spriteBatch.Draw(healthEmpty, new Vector2(14, 12), Color.White);
                spriteBatch.Draw(healthEmpty, new Vector2(31, 12), Color.White);
                spriteBatch.Draw(healthEmpty, new Vector2(48, 12), Color.White);
                spriteBatch.Draw(healthEmpty, new Vector2(65, 12), Color.White);
                spriteBatch.Draw(healthEmpty, new Vector2(82, 12), Color.White);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        public void MoveUp(Player player, GraphicsDeviceManager graphics)
        {
            if (player.position.Y != 20)
            {
                player.position.Y -= speed;
            }
        }
        public void MoveDown(Player player, GraphicsDeviceManager graphics)
        {
            if(player.position.Y != graphics.PreferredBackBufferHeight - 30)
            player.position.Y += speed;
        }
        public void MoveLeft(Player player, GraphicsDeviceManager graphics)
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
                player.MoveUp(player, graphics);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                player.MoveDown(player, graphics);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                player.MoveLeft(player, graphics);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                player.MoveRight(player, graphics);
            }
        }
    }
}

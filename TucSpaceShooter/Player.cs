using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Mime;
using System.Numerics;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace TucSpaceShooter
{
    public class Player : Creature
    {
        public Player(Vector2 position, GraphicsDeviceManager graphics) : base(position)
        {
            this.position.X = graphics.PreferredBackBufferWidth/2 - 30;
            this.position.Y = graphics.PreferredBackBufferHeight-70;
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
                spriteBatch.Draw(bgr, new Vector2(0, (-720 + counter)), Color.White);
            }
            spriteBatch.Draw(bgr, new Vector2(0, counter), Color.White);
            spriteBatch.Draw(bgr, new Vector2(0, (-720 + counter)), Color.White);
            if (Keyboard.GetState().IsKeyDown(Keys.Down)
                || Keyboard.GetState().IsKeyDown(Keys.Up)
                || Keyboard.GetState().IsKeyDown(Keys.Left)
                || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (counter % 3 == 0)
                {
                    spriteBatch.Draw(pShipFire, player.Position, Color.White);
                }
            }
            spriteBatch.Draw(pShip, player.Position, Color.White);
        }
        public void MoveUp(Player player, GraphicsDeviceManager graphics)
        {
            if (player.position.Y != 10)
            {
                player.position.Y -= 2;
            }
        }
        public void MoveDown(Player player, GraphicsDeviceManager graphics)
        {
            if(player.position.Y != graphics.PreferredBackBufferHeight - 50)
            player.position.Y += 2;
        }
        public void MoveLeft(Player player, GraphicsDeviceManager graphics)
        {
            if (player.position.X != -10)
            {
                player.position.X -= 2;
            }
        }
        public void MoveRight(Player player, GraphicsDeviceManager graphics)
        {
            if (player.position.X != graphics.PreferredBackBufferWidth - 50)
            {
                player.position.X += 2;
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

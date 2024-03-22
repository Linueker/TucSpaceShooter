using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace TucSpaceShooter
{
    public class Bullet
    {
        public static List<Bullet> Bullets = new List<Bullet>();
        private static Texture2D BulletTexture;
        private static TimeSpan BulletCooldown = TimeSpan.FromMilliseconds(200);
        private static TimeSpan LastBulletTime = TimeSpan.Zero;

        private Vector2 position;
        private bool isActive;
        private const float Speed = 10; // Speed at which the bullet moves

        // Constructor for individual bullets
        private Bullet(Vector2 position)
        {
            this.position = position;
            this.isActive = true;
        }

        // Call this method from Game1.cs to update the state of all bullets
        public static void UpdateAll(GameTime gameTime, Player player, SoundEffect shoot)
        {
            
            if (gameTime.TotalGameTime - LastBulletTime > BulletCooldown && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                shoot.Play();
                
                Shoot(new Vector2(player.Position.X + 26, player.Position.Y));
                Shoot(new Vector2(player.Position.X + -4, player.Position.Y));
                LastBulletTime = gameTime.TotalGameTime;
            }

            // Update each bullet's position
            for (int i = Bullets.Count - 1; i >= 0; i--)
            {
                Bullets[i].Update();
                if (!Bullets[i].isActive)
                {
                    Bullets.RemoveAt(i);
                }
            }
        }

        // Updates the position of the bullet
        private void Update()
        {
            position.Y -= Speed;
            if (position.Y < 0) isActive = false;
        }

        // Call this method from Game1.cs to draw all bullets
        public static void DrawAll(SpriteBatch spriteBatch)
        {
            foreach (var bullet in Bullets)
            {
                if (bullet.isActive)
                {
                    spriteBatch.Draw(BulletTexture, bullet.position, Color.White);
                }
            }
        }

        // Handles creation of bullets
        private static void Shoot(Vector2 position)
        {
            Bullets.Add(new Bullet(position));
        }

        // Initializes bullet resources; call this in the LoadContent method of Game1.cs
        public static void LoadContent(Texture2D texture)
        {
            BulletTexture = texture;
        }
    }
}

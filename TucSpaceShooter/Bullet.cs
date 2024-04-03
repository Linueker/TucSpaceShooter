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
        public static List<Bullet> EnemyBullets = new List<Bullet>(); 
        public static List<Bullet> BossBullets = new List<Bullet>();
        private static Texture2D BulletTexture;
        private static TimeSpan BulletCooldown = TimeSpan.FromMilliseconds(200);
        private static TimeSpan EnemyBulletCooldown = TimeSpan.FromMilliseconds(1500);
        private static TimeSpan LastBulletTime = TimeSpan.Zero;
        private static TimeSpan LastEnemyBulletTime = TimeSpan.Zero;

        private Vector2 position;
        public bool isActive;
        private const float Speed = 10; // Speed at which the bullet moves
        private const float EnemyBulletSpeed = 2;

        public Vector2 Position { get => position; set => position = value; }

        // Constructor for individual bullets
        public Bullet(Vector2 position)
        {
            this.position = position;
            this.isActive = true;
        }

        // Call this method from Game1.cs to update the state of all bullets
        public static void UpdatePlayerBullets(GameTime gameTime, Player player, SoundEffect shoot)
        {
            
            if (gameTime.TotalGameTime - LastBulletTime > BulletCooldown && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                shoot.Play();
                
                Shoot(new Vector2(player.Position.X + 16, player.Position.Y));
                Shoot(new Vector2(player.Position.X + -14, player.Position.Y));
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
        public static void UpdateEnemyBullets(GameTime gameTime, EnemyTypOne enemyTypOne, EnemyTypeThree enemyTypeThree, EnemeyBoss boss)
        {

            if (gameTime.TotalGameTime - LastEnemyBulletTime > EnemyBulletCooldown)
            {
                EnemyShoot(new Vector2(enemyTypOne.Position.X + 14, enemyTypOne.Position.Y + 30));
                EnemyShoot(new Vector2(enemyTypeThree.Position.X + 25, enemyTypeThree.Position.Y + 50));

                BossShoot(new Vector2(boss.Position.X - 50, boss.Position.Y + 120));
                BossShoot(new Vector2(boss.Position.X - 35, boss.Position.Y + 120));
                BossShoot(new Vector2(boss.Position.X - 20, boss.Position.Y + 120));
                BossShoot(new Vector2(boss.Position.X + 20, boss.Position.Y + 120));
                BossShoot(new Vector2(boss.Position.X + 35, boss.Position.Y + 120));
                BossShoot(new Vector2(boss.Position.X + 50, boss.Position.Y + 120));
                LastEnemyBulletTime = gameTime.TotalGameTime;
            }

            // Update each bullet's position
            for (int i = EnemyBullets.Count - 1; i >= 0; i--)
            {
                EnemyBullets[i].UpdateEnemyBullet();
                if (!EnemyBullets[i].isActive)
                {
                    EnemyBullets.RemoveAt(i);
                }
            }
            for (int i = BossBullets.Count - 1; i >= 0; i--)
            {
                BossBullets[i].UpdateEnemyBullet();
                if (!BossBullets[i].isActive)
                {
                    BossBullets.RemoveAt(i);
                }
            }
        }

        // Updates the position of the bullet
        private void Update()
        {
            position.Y -= Speed;
            if (position.Y < 0) isActive = false;
        }
        public void UpdateEnemyBullet()
        {
            position.Y += EnemyBulletSpeed;
            if (position.Y < 0) isActive = false;
        }

        // Call this method from Game1.cs to draw all bullets
        public static void DrawPlayerBullets(SpriteBatch spriteBatch)
        {
            foreach (var bullet in Bullets)
            {
                if (bullet.isActive)
                {
                    spriteBatch.Draw(BulletTexture, bullet.position, Color.White);
                }
            }
            
        }
        public static void DrawEnemyBullets(SpriteBatch spriteBatch)
        {
            foreach (var bullet in EnemyBullets)
            {
                if (bullet.isActive)
                {
                    spriteBatch.Draw(BulletTexture, bullet.position, Color.White);
                }
            }
            foreach (var bullet in BossBullets)
            {
                if (bullet.isActive)
                {
                    spriteBatch.Draw(BulletTexture, bullet.position, Color.White);
                }
            }
        }
        public static void DrawBossBullets(SpriteBatch spriteBatch)
        {
            foreach (var bullet in BossBullets)
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
        private static void EnemyShoot(Vector2 position)
        {
            EnemyBullets.Add(new Bullet(position));
        }
        private static void BossShoot(Vector2 position)
        {
            BossBullets.Add(new Bullet(position));
        }

        // Initializes bullet resources; call this in the LoadContent method of Game1.cs
        public static void LoadContent(Texture2D texture)
        {
            BulletTexture = texture;
        }
    }
}

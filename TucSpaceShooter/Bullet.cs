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
        public static List<Bullet> BulletsLeft = new List<Bullet>();
        public static List<Bullet> BulletsRight = new List<Bullet>();
        public static List<Bullet> BossBullets = new List<Bullet>();
        private static Texture2D BulletTexture;
        private static Texture2D EnemyBulletTexture;
        private static Texture2D LeftBulletTexture;
        private static Texture2D RightBulletTexture;
        private static TimeSpan BulletCooldown = TimeSpan.FromMilliseconds(200);
        private static TimeSpan EnemyBulletCooldown = TimeSpan.FromMilliseconds(1500);
        private static TimeSpan LastBulletTime = TimeSpan.Zero;
        private static TimeSpan LastEnemyBulletTime = TimeSpan.Zero;

        private Vector2 position;
        public bool isActive;
        private const float Speed = 10; // Speed at which the bullet moves
        private const float EnemyBulletSpeed = 3;

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
        public static void UpdateEnemyBullets(GameTime gameTime, EnemyTypOne enemyTypOne, EnemyTypeThree enemyTypeThree, EnemyTypeTwo enemyTypeTwo, EnemeyBoss boss)
        {

            if (gameTime.TotalGameTime - LastEnemyBulletTime > EnemyBulletCooldown)
            {
                EnemyShoot(new Vector2(enemyTypOne.Position.X + 14, enemyTypOne.Position.Y + 30));

                EnemyShoot(new Vector2(enemyTypeTwo.Position.X + 26, enemyTypeTwo.Position.Y + 30));

                EnemyShoot(new Vector2(enemyTypeThree.Position.X + 25, enemyTypeThree.Position.Y + 50));
                ShootLeft(new Vector2(enemyTypeThree.Position.X, enemyTypeThree.Position.Y + 22));
                ShootRight(new Vector2(enemyTypeThree.Position.X + 50, enemyTypeThree.Position.Y + 22));

                BossShoot(new Vector2(boss.Position.X - 45, boss.Position.Y + 120));
                BossShoot(new Vector2(boss.Position.X - 28, boss.Position.Y + 130));
                BossShoot(new Vector2(boss.Position.X - 11, boss.Position.Y + 120));
                BossShoot(new Vector2(boss.Position.X + 13, boss.Position.Y + 120));
                BossShoot(new Vector2(boss.Position.X + 30, boss.Position.Y + 130));
                BossShoot(new Vector2(boss.Position.X + 48, boss.Position.Y + 120));
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
            for (int i = BulletsLeft.Count - 1; i >= 0; i--)
            {
                BulletsLeft[i].UpdateBulletsLeft();
                if (!BulletsLeft[i].isActive)
                {
                    BulletsLeft.RemoveAt(i);
                }
            }
            for (int i = BulletsRight.Count - 1; i >= 0; i--)
            {
                BulletsRight[i].UpdateBulletsRight();
                if (!BulletsRight[i].isActive)
                {
                    BulletsRight.RemoveAt(i);
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
        public void UpdateBulletsLeft()
        {
            position.X -= EnemyBulletSpeed;
            if (position.X < 0) isActive = false;
        }
        public void UpdateBulletsRight()
        {
            position.X += EnemyBulletSpeed;
            if (position.Y > 540) isActive = false;
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
                    spriteBatch.Draw(EnemyBulletTexture, bullet.position, Color.White);
                }
            }
            
            foreach (var bullet in BulletsLeft)
            {
                if (bullet.isActive)
                {
                    spriteBatch.Draw(LeftBulletTexture, bullet.position, Color.White);
                }
            }
            foreach (var bullet in BulletsRight)
            {
                if (bullet.isActive)
                {
                    spriteBatch.Draw(RightBulletTexture, bullet.position, Color.White);
                }
            }
        }
        public static void DrawBossBullets(SpriteBatch spriteBatch)
        {
            foreach (var bullet in BossBullets)
            {
                if (bullet.isActive)
                {
                    spriteBatch.Draw(EnemyBulletTexture, bullet.position, Color.White);
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
        private static void ShootLeft(Vector2 position)
        {
            BulletsLeft.Add(new Bullet(position));
        }
        private static void ShootRight(Vector2 position)
        {
            BulletsRight.Add(new Bullet(position));
        }
        private static void BossShoot(Vector2 position)
        {
            BossBullets.Add(new Bullet(position));
        }

        // Initializes bullet resources; call this in the LoadContent method of Game1.cs
        public static void LoadContent(Texture2D texture, Texture2D enemyTexture, Texture2D leftBullet, Texture2D rightBullet)
        {
            BulletTexture = texture;
            EnemyBulletTexture = enemyTexture;
            LeftBulletTexture = leftBullet;
            RightBulletTexture = rightBullet;
        }
    }
}

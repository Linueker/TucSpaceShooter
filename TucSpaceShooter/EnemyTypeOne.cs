using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace TucSpaceShooter
{
    public class EnemyTypOne : Enemies
    {
        private int movingSpeed = 1;
        private bool moveRight = true;
        private const int center = 16;
        private bool isDead = false;
        private bool isNotDead = true;
        float timerForDamageThePlayer = 0f;
        float damageDuration = 9f;
        bool damageEnemy = true;
        private static List<EnemyTypOne> enemyTypeOneList = new List<EnemyTypOne>();
        public bool IsNotDead { get { return isDead; } }

        public static List<EnemyTypOne> EnemyTypeOneList { get => enemyTypeOneList; set => enemyTypeOneList = value; }

        public EnemyTypOne(Vector2 position, GraphicsDeviceManager graphics, Texture2D enemyTextureOne, int enemyHealth) :
            base(position, graphics, enemyTextureOne, enemyHealth)
        {
            this.position.X = graphics.PreferredBackBufferWidth / 2 - 30;
            this.position.Y = graphics.PreferredBackBufferHeight;
            enemyTypeOneList.Add(this);
            EnemyHealth = 5;
        }
        public override void MoveToRandomPosition(GraphicsDeviceManager graphics)
        {
            if (isNotDead)
            {
                if (position.Y < graphics.PreferredBackBufferHeight)
                {
                    position.Y += 1;
                }
                else
                {
                    position.Y = 0;
                }
                if (moveRight)
                {
                    position.X += movingSpeed;
                }
                else
                {
                    position.X -= movingSpeed;
                }
                if (position.X <= 20 || position.X >= graphics.PreferredBackBufferWidth - 60)
                {
                    moveRight = !moveRight;
                }
            }
        }
        public override void DrawEnemy(SpriteBatch spriteBatch)
        {
            foreach (EnemyTypOne enemy in enemyTypeOneList)
            {
                if (enemy.EnemyHealth != 0)
                {
                    spriteBatch.Draw(enemy.GetEnemyTexture(), enemy.Position, Color.White);
                    break;
                }

            }
        }
        public void ResetPosition(GraphicsDeviceManager graphics)
        {
            Random random = new Random();
            position.X = random.Next(graphics.PreferredBackBufferWidth - 60);
            position.Y = random.Next(-graphics.PreferredBackBufferHeight, 0);
            EnemyHealth = 5;
        }

        public override void DamageToTheEnemy(GraphicsDeviceManager graphics, Player player, SpriteBatch spriteBatch)
        {
            if (isNotDead)
            {
                foreach (var bullet in Bullet.Bullets)
                {
                    float hitDistance = Vector2.Distance(position, bullet.Position);
                    if (hitDistance <= center)
                    {
                        EnemyHealth--;
                        if (EnemyHealth <= 0)
                        {
                            
                            player.points.AddPoints(player, EnemyType.One);
                            break;
                        }
                        Bullet.Bullets.Remove(bullet);
                        break;
                    }
                }
            }
        }

        public override void MakeDamageToPlayer(GameTime gameTime, Player player)
        {
            if (damageEnemy)
            {
                timerForDamageThePlayer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (timerForDamageThePlayer >= damageDuration)
                {
                    damageEnemy = false;
                }

                float makeDamageToPlayer = Vector2.Distance(position, player.Position);
                float damageRadius = center;
                if (makeDamageToPlayer <= damageRadius)
                {
                    player.Health--;
                    damageEnemy = false;
                }
            }
        }
        public void EnemyBulletCollision(Player player)
        {
            foreach (var bullet in Bullet.EnemyBullets)
            {
                float makeDamageToPlayer = Vector2.Distance(bullet.Position, player.Position);
                float damageRadius = 12;
                if (makeDamageToPlayer <= damageRadius)
                {
                    player.Health--;
                    Bullet.EnemyBullets.Remove(bullet);
                    break;
                }
            }
        }

    }
}
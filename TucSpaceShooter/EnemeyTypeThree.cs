using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TucSpaceShooter
{
    public class EnemyTypeThree : Enemies
    {
        bool moveBack = true;
        int pxelToRight = 1;
        private static List<EnemyTypeThree> enemyTypeThreeList = new List<EnemyTypeThree>();
        private const int center = 24;
        private int movingSpeed = 1;
        private bool moveRight = true;
        public bool isNotDead = true;
        float timerForDamageThePlayer = 0f;
        float damageDuration = 9f;//9
        bool damageEnemy = true;

        public static List<EnemyTypeThree> EnemyTypeThreeList { get => enemyTypeThreeList; set => enemyTypeThreeList = value; }

        public EnemyTypeThree(Vector2 position, GraphicsDeviceManager graphics, Texture2D enemyTextureThree, int enemyHealth) :
            base(position, graphics, enemyTextureThree, enemyHealth)
        {
            this.position.X = graphics.PreferredBackBufferWidth / 2 - 30;
            this.position.Y = graphics.PreferredBackBufferHeight;
            enemyTypeThreeList.Add(this);
            enemyHealth = 8;
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
                if (moveBack)
                {
                    position.Y += movingSpeed;
                }
                else
                {
                    position.Y -= movingSpeed;
                }
                if (position.Y <= 20 || position.Y >= graphics.PreferredBackBufferWidth - 60)
                {
                    moveBack = !moveBack;
                }
                if (moveRight)
                {
                    position.X += movingSpeed;

                }
                else
                {
                    position.X -= movingSpeed;
                    position.Y -= movingSpeed;
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
            foreach (EnemyTypeThree enemy in enemyTypeThreeList)
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
        }
        public override void DamageToTheEnemy(GraphicsDeviceManager graphics, Player player)
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
                            ResetPosition(graphics);
                            EnemyHealth = 8;
                            player.points.AddPoints(player, EnemyType.Three);
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
                float damageRadius = 20;
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

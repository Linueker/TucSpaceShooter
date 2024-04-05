using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace TucSpaceShooter
{
    public class EnemyTypOne : Enemies
    {
        /// <summary>
        /// den klassen är för den första typen av fienderna 
        /// <param name="movingSpeed"> den använder jag för att minska hastigheten och bestämmer åt vilket håll ska fienderna röra sig </param>
        /// <param name="enemyTexture">den kommer och användas i Draw metoden för att skicka enemy bilden till </param>
        /// <param name="enemyHealth">hälsan får fienden som gruppen har gått överens om att den ska ligga på 10, man kan deklarerar 
        /// värdet när man skapar objekt av Enemy typerna så man kan ändra det värdet från 10 till något annat så den är inte const</param>
        /// <param name="isNotDead">kontrolerar om fienderna är döda</param>
        /// <param name="center"> Kontrrollerar centrum av bilden på fienderna så vi kan bestämma om den har blivit träffad eller inte när spelaren sjukter</param>
        /// <param name="damageDuration"> använder den för att bestämma under hur länge ska damage som orsakas av att träffa player ska vara  </param>
        /// <param name="moveRight">använder jag för att bestäma om fienden ska röra sig åt sidan </param>
        /// </summary>
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
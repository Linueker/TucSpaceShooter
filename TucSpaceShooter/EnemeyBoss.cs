using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TucSpaceShooter
{
    public class EnemeyBoss : Enemies
    {
        bool moveRight = true;
        bool moveLeft = false;
        
        private const int center = 70;
        int movingSpeed = 1;
        
        bool isDead = false;
        
        public EnemeyBoss(Vector2 position, GraphicsDeviceManager graphics, Texture2D enemyTextureTwo, int enemyHealth) :
            base(position, graphics, enemyTextureTwo, enemyHealth)
        {
            this.position.X = graphics.PreferredBackBufferWidth / 2 -5;
            this.position.Y = -300;
        }
        public override void MoveToRandomPosition(GraphicsDeviceManager graphics)
        {
            if (!isDead)
            {
                if (position.Y < 80)
                {
                    position.Y += movingSpeed;
                }
                if (position.X > 60 && moveRight && position.Y > 60)
                {
                    position.X -= movingSpeed;
                }
                if (position.X < 61)
                {
                    moveRight = false;
                    moveLeft = true;
                }
                if(moveLeft && position.Y == 80 && position.X <460) 
                {
                    position.X += movingSpeed;
                }
                if(position.X > 459)
                {
                    moveLeft = false;
                    moveRight = true;   
                }
            }
        }
        public async void DamageToTheEnemy(GraphicsDeviceManager graphics, Player player, SpriteBatch spriteBatch, SoundEffect bossExplosionSound)
        {
            if (!isDead)
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
                            player.points.AddPoints(player, EnemyType.Boss);
                            isDead = true;
                            bossExplosionSound.Play();
                            //väntetid så att man ser bossexplosioner innan man kommer till Game Over-screen
                            await Task.Delay(2500);
                            player.Health = 0;
                            
                            break;
                        }
                        Bullet.Bullets.Remove(bullet);
                        break;
                    }
                }
            }
        }
        public void BossBulletCollision(Player player)
        {
            foreach (var bullet in Bullet.BossBullets)
            {
                float makeDamageToPlayer = Vector2.Distance(bullet.Position, player.Position);
                float damageRadius = 12;
                if (makeDamageToPlayer <= damageRadius)
                {
                    player.Health--;
                    Bullet.BossBullets.Remove(bullet);
                    break;
                }
            }
        }

        public void Die()
        {
            isDead = true;
        }
    }
}


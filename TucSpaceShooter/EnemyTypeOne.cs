using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TucSpaceShooter
{
    public class EnemyTypOne : Enemies
    {
        private int movingSpeed = 1;
        private bool moveRight = true;
        private const int center = 16;
        private bool isDead = false;
        private static List<EnemyTypeTwo> enemyTypeTwoList = new List<EnemyTypeTwo>();
        private bool isNotDead = true;
        bool moveBack = true;
        private static List<EnemyTypOne> enemyTypeOneList= new List<EnemyTypOne>();
        public bool IsNotDead { get { return isDead; } }

        public EnemyTypOne(Vector2 position, GraphicsDeviceManager graphics, int enemyHealth) :
            base(position, graphics, enemyHealth)
        {
            this.position.X = graphics.PreferredBackBufferWidth / 2 - 30;
            this.position.Y = graphics.PreferredBackBufferHeight;
            enemyTypeOneList.Add(this);
        }
        public override void MoveToRandomPosition(GraphicsDeviceManager graphics )
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
        public void ResetPosition(GraphicsDeviceManager graphics)
        {
            Random random = new Random();
            position.X = random.Next(graphics.PreferredBackBufferWidth - 60);
            position.Y = random.Next(-graphics.PreferredBackBufferHeight, 0);
        }

        // EnemyTypOne-klassen
        public override void Damage(GraphicsDeviceManager graphics)
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
                            break;
                        }
                    }
                }
            }
        }


    }
}
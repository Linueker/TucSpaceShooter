using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
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
        private Bullet bullet;
        //private bool movingForward = true;
        //private Vector2 firstPositionBoss;
        //private bool firstAttack = true;
        //those two should be send from enemies.cs so all the enemy have those two. ask the grupp first
        // private int health=1;
        //private int dam= 2;
        public EnemyTypOne(Vector2 position, GraphicsDeviceManager graphics) :
            base(position, graphics)
        {
            this.position.X = graphics.PreferredBackBufferWidth / 2 - 30;
            this.position.Y = graphics.PreferredBackBufferHeight;
            //this.enemyRandom = new Random();
            //this.firstPositionBoss = position;
            //this.EnemiesTyp = enemyType;
        }
        public override void MoveToRandomPosition(GraphicsDeviceManager graphics)
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
        //public override void Damage()
        //{
        //    if (bullet.position == position.X)
        //    {
        //        position.X = 0;
        //        position.Y = 0;
        //    }
        //}
    }
}
/*
 * jag vill fixa en lista för Enemy 1
 * ställa sig på rad
 * rand rörelse för rand index i listan. 
 */
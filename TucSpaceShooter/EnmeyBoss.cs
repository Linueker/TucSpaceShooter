using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TucSpaceShooter
{
    public class EnmeyBoss : Enemies
    {
        bool moveRight = true;
        bool moveLeft = false;
        bool moveBack = true;
        int movingSpeed = 1;
        int pxelToRight = 1;
        bool isDead = false;
        bool moveForward=true;
        public EnmeyBoss(Vector2 position, GraphicsDeviceManager graphics, int enemyHealth) :
            base(position, graphics, enemyHealth)
        {
            this.position.X = graphics.PreferredBackBufferWidth / 2 - 70;
            this.position.Y = -180;
        }
        public override void MoveToRandomPosition(GraphicsDeviceManager graphics)
        {
            if (!isDead)
            {
                if (position.Y < 80)
                {
                    position.Y += movingSpeed;
                }
                if (position.X > 20 && moveRight && position.Y > 60)
                {
                    position.X -= movingSpeed;
                }
                if (position.X < 21)
                {
                    moveRight = false;
                    moveLeft = true;
                }
                if(moveLeft && position.Y == 80 && position.X <400) 
                {
                    position.X += movingSpeed;
                }
                if(position.X > 399)
                {
                    moveLeft = false;
                    moveRight = true;   
                }
            }
        }

        public void Die()
        {
            isDead = true;
        }
    }
}


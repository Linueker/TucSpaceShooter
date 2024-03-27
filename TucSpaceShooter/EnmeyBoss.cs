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
        bool moveBack = true;
        int movingSpeed = 1;
        int pxelToRight = 1;
        bool isDead = false;
        bool moveForward=true;
        public EnmeyBoss(Vector2 position, GraphicsDeviceManager graphics, int enemyHealth) :
            base(position, graphics, enemyHealth)
        {
            this.position.X = graphics.PreferredBackBufferWidth / 2 - 30;
            this.position.Y = graphics.PreferredBackBufferHeight;
        }
        public override void MoveToRandomPosition(GraphicsDeviceManager graphics)
        {
            if (!isDead)
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

                if (position.Y <= 30 || position.Y >= graphics.PreferredBackBufferHeight - 60)
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

        public void Die()
        {
            isDead = true;
        }
    }
}


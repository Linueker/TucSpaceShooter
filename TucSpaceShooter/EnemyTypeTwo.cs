using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TucSpaceShooter
{
    public class EnemyTypeTwo : Enemies
    {
        private int movingSpeed = 2;
        private bool moveRight = true;
        //private bool movingForward = true;
        //private Vector2 firstPositionBoss;
        //private bool firstAttack = true;
        bool moveBack = true;
        public EnemyTypeTwo(Vector2 position, GraphicsDeviceManager graphics) :
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
                position.Y += movingSpeed;
            }
            else
            {
                position.Y -= movingSpeed;
            }
            if (position.Y <= 20 || position.Y >= graphics.PreferredBackBufferWidth - 60)
            {
                moveRight = !moveRight;
            }

        }
    }
}

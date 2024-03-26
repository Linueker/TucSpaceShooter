using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TucSpaceShooter
{
    public class EnemyTypeThree : Enemies
    {
        bool moveRight = true;
        bool moveBack = true;
        int movingSpeed = 1;
        int pxelToRight = 1;
        public EnemyTypeThree(Vector2 position, GraphicsDeviceManager graphics) :
            base(position, graphics)
        {
            this.position.X = graphics.PreferredBackBufferWidth / 2 - 30;
            this.position.Y = graphics.PreferredBackBufferHeight;
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
                position.X -= movingSpeed;// attacing 
                position.Y -= movingSpeed;//rolling back
                position.X -= movingSpeed;// last attack
            }
            if (position.X <= 20 || position.X >= graphics.PreferredBackBufferWidth - 60)
            {
                moveRight = !moveRight;
            }
        }
    }
}

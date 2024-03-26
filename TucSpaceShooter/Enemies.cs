using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TucSpaceShooter.Powerup;

namespace TucSpaceShooter
{
    public abstract class Enemies : Creature
    {
        /// <summary>
        /// i den klassen är en abstract som ärver från Creature klassen samtidigt som den ärvar till andra enemy typer.
        /// 
        /// </summary>
        private int movingSpeed = 1;
        private bool moveRight = true;
        private bool moveback = true;
        private bool movingForward = true;
        private Vector2 firstPositionBoss;
        private bool firstAttack = true;
        public Enemies(Vector2 position, GraphicsDeviceManager graphics) : base(position)
        {
            this.position.X = graphics.PreferredBackBufferWidth / 2 - 30;
            this.position.Y = graphics.PreferredBackBufferHeight;
            //this.enemyRandom = new Random();
            this.firstPositionBoss = position;
            this.moveRight = moveRight;
            this.moveback = moveback;

            //this.EnemiesTyp = enemyType;
        }
        /* this should be an abstract method */
        public virtual void MoveToRandomPosition(GraphicsDeviceManager graphics)
        {
        }
        /// <summary>
        /// list bullet kan jag använda mig av. 
        /// </summary>
        public virtual void Damage() 
        {
        }
        /* the  logic i need but not the method  */
        //private void BossMovement(Enemies enemies, GraphicsDeviceManager graphics)
        //{
        //    // Rörelsemönster för fiendens boss-typ
        //    // Fram och tillbaka-rörelse
        //    if (movingForward)
        //    {
        //        enemies.position.X += movingSpeed;
        //        if (enemies.position.X >= graphics.PreferredBackBufferWidth - 60)
        //        {
        //            movingForward = false;
        //        }
        //    }
        //    else
        //    {
        //        enemies.position.X -= movingSpeed;
        //        if (enemies.position.X <= firstPositionBoss.X)
        //        {
        //            movingForward = true;
        //            if (!firstAttack)
        //            {

        //            }
        //            firstAttack = false;
        //        }
        //    }
        //}
    }
}


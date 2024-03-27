using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private int enemyHealth;
        public Enemies(Vector2 position, GraphicsDeviceManager graphics, int enemyHealth) : base(position)
        {
            this.position.X = graphics.PreferredBackBufferWidth / 2 - 30;
            this.position.Y = graphics.PreferredBackBufferHeight;
            this.firstPositionBoss = position;
            this.moveRight = moveRight;
            this.moveback = moveback;
            this.EnemyHealth = enemyHealth;
        }
        public int EnemyHealth
        {
            get { return enemyHealth; }
            set { enemyHealth = value; }
        }
        /* this should be an abstract method */
        public virtual void MoveToRandomPosition(GraphicsDeviceManager graphics)
        {
        }
        /// <summary>
        /// list bullet kan jag använda mig av. 
        /// </summary>
        public virtual void Damage(GraphicsDeviceManager graphics) 
        {
        }

        public virtual void ResetPosition(GraphicsDeviceManager graphics)
        {

        }
    }
}


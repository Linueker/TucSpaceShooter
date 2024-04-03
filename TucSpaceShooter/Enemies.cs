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
        ///en abstrakt klass som används för att strukturerar arbetsflödet i de andra klasserna som vi ska, för fiende
        /// den klassen innehåller 5 abstrkta metoder med en för rörelse vid namnet (MoveToRandomPosition).
        /// den andra är DamageToTheEnemy och den metoden är skapat för att spelaren ska göra en skada för Enemy typerna.

        /// den tredje metoden är ResetPosition() vad den metoden ska göra är att den ska skapa en random plats för fienden för att den ska 
        /// dycka upp i efter att fienden är död.
        /// 
        ///  MakeDamageToPlayer() är den metoden som ansvarar för att fienderna ska kunna göra en skada för spelaren. den metoden är gjort så här
        ///  när spelaren och fienden är jätte nära varandra då ska spelaren få en Damage.
        ///  
        ///  DrawEnemy() i den metoden så målas enemy. 
        ///  
        /// Klassen kommer och ärva från Creature klassen, samt den kommer ha egna egenskaper GraphicsDeviceManager graphics,
        /// Texture2D enemyTexture, 
        /// int enemyHealth
        /// <param name="graphics"> kommer och använda för att skicka _graphics i Game klassen</param>
        /// <param name="enemyTexture">den kommer och användas i Draw metoden för att skicka enemy bilden till </param>
        /// <param name="enemyHealth">hälsan får fienden som gruppen har gått överens om att den ska ligga på 10, man kan deklarerar 
        /// värdet när man skapar objekt av Enemy typerna så man kan ändra det värdet från 10 till något annat så den är inte const</param>
        /// </summary>
        private int movingSpeed = 1;
        private bool moveRight = true;
        private bool moveback = true;
        private bool movingForward = true;
        private Vector2 firstPositionBoss;
        private bool firstAttack = true;
        private int enemyHealth;
        private Texture2D enemyTexture;
        public Enemies(Vector2 position, GraphicsDeviceManager graphics, Texture2D enemyTexture, int enemyHealth) : base(position)
        {
            this.position.X = graphics.PreferredBackBufferWidth / 2 - 30;
            this.position.Y = graphics.PreferredBackBufferHeight;
            this.firstPositionBoss = position;
            this.moveRight = moveRight;
            this.moveback = moveback;
            this.EnemyHealth = enemyHealth;
            this.enemyTexture = enemyTexture;
        }
        public int EnemyHealth
        {
            get { return enemyHealth; }
            set { enemyHealth = value; }
        }
        public Texture2D GetEnemyTexture()
        {
            return enemyTexture;
        }
        public virtual void MoveToRandomPosition(GraphicsDeviceManager graphics)
        {
        }
        public virtual void DamageToTheEnemy(GraphicsDeviceManager graphics, Player player, SpriteBatch spriteBatch)
        {
        }

        public virtual void ResetPosition(GraphicsDeviceManager graphics)
        {

        }
        public virtual void MakeDamageToPlayer(GameTime gameTime, Player player)
        {
        }
        public virtual void DrawEnemy(SpriteBatch spriteBatch)
        {

        }

    }
}


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TucSpaceShooter;

public class EnemyTypeTwo : Enemies
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
    private int movingSpeed = 2;
    private bool moveRight = true;
    private static List<EnemyTypeTwo> enemyTypeTwoList = new List<EnemyTypeTwo>();
    public bool isNotDead = true;
    private const int center = 27;
    bool moveBack = true;
    float timerForDamageThePlayer = 0f;
    float damageDuration = 9f;//9
    bool damageEnemy = true;
    

    public static List<EnemyTypeTwo> EnemyTypeTwoList { get => enemyTypeTwoList; set => enemyTypeTwoList = value; }

    public EnemyTypeTwo(Vector2 position, GraphicsDeviceManager graphics, Texture2D enemyTextureTwo, int enemyHealth) :
        base(position, graphics, enemyTextureTwo, enemyHealth)
    {
        this.position.X = graphics.PreferredBackBufferWidth / 2 - 30;
        this.position.Y = graphics.PreferredBackBufferHeight;
        enemyHealth = 10;
        enemyTypeTwoList.Add(this);
        EnemyHealth = 10;
    }

    public override void MoveToRandomPosition(GraphicsDeviceManager graphics)
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
            if (!moveBack)
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
        }
    }
    public override void DrawEnemy(SpriteBatch spriteBatch)
    {
        foreach (EnemyTypeTwo enemy in enemyTypeTwoList)
        {
            if (enemy.EnemyHealth != 0)
            {
                spriteBatch.Draw(enemy.GetEnemyTexture(), enemy.Position, Color.White);
                break;
            }
        }
    }

    public void ResetPosition(GraphicsDeviceManager graphics)
    {
        Random random = new Random();
        position.X = random.Next(graphics.PreferredBackBufferWidth - 60);
        position.Y = random.Next(-graphics.PreferredBackBufferHeight, 0);
        EnemyHealth = 10;
    }
    

    public override void DamageToTheEnemy(GraphicsDeviceManager graphics, Player player, SpriteBatch spriteBatch)
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
                        //ResetPosition(graphics);
                        player.points.AddPoints(player, EnemyType.Two);
                        break;
                    }
                    Bullet.Bullets.Remove(bullet);
                    break;
                }
            }
        }
    }
    public override void MakeDamageToPlayer(GameTime gameTime, Player player)
    {
        if (damageEnemy)
        {
            timerForDamageThePlayer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timerForDamageThePlayer >= damageDuration)
            {
                damageEnemy = false;
            }

            float makeDamageToPlayer = Vector2.Distance(position, player.Position);
            float damageRadius = center;
            if (makeDamageToPlayer <= damageRadius)
            {
                player.Health--;
                damageEnemy = false;
            }

        }
    }
    public void EnemyBulletCollision(Player player)
    {
        foreach (var bullet in Bullet.EnemyBullets)
        {
            float makeDamageToPlayer = Vector2.Distance(bullet.Position, player.Position);
            float damageRadius = 12;
            if (makeDamageToPlayer <= damageRadius)
            {
                player.Health--;
                Bullet.EnemyBullets.Remove(bullet);
                break;
            }
        }
    }
}
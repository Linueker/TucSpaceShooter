using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TucSpaceShooter;
public class EnemyTypeTwo : Enemies
{
    private int movingSpeed = 2;
    private bool moveRight = true;
    private static List<EnemyTypeTwo> enemyTypeTwoList = new List<EnemyTypeTwo>();
    public bool isNotDead = true;
    private const int center = 27;
    bool moveBack = true;

    public EnemyTypeTwo(Vector2 position, GraphicsDeviceManager graphics, int enemyHealth) :
        base(position, graphics, enemyHealth)
    {
        this.position.X = graphics.PreferredBackBufferWidth / 2 - 30;
        this.position.Y = graphics.PreferredBackBufferHeight;
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

    public void ResetPosition(GraphicsDeviceManager graphics)
    {
        Random random = new Random();
        position.X = random.Next(graphics.PreferredBackBufferWidth - 60);
        position.Y = random.Next(-graphics.PreferredBackBufferHeight, 0);
    }

    public override void Damage(GraphicsDeviceManager graphics, Player player)
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
                        player.points.AddPoints(player, EnemyType.Two);
                        break;
                    }
                }
            }
        }
    }
}
/*
 * Draw metod för all enemy
 * health
 * Boss position 
 */
using TucSpaceShooter;

public class Points
{
    private int points;

    public Points()
    {
        points = 0;
    }

    public int GetEnemyPoints(Player player, EnemyType enemyType)
    {
        int basePoints = 0;

        switch (enemyType)
        {
            case EnemyType.One:
                basePoints = 500;
                break;
            case EnemyType.Two:
                basePoints = 700;
                break;
            case EnemyType.Three:
                basePoints = 1000;
                break;
            case EnemyType.Boss:
                basePoints = 3000;
                break;
            default:
                break;
        }

        if (player.IsDoublePointsActive)
        {
            basePoints *= 2;
        }

        if (player.IsTriplePointsActive)
        {
            basePoints *= 3;
        }

        return basePoints;
    }

    public int GetCurrentPoints()
    {
        return points;
    }

    public void AddPoints(Player player, EnemyType enemyType)
    {
        points += GetEnemyPoints(player, enemyType);
    }
}

public enum EnemyType
{
    One,
    Two,
    Three,
    Boss
}
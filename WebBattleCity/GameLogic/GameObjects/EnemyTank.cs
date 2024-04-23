using System;
using System.Threading.Tasks;
using WebBattleCity.GameLogic.GameLogicEnums;

namespace WebBattleCity.GameLogic.GameObjects;

public class EnemyTank : Tank
{
    public EnemyTank(int x, int y) : base(x, y)
    {
        CurrentVector = Vector.Down;
    }

    public override bool TryDestroy()
    {
        base.TryDestroy();
        return IsDestroyed;
    }

    public void MoveEnemyTank(BattleField battleField)
    {
        Random random = new Random();
        int moveDirection = random.Next(0, 3);

        Vector direction;
        if (Enum.TryParse<Vector>(moveDirection.ToString(), out direction))
        {
            TurnAndMove(direction, battleField);
        }
    }

    public bool IsOnTheSameLine(MyTank myTank)
    {
        if (X == myTank.X || Y == myTank.Y)
        {
            return true;
        }
        return false;
    }

    public Vector FindVectorToShoot(MyTank myTank)
    {
        if (X == myTank.X || Y > myTank.Y)
        {
            return Vector.Down;
        }
        if (X == myTank.X || Y < myTank.Y)
        {
            return Vector.Up;
        }
        if (X > myTank.X || Y == myTank.Y)
        {
            return Vector.Left;
        }
        if (X < myTank.X || Y == myTank.Y)
        {
            return Vector.Right;
        }
        return Vector.Up;
    }

    public override string GetIconName()
    {
        if (CurrentVector == Vector.Up)
        {
            return "enemyTankUp.jpg";
        }
        if (CurrentVector == Vector.Down)
        {
            return "enemyTankDown.jpg";
        }
        if (CurrentVector == Vector.Right)
        {
            return "enemyTankRight.jpg";
        }
        return "enemyTankLeft.jpg";
    }
}
